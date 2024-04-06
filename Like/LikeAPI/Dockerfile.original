#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Like/LikeAPI/LikeAPI.csproj", "Like/LikeAPI/"]
COPY ["Like/Application/LikeApplication.csproj", "Like/Application/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Like/Infrastructure/LikeInfrastructure.csproj", "Like/Infrastructure/"]
RUN dotnet restore "./Like/LikeAPI/LikeAPI.csproj"
COPY . .
WORKDIR "/src/Like/LikeAPI"
RUN dotnet build "./LikeAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./LikeAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LikeAPI.dll"]