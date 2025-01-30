var builder = DistributedApplication.CreateBuilder(args);

var sqlPassword = builder.AddParameter("sql-password", secret: true);

var sql = builder.AddSqlServer("sqlServer", password: sqlPassword, port: 51780)
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

var database = sql.AddDatabase("hackathonDb");

builder.AddContainer("papercut", "jijiechen/papercut", tag: "latest")
    .WithEndpoint(port: 25, targetPort: 25)
    .WithEndpoint(port: 37408, targetPort: 37408, scheme: "http");

builder.AddProject<Projects.Hackathon_Fiap_Web>("api")
    .WithReference(database)
    .WaitFor(database);

await builder.Build().RunAsync();
