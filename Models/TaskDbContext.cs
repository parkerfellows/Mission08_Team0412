using Microsoft.EntityFrameworkCore;
namespace Mission08_Team0412.Models
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options) { }

        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<Category> Categories { get; set; }

        internal void AddTask(TaskItem task)
        {
            throw new NotImplementedException();
        }

        internal void DeleteTask(TaskItem task)
        {
            throw new NotImplementedException();
        }

        internal void UpdateTask(TaskItem task)
        {
            throw new NotImplementedException();
        }
    }
}
