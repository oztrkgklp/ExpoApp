# https://hub.docker.com/_/microsoft-dotnet

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.sln ./
COPY ./backend/src/ExpoAPI /backend/src/ExpoAPI
COPY ./db /db

# # DB
# FROM mcr.microsoft.com/mssql/server:2019-latest
# CMD sqlcmd 

# build app
WORKDIR /backend/src/ExpoAPI
RUN dotnet restore
RUN dotnet publish -c release -o /backend/src/ExpoAPI

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /backend/src/ExpoAPI
COPY --from=build /backend/src/ExpoAPI ./
EXPOSE 80/tcp
ENV ASPNETCORE_URLS http://*:80
ENTRYPOINT ["dotnet", "ExpoAPI.dll"]