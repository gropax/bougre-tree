#FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

#FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
FROM microsoft/dotnet:2.2-sdk AS build
# Setup NodeJs
RUN apt-get update && \
    apt-get install -y wget && \
    apt-get install -y gnupg2 && \
    wget -qO- https://deb.nodesource.com/setup_10.x | bash - && \
    apt-get install -y build-essential nodejs
	#&& \ npm install -g @angular/cli
# End setup
WORKDIR /src
COPY ["Tree/Tree.csproj", "Tree/"]
RUN dotnet restore "Tree/Tree.csproj"
COPY . .
WORKDIR "/src/Tree"
RUN dotnet build "Tree.csproj" -c Release -o /app

FROM build AS publish
RUN rm -rf ./ClientApp/node_modules
RUN npm install -g @angular/cli
RUN dotnet publish "Tree.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Tree.dll"]