# docker build --platform linux/arm64  --file Dockerfile --tag dercraker0/forza5-telemetry:dev --push .

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 5690

FROM base AS final
WORKDIR /app
COPY /home/runner/work/FH5_Telemetry/FH5_Telemetry/bin/Release/net7.0/publish/ .
ENTRYPOINT ["dotnet", "ForzaLiveTelemety.dll"]