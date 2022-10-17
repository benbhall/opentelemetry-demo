FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

EXPOSE 5001
ENV ASPNETCORE_URLS=http://+:5001

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /build
COPY src/OpenTelemetryDemo/OpenTelemetryDemo.ServiceA .
RUN dotnet restore  

RUN dotnet publish -c Debug -o /app

FROM base AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "OpenTelemetryDemo.ServiceA.dll"]