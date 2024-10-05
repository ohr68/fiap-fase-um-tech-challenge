FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/FIAP.FaseUm.TechChallenge.Api/FIAP.FaseUm.TechChallenge.Api.csproj", "src/FIAP.FaseUm.TechChallenge.Api/"]
COPY ["src/FIAP.FaseUm.TechChallenge.Application/FIAP.FaseUm.TechChallenge.Application.csproj", "src/FIAP.FaseUm.TechChallenge.Application/"]
COPY ["src/FIAP.FaseUm.TechChallenge.Custom.Exceptions/FIAP.FaseUm.TechChallenge.Custom.Exceptions.csproj", "src/FIAP.FaseUm.TechChallenge.Custom.Exceptions/"]
COPY ["src/FIAP.FaseUm.TechChallenge.Domain/FIAP.FaseUm.TechChallenge.Domain.csproj", "src/FIAP.FaseUm.TechChallenge.Domain/"]
COPY ["src/FIAP.FaseUm.TechChallenge.Extensions/FIAP.FaseUm.TechChallenge.Extensions.csproj", "src/FIAP.FaseUm.TechChallenge.Extensions/"]
COPY ["src/FIAP.FaseUm.TechChallenge.Infra.Data/FIAP.FaseUm.TechChallenge.Infra.Data.csproj", "src/FIAP.FaseUm.TechChallenge.Infra.Data/"]
COPY ["src/FIAP.FaseUm.TechChallenge.IoC/FIAP.FaseUm.TechChallenge.IoC.csproj", "src/FIAP.FaseUm.TechChallenge.IoC/"]
RUN dotnet restore "./src/FIAP.FaseUm.TechChallenge.Api/FIAP.FaseUm.TechChallenge.Api.csproj"
COPY . .
WORKDIR "/src/src/FIAP.FaseUm.TechChallenge.Api"
RUN dotnet build "./FIAP.FaseUm.TechChallenge.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FIAP.FaseUm.TechChallenge.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FIAP.FaseUm.TechChallenge.Api.dll"]