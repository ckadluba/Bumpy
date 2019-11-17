FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-stretch AS build
WORKDIR /src
COPY . .
WORKDIR /src/Bumpy.WebApi
RUN dotnet restore "Bumpy.WebApi.csproj"

WORKDIR /src/Bumpy.Domain
RUN dotnet restore "Bumpy.Domain.csproj"

WORKDIR /src/Bumpy.Infrastructure
RUN dotnet restore "Bumpy.Infrastructure.csproj"

WORKDIR /src/Bumpy.WebApi
RUN dotnet build "Bumpy.WebApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Bumpy.WebApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Bumpy.WebApi.dll"]
