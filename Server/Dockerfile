FROM mcr.microsoft.com/dotnet/sdk:5.0 AS base

ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
EXPOSE 443

WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

WORKDIR /src

# copy csprojs and restore
COPY ./Shared/*.csproj ./Shared/
COPY ./Client/*.csproj ./Client/
COPY ./Server/*.csproj ./Server/

RUN dotnet restore "./Shared/hosted-wasm.Shared.csproj"
RUN dotnet restore "./Client/hosted-wasm.Client.csproj"
RUN dotnet restore "./Server/hosted-wasm.Server.csproj"

# copy code and build
COPY . .

RUN dotnet build "./Server/hosted-wasm.Server.csproj" -c Release -o /app/build

# publish app
FROM build AS publish
RUN dotnet publish "./Server/hosted-wasm.Server.csproj" -c Release -o /app/publish

# final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "hosted-wasm.Server.dll"]