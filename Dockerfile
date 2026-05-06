FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY src/Clothub.Domain/Clothub.Domain.csproj src/Clothub.Domain/
COPY src/Clothub.Application/Clothub.Application.csproj src/Clothub.Application/
COPY src/Clothub.Persistence/Clothub.Persistence.csproj src/Clothub.Persistence/
COPY src/Clothub.API/Clothub.API.csproj src/Clothub.API/
RUN dotnet restore src/Clothub.API/Clothub.API.csproj

COPY src/ src/
RUN dotnet publish src/Clothub.API/Clothub.API.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
EXPOSE 10000
ENV ASPNETCORE_URLS=http://+:10000
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Clothub.API.dll"]
