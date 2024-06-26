# development
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS development

RUN mkdir -p /home/dotnet/EST.MIT.Events.Function.Test/ /home/dotnet/EST.MIT.Events.Function/

COPY --chown=dotnet:dotnet ./EST.MIT.Events.Function/*.csproj ./EST.MIT.Events.Function/
RUN dotnet restore ./EST.MIT.Events.Function/EST.MIT.Events.Function.csproj

COPY --chown=dotnet:dotnet ./EST.MIT.Events.Function.Test/*.csproj ./EST.MIT.Events.Function.Test/
RUN dotnet restore ./EST.MIT.Events.Function.Test/EST.MIT.Events.Function.Test.csproj

COPY ./EST.MIT.Events.Function /src
RUN cd /src && \
    mkdir -p /home/site/wwwroot && \
    dotnet publish *.csproj --output /home/site/wwwroot

FROM mcr.microsoft.com/azure-functions/dotnet-isolated:4-dotnet-isolated8.0
ENV AzureWebJobsScriptRoot=/home/site/wwwroot \
    AzureFunctionsJobHost__Logging__Console__IsEnabled=true

COPY --from=development ["/home/site/wwwroot", "/home/site/wwwroot"]

# production
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS production

COPY ./EST.MIT.Events.Function /src
RUN cd /src && \
    mkdir -p /home/site/wwwroot && \
    dotnet publish *.csproj --output /home/site/wwwroot

FROM mcr.microsoft.com/azure-functions/dotnet-isolated:4-dotnet-isolated8.0
ENV AzureWebJobsScriptRoot=/home/site/wwwroot \
    AzureFunctionsJobHost__Logging__Console__IsEnabled=true

COPY --from=production ["/home/site/wwwroot", "/home/site/wwwroot"]
