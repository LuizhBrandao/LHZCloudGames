# Stage 1: Build & Publish
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copia os arquivos de projeto para otimizar o cache das camadas de restore
COPY ["LHZCloudGames/LHZCloudGames.Api/LHZCloudGames.Api.csproj", "LHZCloudGames/LHZCloudGames.Api/"]
COPY ["LHZCloudGames/LHZCloudGames.Application/LHZCloudGames.Application.csproj", "LHZCloudGames/LHZCloudGames.Application/"]
COPY ["LHZCloudGames/LHZCloudGames.Domain/LHZCloudGames.Domain.csproj", "LHZCloudGames/LHZCloudGames.Domain/"]
COPY ["LHZCloudGames/LHZCloudGames.Infrastructure/LHZCloudGames.Infrastructure.csproj", "LHZCloudGames/LHZCloudGames.Infrastructure/"]
COPY ["LHZCloudGames/LHZCloudGames.Tests/LHZCloudGames.Tests.csproj", "LHZCloudGames/LHZCloudGames.Tests/"]
COPY ["LHZCloudGames/LHZCloudGames.slnx", "LHZCloudGames/"]

# Restaura dependências
RUN dotnet restore "LHZCloudGames/LHZCloudGames.slnx"

# Copia todo o código-fonte restante
COPY LHZCloudGames/ LHZCloudGames/

# Compila e publica em modo Release otimizado
WORKDIR "/src/LHZCloudGames/LHZCloudGames.Api"
RUN dotnet publish "LHZCloudGames.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false --no-restore

# Stage 2: Runtime final enxuto
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

# Variáveis de ambiente para porta padrão e comportamento em contêiner
ENV ASPNETCORE_URLS=http://+:8080 \
    ASPNETCORE_ENVIRONMENT=Production \
    DOTNET_RUNNING_IN_CONTAINER=true

# Copia os binários compilados
COPY --from=build /app/publish .

# Executa com usuário não-root padrão para maior segurança
USER app

EXPOSE 8080

ENTRYPOINT ["dotnet", "LHZCloudGames.Api.dll"]
