# Imagen base para tiempo de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Imagen de compilación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["SweetManagerWebService/SweetManagerWebService.csproj", "SweetManagerWebService/"]
RUN dotnet restore "SweetManagerWebService/SweetManagerWebService.csproj"
COPY . .
WORKDIR "/src/SweetManagerWebService"
RUN dotnet build "SweetManagerWebService.csproj" -c Release -o /app/build

# Publicación
FROM build AS publish
RUN dotnet publish "SweetManagerWebService.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Imagen final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:80
ENTRYPOINT ["dotnet", "SweetManagerWebService.dll"]
