using Test.Model;

namespace Test.Services;

public class TaskService
{
    private readonly AppDbContext _context;

    public TaskService(AppDbContext context)
    {
        _context = context;
    }

    public List<TaskModel> GetTasksForUser(int userId, bool canSee = true)
    {
        var query = _context.Tasks.Where(t => t.CreatorId == userId || t.AssigneeId == userId);
        if (canSee)
        {
            query = query.Where(t => t.CanSee || t.CreatorId == userId);
        }
        return query.ToList();
    }

    public TaskModel GetTaskById(int taskId)
    {
        return _context.Tasks.Find(taskId);
    }

    public void AddTask(TaskModel task)
    {
        task.CreatedDate = DateTime.Now;
        _context.Tasks.Add(task);
        _context.SaveChanges();
    }

    public void UpdateTask(TaskModel task)
    {
        task.UpdatedDate = DateTime.Now;
        _context.Tasks.Update(task);
        _context.SaveChanges();
    }

    public void DeleteTask(int taskId)
    {
        var task = _context.Tasks.Find(taskId);
        if (task != null)
        {
            _context.Tasks.Remove(task);
            _context.SaveChanges();
        }
    }

    public List<TaskModel> GetSubordinateTasks(int managerId)
    {
        var subordinates = _context.Users.Where(u => u.Role == Role.Employee).Select(u => u.Id);
        return _context.Tasks.Where(t => subordinates.Contains(t.AssigneeId)).ToList();
    }

    public List<TaskStatistics> GetTaskStatsByMonth()
    {
        return _context.Tasks.FromSqlRaw("EXEC GetTaskStatsByMonth").ToList();
        // tu lepiej zrobic linq albo metode w bazie danych, ale dla celow przykladu zostawiam tak
    }
}
