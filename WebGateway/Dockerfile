FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
LABEL maintainer="Lustars dev team"
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["WebGateway.App/WebGateway.App.csproj", "WebGateway.App/"]
RUN dotnet restore "WebGateway.App/WebGateway.App.csproj"
COPY . .
WORKDIR "/src/WebGateway.App"
RUN dotnet build "WebGateway.App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebGateway.App.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebGateway.App.dll"]
