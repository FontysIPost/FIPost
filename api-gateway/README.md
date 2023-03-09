![ipost-logo](https://github.com/FIPost/docs/blob/master/assets/logo-name.png?raw=true)
<h3 align="middle">
  <a href="https://github.com/FIPost/docs">Documentation</a>
  <a>•</a>
  <a href="https://github.com/FIPost/docs/blob/master/CONTRIBUTING.md">Contributing</a>
  <a>•</a>
  <a href="https://github.com/FIPost/docs/blob/master/CONTACT.md">Contact</a>
</h3>

# API Gateway
[![Build](https://github.com/FIPost/api-gateway/actions/workflows/build.yml/badge.svg)](https://github.com/FIPost/api-gateway/actions/workflows/build.yml)
[![Release](https://github.com/FIPost/api-gateway/actions/workflows/docker-publish.yml/badge.svg)](https://github.com/FIPost/api-gateway/actions/workflows/docker-publish.yml)
[![Board Status](https://dev.azure.com/405273/a464a51f-a9d3-415a-983c-ecc9f9e1e117/e58d8192-5262-4682-856c-da357d004679/_apis/work/boardbadge/8203b7d2-166a-4745-ab05-5fc958846334)](https://dev.azure.com/405273/a464a51f-a9d3-415a-983c-ecc9f9e1e117/_boards/board/t/e58d8192-5262-4682-856c-da357d004679/Microsoft.RequirementCategory)

API Gateway for communication between the frontend and backend. Abstracts calls into user-friendly endpoints and adds a secure entrypoint layer on top of the individual microservices.

## Getting Started

### Docker
Run the gateway.

This uses `appsettings.Docker.json`.

```sh
docker-compose up --build
```

Run the other services.
```sh
# /pakketservice/
docker-compose up --build 
```
```sh
# /locatieservice/
docker-compose up --build 
```

```sh
# /personeel-service/
docker-compose up --build 
```

#### Error: Docker Network Missing
If you get the following error:
Network `ipost-network` declared as external, but could not be found. Run the following:
```zsh
docker network create ipost-network
```


## Debug the gateway.

### Step 1
Run the gateway with `Visual Studio` or the `.NET CLI`. 

This uses `appsettings.Development.json`.

### Step 2
Run the other services in `Docker`.

```sh
# /pakketservice/
docker-compose up --build 
```
```sh
# /locatieservice/
docker-compose up --build 
```

```sh
# /personeel-service/
docker-compose up --build 
```