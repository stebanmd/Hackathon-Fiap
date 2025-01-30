$name = Read-Host -Prompt "Enter the name of the migration"

dotnet ef migrations add -s "./src/Hackathon.Fiap.Web/Hackathon.Fiap.Web.csproj" -p "./src/Hackathon.Fiap.Infrastructure/Hackathon.Fiap.Infrastructure.csproj" "${name}" -c "Hackathon.Fiap.Infrastructure.Data.AppDbContext" -o "Data/Migrations"