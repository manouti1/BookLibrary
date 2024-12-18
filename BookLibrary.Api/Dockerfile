FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Adjust the path if the .csproj file is in a subfolder
COPY ["src/BookLibrary/BookLibrary.csproj", "./BookLibrary/"]
RUN dotnet restore "./BookLibrary/BookLibrary.csproj"

# Copy the remaining files into the container
COPY . .

# Set the working directory correctly
WORKDIR "/src/BookLibrary"

# Build the application
RUN dotnet build "./BookLibrary.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BookLibrary.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookLibrary.dll"]
