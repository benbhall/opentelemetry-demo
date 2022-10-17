FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

EXPOSE 6001
ENV ASPNETCORE_URLS=http://+:6001

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /build
COPY src/OpenTelemetryDemo/OpenTelemetryDemo.ServiceB .
RUN dotnet restore  

RUN dotnet publish -c Debug -o /app

FROM base AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "OpenTelemetryDemo.ServiceB.dll"]