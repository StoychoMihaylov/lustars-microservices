ARG DOTNET_SDK_VERSION=3.1
ARG ASP_RUNTIME_VERSION=3.1

FROM mcr.microsoft.com/dotnet/core/sdk:${DOTNET_SDK_VERSION} AS build-env
WORKDIR /src
COPY *.csproj ./
RUN dotnet restore AuthAPI.App.csproj
COPY . ./
RUN dotnet publish AuthAPI.App.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/core/aspnet:${ASP_RUNTIME_VERSION}
WORKDIR /app
COPY --from=build-env /src/out .
ENTRYPOINT ["dotnet", "AuthAPI.App.dll"]

