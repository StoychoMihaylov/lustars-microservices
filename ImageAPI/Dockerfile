FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
LABEL maintainer="Lustars dev team"
WORKDIR /app
EXPOSE 5003

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["ImageAPI.App/ImageAPI.App.csproj", "ImageAPI.App/"]
RUN dotnet restore "ImageAPI.App/ImageAPI.App.csproj"
COPY . .
WORKDIR "/src/ImageAPI.App"
RUN dotnet build "ImageAPI.App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ImageAPI.App.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ImageAPI.App.dll"]