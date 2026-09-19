FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY CardShopInventory/CardShopInventory.csproj CardShopInventory/
RUN dotnet restore CardShopInventory/CardShopInventory.csproj

COPY CardShopInventory/ CardShopInventory/
WORKDIR /src/CardShopInventory
RUN dotnet publish -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["/bin/sh", "-c", "dotnet CardShopInventory.dll --urls http://+:${PORT:-8080}"]