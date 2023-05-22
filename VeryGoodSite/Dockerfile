FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["VeryGoodSite/VeryGoodSite.csproj", "VeryGoodSite/"]
RUN dotnet restore "VeryGoodSite/VeryGoodSite.csproj"
COPY . .
WORKDIR "/src/VeryGoodSite"
RUN dotnet build "VeryGoodSite.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VeryGoodSite.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VeryGoodSite.dll"]
