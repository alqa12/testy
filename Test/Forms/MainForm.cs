using System.Text;
using System.Windows.Forms;
using Test.Model;
using Test.Services;

namespace Test.Forms;

public class MainForm : Form
{
    private TaskService _taskService;
    private User _currentUser;
    private DataGridView _taskGridView;

    public MainForm(User currentUser)
    {
        _currentUser = currentUser;
        _taskService = new TaskService(new AppDbContext());
        InitializeComponents();
        LoadTasks();
    }

    private void InitializeComponents()
    {
        _taskGridView = new DataGridView { Dock = DockStyle.Fill };
        Controls.Add(_taskGridView);
        
        if (_currentUser.Role == Role.Manager)
        {
            var managerTab = new TabPage("Manager");
            var statsButton = new Button { Text = "Generate Stats" };
            statsButton.Click += (sender, args) => ShowStats();
            managerTab.Controls.Add(statsButton);
            Controls.Add(managerTab);
        }
    }

    private void LoadTasks()
    {
        var tasks = _taskService.GetTasksForUser(_currentUser.Id);
        _taskGridView.DataSource = tasks;
    }
    
    private void ShowStats()
    {
        var stats = _taskService.GetTaskStatsByMonth();

        if (stats.Count == 0)
        {
            MessageBox.Show("Brak statystyk do wyświetlenia.", "Statystyki", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        using (var statsForm = new StatsForm(stats))
        {
            statsForm.ShowDialog();
        }
    }
}
