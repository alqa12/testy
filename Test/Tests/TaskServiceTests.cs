using Microsoft.EntityFrameworkCore;
using Test.Model;
using Test.Services;
using Xunit;

namespace Test.Tests;

public class TaskServiceTests
{
    private readonly TaskService _taskService;

    public TaskServiceTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "XXXX") // tu zosatwie w tej formie niestety
            .Options;
        var context = new AppDbContext(options);
        _taskService = new TaskService(context);
    }

    [Fact]
    public void AddTask_ShouldAddTask()
    {
        var task = new TaskModel { Title = "Test Task", Priority = TaskPriority.Medium, CreatorId = 1 };
        _taskService.AddTask(task);
        Assert.Single(_taskService.GetTasksForUser(1));
    }

    [Fact]
    public void DeleteTask_ShouldRemoveTask()
    {
        var task = new TaskModel { Title = "Test Task", Priority = TaskPriority.Medium, CreatorId = 1 };
        _taskService.AddTask(task);
        _taskService.DeleteTask(task.Id);
        Assert.Empty(_taskService.GetTasksForUser(1));
    }
}
