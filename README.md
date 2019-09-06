# ApacheLog

[![Build Status](https://ci.cnetms.info/buildStatus/icon?job=ApacheLog)](#)

Reads data from Apache log files for the purpose of creating IP address blacklists and whitelists.

## Developer Instructions

1. Use the UserSecretsId in Apache.Log.API.csproj to [create a user secrets file](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets)
2. Create a database and add a connection string to ConnectionStrings:ApacheLogContext in the user secrets file
3. Apply migrations from the console using:

```Shell
dotnet ef database update --startup-project src/Apache.Log.API/ --context ApacheLogContext --project src/Apache.Log.Data/
```

4. For development over SSL trust the dotnet developer certificate with:
5. ```Shell
6. dotnet dev-certs https --trust
7. ```

### Adding Migrations

Use the following command to add migrations, where <MigrationName> is the name of the migration:

```Shell
dotnet ef migrations add <MigrationName> --startup-project src/Apache.Log.API/ --context ApacheLogContext --proje
ct src/Apache.Log.Data/
```
