using TodoAppFS.DTOs;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

const string GetTaskEndpointName = "GetTask";

List<TaskDTO> tasks = [
    new (
        1,
        "Get food",
        false
        ),

        new (
        2,
        "Get water",
        false
        )
    ];

// GET /tasks
app.MapGet("/tasks", () => tasks);

// GET by ID
app.MapGet("/tasks/{id}", (int id) => tasks.Find(task => task.Id == id)).WithName(GetTaskEndpointName);

//POST create a new tasks
app.MapPost("/tasks", (CreateTaskDTO newTask) =>
{
    TaskDTO task = new
    (
        tasks.Count + 1,
        newTask.Name,
        newTask.IsDone
    );

    tasks.Add(task);

    return Results.CreatedAtRoute(GetTaskEndpointName, new { id = task.Id}, task);
});

app.Run();
