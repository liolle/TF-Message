# SDK containing the CLI
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS sdk
# Image optimized to run ASP.NET Core apps 
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS aspnet

FROM sdk AS build
ARG INVALIDATE_CACHE
WORKDIR src
COPY ./Models ./Models
COPY ./Database ./Database
WORKDIR ReplicaWorker
COPY ./ReplicaWorker .
RUN dotnet publish -c release -o app ReplicaWorker.csproj 

FROM aspnet AS final 
ARG INVALIDATE_CACHE
WORKDIR /src/ReplicaWorker/app
COPY --from=build ./src/ReplicaWorker/app .
WORKDIR /

ENTRYPOINT ["dotnet", "src/ReplicaWorker/app/ReplicaWorker.dll"]
