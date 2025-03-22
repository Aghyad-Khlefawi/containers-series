FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
RUN apt-get update && apt-get install -y curl
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["HrApp.csproj", "./"]
RUN dotnet restore "HrApp.csproj"
COPY . .
RUN dotnet build "HrApp.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "HrApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

RUN useradd -ms /bin/bash appuser
USER appuser
HEALTHCHECK --interval=1s --timeout=10s --start-period=5s --retries=3 CMD curl -f http://localhost:8080/ || exit 1 
ENTRYPOINT ["dotnet", "HrApp.dll"]
