var builder = DistributedApplication.CreateBuilder(args);

var sqlPassword = builder.AddParameter("sql-password", secret: true);

/*
 * If there this error happens when starting the containers:
 * 
 * "could not start the proxy for the service: listen tcp [::1]:51780: bind: An attempt was made to access a socket in a way forbidden by its access permissions.
 * 
 * Open your terminal as administrator and run:
 * 
 * net stop hns
 * net start hns
 * 
 * This will reset the Host Network System
 *
 */
var sql = builder.AddSqlServer("sqlServer", password: sqlPassword, port: 51780)
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

var database = sql.AddDatabase("hackathonDb");

builder.AddContainer("papercut", "jijiechen/papercut", tag: "latest")
    .WithEndpoint(port: 25, targetPort: 25)
    .WithEndpoint(port: 37408, targetPort: 37408, scheme: "http");

builder.AddProject<Projects.Hackathon_Fiap_Api_Patients>("api-patients")
    .WithReference(database)
    .WaitFor(database);

builder.AddProject<Projects.Hackathon_Fiap_Api_Doctors>("api-doctors")
    .WithReference(database)
    .WaitFor(database); ;

await builder.Build().RunAsync();
