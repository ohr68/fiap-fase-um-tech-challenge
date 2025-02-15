FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/FIAP.FaseUm.TechChallenge.Worker/FIAP.FaseUm.TechChallenge.Worker.csproj", "src/FIAP.FaseUm.TechChallenge.Worker/"]
COPY ["src/FIAP.FaseUm.TechChallenge.Domain/FIAP.FaseUm.TechChallenge.Domain.csproj", "src/FIAP.FaseUm.TechChallenge.Domain/"]
COPY ["src/FIAP.FaseUm.TechChallenge.Custom.Exceptions/FIAP.FaseUm.TechChallenge.Custom.Exceptions.csproj", "src/FIAP.FaseUm.TechChallenge.Custom.Exceptions/"]
COPY ["src/FIAP.FaseUm.TechChallenge.Infra.Data/FIAP.FaseUm.TechChallenge.Infra.Data.csproj", "src/FIAP.FaseUm.TechChallenge.Infra.Data/"]
RUN dotnet restore "src/FIAP.FaseUm.TechChallenge.Worker/FIAP.FaseUm.TechChallenge.Worker.csproj"
COPY . .
WORKDIR "/src/src/FIAP.FaseUm.TechChallenge.Worker"
RUN dotnet build "FIAP.FaseUm.TechChallenge.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "FIAP.FaseUm.TechChallenge.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FIAP.FaseUm.TechChallenge.Worker.dll"]
