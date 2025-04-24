FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 5690/udp

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY ForzaLiveTelemetry/*.csproj ForzaLiveTelemetry/
COPY ForzaLiveTelemetry.Domain/*.csproj ForzaLiveTelemetry.Domain/
COPY ForzaLiveTelemetry.EFCore/*.csproj ForzaLiveTelemetry.EFCore/

RUN dotnet restore ForzaLiveTelemetry/*.csproj
COPY . .
RUN dotnet build ForzaLiveTelemetry/*.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish ForzaLiveTelemetry/*.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM base AS runtime

WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "ForzaLiveTelemetry.dll"]
