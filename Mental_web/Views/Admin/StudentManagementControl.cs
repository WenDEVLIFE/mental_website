using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Mental_web.UI;

namespace Mental_web.Views.Admin
{
    public partial class StudentManagementControl : UserControl
    {
        private DataGridView _grid;

        public StudentManagementControl()
        {
            InitializeComponent();
            SetupUI();
            LoadMockData();
        }

        private void SetupUI()
        {
            this.BackColor = AppTheme.BackgroundColor;
            this.Dock = DockStyle.Fill;

            var lblTitle = new Label {
                Text = "Student Records Management",
                Font = AppTheme.HeaderFont,
                ForeColor = AppTheme.PrimaryColor,
                Location = new Point(30, 30),
                AutoSize = true
            };
            this.Controls.Add(lblTitle);

            _grid = new DataGridView {
                Location = new Point(30, 90),
                Size = new Size(740, 420),
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AllowUserToAddRows = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                ColumnHeadersHeight = 40,
                EnableHeadersVisualStyles = false
            };

            // Modern Styling for Grid Headers
            _grid.ColumnHeadersDefaultCellStyle.BackColor = AppTheme.PrimaryColor;
            _grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            _grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            _grid.DefaultCellStyle.SelectionBackColor = AppTheme.SecondaryColor;
            _grid.DefaultCellStyle.SelectionForeColor = AppTheme.PrimaryColor;
            _grid.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            
            this.Controls.Add(_grid);
        }

        private void LoadMockData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            dt.Columns.Add("Department");
            dt.Columns.Add("Status");
            dt.Columns.Add("Last Assessment");

            dt.Rows.Add("2024-001", "Mark Andrey Bonaobra", "BSIT", "Active", "85%");
            dt.Rows.Add("2024-002", "Justine Cisneros", "BSIT", "Active", "92%");
            dt.Rows.Add("2024-003", "Jhuslene Angela Castillo", "BSCS", "Pending", "N/A");
            dt.Rows.Add("2024-004", "Lovely Lopez", "BSIT", "Active", "78%");
            dt.Rows.Add("2024-005", "Justine Karl Matilla", "BSCS", "Active", "88%");

            _grid.DataSource = dt;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.Name = "StudentManagementControl";
            this.Size = new System.Drawing.Size(800, 550);
            this.ResumeLayout(false);
        }
    }
}
