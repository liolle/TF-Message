# SDK containing the CLI
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS sdk
# Image optimized to run ASP.NET Core apps 
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS aspnet

FROM sdk AS build
ARG INVALIDATE_CACHE
WORKDIR src
COPY ./Models ./Models
COPY ./Database ./Database
WORKDIR Read
COPY ./Read .
RUN dotnet publish -c release -o app Read.csproj 

FROM aspnet AS final 
ARG INVALIDATE_CACHE
WORKDIR /src/Read/app
COPY --from=build ./src/Read/app .
ENTRYPOINT ["dotnet", "Read.dll"]
