#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Presentation/CourseApp.API/CourseApp.API.csproj", "src/Presentation/CourseApp.API/"]
COPY ["src/Application/CourseApp.Application/CourseApp.Application.csproj", "src/Application/CourseApp.Application/"]
COPY ["src/Domain/CourseApp.Domain/CourseApp.Domain.csproj", "src/Domain/CourseApp.Domain/"]
COPY ["src/Infrasructure/CourseApp.Infrastructure/CourseApp.Infrastructure.csproj", "src/Infrasructure/CourseApp.Infrastructure/"]
COPY ["src/Infrasructure/CourseApp.Infrastructure/CourseApp.Infrastructure.csproj", "src/Infrasructure/CourseApp.Infrastructure/"]
RUN dotnet restore "./src/Presentation/CourseApp.API/./CourseApp.API.csproj"
COPY . .
WORKDIR "/src/src/Presentation/CourseApp.API"
RUN dotnet build "./CourseApp.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CourseApp.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CourseApp.API.dll"]