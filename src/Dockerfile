FROM mcr.microsoft.com/dotnet/aspnet:9.0

WORKDIR /app

COPY publish .

EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "AutoParts.dll"]

#Tem que fazer o publish fora do DOckerfile , rode dotnet publish AutoParts/AutoParts.csproj -c Release -o ./publish na pasta /src