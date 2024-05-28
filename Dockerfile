# Usar la imagen base de .NET SDK para la fase de construcción
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copiar los archivos del proyecto y restaurar las dependencias
COPY *.csproj ./
RUN dotnet restore

# Copiar el resto de los archivos y construir la aplicación
COPY . ./
RUN dotnet publish -c Release -o out

# Usar la imagen base de .NET Runtime para la fase de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Configurar la aplicación para escuchar en el puerto 80
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

# Comando para ejecutar la aplicación
ENTRYPOINT ["dotnet", "1. API.dll"]