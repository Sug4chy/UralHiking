FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["UralHiking/UralHiking.csproj", "UralHiking/"]
RUN dotnet restore "UralHiking/UralHiking.csproj"
COPY . .
WORKDIR "/src/UralHiking"
RUN dotnet build "./UralHiking.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./UralHiking.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN "mkdir /app/static"
RUN "mkdir /app/static/images"
ENTRYPOINT ["dotnet", "UralHiking.dll"]
