using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Mental_web.UI;
using Mental_web.Data;

namespace Mental_web.Views.Admin
{
    public partial class AppointmentQueueControl : UserControl
    {
        private FlowLayoutPanel _queuePanel = null!;
        private MentalHealthContext _db = null!;

        public AppointmentQueueControl()
        {
            InitializeComponent();
            _db = Program.ServiceProvider.GetRequiredService<MentalHealthContext>();
            SetupUI();
            LoadQueue();
        }

        private void SetupUI()
        {
            this.BackColor = AppTheme.BackgroundColor;
            this.Dock = DockStyle.Fill;

            var lblTitle = new Label {
                Text = "Appointment Approval Queue",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = AppTheme.PrimaryColor,
                Location = new Point(30, 30),
                AutoSize = true
            };
            this.Controls.Add(lblTitle);

            var lblSub = new Label {
                Text = "Review and manage pending student appointment requests.",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.Gray,
                Location = new Point(32, 70),
                AutoSize = true
            };
            this.Controls.Add(lblSub);

            _queuePanel = new FlowLayoutPanel {
                Location = new Point(30, 110),
                Size = new Size(740, 420),
                AutoScroll = true,
                BackColor = Color.Transparent
            };
            this.Controls.Add(_queuePanel);
        }

        private void LoadQueue()
        {
            _queuePanel.Controls.Clear();
            var pending = _db.Appointments
                .Include(a => a.Student)
                .Include(a => a.Counselor)
                .Where(a => a.Status == "Pending")
                .OrderBy(a => a.Date)
                .ToList();

            if (!pending.Any())
            {
                var lblEmpty = new Label { Text = "No pending appointments at this time.", Font = new Font("Segoe UI", 12), ForeColor = Color.Gray, Size = new Size(700, 100), TextAlign = ContentAlignment.MiddleCenter };
                _queuePanel.Controls.Add(lblEmpty);
                return;
            }

            foreach (var app in pending)
            {
                CreateRequestCard(app);
            }
        }

        private void CreateRequestCard(Appointment app)
        {
            Panel card = new Panel {
                Size = new Size(710, 100),
                BackColor = Color.White,
                Margin = new Padding(0, 0, 0, 15)
            };
            UIHelper.MakeRounded(card, 15);

            var lblStudent = new Label {
                Text = app.Student.FirstName + " " + app.Student.LastName,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true,
                ForeColor = AppTheme.PrimaryColor
            };

            var lblInfo = new Label {
                Text = $"Requested for {app.Date:MMM dd, yyyy} at {app.Time}\nCounselor: {app.Counselor.Name}",
                Font = new Font("Segoe UI", 9),
                Location = new Point(20, 45),
                Size = new Size(400, 40),
                ForeColor = Color.DimGray
            };

            Button btnApprove = new Button {
                Text = "Approve",
                Size = new Size(110, 40),
                Location = new Point(460, 30),
                BackColor = AppTheme.PrimaryColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnApprove.FlatAppearance.BorderSize = 0;
            btnApprove.Click += (s, e) => UpdateStatus(app, "Approved");
            UIHelper.MakeRounded(btnApprove, 10);

            Button btnDecline = new Button {
                Text = "Decline",
                Size = new Size(110, 40),
                Location = new Point(580, 30),
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnDecline.FlatAppearance.BorderSize = 0;
            btnDecline.Click += (s, e) => UpdateStatus(app, "Declined");
            UIHelper.MakeRounded(btnDecline, 10);

            card.Controls.Add(lblStudent);
            card.Controls.Add(lblInfo);
            card.Controls.Add(btnApprove);
            card.Controls.Add(btnDecline);
            _queuePanel.Controls.Add(card);
        }

        private void UpdateStatus(Appointment app, string status)
        {
            var existing = _db.Appointments.Find(app.AppointmentId);
            if (existing != null)
            {
                existing.Status = status;
                
                // Add notification for student
                _db.Notifications.Add(new Notification {
                    StudentId = app.StudentId,
                    Message = $"Your appointment on {app.Date:MMM dd} has been {status}.",
                    Status = "Unread",
                    CreatedAt = DateTime.Now
                });
                
                _db.SaveChanges();
                LoadQueue();
                MessageBox.Show($"Appointment {status} successfully!", "Success");
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.Name = "AppointmentQueueControl";
            this.Size = new System.Drawing.Size(800, 600);
            this.ResumeLayout(false);
        }
    }
}
