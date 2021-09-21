# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /ExpoAPI

# copy csproj and restore as distinct layers
COPY ./backend/src/ExpoAPI/* ./

# build app
WORKDIR /ExpoAPI
RUN dotnet restore
RUN rm /ExpoAPI/Release/net6.0/ExpoAPI.AssemblyInfo.cs
RUN rm /ExpoAPI/Release/net6.0/.NETCoreApp,Version=v6.0.AssemblyAttributes.cs
RUN dotnet publish -c Release -o /ExpoAPI

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal
WORKDIR /ExpoAPI
COPY --from=build /ExpoAPI ./
EXPOSE 5009/tcp
ENV ASPNETCORE_URLS http://*:5009
ENTRYPOINT ["dotnet", "ExpoAPI.dll"]