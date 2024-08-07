#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Ordering/Ordering.Api/Ordering.Api.csproj", "Services/Ordering/Ordering.Api/"]
COPY ["Services/Ordering/Ordering.Application/Ordering.Application.csproj", "Services/Ordering/Ordering.Application/"]
COPY ["Packaged/BasePackage/Base.Behavior/Base.Behavior.csproj", "Packaged/BasePackage/Base.Behavior/"]
COPY ["Packaged/BasePackage/Base/Base.csproj", "Packaged/BasePackage/Base/"]
COPY ["Packaged/BasePackage/Base.Exceptions/Base.Exceptions.csproj", "Packaged/BasePackage/Base.Exceptions/"]
COPY ["Packaged/BasePackage/Base.Extensions/Base.Extensions.csproj", "Packaged/BasePackage/Base.Extensions/"]
COPY ["Services/Ordering/Ordering.Domain/Ordering.Domain.csproj", "Services/Ordering/Ordering.Domain/"]
COPY ["Services/Ordering/Ordering.Infrastructure/Ordering.Infrastructure.csproj", "Services/Ordering/Ordering.Infrastructure/"]
RUN dotnet restore "./Services/Ordering/Ordering.Api/Ordering.Api.csproj"
COPY . .
WORKDIR "/src/Services/Ordering/Ordering.Api"
RUN dotnet build "./Ordering.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Ordering.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ordering.Api.dll"]