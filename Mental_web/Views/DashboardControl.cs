using System.Windows.Forms;
using System.Drawing;
using Mental_web.UI;

namespace Mental_web.Views
{
    public partial class DashboardControl : UserControl
    {
        public DashboardControl()
        {
            InitializeComponent();
            SetupUI();
        }

        private void SetupUI()
        {
            this.BackColor = AppTheme.BackgroundColor;
            this.Dock = DockStyle.Fill;

            var lblWelcome = new Label {
                Text = "Hello, Mark!",
                Font = AppTheme.HeaderFont,
                ForeColor = AppTheme.PrimaryColor,
                Location = new Point(30, 30),
                AutoSize = true
            };
            this.Controls.Add(lblWelcome);

            // Circular Progress Area
            var progressPanel = new Panel {
                Size = new Size(200, 250),
                Location = new Point(30, 90),
                BackColor = Color.White
            };
            var progressBar = new Components.CircularProgressBar {
                Location = new Point(40, 40),
                Size = new Size(120, 120),
                Value = 85
            };
            var lblProgress = new Label {
                Text = "Mental Health Score",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Location = new Point(0, 180),
                Width = 200,
                TextAlign = ContentAlignment.TopCenter
            };
            progressPanel.Controls.Add(progressBar);
            progressPanel.Controls.Add(lblProgress);
            this.Controls.Add(progressPanel);

            // Stats Cards
            CreateStatCard("Next Appointment", "Dr. Reyes", "Tomorrow, 10 AM", new Point(250, 90));
            CreateStatCard("Resources Read", "12", "Keep it up!", new Point(500, 90));

            // Recent Tips Section
            var lblTips = new Label {
                Text = "Recommended Tips for You",
                Font = AppTheme.HeaderFont,
                ForeColor = AppTheme.PrimaryColor,
                Location = new Point(30, 360),
                AutoSize = true
            };
            this.Controls.Add(lblTips);

            CreateTipCard("Practice Deep Breathing", "5 mins a day reduces stress significantly.", new Point(30, 410));
            CreateTipCard("Hydration & Mood", "Drinking enough water can improve focus.", new Point(390, 410));
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
