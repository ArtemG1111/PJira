FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /PJira

COPY . .
WORKDIR /PJira/src/PJira.API/
RUN dotnet restore
RUN dotnet build -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0

ENV ASPNETCORE_ENVIRONMENT=Development
EXPOSE 8080

WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "PJira.API.dll"]
