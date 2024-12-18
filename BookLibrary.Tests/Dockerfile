FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY ["BookLibrary.Tests/BookLibrary.Tests.csproj", "BookLibrary.Tests/"]
COPY ["BookLibrary.Api/BookLibrary.csproj", "BookLibrary/"]

RUN dotnet restore "BookLibrary.Tests/BookLibrary.Tests.csproj"
COPY . .
RUN dotnet build -c Release
RUN dotnet test --no-restore --verbosity detailed --logger "xunit;LogFileName=test-results.xml"
