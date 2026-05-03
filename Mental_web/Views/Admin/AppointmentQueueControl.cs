using System;
using System.Drawing;
using System.Windows.Forms;
using Mental_web.UI;

namespace Mental_web.Views.Admin
{
    public partial class AppointmentQueueControl : UserControl
    {
        private FlowLayoutPanel _queuePanel = null!;

        public AppointmentQueueControl()
        {
            InitializeComponent();
            SetupUI();
            LoadQueue();
        }

        private void SetupUI()
        {
            this.BackColor = AppTheme.BackgroundColor;
            this.Dock = DockStyle.Fill;

            var lblTitle = new Label {
                Text = "Appointment Approval Queue",
                Font = AppTheme.HeaderFont,
                ForeColor = AppTheme.PrimaryColor,
                Location = new Point(30, 30),
                AutoSize = true
            };
            this.Controls.Add(lblTitle);

            _queuePanel = new FlowLayoutPanel {
                Location = new Point(30, 90),
                Size = new Size(740, 420),
                AutoScroll = true,
                BackColor = Color.Transparent
            };
            this.Controls.Add(_queuePanel);
        }

        private void LoadQueue()
        {
            _queuePanel.Controls.Clear();
            string[] students = { "Alice Johnson", "Bob Miller", "Charlie Davis" };
            string[] times = { "Today, 2:00 PM", "Tomorrow, 9:00 AM", "May 5th, 1:30 PM" };
            
            for (int i = 0; i < students.Length; i++)
            {
                CreateRequestCard(students[i], times[i]);
            }
        }

        private void CreateRequestCard(string student, string time)
        {
            Panel card = new Panel {
                Size = new Size(700, 80),
                BackColor = Color.White,
                Margin = new Padding(0, 0, 0, 15)
            };

            Label lblInfo = new Label {
                Text = $"{student} - Requested for {time}",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Location = new Point(20, 25),
                AutoSize = true
            };

            Button btnApprove = new Button {
                Text = "Approve",
                Size = new Size(100, 35),
                Location = new Point(460, 22),
                BackColor = AppTheme.PrimaryColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnApprove.FlatAppearance.BorderSize = 0;
            btnApprove.Click += (s, e) => { card.Visible = false; MessageBox.Show("Appointment Approved!"); };

            Button btnDecline = new Button {
                Text = "Decline",
                Size = new Size(100, 35),
                Location = new Point(570, 22),
                BackColor = Color.FromArgb(192, 57, 43), // Red
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnDecline.FlatAppearance.BorderSize = 0;
            btnDecline.Click += (s, e) => { card.Visible = false; };

            card.Controls.Add(lblInfo);
            card.Controls.Add(btnApprove);
            card.Controls.Add(btnDecline);
            _queuePanel.Controls.Add(card);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.Name = "AppointmentQueueControl";
            this.Size = new System.Drawing.Size(800, 550);
            this.ResumeLayout(false);
        }
    }
}
