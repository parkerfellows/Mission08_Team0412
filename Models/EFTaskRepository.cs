﻿using Microsoft.EntityFrameworkCore;

namespace Mission08_Team0412.Models
{
    public class EFTaskRepository : ITaskRepository
    {
        private TaskDbContext _context;
        public EFTaskRepository(TaskDbContext temp) 
        {
            _context = temp;
        }
        public List<TaskItem> Tasks => _context.Tasks.Include(t => t.Category).ToList();

        List<TaskItem> ITaskRepository.Tasks { get => Tasks; set => throw new NotImplementedException(); }
    }
}
