#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/MineSweeper.API/MineSweeper.API.csproj", "src/MineSweeper.API/"]
COPY ["src/MineSweeper.Infra.Data/MineSweeper.Infra.Data.csproj", "src/MineSweeper.Infra.Data/"]
COPY ["src/MineSweeper.Domain/MineSweeper.Domain.csproj", "src/MineSweeper.Domain/"]
COPY ["src/MineSweeper.Application/MineSweeper.Application.csproj", "src/MineSweeper.Application/"]
COPY ["src/MineSweeper.CrossCutting.Auth/MineSweeper.CrossCutting.Auth.csproj", "src/MineSweeper.CrossCutting.Auth/"]
RUN dotnet restore "src/MineSweeper.API/MineSweeper.API.csproj"
COPY . .
WORKDIR "/src/src/MineSweeper.API"
RUN dotnet build "MineSweeper.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MineSweeper.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet MineSweeper.API.dll