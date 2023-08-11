# Base Image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80 443


# Copy Solution File to support Multi-Project
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY TalentConsulting.TalentSuite.Users.API.sln ./

# Copy Dependencies
COPY ["src/TalentConsulting.TalentSuite.Users.API/TalentConsulting.TalentSuite.Users.API.csproj", "src/TalentConsulting.TalentSuite.Users.API/"]
COPY ["src/TalentConsulting.TalentSuite.Users.Core/TalentConsulting.TalentSuite.Users.Core.csproj", "src/TalentConsulting.TalentSuite.Users.Core/"]
COPY ["src/TalentConsulting.TalentSuite.Users.Infrastructure/TalentConsulting.TalentSuite.Users.Infrastructure.csproj", "src/TalentConsulting.TalentSuite.Users.Infrastructure.Infrastructure/"]
COPY ["src/TalentConsulting.TalentSuite.Users.Common/TalentConsulting.TalentSuite.Reports.Users.csproj", "src/TalentConsulting.TalentSuite.Users.Common/"]

# Restore Project
RUN dotnet restore "src/TalentConsulting.TalentSuite.Users.API/TalentConsulting.TalentSuite.Users.API.csproj"

# Copy Everything
COPY . .

# Build
WORKDIR "/src/src/TalentConsulting.TalentSuite.Users.API"
RUN dotnet build "TalentConsulting.TalentSuite.Users.API.csproj" -c Release -o /app/build

# publish
FROM build AS publish
WORKDIR "/src/src/TalentConsulting.TalentSuite.Users.API"
RUN dotnet publish "TalentConsulting.TalentSuite.Users.API.csproj" -c Release -o /app/publish

# Build runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TalentConsulting.TalentSuite.Users.API.dll"]