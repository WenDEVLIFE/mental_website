using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Mental_web.UI;
using Mental_web.Data;

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

            var dbContext = Program.ServiceProvider.GetRequiredService<MentalHealthContext>();
            
            var lblWelcome = new Label {
                Text = "Admin Overview",
                Font = AppTheme.HeaderFont,
                ForeColor = AppTheme.PrimaryColor,
                Location = new Point(30, 30),
                AutoSize = true
            };
            this.Controls.Add(lblWelcome);

            int totalStudents = dbContext.Students.Count();
            int pendingApps = dbContext.Appointments.Count(a => a.Status == "Pending");

            CreateStatCard("Total Students", totalStudents.ToString(), "Active accounts", new Point(30, 90));
            CreateStatCard("Pending Appointments", pendingApps.ToString(), "Requires action", new Point(300, 90));

            var lblLogs = new Label {
                Text = "Recent Activity Logs",
                Font = AppTheme.HeaderFont,
                ForeColor = AppTheme.PrimaryColor,
                Location = new Point(30, 300),
                AutoSize = true
            };
            this.Controls.Add(lblLogs);
            
            var logs = dbContext.ActivityLogs.OrderByDescending(l => l.Timestamp).Take(5).ToList();
            int yPos = 350;
            foreach(var log in logs)
            {
                var lblL = new Label {
                    Text = $"[{log.Timestamp}] {log.Role}: {log.Action}",
                    Font = new Font("Segoe UI", 10),
                    Location = new Point(30, yPos),
                    AutoSize = true
                };
                this.Controls.Add(lblL);
                yPos += 30;
            }
        }

        private void CreateStatCard(string title, string mainVal, string subVal, Point loc)
        {
            Panel card = new Panel {
                Size = new Size(250, 180),
                Location = loc,
                BackColor = Color.White
            };
            var lblT = new Label { Text = title, Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = AppTheme.PrimaryColor, Location = new Point(15, 15), AutoSize = true };
            var lblM = new Label { Text = mainVal, Font = new Font("Segoe UI", 24, FontStyle.Bold), Location = new Point(15, 50), AutoSize = true };
            var lblS = new Label { Text = subVal, Font = new Font("Segoe UI", 9), ForeColor = Color.Gray, Location = new Point(15, 110), AutoSize = true };
            card.Controls.Add(lblT);
            card.Controls.Add(lblM);
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
