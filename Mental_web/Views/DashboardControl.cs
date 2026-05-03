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

            var lblSub = new Label {
                Text = "How are you feeling today?",
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.Gray,
                Location = new Point(30, 70),
                AutoSize = true
            };
            this.Controls.Add(lblSub);

            // Mock Stats Cards
            CreateStatCard("Last Assessment", "85%", "Good Standing", new Point(30, 130));
            CreateStatCard("Next Appointment", "Dr. Reyes", "Tomorrow, 10 AM", new Point(280, 130));
            CreateStatCard("Resources Read", "12", "Keep it up!", new Point(530, 130));
        }

        private void CreateStatCard(string title, string mainVal, string subVal, Point loc)
        {
            Panel card = new Panel {
                Size = new Size(230, 150),
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
