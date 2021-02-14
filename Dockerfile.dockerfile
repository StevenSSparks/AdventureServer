FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /appbuild

# Copy csproj and restore as distinct layers
COPY *.sln .
COPY AdventureServer/*.csproj ./AdventureServer
RUN dotnet restore

# Copy everything else and build
COPY AdventureServer/. ./AdventureServer/
WORKDIR /appbuild/AdventureServer
RUN dotnet publish -c Release -o /appbuild/out --no-restore

# Build runtime image
FROM microsoft/aspnetcore:5.0
WORKDIR /appbuild/out
COPY --from=build-env /appbuild/out ./
ENTRYPOINT ["dotnet", "AdventureServer.dll"]