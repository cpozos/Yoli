FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY /Yoli.Core.sln .
COPY /src/Generators/Generators.csproj ./src/Generators/
COPY /src/Domain/Domain.csproj ./src/Domain/
COPY /src/App/App.csproj ./src/App/
COPY /src/Infraestructure/Infraestructure.csproj ./src/Infraestructure/
COPY /src/WebApi/WebApi.csproj ./src/WebApi/
RUN dotnet restore
COPY . .
RUN dotnet publish -c release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
EXPOSE 80
# Copies all from build container inside its /app folder to this container's /app folder
COPY --from=build /app .
ENTRYPOINT ["dotnet", "WebApi.dll"]

# docker build -t yoli .
# docker run -it -rm -p 3000:80 --name yolicontainer yoli