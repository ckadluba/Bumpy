FROM microsoft/dotnet:3.0-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:3.0-sdk AS build
WORKDIR /src
COPY ["Bumpy.csproj", "./"]
RUN dotnet restore "./Bumpy.csproj"
COPY . .
WORKDIR /src
RUN dotnet build "Bumpy.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Bumpy.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Bumpy.dll"]
