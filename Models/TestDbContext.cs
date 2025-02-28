using System;
using Microsoft.EntityFrameworkCore;
using Mission08_Team0412.Models;

class TestDbContext
{
    static void Main()
    {
        try
        {
            var optionsBuilder = new DbContextOptionsBuilder<TaskDbContext>();
            optionsBuilder.UseSqlite("Data Source=task.db");

            using (var context = new TaskDbContext(optionsBuilder.Options))
            {
                Console.WriteLine("DbContext created successfully!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
