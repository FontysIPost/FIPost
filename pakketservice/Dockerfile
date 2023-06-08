#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ENV ASPNETCORE_ENVIRONMENT="Production"
ENV ENVIRONMENT="Production"

WORKDIR /src
COPY ["PakketService.csproj", ""]
RUN dotnet restore "./PakketService.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "PakketService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PakketService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PakketService.dll"]