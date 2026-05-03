using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Mental_web.UI;
using Mental_web.Data;

namespace Mental_web.Views.Admin
{
    public partial class AdminDashboardControl : UserControl
    {
        private MentalHealthContext _db;
        private ListBox _lstResources;
        private TextBox _txtAnnounce;

        public AdminDashboardControl()
        {
            InitializeComponent();
            _db = Program.ServiceProvider.GetRequiredService<MentalHealthContext>();
            SetupUI();
        }

        private void SetupUI()
        {
            this.BackColor = AppTheme.BackgroundColor;
            this.Dock = DockStyle.Fill;

            var lblWelcome = new Label {
                Text = "Admin Dashboard",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = AppTheme.PrimaryColor,
                Location = new Point(30, 30),
                AutoSize = true
            };
            this.Controls.Add(lblWelcome);

            int totalStudents = _db.Students.Count();
            int pendingApps = _db.Appointments.Count(a => a.Status == "Pending");
            int resourceCount = _db.Resources.Count();

            // Stats Cards
            CreateStatCard("Students", totalStudents.ToString(), "Active", new Point(30, 90), Color.FromArgb(232, 245, 233), Color.FromArgb(46, 125, 50));
            CreateStatCard("Pending", pendingApps.ToString(), "Appointments", new Point(250, 90), Color.FromArgb(255, 243, 224), Color.FromArgb(230, 81, 0));
            CreateStatCard("Resources", resourceCount.ToString(), "Published", new Point(470, 90), Color.FromArgb(227, 242, 253), Color.FromArgb(21, 101, 192));

            // Announcement Section
            var groupAnn = new GroupBox { Text = "Send System Announcement", Location = new Point(30, 280), Size = new Size(350, 250), Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            _txtAnnounce = new TextBox { Multiline = true, Location = new Point(15, 30), Size = new Size(320, 150), Font = new Font("Segoe UI", 10) };
            var btnSend = new Button { Text = "Broadcast to All Students", Location = new Point(15, 190), Size = new Size(320, 40), BackColor = AppTheme.PrimaryColor, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnSend.Click += (s, e) => SendAnnouncement();
            groupAnn.Controls.Add(_txtAnnounce);
            groupAnn.Controls.Add(btnSend);
            this.Controls.Add(groupAnn);

            // Resource Management Section
            var groupRes = new GroupBox { Text = "Manage Resources", Location = new Point(400, 280), Size = new Size(370, 250), Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            _lstResources = new ListBox { Location = new Point(15, 30), Size = new Size(340, 150), Font = new Font("Segoe UI", 10) };
            RefreshResources();
            
            var btnDelRes = new Button { Text = "Delete Selected", Location = new Point(15, 190), Size = new Size(160, 40), FlatStyle = FlatStyle.Flat, ForeColor = Color.Red };
            btnDelRes.Click += (s, e) => DeleteResource();
            var btnAddRes = new Button { Text = "Add New", Location = new Point(185, 190), Size = new Size(170, 40), FlatStyle = FlatStyle.Flat, ForeColor = AppTheme.PrimaryColor };
            btnAddRes.Click += (s, e) => AddResource();
            
            groupRes.Controls.Add(_lstResources);
            groupRes.Controls.Add(btnDelRes);
            groupRes.Controls.Add(btnAddRes);
            this.Controls.Add(groupRes);
        }

        private void RefreshResources()
        {
            _lstResources.Items.Clear();
            var resources = _db.Resources.ToList();
            foreach (var r in resources) _lstResources.Items.Add(r.Title);
        }

        private void AddResource()
        {
            string title = Microsoft.VisualBasic.Interaction.InputBox("Enter Resource Title:", "Add Resource", "");
            if (!string.IsNullOrWhiteSpace(title))
            {
                _db.Resources.Add(new Resource { Title = title, ContentType = "Article", ContentBody = "New content placeholder", CreatedAt = DateTime.Now });
                _db.SaveChanges();
                RefreshResources();
            }
        }

        private void DeleteResource()
        {
            if (_lstResources.SelectedItem != null)
            {
                string title = _lstResources.SelectedItem.ToString();
                var res = _db.Resources.FirstOrDefault(r => r.Title == title);
                if (res != null)
                {
                    _db.Resources.Remove(res);
                    _db.SaveChanges();
                    RefreshResources();
                }
            }
        }

        private void SendAnnouncement()
        {
            string msg = _txtAnnounce.Text;
            if (!string.IsNullOrWhiteSpace(msg))
            {
                var students = _db.Students.ToList();
                foreach (var s in students)
                {
                    _db.Notifications.Add(new Notification { StudentId = s.StudentId, Message = msg, Status = "Unread", CreatedAt = DateTime.Now });
                }
                _db.SaveChanges();
                _txtAnnounce.Clear();
                MessageBox.Show("Announcement broadcasted successfully!", "Success");
            }
        }

        private void CreateStatCard(string title, string mainVal, string subVal, Point loc, Color backColor, Color foreColor)
        {
            Panel card = new Panel { Size = new Size(200, 160), Location = loc, BackColor = Color.White };
            var lblT = new Label { Text = title, Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = Color.Gray, Location = new Point(15, 15), AutoSize = true };
            var lblM = new Label { Text = mainVal, Font = new Font("Segoe UI", 24, FontStyle.Bold), Location = new Point(15, 45), AutoSize = true, ForeColor = foreColor };
            var lblS = new Label { Text = subVal, Font = new Font("Segoe UI", 9), ForeColor = Color.Silver, Location = new Point(15, 120), AutoSize = true };
            card.Controls.Add(lblT);
            card.Controls.Add(lblM);
            card.Controls.Add(lblS);
            this.Controls.Add(card);
            UIHelper.MakeRounded(card, 20);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.Name = "AdminDashboardControl";
            this.Size = new System.Drawing.Size(800, 600);
            this.ResumeLayout(false);
        }
    }
}
