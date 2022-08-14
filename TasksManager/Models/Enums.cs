namespace TasksManager.Models;

// Priority (High, Middle, Low)
public enum PriorityEnum
{
    High = 1,
    Middle = 0,
    Low = -1
}

// Status (New, In Progress, Finished)
public enum StatusEnum
{
    New = -1,
    InProgress = 0,
    Finished = 1
}
