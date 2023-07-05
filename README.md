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

FIPost is a fast and efficient opensource internal post website and app for Fontys,
this repository contains multiple API which is responsible for different functionality in a microservice architecture.
Keep in mind that this project is broader than just the FHICT.

This project is part of a long term pilot and is supposed to be developed over multiple semesters
by different groups of 6~ software engineer students. Transferability is therefore a must.

6s > s3 > s6 > year pause > s6 team (current)

### ✉️ Contact Us
Mandatory to [join the Discord server!](https://discord.gg/3xFK8ZAA3d) The project overseer will guide you to setup the project if needed and other further inquiries.
There were some bad management and documentations for the past years so this ends now, rapid development for `2023FALL+` made by `2023SPRING` students.

## 🎯 Goals

* This project aims to modernise the internal post system of Fontys. Currently most of the administrative work is done manually.

* This project moves these processes to a semi-automated system with similar functionalities as PostNL.
  Some of the features include: seeing a package status and tracing locations.

* This project is designed with a microservices architecture to scale and manage the administrative post process of the entire Fontys organisation
  with over 5000 employees and 44.000 students. So this allows us to scale our software to support a large user base.

* To separate all concerns and keep the repositories SOLID each domain has been given each own codebase with its own repository.
  This is also to help maintain collaboration efficiently and opensource.

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
## 🤝 Credits & Collaboration

Currently this project is being developed by semester 6 software students of the FHICT Spring 2023.
Because this project is larger than most and should end up in production,
it is important that everything is well documented. Even though the project will be managed by PT groups,
every bit of help is appreciated and everyone who is willing to help out is welcome.

Check [CONTRIBUTING](https://github.com/FontysIPost/FIPost/blob/dev/.github/CONTRIBUTING.md) and [WIKI](https://github.com/FontysIPost/FIPost) for information.

