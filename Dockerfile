# 1. Etapa base para ejecución
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER app
WORKDIR /app
# En .NET 8/10, el puerto por defecto para contenedores no-root es el 8080
EXPOSE 8080 

# 2. Etapa de construcción
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copiamos los archivos de proyecto de las 4 capas para restaurar dependencias
COPY ["SistemaNotas.Api/SistemaNotas.Api.csproj", "SistemaNotas.Api/"]
COPY ["SistemaNotas.Application/SistemaNotas.Application.csproj", "SistemaNotas.Application/"]
COPY ["SistemaNotas.Infrastructure/SistemaNotas.Infrastructure.csproj", "SistemaNotas.Infrastructure/"]
COPY ["SistemaNotas.Domain/SistemaNotas.Domain.csproj", "SistemaNotas.Domain/"]

# Restauramos dependencias
RUN dotnet restore "SistemaNotas.Api/SistemaNotas.Api.csproj"

# Copiamos todo el código fuente restante
COPY . .
WORKDIR "/src/SistemaNotas.Api"

# Compilamos en modo Release
RUN dotnet build "SistemaNotas.Api.csproj" -c Release -o /app/build

# 3. Etapa de publicación
FROM build AS publish
RUN dotnet publish "SistemaNotas.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# 4. Etapa final (Copia los binarios al contenedor base)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SistemaNotas.Api.dll"]