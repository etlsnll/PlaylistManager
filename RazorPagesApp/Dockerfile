FROM microsoft/aspnetcore:2.0-nanoserver-sac2016 AS base
WORKDIR /app
EXPOSE 22573
EXPOSE 44387

FROM microsoft/aspnetcore-build:2.0-nanoserver-sac2016 AS build
WORKDIR /src
COPY RazorPagesApp/RazorPagesApp.csproj RazorPagesApp/
RUN dotnet restore RazorPagesApp/RazorPagesApp.csproj
COPY . .
WORKDIR /src/RazorPagesApp
RUN dotnet build RazorPagesApp.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish RazorPagesApp.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "RazorPagesApp.dll"]
