# Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy and restore for Shopping.API
COPY Shopping.API/Shopping.API.csproj ./Shopping.API/
RUN dotnet restore "Shopping.API/Shopping.API.csproj"

# Copy all source files
COPY . .

# Build Shopping.API
WORKDIR "/src/Shopping.API"
RUN dotnet build "Shopping.API.csproj" -c Release -o /app/build

# Publish API
FROM build AS publish
RUN dotnet publish "Shopping.API.csproj" -c Release -o /app/publish

# Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Shopping.API.dll"]
