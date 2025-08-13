using TodoAppFS.DTOs;
using TodoAppFS.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapTasksEndpoints();

app.Run();
