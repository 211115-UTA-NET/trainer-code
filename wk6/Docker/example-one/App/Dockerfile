FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
#FROM <repo>/<user>/<image>:<tag>(defaults to "latest")
#FROM node:12-alpine
# COPY bin/Release/net6.0/publish/ App/
# #COPY <relative path from Dockerfile to target files> <target location in image>
WORKDIR /App
# #Change the images current working directory
COPY . .
# Copy the contents of the folder where the Dockerfile lives, into the image
RUN dotnet publish -c Release

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /App

COPY  --from=build /App/bin/Release/net6.0/publish/ .

ENTRYPOINT ["dotnet", "DotNet.Docker.dll"]
#Entrypoint sets the container to be an executable, and specifies
#what to run on startup.