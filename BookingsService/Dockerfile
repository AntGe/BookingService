FROM microsoft/dotnet:2.0-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.0-sdk AS build
WORKDIR /src
COPY ../BookingsService/BookingsService.csproj ../BookingsService/
RUN dotnet restore ../BookingsService/BookingsService.csproj
COPY . .
WORKDIR /src/../BookingsService
RUN dotnet build BookingsService.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish BookingsService.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "BookingsService.dll"]
