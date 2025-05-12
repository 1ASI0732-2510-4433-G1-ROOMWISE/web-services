FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5181
EXPOSE 7138

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SweetManagerWebService/SweetManagerWebService.csproj", "SweetManagerWebService/"]
RUN dotnet restore "SweetManagerWebService/SweetManagerWebService.csproj"
COPY . .
WORKDIR "/src/SweetManagerWebService"
RUN dotnet build "SweetManagerWebService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "SweetManagerWebService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_ENVIRONMENT=Production
ENTRYPOINT ["dotnet", "SweetManagerWebService.dll"]
