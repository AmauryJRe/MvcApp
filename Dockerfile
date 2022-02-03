# FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
# WORKDIR /source

# # copy csproj and restore as distinct layers
# COPY mvcapp/*.csproj .
# RUN dotnet restore

# # copy and publish app and libraries
# COPY mvcapp/. .
# RUN dotnet publish -c release -o /app --no-restore

# # final stage/image
# FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS runtime
# WORKDIR /app
# COPY --from=build /app .
# ENTRYPOINT ["dotnet", "mvcapp.dll"]

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY ./*.sln ./
COPY ./*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p ./${file%.*}/ && mv $file ./${file%.*}/; done

RUN dotnet restore

# copy and publish app and libraries
COPY . .
RUN dotnet test --verbosity=normal --results-directory /TestResults/ --logger "trx;LogFileName=test_results.xml" ./mvcapptests/mvcapptests.csproj

RUN dotnet publish -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS runtime
WORKDIR /app
COPY --from=build /app .
COPY --from=build /TestResults /TestResults
RUN ls -l
ENTRYPOINT ["dotnet", "mvcapp.dll"]