using TodoAppFS.Data;
using TodoAppFS.Entities;
using TodoAppFS.DTOs;
using TodoAppFS.Mapping;

namespace TodoAppFS.Endpoints
{
    public static class TasksEndpoints
    {

        const string GetTaskEndpointName = "GetTask";

        private static readonly List<TaskDTO> tasks = [
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

        public static RouteGroupBuilder MapTasksEndpoints (this WebApplication app)
        {
            var tasksGroup = app.MapGroup("tasks").WithParameterValidation();


            // GET /tasks
            tasksGroup.MapGet("/", () => tasks);

            // GET by ID
            tasksGroup.MapGet("/{id}", (int id, TasksContext tasksContext) =>
            {
                //var task = tasks.Find(task => task.Id == id);

                TaskEntity? task = tasksContext.Tasks.Find(id);

                return task is null ? Results.NotFound() : Results.Ok(task.ToDTO());

            }).WithName(GetTaskEndpointName);

            //POST create a new tasks
            tasksGroup.MapPost("/", (CreateTaskDTO newTask, TasksContext tasksContext) =>
            {

                TaskEntity task = newTask.ToEntity();

                tasksContext.Tasks.Add(task);
                tasksContext.SaveChanges();

                TaskDTO taskDTO = task.ToDTO();

                return Results.CreatedAtRoute(GetTaskEndpointName, new { id = task.Id }, taskDTO);
            });

            //PUT for update tasks
            tasksGroup.MapPut("/{id}", (int id, UpdateTaskDTO taskUpdated) =>
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
            tasksGroup.MapDelete("/{id}", (int id) =>
            {
                tasks.RemoveAll(task => task.Id == id);

                return Results.NoContent();
            });

            return tasksGroup;
        }
    }
}
