using TodoAppFS.Data;
using TodoAppFS.Entities;
using TodoAppFS.DTOs;
using TodoAppFS.Mapping;
using Microsoft.EntityFrameworkCore;

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
            tasksGroup.MapGet("/", (TasksContext tasksContext) => 
            tasksContext.Tasks
                        .Select(task => task.ToDTO())
                        .AsNoTracking());

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
            tasksGroup.MapPut("/{id}", (int id, UpdateTaskDTO taskUpdated, TasksContext tasksContext) =>
            {

                //var index = tasks.FindIndex(task => task.Id == id);

                TaskEntity? task = tasksContext.Tasks.Find(id);

                task.Name = taskUpdated.Name;
                task.IsDone = taskUpdated.IsDone;
                   
                if(task == null)
                {
                return Results.NotFound();
                }

                tasksContext.Entry(task)
                .CurrentValues
                .SetValues(taskUpdated.ToEntity(id));

                tasksContext.SaveChanges();
                
                return Results.NoContent();

            });

            //DELETE For deleting tasks
            tasksGroup.MapDelete("/{id}", (int id, TasksContext tasksContext) =>
            {
                tasksContext.Tasks
                            .Where(task => task.Id == id)
                            .ExecuteDelete();

                return Results.NoContent();
            });

            return tasksGroup;
        }
    }
}
