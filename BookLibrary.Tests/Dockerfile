# Use the official .NET SDK image for building and running tests
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory to /src
WORKDIR /src

# Copy the necessary project files
COPY ["BookLibrary.Tests/BookLibrary.Tests.csproj", "BookLibrary.Tests/"]
COPY ["BookLibrary.Api/BookLibrary.csproj", "BookLibrary/"]

# Restore dependencies for the test project
RUN dotnet restore "BookLibrary.Tests/BookLibrary.Tests.csproj"

# Copy the rest of the source code
COPY . . 

# Build the test project
WORKDIR /src/BookLibrary.Tests
RUN dotnet build -c Release

# Run the tests and generate test result report in xUnit format
RUN dotnet test --no-restore --verbosity normal --logger "xunit;LogFileName=test-results.xml"

# Final stage to produce the test output and include the runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

# Set the working directory
WORKDIR /app

# Copy the build artifacts (including the test DLL and test results)
COPY --from=build /src/BookLibrary.Tests . 

# Install the runtime
RUN apt-get update && apt-get install -y \
    aspnetcore-runtime-8.0

# Set the entry point for running tests
ENTRYPOINT ["dotnet", "BookLibrary.Tests.dll"]
