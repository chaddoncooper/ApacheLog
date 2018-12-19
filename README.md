# ApacheLog

Reads data from Apache log files for the purpose of creating IP address blacklists and whitelists.

## Developer Instructions

1. Use the UserSecretsId in Apache.Log.API.csproj to [create a user secrets file](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets)
2. Create a database and add a connection string to ConnectionStrings:ApacheLogContext in the user secrets file
3. Apply migrations from the console using:

```Shell
dotnet ef database update --startup-project src/Apache.Log.API/ --context ApacheLogContext --project src/Apache.
Log.Data/
```
