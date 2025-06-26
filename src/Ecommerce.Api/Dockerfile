FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 5000

ENV ASPNETCORE_URLS=http://+:5000

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["src/Ecommerce.Api/Ecommerce.Api.csproj", "src/Ecommerce.Api/"]
RUN dotnet restore "src/Ecommerce.Api/Ecommerce.Api.csproj"
COPY . .
WORKDIR "/src/src/Ecommerce.Api"
RUN dotnet build "Ecommerce.Api.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Ecommerce.Api.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ecommerce.Api.dll"]
