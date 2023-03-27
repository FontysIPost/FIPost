FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 5003

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
ENV ASPNETCORE_ENVIRONMENT="Production"
ENV ENVIRONMENT="Production"

WORKDIR /src
COPY ["personeel-service.csproj", ""]
RUN dotnet restore "./personeel-service.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "personeel-service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "personeel-service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "personeel-service.dll"]