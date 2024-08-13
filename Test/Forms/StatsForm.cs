using System.Windows.Forms;
using Test.Model;

namespace Test.Forms;

public class StatsForm : Form
{
    private DataGridView _statsGridView;

    public StatsForm(List<TaskStatistics> stats)
    {
        Text = "Statystyki zadań";
        Width = 400;
        Height = 300;

        _statsGridView = new DataGridView
        {
            Dock = DockStyle.Fill,
            AutoGenerateColumns = true,
            DataSource = stats
        };

        Controls.Add(_statsGridView);
    }
}
