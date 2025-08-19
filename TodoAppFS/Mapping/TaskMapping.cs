using System.Runtime.CompilerServices;
using System.Xml.Linq;
using TodoAppFS.DTOs;
using TodoAppFS.Entities;

namespace TodoAppFS.Mapping
{
    public static class TaskMapping
    {

        public static TaskEntity ToEntity(this CreateTaskDTO task)
        {
            return new TaskEntity()
            {
                Name = task.Name,
                IsDone = task.IsDone
            };
                
        }

        public static TaskDTO ToDTO(this TaskEntity task)
        {
            return new(
               task.Id,
               task.Name,
               task.IsDone
             );
        }
    }
}
