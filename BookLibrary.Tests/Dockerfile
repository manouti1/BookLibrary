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

# Run the tests with verbose output
RUN dotnet test --no-restore --verbosity normal

# Final stage to produce the test output (optional)
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS final
WORKDIR /app
COPY --from=build /src/BookLibrary.Tests .
ENTRYPOINT ["dotnet", "BookLibrary.Tests.dll"]
