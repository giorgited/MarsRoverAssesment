FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 5005

ENV ASPNETCORE_URLS=http://+:5005

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["MarsRover.Web/MarsRover.Web.csproj", "MarsRover.Web/"]
COPY ["MarsRover.Logic/MarsRover.Logic.csproj", "MarsRover.Logic/"]
RUN dotnet restore "MarsRover.Web/MarsRover.Web.csproj"
COPY . .
WORKDIR "/src/MarsRover.Web"
RUN dotnet build "MarsRover.Web.csproj" -c Debug -o /app

FROM build AS publish
RUN dotnet publish "MarsRover.Web.csproj" -c Debug -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MarsRover.Web.dll"]