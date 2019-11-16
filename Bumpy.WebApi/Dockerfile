FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-stretch AS build
WORKDIR /src
COPY ["Bumpy.WebApi.csproj", "./"]
RUN dotnet restore "./Bumpy.WebApi.csproj"
COPY . .
WORKDIR /src
RUN dotnet build "Bumpy.WebApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Bumpy.WebApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Bumpy.WebApi.dll"]
