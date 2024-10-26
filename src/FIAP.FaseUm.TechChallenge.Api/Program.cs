using FIAP.FaseUm.TechChallenge.Api.Filters;
using FIAP.FaseUm.TechChallenge.IoC;
using Prometheus;
using FIAP.FaseUm.TechChallenge.Infra.Data.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddProblemDetails(options =>
        options.CustomizeProblemDetails = ctx =>
        {
            ctx.ProblemDetails.Extensions.Add("trace-id", ctx.HttpContext.TraceIdentifier);
            ctx.ProblemDetails.Extensions.Add("instance", $"{ctx.HttpContext.Request.Method} {ctx.HttpContext.Request.Path}");
        });

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new GlobalExceptionFilter());
});

builder.Services.UseHttpClientMetrics();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureServices(builder.Configuration, builder.Environment.IsDevelopment());

var app = builder.Build();

//Starting the metrics exporter, will expose "/metrics"
app.UseMetricServer();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//adding metrics related to HTTP
app.UseHttpMetrics(options =>
{
    options.AddCustomLabel("host", context => context.Request.Host.Host);
});

app.UseAuthorization();

app.MapMetrics();
app.MapControllers();

// When the app runs, it first creates the Database.
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<TechChallengeFaseUmDbContext>();
    context.Database.EnsureCreated();
}

app.Run();

public partial class Program { }