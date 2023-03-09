![ipost-logo](https://github.com/FIPost/docs/blob/master/assets/logo-name.png?raw=true)
<h3 align="middle">
  <a href="https://github.com/FIPost/docs">Documentation</a>
  <a>•</a>
  <a href="https://github.com/FIPost/docs/wiki">Wiki</a>
  <a>•</a>
  <a href="https://github.com/FIPost/docs/blob/master/CONTRIBUTING.md">Contributing</a>
  <a>•</a>
  <a href="https://github.com/FIPost/docs/blob/master/CONTACT.md">Contact</a>
</h3>

# Location Service
[![Build .NET API](https://github.com/FIPost/locatieservice/actions/workflows/build-and-test.yml/badge.svg)](https://github.com/FIPost/locatieservice/actions/workflows/build.yml)
[![Docker Publish](https://github.com/FIPost/locatieservice/actions/workflows/docker-publish.yml/badge.svg)](https://github.com/FIPost/locatieservice/actions/workflows/docker-publish.yml)
[![Board Status](https://dev.azure.com/405273/a464a51f-a9d3-415a-983c-ecc9f9e1e117/e58d8192-5262-4682-856c-da357d004679/_apis/work/boardbadge/8203b7d2-166a-4745-ab05-5fc958846334)](https://dev.azure.com/405273/a464a51f-a9d3-415a-983c-ecc9f9e1e117/_boards/board/t/e58d8192-5262-4682-856c-da357d004679/Microsoft.RequirementCategory)

## Getting Started
```zsh
dotnet build
```
```zsh
dotnet restore
```
```zsh
dotnet run
```

## Run with Docker
```
docker-compose up --build
```

### Run external database
Set your own database by editing the connectionstring in `appsettings.json`. <br/>
Then run:
```zsh
docker run -p 5002:5002 --name location-service-app location-service
```

#### Error: Docker Network Missing
If you get the following error:
Network `ipost-network` declared as external, but could not be found. Run the following:
```zsh
docker network create ipost-network
```

## Migration

```bash
dotnet ef migrations add migrationName
```

```bash
dotnet ef database update
```

### Revert migration
```bash
dotnet ef database update targetMigration
```
