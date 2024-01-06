# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:7.0 as build
WORKDIR /app

# Copy only the necessary files for restoring dependencies
COPY Carsties.sln .
COPY src/AuctionService/AuctionService.csproj src/AuctionService/
COPY src/Contracts/Contracts.csproj src/Contracts/
COPY src/SearchService/SearchService.csproj src/SearchService/
COPY src/IdentityService/IdentityService.csproj src/IdentityService/
COPY src/GatewayService/GatewayService.csproj src/GatewayService/

# Restore dependencies
RUN dotnet restore

# Copy the entire source code
COPY . .

# Build the application
WORKDIR /app/src/IdentityService
RUN dotnet publish -c Release -o /app/src/out

# Stage 2: Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/src/out .

# Set the entry point for the application
ENTRYPOINT ["dotnet", "IdentityService.dll"]
