
using Microsoft.EntityFrameworkCore;
using TodoAppFS.Entities;

namespace TodoAppFS.Data
{
    public class TasksContext(DbContextOptions<TasksContext> options) 
        : DbContext(options)
    {
        public DbSet<TaskEntity> Tasks => Set<TaskEntity>();
    }
}
