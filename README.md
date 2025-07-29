Instructivo de Ejecución del Proyecto DominexRateHub

Requisitos Previos
Antes de ejecutar el proyecto, asegúrate de cumplir con lo siguiente:

Visual Studio 2022 o superior con soporte para proyectos .NET 6/7/8.

.NET SDK 8.0 instalado. Descárgalo desde: https://dotnet.microsoft.com/download

Docker Desktop instalado y corriendo. Descárgalo desde: https://www.docker.com/products/docker-desktop/

Postman (opcional, para pruebas): https://www.postman.com/downloads/

DominexRateHub/
│
├── src/
│   ├── DominexRateOrchestrator/
│   ├── BancoPopularExchangeRateService/
│   ├── BancoBhdLeonExchangeRateService/
│   └── BanreservasExchangeRateService/
│
├── solution files/
│   └── docker-compose.yml
│
└── DominexRateCollection.postman_collection.json

Iniciar Docker

docker compose build
docker compose up
