# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 as build-image
WORKDIR /home/app

# Copy everything and restore dependencies
COPY . ./
RUN dotnet restore

# Publish the application
RUN dotnet publish ./HishabNikash/HishabNikash.csproj -c Release -o /publish/

# Stage 2: Run the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /publish

# Copy the published app from the build stage
COPY --from=build-image /publish .

# Copy the SSL certificate (ensure this path exists locally)
COPY ./certs/aspnetapp.pfx /https/aspnetapp.pfx

# Define URL for Kestrel (both HTTP and HTTPS)
ENV ASPNETCORE_URLS=http://+:5000;https://+:5001

# Provide path to the SSL certificate and password as environment variables
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx
ENV ASPNETCORE_Kestrel__Certificates__Default__Password=password1234

# Expose both HTTP and HTTPS ports
EXPOSE 5000 5001

ENTRYPOINT ["dotnet", "HishabNikash.dll"]
