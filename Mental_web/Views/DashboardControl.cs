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

        public DashboardControl() : this(new UserSession()) { }

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
                Text = $"Welcome back, {_session.Username}!",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = AppTheme.PrimaryColor,
                Location = new Point(30, 30),
                AutoSize = true
            };
            this.Controls.Add(lblWelcome);

            var lblSub = new Label {
                Text = "Your mental health overview for today.",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.Gray,
                Location = new Point(32, 65),
                AutoSize = true
            };
            this.Controls.Add(lblSub);

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
                Size = new Size(240, 280),
                Location = new Point(30, 110),
                BackColor = Color.White
            };
            var progressBar = new Components.CircularProgressBar {
                Location = new Point(50, 40),
                Size = new Size(140, 140),
                Value = score,
                InnerColor = Color.White,
                ProgressColor = AppTheme.PrimaryColor
            };
            var lblScoreTitle = new Label {
                Text = "Mental Health Score",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(0, 195),
                Width = 240,
                TextAlign = ContentAlignment.TopCenter
            };
            var lblStatus = new Label {
                Text = status,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray,
                Location = new Point(0, 220),
                Width = 240,
                TextAlign = ContentAlignment.TopCenter
            };
            progressPanel.Controls.Add(progressBar);
            progressPanel.Controls.Add(lblScoreTitle);
            progressPanel.Controls.Add(lblStatus);
            this.Controls.Add(progressPanel);
            UIHelper.MakeRounded(progressPanel, 24);

            var nextApp = dbContext.Appointments
                .Include(a => a.Counselor)
                .Where(a => a.StudentId == _session.UserId && a.Date >= System.DateTime.Today && a.Status != "Cancelled")
                .OrderBy(a => a.Date)
                .FirstOrDefault();

            string cName = nextApp != null ? nextApp.Counselor.Name : "No upcoming";
            string cDate = nextApp != null ? $"{nextApp.Date.ToShortDateString()}, {nextApp.Time}" : "appointments.";

            // Stats Cards
            CreateStatCard("Next Appointment", cName, cDate, new Point(290, 110), Color.FromArgb(232, 245, 233), Color.FromArgb(46, 125, 50));
            
            int resourceCount = dbContext.Resources.Count();
            CreateStatCard("Resources Available", $"{resourceCount} Articles", "Mental health tips", new Point(550, 110), Color.FromArgb(227, 242, 253), Color.FromArgb(25, 118, 210));

            // System Announcements
            var lblAnn = new Label {
                Text = "System Announcements",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = AppTheme.PrimaryColor,
                Location = new Point(30, 410),
                AutoSize = true
            };
            this.Controls.Add(lblAnn);

            var notes = dbContext.Notifications
                .Where(n => n.StudentId == _session.UserId)
                .OrderByDescending(n => n.CreatedAt)
                .Take(2)
                .ToList();

            int yPos = 450;
            if (notes.Count > 0)
            {
                foreach (var note in notes)
                {
                    CreateNotificationItem(note.Message, note.CreatedAt?.ToString("MMM dd, h:mm tt") ?? "", new Point(30, yPos));
                    yPos += 80;
                }
            }
            else
            {
                CreateNotificationItem("No new notifications.", "", new Point(30, yPos));
            }
        }

        private void CreateStatCard(string title, string mainVal, string subVal, Point loc, Color backColor, Color foreColor)
        {
            Panel card = new Panel {
                Size = new Size(240, 280),
                Location = loc,
                BackColor = Color.White
            };
            
            var iconBox = new Panel {
                Size = new Size(50, 50),
                Location = new Point(20, 20),
                BackColor = backColor
            };
            card.Controls.Add(iconBox);
            UIHelper.MakeRounded(iconBox, 14);

            var lblT = new Label { 
                Text = title, 
                Font = new Font("Segoe UI", 10, FontStyle.Bold), 
                ForeColor = AppTheme.PrimaryColor, 
                Location = new Point(20, 85), 
                AutoSize = true 
            };
            var lblM = new Label { 
                Text = mainVal, 
                Font = new Font("Segoe UI", 18, FontStyle.Bold), 
                Location = new Point(20, 120), 
                Width = 200,
                Height = 80
            };
            var lblS = new Label { 
                Text = subVal, 
                Font = new Font("Segoe UI", 9), 
                ForeColor = Color.Gray, 
                Location = new Point(20, 210), 
                AutoSize = true 
            };
            
            card.Controls.Add(lblT);
            card.Controls.Add(lblM);
            card.Controls.Add(lblS);
            this.Controls.Add(card);
            UIHelper.MakeRounded(card, 24);
        }

        private void CreateNotificationItem(string msg, string date, Point loc)
        {
            Panel item = new Panel {
                Size = new Size(760, 65),
                Location = loc,
                BackColor = Color.White
            };
            
            var indicator = new Panel {
                Size = new Size(5, 65),
                Location = new Point(0, 0),
                BackColor = AppTheme.PrimaryColor
            };
            item.Controls.Add(indicator);

            var lblM = new Label {
                Text = msg,
                Font = new Font("Segoe UI", 10),
                Location = new Point(20, 10),
                Width = 600,
                Height = 45
            };
            var lblD = new Label {
                Text = date,
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.Silver,
                Location = new Point(630, 10),
                Width = 120,
                TextAlign = ContentAlignment.TopRight
            };
            
            item.Controls.Add(lblM);
            item.Controls.Add(lblD);
            this.Controls.Add(item);
            UIHelper.MakeRounded(item, 15);
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
