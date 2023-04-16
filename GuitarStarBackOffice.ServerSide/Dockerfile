FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["GuitarStarBackOffice.ServerSide/GuitarStarBackOffice.ServerSide.csproj", "GuitarStarBackOffice.ServerSide/"]
COPY ["GuitarStarBackOffice/Shared/GuitarStarBackOffice.Shared.csproj", "GuitarStarBackOffice/Shared/"]
RUN dotnet restore "GuitarStarBackOffice.ServerSide/GuitarStarBackOffice.ServerSide.csproj"
COPY . .
WORKDIR "/src/GuitarStarBackOffice.ServerSide"
RUN dotnet build "GuitarStarBackOffice.ServerSide.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GuitarStarBackOffice.ServerSide.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GuitarStarBackOffice.ServerSide.dll"]


