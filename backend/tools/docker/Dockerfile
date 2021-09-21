# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY ./backend/src/ExpoAPI /app/ExpoAPI

# build app
WORKDIR /app/ExpoAPI
RUN dotnet restore
RUN dotnet publish -c Release -o /app/ExpoAPI

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal
WORKDIR /app/ExpoAPI
COPY --from=build /app/ExpoAPI ./
EXPOSE 5009/tcp
ENV ASPNETCORE_URLS http://*:5009
ENTRYPOINT ["dotnet", "ExpoAPI.dll"]