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
app.MapGet("/tasks/{id}", (int id) =>
{
    var task = tasks.Find(task => task.Id == id);


    return task is null ? Results.NotFound() : Results.Ok(task);

}).WithName(GetTaskEndpointName);

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

//PUT for update tasks
app.MapPut("/tasks/{id}", (int id, UpdateTaskDTO taskUpdated) =>
{

    var index = tasks.FindIndex(task => task.Id == id);

    if (index == -1)
        return Results.NotFound();

    tasks[index] = new TaskDTO(
        id,
        taskUpdated.Name,
        taskUpdated.IsDone
        );

    return Results.NoContent();

});

//DELETE For deleting tasks
app.MapDelete("/tasks/{id}", (int id) =>
{
    tasks.RemoveAll(task => task.Id == id);

    return Results.NoContent();
});

app.Run();
