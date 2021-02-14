FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.sln ./
COPY ./AdventureServer/*.csproj ./AdventureServer
COPY ./AdventureServer_Test/*.csproj ./AdventureServer_Test
RUN dotnet restore

# Copy everything else and build
COPY AdventureServer/. ./AdventureServer/
COPY AdventureServer_Test/. ./AdventureServer_Test/
WORKDIR /appbuild/AdventureServer 
RUN dotnet publish -c Release -o /appbuild/out --no-restore

# Build runtime image
FROM microsoft/aspnetcore:5.0
WORKDIR /app/out
COPY --from=build-env /app/out ./
ENTRYPOINT ["dotnet", "AdventureServer.dll"]