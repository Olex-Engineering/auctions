FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
EXPOSE 80

COPY ["Carstiest.sln", "Carstiest.sln"]
COPY ["src/AuctionService/AuctionService.csproj", "src/AuctionService/"]
COPY ["src/SearchService/SearchService.csproj", "src/SearchService/"]
COPY ["src/GatewayService/GatewayService.csproj", "src/GatewayService/"]
COPY ["src/Contracts/Contracts.csproj", "src/Contracts/"]
COPY ["src/IdentityService/IdentityService.csproj", "src/IdentityService/"]

# Restore package deps
RUN dotnet restore Carstiest.sln

# Copy the app folders over
COPY src/SearchService src/SearchService
COPY src/Contracts src/Contracts

WORKDIR "/app/src/SearchService"
RUN dotnet publish -c Release -o /app/src/out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/src/out .
ENTRYPOINT ["dotnet", "SearchService.dll"]
