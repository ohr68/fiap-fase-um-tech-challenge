using FIAP.FaseUm.TechChallenge.Infra.Data.Extensions;
using FIAP.FaseUm.TechChallenge.Worker.Consumers;
using MassTransit;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddInfraLayer(builder.Configuration, builder.Environment.IsDevelopment());

builder.Services.AddMassTransit(busConfig =>
{
    busConfig.AddConsumer<CriarContatoConsumer>();
    busConfig.AddConsumer<AlterarContatoConsumer>();
    busConfig.AddConsumer<RemoverContatoConsumer>();
    
    busConfig.UsingRabbitMq((context, config) =>
    {
        var host = builder.Configuration.GetValue<string>("MassTransit:Host") ??
                   throw new ArgumentNullException("MassTransit:Host");
        var user = builder.Configuration.GetValue<string>("MassTransit:User") ??
                   throw new ArgumentNullException("MassTransit:User");
        var password = builder.Configuration.GetValue<string>("MassTransit:Password") ??
                       throw new ArgumentNullException("MassTransit:Password");

        config.Host(host, "/", x =>
        {
            x.Username(user);
            x.Password(password);
        });

        config.ConfigureEndpoints(context);
    });
});

var host = builder.Build();
host.Run();