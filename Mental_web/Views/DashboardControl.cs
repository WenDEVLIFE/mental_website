using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Mental_web.UI;
using Mental_web.Data;

namespace Mental_web.Views
{
    public partial class DashboardControl : UserControl
    {
        private UserSession _session;

        public DashboardControl(UserSession session)
        {
            _session = session;
            InitializeComponent();
            SetupUI();
        }

        private void SetupUI()
        {
            this.BackColor = AppTheme.BackgroundColor;
            this.Dock = DockStyle.Fill;

            var dbContext = Program.ServiceProvider.GetRequiredService<MentalHealthContext>();
            
            var lblWelcome = new Label {
                Text = $"Hello, {_session.Username}!",
                Font = AppTheme.HeaderFont,
                ForeColor = AppTheme.PrimaryColor,
                Location = new Point(30, 30),
                AutoSize = true
            };
            this.Controls.Add(lblWelcome);

            int score = 0;
            string status = "Take an assessment!";
            var lastAss = dbContext.SelfAssessments
                .Where(s => s.StudentId == _session.UserId)
                .OrderByDescending(s => s.DateTaken)
                .FirstOrDefault();
                
            if (lastAss != null && lastAss.Score.HasValue)
            {
                score = lastAss.Score.Value;
                status = lastAss.Result ?? "Completed";
            }

            // Circular Progress Area
            var progressPanel = new Panel {
                Size = new Size(200, 250),
                Location = new Point(30, 90),
                BackColor = Color.White
            };
            var progressBar = new Components.CircularProgressBar {
                Location = new Point(40, 40),
                Size = new Size(120, 120),
                Value = score
            };
            var lblProgress = new Label {
                Text = "Mental Health Score\n" + status,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Location = new Point(0, 180),
                Width = 200,
                TextAlign = ContentAlignment.TopCenter
            };
            progressPanel.Controls.Add(progressBar);
            progressPanel.Controls.Add(lblProgress);
            this.Controls.Add(progressPanel);

            var nextApp = dbContext.Appointments
                .Include(a => a.Counselor)
                .Where(a => a.StudentId == _session.UserId && a.Date >= System.DateTime.Today && a.Status != "Cancelled")
                .OrderBy(a => a.Date)
                .FirstOrDefault();

            string cName = nextApp != null ? nextApp.Counselor.Name : "No upcoming";
            string cDate = nextApp != null ? $"{nextApp.Date.ToShortDateString()}, {nextApp.Time}" : "appointments.";

            // Stats Cards
            CreateStatCard("Next Appointment", cName, cDate, new Point(250, 90));
            
            int resourceCount = dbContext.Resources.Count();
            CreateStatCard("Resources Available", $"{resourceCount} Articles", "Keep learning!", new Point(500, 90));

            // Recent Tips Section
            var lblTips = new Label {
                Text = "System Notifications",
                Font = AppTheme.HeaderFont,
                ForeColor = AppTheme.PrimaryColor,
                Location = new Point(30, 360),
                AutoSize = true
            };
            this.Controls.Add(lblTips);

            var notes = dbContext.Notifications
                .Where(n => n.StudentId == _session.UserId)
                .OrderByDescending(n => n.CreatedAt)
                .Take(2)
                .ToList();

            if (notes.Count > 0)
            {
                CreateTipCard("Notification", notes[0].Message, new Point(30, 410));
            }
            else
            {
                CreateTipCard("Welcome", "No new notifications.", new Point(30, 410));
            }

            if (notes.Count > 1)
            {
                CreateTipCard("Notification", notes[1].Message, new Point(390, 410));
            }
        }

        private void CreateTipCard(string title, string desc, Point loc)
        {
            Panel card = new Panel {
                Size = new Size(340, 100),
                Location = loc,
                BackColor = Color.White
            };
            var lblT = new Label { Text = title, Font = new Font("Segoe UI", 11, FontStyle.Bold), Location = new Point(15, 15), AutoSize = true, ForeColor = AppTheme.PrimaryColor };
            var lblD = new Label { Text = desc, Font = new Font("Segoe UI", 9), Location = new Point(15, 45), Size = new Size(310, 40) };
            card.Controls.Add(lblT);
            card.Controls.Add(lblD);
            this.Controls.Add(card);
            UIHelper.MakeRounded(card, 20);
        }

        private void CreateStatCard(string title, string mainVal, string subVal, Point loc)
        {
            Panel card = new Panel {
                Size = new Size(230, 250),
                Location = loc,
                BackColor = Color.White,
            };
            
            Label lblTitle = new Label {
                Text = title,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = AppTheme.PrimaryColor,
                Location = new Point(15, 15),
                AutoSize = true
            };
            
            Label lblMain = new Label {
                Text = mainVal,
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                Location = new Point(15, 45),
                AutoSize = true
            };
            
            Label lblSub = new Label {
                Text = subVal,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray,
                Location = new Point(15, 100),
                AutoSize = true
            };

            card.Controls.Add(lblTitle);
            card.Controls.Add(lblMain);
            card.Controls.Add(lblSub);
            this.Controls.Add(card);
            UIHelper.MakeRounded(card, 25);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.Name = "DashboardControl";
            this.Size = new System.Drawing.Size(800, 550);
            this.ResumeLayout(false);
        }
    }
}
