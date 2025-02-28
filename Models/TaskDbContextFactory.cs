using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Mission08_Team0412.Models
{
    public class TaskDbContextFactory : IDesignTimeDbContextFactory<TaskDbContext>
    {
        public TaskDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TaskDbContext>();
            optionsBuilder.UseSqlite("Data Source=task.db");// Ensure SQLite is used

            return new TaskDbContext(optionsBuilder.Options);
        }
    }
}

