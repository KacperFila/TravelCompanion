FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
EXPOSE 5001
EXPOSE 5005

ARG APP_UID=1000
ENV ASPNETCORE_URLS=http://+:5000
ENV DOTNET_RUNNING_IN_CONTAINER=true

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

ENV POSTGRES_HOST=travelcompanion-postgres
ENV POSTGRES_DB=travelCompanion
ENV POSTGRES_USER=postgres
ENV POSTGRES_PASSWORD=postgres
ENV HANGFIRE_POSTGRES_HOST=travelcompanion-postgres
ENV HANGFIRE_POSTGRES_DB=travelCompanion
ENV HANGFIRE_POSTGRES_USER=postgres
ENV HANGFIRE_POSTGRES_PASSWORD=postgres

COPY ["src/Bootstrapper/TravelCompanion.Bootstrapper/TravelCompanion.Bootstrapper.csproj", "src/Bootstrapper/TravelCompanion.Bootstrapper/"]
COPY ["src/Modules/Emails/TravelCompanion.Modules.Emails.Api/TravelCompanion.Modules.Emails.Api.csproj", "src/Modules/Emails/TravelCompanion.Modules.Emails.Api/"]
COPY ["src/Modules/Emails/TravelCompanion.Modules.Emails.Core/TravelCompanion.Modules.Emails.Core.csproj", "src/Modules/Emails/TravelCompanion.Modules.Emails.Core/"]
COPY ["src/Shared/TravelCompanion.Shared.Infrastructure/TravelCompanion.Shared.Infrastructure.csproj", "src/Shared/TravelCompanion.Shared.Infrastructure/"]
COPY ["src/Shared/TravelCompanion.Shared.Abstractions/TravelCompanion.Shared.Abstractions.csproj", "src/Shared/TravelCompanion.Shared.Abstractions/"]
COPY ["src/Modules/Travels/TravelCompanion.Modules.Travels.Shared/TravelCompanion.Modules.Travels.Shared.csproj", "src/Modules/Travels/TravelCompanion.Modules.Travels.Shared/"]
COPY ["src/Modules/Users/TravelCompanion.Modules.Users.Shared/TravelCompanion.Modules.Users.Shared.csproj", "src/Modules/Users/TravelCompanion.Modules.Users.Shared/"]
COPY ["src/Modules/Travels/TravelCompanion.Modules.Travels.Core/TravelCompanion.Modules.Travels.Core.csproj", "src/Modules/Travels/TravelCompanion.Modules.Travels.Core/"]
COPY ["src/Modules/TravelPlans/TravelCompanion.Modules.TravelPlans.Shared/TravelCompanion.Modules.TravelPlans.Shared.csproj", "src/Modules/TravelPlans/TravelCompanion.Modules.TravelPlans.Shared/"]
COPY ["src/Modules/TravelPlans/TravelCompanion.Modules.TravelPlans.Domain/TravelCompanion.Modules.TravelPlans.Domain.csproj", "src/Modules/TravelPlans/TravelCompanion.Modules.TravelPlans.Domain/"]
COPY ["src/Modules/TravelPlans/TravelCompanion.Modules.TravelPlans.Api/TravelCompanion.Modules.TravelPlans.Api.csproj", "src/Modules/TravelPlans/TravelCompanion.Modules.TravelPlans.Api/"]
COPY ["src/Modules/TravelPlans/TravelCompanion.Modules.TravelPlans.Infrastructure/TravelCompanion.Modules.TravelPlans.Infrastructure.csproj", "src/Modules/TravelPlans/TravelCompanion.Modules.TravelPlans.Infrastructure/"]
COPY ["src/Modules/TravelPlans/TravelCompanion.Modules.TravelPlans.Application/TravelCompanion.Modules.TravelPlans.Application.csproj", "src/Modules/TravelPlans/TravelCompanion.Modules.TravelPlans.Application/"]
COPY ["src/Modules/Travels/TravelCompanion.Modules.Travels.Api/TravelCompanion.Modules.Travels.Api.csproj", "src/Modules/Travels/TravelCompanion.Modules.Travels.Api/"]
COPY ["src/Modules/Users/TravelCompanion.Modules.Users.Api/TravelCompanion.Modules.Users.Api.csproj", "src/Modules/Users/TravelCompanion.Modules.Users.Api/"]
COPY ["src/Modules/Users/TravelCompanion.Modules.Users.Core/TravelCompanion.Modules.Users.Core.csproj", "src/Modules/Users/TravelCompanion.Modules.Users.Core/"]

RUN dotnet restore "./src/Bootstrapper/TravelCompanion.Bootstrapper/TravelCompanion.Bootstrapper.csproj"

COPY . .

WORKDIR "/src/src/Bootstrapper/TravelCompanion.Bootstrapper"
RUN dotnet build "TravelCompanion.Bootstrapper.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "TravelCompanion.Bootstrapper.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
ARG APP_UID=1000
USER $APP_UID
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TravelCompanion.Bootstrapper.dll"]

FROM base AS debug
WORKDIR /app
COPY --from=publish /app/publish .

ENV DOTNET_USE_POLLING_FILE_WATCHER=1
ENV DOTNET_HOSTBUILDER__RELOADCONFIGONCHANGE=false
ENV ASPNETCORE_ENVIRONMENT=Development

CMD ["dotnet", "TravelCompanion.Bootstrapper.dll", "--urls", "http://+:5000"]
