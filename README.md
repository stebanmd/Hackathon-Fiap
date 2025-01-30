# Hackathon-Fiap

This project uses Clean Archtecture.


## Setup

To run the application make sure the project `Hackathon.Fiap.AspireHost` is your startup project.

Set the user secrets for the AspireHost project:

```
cd src/Hackathon.Fiap.AspireHost/

dotnet user-secrets set Parameters:sql-password <yourSecretPassword>
```

## Migrations

To create the migrations, run the following script: `addmigration.ps1` on powershell and inform the migration name.