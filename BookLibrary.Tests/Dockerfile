# Use the official .NET SDK image for building and running tests
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory to /src
WORKDIR /src

# Copy the necessary project files
COPY BookLibrary.Tests/BookLibrary.Tests.csproj BookLibrary.Tests/
COPY BookLibrary.Api/BookLibrary.csproj BookLibrary.Api/

# Restore dependencies for the test project
RUN dotnet restore "BookLibrary.Tests/BookLibrary.Tests.csproj"

# Copy the rest of the source code (including necessary files for build and test)
COPY . .

# Build the test project
WORKDIR /src/BookLibrary.Tests
RUN dotnet build -c Release

# Run the tests and generate the test result report in xUnit format
RUN dotnet test --no-restore --verbosity detailed --logger "xunit;LogFileName=test-results.xml"