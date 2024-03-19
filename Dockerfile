FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.sln .

COPY src/core/Domain/*.csproj ./src/core/Domain/
COPY src/core/UserCase/UserCase.csproj ./src/core/UserCase/
COPY src/external/MongoRepository/MongoRepository.csproj ./src/external/MongoRepository/
COPY src/external/AwsServices/AwsServices.csproj ./src/external/AwsServices/
COPY src/interface/gateways/DatabaseGateway/DatabaseGateway.csproj ./src/interface/gateways/DatabaseGateway/
COPY src/interface/gateways/CloudGateway/CloudGateway.csproj ./src/interface/gateways/CloudGateway/
COPY src/interface/gateways/MeioPagamentoGateway/MeioPagamentoGateway.csproj ./src/interface/gateways/MeioPagamentoGateway/
COPY src/interface/presenters/WebAPI/WebAPI.csproj ./src/interface/presenters/WebAPI/
COPY test/UserCase.Tests/UserCase.Tests.csproj ./test/UserCase.Tests/ 
RUN dotnet restore 

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
EXPOSE 80
ENV ASPNETCORE_URLS=http://*:80
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "WebAPI.dll"]