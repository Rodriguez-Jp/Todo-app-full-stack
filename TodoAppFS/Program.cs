using TodoAppFS.Data;
using TodoAppFS.DTOs;
using TodoAppFS.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("TasksApp");

builder.Services.AddSqlite<TasksContext>(connString);

var app = builder.Build();

app.MapTasksEndpoints();

app.MigrateDB();

app.Run();
