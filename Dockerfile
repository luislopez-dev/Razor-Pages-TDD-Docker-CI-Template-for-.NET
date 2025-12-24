FROM docker pull mcr.microsoft.com/dotnet/sdk:9.0 AS build-env

WORKDIR /app

COPY ["Src/Presentation/Presentation.csproj", "Presentation/"]
COPY ["Src/Application/Application.csproj", "Application/"]
COPY ["Src/Domain/Domain.csproj", "Domain/"]
COPY ["Src/Infrastructure/Infrastructure.csproj", "Infrastructure/"]

RUN dotnet restore "Presentation/Presentation.csproj"
RUN dotnet restore "Application/Application.csproj"
RUN dotnet restore "Domain/Domain.csproj"
RUN dotnet restore "Infrastructure/Infrastructure.csproj"

COPY . ./

WORKDIR "Presentation/"

RUN dotnet publish -c Release -o out

FROM docker pull mcr.microsoft.com/dotnet/aspnet:9.0

RUN sed -i 's/CipherString = DEFAULT@SECLEVEL=2/CipherString = DEFAULT@SECLEVEL=1/g' /etc/ssl/openssl.cnf

COPY --from=build-env /app/Presentation/out .

ENTRYPOINT [ "dotnet", "Presentation.dll" ]

EXPOSE 8080