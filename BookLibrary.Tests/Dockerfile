# Use the official .NET SDK image for building and running tests
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src
# Copy the unit test project and its dependencies
COPY ["BookLibrary.Tests/BookLibrary.Tests.csproj", "BookLibrary.Tests/"]
COPY ["BookLibrary.Api/BookLibrary.csproj", "BookLibrary.Api/"]

# Restore dependencies for both the test and main projects
RUN dotnet restore "BookLibrary.Tests/BookLibrary.Tests.csproj"

# Copy the rest of the source code for both projects
COPY . .

# Build the solution
WORKDIR /src/BookLibrary.Tests
RUN dotnet build -c Release

# Run the tests (this step can be adjusted based on your test framework)
RUN dotnet test --no-restore --verbosity normal

# Final stage to produce the test output (Optional)
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS final
WORKDIR /app
COPY --from=build /src/BookLibrary.Tests .
ENTRYPOINT ["dotnet", "BookLibrary.Tests.dll"]
