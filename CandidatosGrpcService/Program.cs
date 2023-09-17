using System.Data;

using CandidatosGrpcService.Factories;
using CandidatosGrpcService.Repository;
using CandidatosGrpcService.Services;

using ClassLibrary.Extensions;

using Dapper;

using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddTransient<SqlConnection>(provider =>
{
	var connectionString = builder.Configuration.GetConnectionString("mssql");
	var connection = new SqlConnection(connectionString);
	connection.Open();
	return connection;
});

SqlMapper.AddTypeHandler(new SqlServerGuidTypeHandler());

builder.Services.AddTransient<CandidatosFactory>();
builder.Services.AddTransient<CandidatosRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.MapGrpcService<CandidatosService>();

app.Run();
