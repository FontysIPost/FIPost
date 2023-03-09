# build stage
FROM node:lts-alpine as build-stage
WORKDIR /app

ARG GATEWAY_URL="https://localhost:8123"
ARG APP_URL="http://localhost:8080"
ENV VUE_APP_API_GATEWAY ${GATEWAY_URL}
ENV VUE_APP_URL ${APP_URL}}

COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build

# production stage
FROM nginx:stable-alpine as production-stage
COPY ./nginx/default.conf /etc/nginx/conf.d/default.conf
COPY --from=build-stage /app/dist /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
