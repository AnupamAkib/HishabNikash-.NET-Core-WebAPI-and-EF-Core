# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 as build-image
WORKDIR /home/app
COPY ./*.sln ./
COPY ./*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p ./${file%.*}/ && mv $file ./${file%.*}/; done
RUN dotnet restore

COPY . .
RUN dotnet publish ./HishabNikash/HishabNikash.csproj -o /publish/

# Stage 2: Run the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /publish
COPY --from=build-image /publish .

# Define URL for Kestrel (HTTP only)
ENV ASPNETCORE_URLS=http://+:5000

ENTRYPOINT ["dotnet", "HishabNikash.dll"]
