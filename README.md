<img align="right" width="30%" src="./resources/logo.png"></img>
<h3 align="middle">
<a href="https://github.com/FontysIPost/FIPost">Wiki</a>
<a>•</a>
<a href="https://github.com/FontysIPost/FIPost/CONTRIBUTING.md">Contributing</a>
<a>•</a>
<a href="https://github.com/FontysIPost/FIPost/CONTACT.md">Contact</a>
</h3>

# 📬Fontys Internal Post (FIPost)

FIPost is a fast and efficient opensource internal post website and app for Fontys,
this repository contains multiple API which is responsible for different functionality in a microservice architecture.
Keep in mind that this project is broader than just the FHICT.

This project is part of a long term pilot and is supposed to be developed over multiple semesters
by different groups of 6~ software engineer students. Transferability is therefore a must.

s3 > s6 > year pause > s6 team (current)

## 🎯Goals

* This project aims to modernise the internal post system of Fontys. Currently most of the administrative work is done manually.

* This project moves these processes to a semi-automated system with similar functionalities as PostNL.
  Some of the features include: seeing a package status and tracing locations.

* This project is designed with a microservices architecture to scale and manage the administrative post process of the entire Fontys organisation
  with over 5000 employees and 44.000 students. So this allows us to scale our software to support a large user base.

* To separate all concerns and keep the repositories SOLID each domain has been given each own codebase with its own repository.
  This is also to help maintain collaboration efficiently and opensource.




## 🙌 Collaboration

Currently this project is being developed by semester 6 software students of the FHICT Spring 2024.
Because this project is larger than most and should end up in production,
it is important that everything is well documented. Even though the project will be managed by PT groups,
every bit of help is appreciated and everyone who is willing to help out is welcome.

Check [CONTRIBUTING](https://github.com/FIPost/FIPost/CONTRIBUTING.md) and [WIKI](https://github.com/FontysIPost/FIPost) for information.

## Tech stack
- Node version: ??
- NPM version: ??
- VueJS - TypeScript, JavaScript, SCSS, HTML
- Target Framework: .NET Core 3.1
- Language: C#


## Getting started
```
git clone --recursive https://github.com/FontysIPost/FIPost.git
```

## ✉️ Contact Us
Questions? [<ins>Contact us here </ins>](https://github.com/FIPost/docs/blob/master/CONTACT.md) !

### Structure and services
- [Frontend UI](https://github.com/FontysIPost/FIPost/tree/master/ui)
- [API Gateway](https://github.com/FontysIPost/FIPost/tree/master/api-gateway)
- [Authenthication](https://github.com/FontysIPost/FIPost/tree/master/authentication-service)
- [pakketservice](https://github.com/FIPost/tree/master/pakketservice)
- [locatieservice](https://github.com/FontysIPost/FIPost/tree/master/locatieservice)
- [personeelservice](https://github.com/FontysIPost/FIPost/tree/master/personeel-service)