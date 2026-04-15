# Stage 1: Base Runtime (Lightweight)
FROM mcr.microsoft.com/dotnet/aspnet:9.0-noble AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Stage 2: SDK for Building (Heavyweight)
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["Portfolio.csproj", ""]

RUN ls
RUN dotnet restore "Portfolio.csproj"
COPY . .
WORKDIR "/src/portfolio"
RUN dotnet build "Portfolio.csproj" -c Release -o /app/build

# Stage 3: Publish the artifacts
FROM build AS publish
RUN dotnet publish "Portfolio.csproj" -c Release -o /app/publish

# Stage 4: Final Image (Production ready)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Portfolio.dll"]
