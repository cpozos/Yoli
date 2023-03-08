# Build stage

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source
COPY ./src ./src
COPY ./scripts ./scripts

RUN dotnet restore "./src/WebApi/WebApi.csproj"
RUN dotnet publish "./src/WebApi/WebApi.csproj" -c release -o /app --no-restore

# Server stage

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app

# Copies all from build container inside its /app folder to this container's /app folder
COPY --from=build /app ./

RUN ls

EXPOSE 80

# Command to run the published application (dll)
ENTRYPOINT ["dotnet", "WebApi.dll"]

# docker build --no-cache -t yoli .
# docker run -it --rm -p 7029:80 --name yolicontainer yoli