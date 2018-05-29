FROM microsoft/dotnet:2.0-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.0-sdk AS build
WORKDIR /src
COPY PaymentsService/PaymentsService.csproj PaymentsService/
RUN dotnet restore PaymentsService/PaymentsService.csproj
COPY . .
WORKDIR /src/PaymentsService
RUN dotnet build PaymentsService.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish PaymentsService.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "PaymentsService.dll"]
