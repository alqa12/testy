using Microsoft.EntityFrameworkCore;
using Test.Model;

public class AppDbContext : DbContext
{
    public DbSet<TaskModel> Tasks { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //  var response = await _httpClient.GetStringAsync("http://localhost:5000/task");
        optionsBuilder.UseSqlServer("conncetionString");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskModel>()
            .HasOne(t => t.Assignee)
            .WithMany(u => u.Tasks)
            .HasForeignKey(t => t.AssigneeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TaskModel>()
            .HasOne(t => t.Creator)
            .WithMany()
            .HasForeignKey(t => t.CreatorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}