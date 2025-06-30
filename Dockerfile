FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY CoScheduleOA.csproj ./
RUN dotnet restore CoScheduleOA.csproj

COPY . .
RUN dotnet publish CoScheduleOA.csproj -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app ./
ENV ASPNETCORE_URLS=http://+:80
ENTRYPOINT ["dotnet", "CoScheduleOA.dll"]
