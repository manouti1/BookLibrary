# Use a pre-built image with .NET runtime
FROM mcr.microsoft.com/dotnet/runtime:8.0-aspnet AS base
USER $APP_UID
WORKDIR /app


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BookLibrary.Tests/BookLibrary.Tests.csproj", "BookLibrary.Tests/"]
COPY ["BookLibrary.Api/BookLibrary.csproj", "BookLibrary.Api/"]
RUN dotnet restore "./BookLibrary.Tests/BookLibrary.Tests.csproj"
COPY . .
WORKDIR "/src/BookLibrary.Tests"
RUN dotnet build "./BookLibrary.Tests.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BookLibrary.Tests.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app

# Copy only the test assembly
COPY --from=publish /app/publish/BookLibrary.Tests.dll /app/

# Entrypoint to run tests
ENTRYPOINT ["dotnet", "BookLibrary.Tests.dll"]