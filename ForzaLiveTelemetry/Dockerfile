# docker build --platform linux/arm64  --file Dockerfile --tag dercraker0/forza5-telemetry:dev --push .

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 5690/udp

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ForzaLiveTelemetry.csproj", "."]
RUN dotnet restore "./ForzaLiveTelemetry.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "ForzaLiveTelemetry.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ForzaLiveTelemetry.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ForzaLiveTelemetry.dll"]