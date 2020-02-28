#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Bumpy.API.WebApi/Bumpy.API.WebApi.csproj", "Bumpy.API.WebApi/"]
COPY ["Bumpy.Infrastructure/Bumpy.Infrastructure.csproj", "Bumpy.Infrastructure/"]
COPY ["Bumpy.Domain/Bumpy.Domain.csproj", "Bumpy.Domain/"]
RUN dotnet restore "Bumpy.API.WebApi/Bumpy.API.WebApi.csproj"
COPY . .
WORKDIR "/src/Bumpy.API.WebApi"
RUN dotnet build "Bumpy.API.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Bumpy.API.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bumpy.API.WebApi.dll"]