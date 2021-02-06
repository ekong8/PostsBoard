FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
COPY src /app
WORKDIR /app

RUN dotnet restore
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS runtime
WORKDIR /app
COPY --from=build /app/PostsBoard/out .
ENV ASPNETCORE_URLS http://*:5000
ENTRYPOINT ["dotnet", "PostsBoard.dll"]