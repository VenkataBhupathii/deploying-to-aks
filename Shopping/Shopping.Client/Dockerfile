# Use the official ASP.NET image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Use the .NET SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the Shopping.Client.csproj file into the container
COPY ["Shopping.Client/Shopping.Client.csproj", "Shopping.Client/"]
RUN dotnet restore "Shopping.Client/Shopping.Client.csproj"

# Copy the rest of the source code
COPY . .

# Set the working directory for the build process
WORKDIR "/src/Shopping.Client"

# Build the application
RUN dotnet build "Shopping.Client.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "Shopping.Client.csproj" -c Release -o /app/publish

# Final stage for running the application
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Set entry point for running the application
ENTRYPOINT ["dotnet", "Shopping.Client.dll"]
