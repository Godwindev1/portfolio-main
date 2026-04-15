# Stage 1: Base Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0-noble AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Stage 2: SDK for Building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the csproj first to leverage Docker layer caching
COPY ["Portfolio.csproj", "./"]
RUN dotnet restore "Portfolio.csproj"

# Copy the rest of the source code
COPY . .

# Build the project
# Note: Stay in /src because that is where the .csproj is located
RUN dotnet build "Portfolio.csproj" -c Release -o /app/build

# Stage 3: Publish
FROM build AS publish
RUN dotnet publish "Portfolio.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 4: Final Image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Portfolio.dll"]