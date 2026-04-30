# Base image (runtime)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

# Build image
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# csproj kopyala ve restore et
COPY ["FinTrack.API.csproj", "./"]
RUN dotnet restore "FinTrack.API.csproj"

# tüm dosyaları kopyala
COPY . .

# publish
RUN dotnet publish "FinTrack.API.csproj" -c Release -o /app/publish

# Final image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "FinTrack.API.dll"]