[![Build microservices](https://github.com/FontysIPost/FIPost/actions/workflows/build-and-test-microservices.yml/badge.svg)](https://github.com/FontysIPost/FIPost/actions/workflows/build-and-test-microservices.yml)
[![SonarCloud analysis](https://github.com/FontysIPost/FIPost/actions/workflows/sonarcloud-scanner.yml/badge.svg)](https://github.com/FontysIPost/FIPost/actions/workflows/sonarcloud-scanner.yml)

<img align="right" width="30%" src="./resources/logo.png"></img>

<h3 align="middle">
<a href="https://github.com/FontysIPost/FIPost/wiki">Wiki</a>
<a>•</a>
<a href="https://github.com/FontysIPost/FIPost/blob/dev/.github/CONTRIBUTING.md">Contributing</a>
<a>•</a>
<a href="https://discord.gg/3xFK8ZAA3d">Contact</a>
</h3>

# 📬 Backend - Fontys Internal Post (FIPost)

Backend repository for [Frontend](https://github.com/FontysIPost/Frontend),
this repository contains multiple API which is responsible for different functionality in a microservice architecture.
The frontend communicates with the api-gateway to reroute the necessary microservice.

### ✉️ Contact Us
Mandatory to [join the Discord server!](https://discord.gg/3xFK8ZAA3d) The project overseer will guide you to setup the project if needed and other further inquiries.
There were some bad management and documentations for the past years so this ends now, rapid development for `2023FALL+` made by `2023SPRING` students.

## ⚒️ Development
### 📐Stack
- [.NET 6](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-aspnetcore-6.0.0-windows-x64-installer)
-  IIS(Internet Information Services)

### 📁 Structure & services
- [./api-gateway/](https://github.com/FontysIPost/FIPost/tree/master/api-gateway) API gateway to reroute and log the system of services
- [./authentication-service/](https://github.com/FontysIPost/FIPost/tree/master/authentication-service) Login service
- [./pakketservice/](https://github.com/FIPost/tree/master/pakketservice) Post, Letter and package management
- [./Locatie-service/](https://github.com/FontysIPost/FIPost/tree/master/locatieservice) Track and trace and location register
- [./EmployeeService/](https://github.com/FontysIPost/FIPost/tree/master/personeel-service) Employee management for package registration

### 🏁 Getting started:
1. Clone the repository:
```sh
git clone --recursive https://github.com/FontysIPost/FIPost.git
```
___
2. Setting up MSSQL Database:

Create a `MSSQL` Database in `Fontys Portal website > Selfservice portal > MSSQL database` and navigate to four `appsettings.Development.json` in `./locatie-service`, `./pakketservice`, `./authenthication-service` and `./EmployeeService` and put your credentials:
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*",
  "ConnectionString": "Server=;Database=;User Id=;Password=;"
}
```
OPTIONAL:
> Or you can also create a local mssql db in Docker (For outside developers or setting up faster), first pull it:
```sh
docker pull mcr.microsoft.com/mssql/server:2019-latest
```
> Run the container:
```sh
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Ipost11%" -p 1433:1433 -d --name MSSQLIPost mcr.microsoft.com/mssql/server:2019-latest
```
> Use this connectionString:
```
Server=localhost,1433;Database=master;User Id=sa;Password=Ipost11%;
```
___
3. Run the migrations:

in `EmployeeService`, `Locatie-service`, and `pakketservice` with the following command in the terminal:
```
dotnet ef database update
```
In order to use the functionality in the application, you have to populate 5 out of 6 tables (`buildings`, `cities`, `Package`, `Person`, and `rooms` but not `Ticket`), in the table Person, `0 = Admin` and `1 = Employee` for different access. The app will crash without these data.

See [DATABASE wiki](https://github.com/FontysIPost/FIPost/wiki/Database) how you can populate the exact data into the 5 tables.
___
4. Run all the following services, `EmployeeService: IIS Express`, `PakketService: IIS Express`, `LocatieService: IIS Express`, `authentication-service: IIS Express` and `api-gateway: IIS Express` with the frontend running.

Everything should work and ready to develop!
> Don't forget to run the frontend of FIPost.


