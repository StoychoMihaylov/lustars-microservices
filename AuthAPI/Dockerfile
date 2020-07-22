FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
LABEL maintainer="Lustars dev team"
WORKDIR /app
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["AuthAPI.App/AuthAPI.App.csproj", "AuthAPI.App/"]
RUN dotnet restore "AuthAPI.App/AuthAPI.App.csproj"
COPY . .
WORKDIR "/src/AuthAPI.App"
RUN dotnet build "AuthAPI.App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AuthAPI.App.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuthAPI.App.dll"]
