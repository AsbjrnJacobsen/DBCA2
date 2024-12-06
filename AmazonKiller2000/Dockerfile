FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AmazonKiller2000/AmazonKiller2000.csproj", "AmazonKiller2000/"]
RUN dotnet restore "AmazonKiller2000/AmazonKiller2000.csproj"
COPY . .
WORKDIR "/src/AmazonKiller2000"
RUN dotnet build "AmazonKiller2000.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "AmazonKiller2000.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AmazonKiller2000.dll"]
