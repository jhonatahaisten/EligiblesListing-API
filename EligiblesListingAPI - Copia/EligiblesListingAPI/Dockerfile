#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["EligiblesListingAPI/EligiblesListingAPI.csproj", "EligiblesListingAPI/"]
COPY ["EligiblesListingAPI.Application/EligiblesListingAPI.Application.csproj", "EligiblesListingAPI.Application/"]
COPY ["EligiblesListingAPI.Domain/EligiblesListingAPI.Domain.csproj", "EligiblesListingAPI.Domain/"]
COPY ["EligiblesListingAPI.Infrastructure/EligiblesListingAPI.Infrastructure.csproj", "EligiblesListingAPI.Infrastructure/"]
RUN dotnet restore "./EligiblesListingAPI/EligiblesListingAPI.csproj"
COPY . .
WORKDIR "/src/EligiblesListingAPI"
RUN dotnet build "./EligiblesListingAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EligiblesListingAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EligiblesListingAPI.dll"]