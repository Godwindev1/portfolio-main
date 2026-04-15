# Stage 1: Base Runtime (Optimized for Ubuntu 24.04)
FROM mcr.microsoft.com/dotnet/aspnet:8.0-noble AS base
WORKDIR /app
# We only expose 8080 since Nginx handles the SSL on the host
EXPOSE 8080

# Stage 2: SDK for Building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore as distinct layers for faster caching
COPY ["Portfolio.csproj", "./"]
RUN dotnet restore "Portfolio.csproj"

# Copy everything else and build
COPY . .
RUN dotnet build "Portfolio.csproj" -c Release -o /app/build

# Stage 3: Publish
FROM build AS publish
RUN dotnet publish "Portfolio.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 4: Final Image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# The "Mixed" Strategy:
# 1. It attempts to run migrations using a custom flag.
# 2. If migrations succeed (or if you handle the flag in code), it starts the app.
# 3. $0 and $@ allow you to pass extra arguments during 'docker run' if needed.
ENTRYPOINT ["sh", "-c", "dotnet Portfolio.dll --apply-migrations && dotnet $0 $@"]

# Default argument passed to the entrypoint
CMD ["Portfolio.dll"]