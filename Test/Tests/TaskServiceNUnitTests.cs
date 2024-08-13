using NUnit.Framework;
using Test.Model;
using Test.Services;
namespace Test.Tests;

public class TaskServiceNUnitTests
{
    private readonly TaskService _taskService;
    

    [Test]
    public void Add_ShouldReturnSumOfTwoNumbers()
    {
        // Arrange
        var task = new TaskModel {Title = "Test Task", Priority = TaskPriority.Medium, CreatorId = 1};

        // Act
        _taskService.AddTask(task);

        // Assert
        var tasks = _taskService.GetTasksForUser(1);
        Assert.AreEqual(1, tasks.Count);
        Assert.AreEqual("Test Task", tasks[0].Title);
        Assert.AreEqual("Test Description", tasks[0].Description);
 
        //oczywiscie, mozna sprawdzac czy istnieje taki user, czy lista taskow jest pusta, albo sprawdzac uwzgledniajac id czy liczbe czy cokolwiek itp, te testy są poprstu przykładem
    }
    
    [Test]
    public void UpdateTask_ShouldUpdateExistingTask()
    {
        var task = new TaskModel
        {
            Title = "Initial Title",
            Description = "Initial Description",
            Priority = TaskPriority.Low,
            AssigneeId = 1,
            CreatorId = 2,
            CanSee = true
        }; 
        //mozna to wyciagnac do metody SetUp, ale dla celow przykladu zostawiam tak

        _taskService.AddTask(task);

        var taskToUpdate = _taskService.GetTasksForUser(1).First(); //naciaganie na pierwszy task, ale w rzeczywistosci trzeba bylo by sprawdzic czy task istnieje, czy jest przypisany do usera itp
        taskToUpdate.Title = "Updated Title";
        taskToUpdate.Description = "Updated Description";

        _taskService.UpdateTask(taskToUpdate);

        var updatedTask = _taskService.GetTasksForUser(1).First();
        Assert.AreEqual("Updated Title", updatedTask.Title);
        Assert.AreEqual("Updated Description", updatedTask.Description);
        Assert.IsNotNull(updatedTask.UpdatedDate);
    }
    [Test]
    public void DeleteTask_ShouldRemoveTaskFromList()
    {
        var task = new TaskModel
        {
            Title = "Task to be Deleted",
            Description = "Description",
            Priority = TaskPriority.Medium,
            AssigneeId = 1,
            CreatorId = 2,
            CanSee = true
        };

        _taskService.AddTask(task);
        var taskId = _taskService.GetTasksForUser(1).First().Id;

        _taskService.DeleteTask(taskId);

        var tasks = _taskService.GetTasksForUser(1).First();
        Assert.IsEmpty(tasks);
    }
}