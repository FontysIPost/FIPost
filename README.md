[![SonarCloud analysis](https://github.com/FontysIPost/FIPost/actions/workflows/sonarcloud-scanner.yml/badge.svg)](https://github.com/FontysIPost/FIPost/actions/workflows/sonarcloud-scanner.yml)

<img align="right" width="30%" src="./resources/logo.png"></img>

<h3 align="middle">
<a href="https://github.com/FontysIPost/FIPost">Wiki</a>
<a>•</a>
<a href="https://github.com/FontysIPost/FIPost/blob/dev/.github/CONTRIBUTING.md">Contributing</a>
<a>•</a>
<a href="https://github.com/FontysIPost/FIPost/CONTACT.md">Contact</a>
</h3>

# 📬 Fontys Internal Post (FIPost)

FIPost is a fast and efficient opensource internal post website and app for Fontys,
this repository contains multiple API which is responsible for different functionality in a microservice architecture.
Keep in mind that this project is broader than just the FHICT.

This project is part of a long term pilot and is supposed to be developed over multiple semesters
by different groups of 6~ software engineer students. Transferability is therefore a must.

s3 > s6 > year pause > s6 team (current)

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
- **Node version:** 14.15.5
- **NPM version:** 6.14.11
- **Frontend:** [VueJS](https://vuejs.org/guide/introduction.html) - HTML/[SCSS](https://sass-lang.com/documentation/syntax)/JavaScript and [TypeScript](https://www.typescriptlang.org/docs/)
- **Backend:** [.NET Core 3.1](https://download.visualstudio.microsoft.com/download/pr/b70ad520-0e60-43f5-aee2-d3965094a40d/667c122b3736dcbfa1beff08092dbfc3/dotnet-sdk-3.1.426-win-x64.exe)

### 📁 Structure & services
- [./ui/](https://github.com/FontysIPost/FIPost/tree/master/ui) Frontend UI of Fontys Internal Post system
- [./api-gateway/](https://github.com/FontysIPost/FIPost/tree/master/api-gateway) API gateway to reroute and log the system of services
- [./authentication-service/](https://github.com/FontysIPost/FIPost/tree/master/authentication-service) Login service
- [./pakketservice/](https://github.com/FIPost/tree/master/pakketservice) Post, Letter and package management
- [./Locatie-service/](https://github.com/FontysIPost/FIPost/tree/master/locatieservice) Track and trace and location register
- [./EmployeeService/](https://github.com/FontysIPost/FIPost/tree/master/personeel-service) Employee management for package registration


### 🏁 Getting started:
Clone the repository:
```sh
git clone --recursive https://github.com/FontysIPost/FIPost.git
```
Navigate to `./ui` folder and install dependencies:
```sh
# Check which node you're using: node -v
# To switch node version: nvm use v14.15.5
npm i
```
Copy `.env.example` and past it as `.env` and populate these ports
```dotenv
VUE_APP_API_GATEWAY=https://Localhost:44311
VUE_APP_URL=Localhost:8080
```

Run the frontend UI and open `http://localhost:8080/` when ready:
```sh
npm run serve
```
Create a `MSSQL` Database and navigate to three `appsettings.Development.json` in `./locatie-service`, `./pakketservice` and `./personeel-service` and put your credentials:
```json
{
  "ConnectionString": "Server=;Database=;User Id=;Password=;",
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  }
}
```
Run all the following services, `personeel-service: IIS Express`, `PakketService: IIS Express`, `LocatieService: IIS Express`, `authentication-service: IIS Express` and `api-gateway: IIS Express`

In order to use the functionality in the application, you have to put your login info into your database table Person. Then you can access the dashboard.


## 🤝 Credits & Collaboration

Currently this project is being developed by semester 6 software students of the FHICT Spring 2024.
Because this project is larger than most and should end up in production,
it is important that everything is well documented. Even though the project will be managed by PT groups,
every bit of help is appreciated and everyone who is willing to help out is welcome.

Check [CONTRIBUTING](https://github.com/FontysIPost/FIPost/blob/dev/.github/CONTRIBUTING.md) and [WIKI](https://github.com/FontysIPost/FIPost) for information.


## ✉️ Contact Us
Questions? [<ins>Contact us here </ins>](https://github.com/FIPost/docs/blob/master/CONTACT.md) !

