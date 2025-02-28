using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Mission08_Team0412.Models
{
    public class TaskItem
    {
        [Key]
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string? DueDate { get; set; }
        public int Quadrant { get; set; }
        [ForeignKey(name: "CategoryId")]
        public Category Category { get; set; }
        public bool Completed { get; set; }


    }
}
