FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Library.WebApi/LibraryApi.WebApi.csproj", "Library.WebApi/"]
COPY ["LibraryApi.Business/LibraryApi.Business.csproj", "LibraryApi.Business/"]
COPY ["LibraryApi.DataAccess/LibraryApi.DataAccess.csproj", "LibraryApi.DataAccess/"]
COPY ["LibraryApi.Entities/LibraryApi.Entities.csproj", "LibraryApi.Entities/"]
RUN dotnet restore "Library.WebApi/LibraryApi.WebApi.csproj"

COPY . .
RUN dotnet publish "Library.WebApi/LibraryApi.WebApi.csproj" \
    --configuration Release \
    --output /app/publish \
    --no-restore \
    /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_HTTP_PORTS=8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "LibraryApi.WebApi.dll"]
