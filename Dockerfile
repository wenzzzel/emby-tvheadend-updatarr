# Use the official .NET 9.0 runtime as the base image
FROM mcr.microsoft.com/dotnet/runtime:9.0 AS base
WORKDIR /app

# Use the .NET 9.0 SDK for building the application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["emby-tvheadend-updatarr/emby-tvheadend-updatarr.csproj", "emby-tvheadend-updatarr/"]
RUN dotnet restore "emby-tvheadend-updatarr/emby-tvheadend-updatarr.csproj"

# Copy the source code and build the application
COPY ["emby-tvheadend-updatarr/", "emby-tvheadend-updatarr/"]
WORKDIR "/src/emby-tvheadend-updatarr"
RUN dotnet build "emby-tvheadend-updatarr.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "emby-tvheadend-updatarr.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Set environment variables with default values
ENV RUN_ONCE=false

# Create output directory
RUN mkdir -p /output

# Set the entry point
ENTRYPOINT ["dotnet", "emby-tvheadend-updatarr.dll"]
