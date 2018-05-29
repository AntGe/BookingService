FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY BookingService/BookingService.csproj BookingService/
RUN dotnet restore BookingService/BookingService.csproj
COPY . .
WORKDIR /src/BookingService
RUN dotnet build BookingService.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish BookingService.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "BookingService.dll"]
