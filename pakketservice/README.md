![ipost-logo](https://github.com/FIPost/docs/blob/master/assets/logo-name.png?raw=true)
<h3 align="middle">
  <a href="https://github.com/FIPost/docs">Documentation</a>
  <a>•</a>
  <a href="https://github.com/FIPost/docs/blob/master/CONTRIBUTING.md">Contributing</a>
  <a>•</a>
  <a href="https://github.com/FIPost/docs/blob/master/CONTACT.md">Contact</a>
</h3>

# Package Service
[![Build](https://github.com/FIPost/pakketservice/actions/workflows/build.yml/badge.svg)](https://github.com/FIPost/pakketservice/actions/workflows/build.yml)
[![Release](https://github.com/FIPost/pakketservice/actions/workflows/docker-publish.yml/badge.svg)](https://github.com/FIPost/pakketservice/actions/workflows/docker-publish.yml)
[![Board Status](https://dev.azure.com/405273/a464a51f-a9d3-415a-983c-ecc9f9e1e117/e58d8192-5262-4682-856c-da357d004679/_apis/work/boardbadge/8203b7d2-166a-4745-ab05-5fc958846334)](https://dev.azure.com/405273/a464a51f-a9d3-415a-983c-ecc9f9e1e117/_boards/board/t/e58d8192-5262-4682-856c-da357d004679/Microsoft.RequirementCategory)

.NET Core 3.1 API service for Fontys Internal Packages.

## Getting Started with Docker
```zsh
docker-compose up --build
```

<b>Note</b><br/>
Make sure that you are in the directory of `docker-compose.yml` when running these commands.

### Inspect the database
I use `Azure Data Studio` to do this, but you can also use any other database program compatible for sql server.

In `docker-compose.yml` you can see that port 1433 is exposed on the MSSQL container. You can connect to this database with the following properties:

| Property | Value       |
|--------------|-------------|
| Server | `localhost`          |
| Authentication type | `sa`    |
| Password | `Your_password123` |
| Database | `<Default>`        |
| Server group | `<Default>`    |


### Insert Data
POST: `http://localhost:5001/api/Packages`

```json
{
    "ReceiverId": "1",
    "TrackAndTraceId": "1",
    "CollectionPointId": "1",
    "Sender": "Cheese Factory",
    "Name": "1KG of hot cheese"
}
```

POST: `http://localhost:5001/api/Packages`
```json
{
    "ReceiverId": "2",
    "TrackAndTraceId": "3",
    "CollectionPointId": "3",
    "Sender": "Apple",
    "Name": "10x Ipads"
}
```

### Get Data
GET: `http://localhost:5001/api/Packages`
