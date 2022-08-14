using System.Diagnostics.CodeAnalysis;

namespace TasksManager.Models
{
    public class Taskitem
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public PriorityEnum Priority { get; set; }
        public StatusEnum Status { get; set; }
    }
}