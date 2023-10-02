# docker build --platform linux/arm64  --file Dockerfile --tag dercraker0/forza5-telemetry:dev --push .

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY "/bin/Release/net7.0/publish/" .
EXPOSE 80
EXPOSE 443
EXPOSE 5690
ENTRYPOINT ["dotnet", "ForzaLiveTelemety.dll"]