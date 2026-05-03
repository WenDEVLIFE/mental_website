using System.Drawing;
using System.Windows.Forms;
using Mental_web.UI;

namespace Mental_web.Views.Admin
{
    public partial class AdminDashboardControl : UserControl
    {
        public AdminDashboardControl()
        {
            InitializeComponent();
            SetupUI();
        }

        private void SetupUI()
        {
            this.BackColor = AppTheme.BackgroundColor;
            this.Dock = DockStyle.Fill;

            var lblTitle = new Label {
                Text = "Admin Overview",
                Font = AppTheme.HeaderFont,
                ForeColor = AppTheme.PrimaryColor,
                Location = new Point(30, 30),
                AutoSize = true
            };
            this.Controls.Add(lblTitle);

            // Admin Stats
            CreateStatCard("Total Students", "1,248", "Active Users", new Point(30, 90));
            CreateStatCard("Pending Requests", "14", "Requires Action", new Point(280, 90));
            CreateStatCard("Today's Sessions", "8", "Scheduled", new Point(530, 90));
            
            var lblNotice = new Label {
                Text = "Welcome to the Admin Portal. Use the sidebar to manage students and appointments.",
                Font = new Font("Segoe UI", 11),
                Location = new Point(30, 380),
                Size = new Size(740, 50),
                ForeColor = Color.Gray
            };
            this.Controls.Add(lblNotice);
        }

        private void CreateStatCard(string title, string val, string sub, Point loc)
        {
            Panel card = new Panel {
                Size = new Size(230, 180),
                Location = loc,
                BackColor = Color.White
            };
            var lblT = new Label { Text = title, Font = new Font("Segoe UI", 10, FontStyle.Bold), Location = new Point(15, 15), AutoSize = true, ForeColor = AppTheme.PrimaryColor };
            var lblV = new Label { Text = val, Font = new Font("Segoe UI", 24, FontStyle.Bold), Location = new Point(15, 50), AutoSize = true };
            var lblS = new Label { Text = sub, Font = new Font("Segoe UI", 9), Location = new Point(15, 130), AutoSize = true, ForeColor = Color.Gray };
            
            card.Controls.Add(lblT);
            card.Controls.Add(lblV);
            card.Controls.Add(lblS);
            this.Controls.Add(card);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.Name = "AdminDashboardControl";
            this.Size = new System.Drawing.Size(800, 550);
            this.ResumeLayout(false);
        }
    }
}
