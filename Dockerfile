FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MlServer.Hosting/MlServer.Hosting.csproj", "MlServer.Hosting/"]
COPY ["MlServer.Orchestrator.Learners/MlServer.Orchestrator.Learners.csproj", "MlServer.Orchestrator.Learners/"]
COPY ["MlServer.Contracts/MlServer.Contracts.csproj", "MlServer.Contracts/"]
COPY ["MlServer.Learners/MlServer.Learners.csproj", "MlServer.Learners/"]
COPY ["MlServer.Database/MlServer.Database.csproj", "MlServer.Database/"]
COPY ["MlServer.Application/MlServer.Application.csproj", "MlServer.Application/"]
RUN dotnet restore "MlServer.Hosting/MlServer.Hosting.csproj"
COPY . .
WORKDIR "/src/MlServer.Hosting"
RUN dotnet build "MlServer.Hosting.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MlServer.Hosting.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MlServer.Hosting.dll"]
