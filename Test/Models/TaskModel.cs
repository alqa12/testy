namespace Test.Model;

public class TaskModel
{
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskPriority Priority { get; set; }
        public int AssigneeId { get; set; }
        public User Assignee { get; set; }
        public int CreatorId { get; set; }
        public User Creator { get; set; }
        public bool CanSee { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

}