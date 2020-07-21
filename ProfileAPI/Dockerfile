FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
LABEL maintainer="Lustars dev team"
WORKDIR /app
EXPOSE 5002

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["ProfileAPI.App/ProfileAPI.App.csproj", "ProfileAPI.App/"]
RUN dotnet restore "ProfileAPI.App/ProfileAPI.App.csproj"
COPY . .
WORKDIR "/src/ProfileAPI.App"
RUN dotnet build "ProfileAPI.App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProfileAPI.App.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProfileAPI.App.dll"]
