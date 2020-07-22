FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build

WORKDIR /app

EXPOSE 5000

CMD dotnet restore && dotnet watch --project /app/WebGateway.App run