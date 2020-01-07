# ApacheLog

[![Build Status](https://ci.cnetms.info/buildStatus/icon?job=ApacheLog)](#)

Reads data from Apache log files for the purpose of creating IP address blacklists and whitelists.

## Developer Instructions

1. Use the UserSecretsId in Apache.Log.API.csproj to [create a user secrets file](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets)
2. Create a database and add a connection string to ConnectionStrings:ApacheLogContext in the user secrets file
3. Add ef core tools: `dotnet tool install --global dotnet-ef`
4. Apply migrations from the console using: `dotnet ef database update --startup-project src/Apache.Log.API/ --context ApacheLogContext --project src/Apache.Log.Data/`
5. Run NPM Install : `npm install --prefix ./src/Apache.Log.Presentation/ ./src/Apache.Log.Presentation/`

## Developer Instructions 

1. Run the Dotnet core API in watch mode with: `cd ./src/Apache.LogAPI && dotnet watch run`
2. Run the Angular application with: `cd ./src/Apache.Log.Presentation/ && ng serve`

### Adding Migrations

Use the following command to add migrations, where <MigrationName> is the name of the migration:

```Shell
dotnet ef migrations add <MigrationName> --startup-project src/Apache.Log.API/ --context ApacheLogContext --project src/Apache.Log.Data/
```
