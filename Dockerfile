# Etapa de construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src

# Copia el archivo de solución y los archivos de proyecto
COPY EccomerceApi.sln ./

COPY Data/Data.csproj Data/
COPY AplicationLayer/AplicationLayer.csproj AplicationLayer/
COPY EnterpriseLayer/EnterpriseLayer.csproj EnterpriseLayer/
COPY idenitywebapiauthenitcation/EccomerceApi.csproj idenitywebapiauthenitcation/
COPY Mappers/Mappers.csproj Mappers/
COPY Models/Models.csproj Models/
COPY Presenters/Presenters.csproj Presenters/
COPY Repository/Repository.csproj Repository/

# Restaura las dependencias
RUN dotnet restore

# Copia el resto del código fuente y compila la aplicación
COPY . .
RUN dotnet publish idenitywebapiauthenitcation/EccomerceApi.csproj -c Release -o /app

# Etapa final
FROM mcr.microsoft.com/dotnet/aspnet:8.0
ENV ASPNETCORE_URLS http://+:8080
ENV ASPNETCORE_ENVIRONMENT Production
EXPOSE 8080
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "EccomerceApi.dll"]