# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Asegura que el contenedor escuche en el puerto 80 y habilita entorno de desarrollo
ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Development

COPY --from=build /app/out ./

ENTRYPOINT ["dotnet", "BancoBhdLeonExchangeRateService.dll"]
