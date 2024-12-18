# Use the official .NET SDK image for building and running tests
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory to /src
WORKDIR /src

# Copy the necessary project files
COPY ["BookLibrary.Tests/BookLibrary.Tests.csproj", "BookLibrary.Tests/"]
COPY ["BookLibrary.Api/BookLibrary.csproj", "BookLibrary/"]

# Restore dependencies for the test project
RUN dotnet restore "BookLibrary.Tests/BookLibrary.Tests.csproj"

# Copy the rest of the source code (including test code)
COPY . .

# Build the test project
WORKDIR /src/BookLibrary.Tests
RUN dotnet build -c Release

# Run the tests and make sure to produce the test DLL output
RUN dotnet test --no-restore --verbosity normal

# Final stage to produce the test output (optional)
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS final

# Set the working directory in the final stage
WORKDIR /app

# Copy the necessary test DLL from the build stage
COPY --from=build /src/BookLibrary.Tests/bin/Release/net8.0/BookLibrary.Tests.dll .

# Set the entrypoint to run the tests in the final container
ENTRYPOINT ["dotnet", "BookLibrary.Tests.dll"]
