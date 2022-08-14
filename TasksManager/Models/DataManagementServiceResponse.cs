using System.Diagnostics.CodeAnalysis;

namespace TasksManager.Models;

[ExcludeFromCodeCoverage]
public class TaskManagementServiceResponse
{
    public int StatusCode { get; set; }
    public string? StatusMessage { get; set; }
}