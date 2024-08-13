namespace Test.Model;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public Role Role { get; set; } 
    public User Assignee { get; set; }
    public string Password { get; set; } = string.Empty;

    public ICollection<TaskModel> Tasks { get; set; } = new List<TaskModel>();
}