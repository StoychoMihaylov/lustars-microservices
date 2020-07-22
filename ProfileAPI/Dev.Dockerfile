FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build

WORKDIR /app

EXPOSE 5002

CMD dotnet restore && dotnet watch --project /app/ProfileAPI.App run