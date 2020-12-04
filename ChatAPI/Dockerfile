FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
LABEL maintainer="Lustars dev team"
WORKDIR /app
EXPOSE 5005

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["ChatAPI.App/ChatAPI.App.csproj", "ChatAPI.App/"]
RUN dotnet restore "ChatAPI.App/ChatAPI.App.csproj"
COPY . .
WORKDIR "/src/ChatAPI.App"
RUN dotnet build "ChatAPI.App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ChatAPI.App.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChatAPI.App.dll"]