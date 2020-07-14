#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
LABEL maintainer="Lustars dev team"
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["AuthAPI.App/AuthAPI.App.csproj", "AuthAPI.App/"]
COPY . .
RUN dotnet restore "AuthAPI.App/AuthAPI.App.csproj"
WORKDIR "/src/AuthAPI.App"
RUN dotnet build "AuthAPI.App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AuthAPI.App.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuthAPI.App.dll"]
