[![Publish Docker image](https://github.com/FIPost/ui/actions/workflows/docker-publish.yml/badge.svg)](https://github.com/FIPost/ui/actions/workflows/docker-publish.yml)
[![Build image](https://github.com/FIPost/ui/actions/workflows/build.yml/badge.svg)](https://github.com/FIPost/ui/actions/workflows/build.yml)
![ipost-logo](https://github.com/FIPost/docs/blob/master/assets/logo-name.png?raw=true)

# ui
<h3 align="center">
  <a href="https://github.com/FIPost/docs">Documentation</a>
</h3>

VueJS front-end for the logistal process of managing packages.

## Getting Started
Install node modules.
```zsh
npm install
```

Run 
```sh
npm run serve
```

You will get the following output.
```sh
  App running at:
  - Local:   http://localhost:8080/ 
  - Network: http://192.168.1.74:8080/

Issues checking in progress...
No issues found.
```

Copy the `.env.example` file and paste it as a new file named `.env`.

Use the ip address from the line `- Network: http://192.168.1.74:8080/` and replace `ipadress` in you `.env` file.

```env
VUE_APP_API_GATEWAY=http://192.168.1.74:8123
VUE_APP_URL=http://192.168.1.74:8080
```

Press `ctl` + `c` to exit the current build and rerun.
Serve VueJS
```zsh
npm run serve
```

## Getting Started with Docker
The docker image used in this build uses a Nginx webserver to serve the SPA VueJS content on port `80`.

### Build
Choose on of the following.

#### Default build
```sh
docker build -t ui .
```

#### Custom build
Replace example.nl with your custom urls.

```sh
docker build -t ui --build-arg VUE_APPURL=www.example.nl --build-arg VUE_APP_APIGATEWAY=gateway.example.nl .
```

### Run
```sh
docker run -p 8080:80 --name ui-app ui
```
