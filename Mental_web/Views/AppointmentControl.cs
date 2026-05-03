using System;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Mental_web.UI;
using Mental_web.Data;

namespace Mental_web.Views
{
    public partial class AppointmentControl : UserControl
    {
        private UserSession _session;
        private MentalHealthContext _db;
        private ComboBox _cmbCounselor;
        private DateTimePicker _dtpDate;
        private ComboBox _cmbTime;
        private FlowLayoutPanel _historyPanel;

        public AppointmentControl(UserSession session)
        {
            _session = session;
            _db = Program.ServiceProvider.GetRequiredService<MentalHealthContext>();
            InitializeComponent();
            SetupUI();
            LoadData();
        }

        // Parameterless for designer
        public AppointmentControl() : this(new UserSession()) { }

        private void SetupUI()
        {
            this.BackColor = AppTheme.BackgroundColor;
            this.Dock = DockStyle.Fill;

            var lblTitle = new Label {
                Text = "Counseling Appointments",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = AppTheme.PrimaryColor,
                Location = new Point(30, 20),
                AutoSize = true
            };
            this.Controls.Add(lblTitle);

            // Tab Control for Book vs History
            TabControl tabs = new TabControl {
                Location = new Point(30, 80),
                Size = new Size(740, 480),
                Font = new Font("Segoe UI", 10)
            };

            // Booking Page
            TabPage tpBook = new TabPage("Schedule Session");
            tpBook.BackColor = Color.White;
            
            int y = 30;
            CreateLabel(tpBook, "Select Preferred Counselor:", ref y);
            _cmbCounselor = new ComboBox { Location = new Point(30, y), Width = 400, DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 11) };
            tpBook.Controls.Add(_cmbCounselor);
            y += 70;

            CreateLabel(tpBook, "Choose Date:", ref y);
            _dtpDate = new DateTimePicker { Location = new Point(30, y), Width = 400, MinDate = DateTime.Today.AddDays(1), Font = new Font("Segoe UI", 11) };
            tpBook.Controls.Add(_dtpDate);
            y += 70;

            CreateLabel(tpBook, "Preferred Time Slot:", ref y);
            _cmbTime = new ComboBox { Location = new Point(30, y), Width = 400, DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 11) };
            _cmbTime.Items.AddRange(new string[] { "09:00 AM", "10:30 AM", "01:00 PM", "02:30 PM", "04:00 PM" });
            _cmbTime.SelectedIndex = 0;
            tpBook.Controls.Add(_cmbTime);
            y += 80;

            var btnBook = new Button {
                Text = "Request Appointment",
                Location = new Point(30, y),
                Size = new Size(400, 50),
                BackColor = AppTheme.PrimaryColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnBook.Click += (s, e) => BookSession();
            tpBook.Controls.Add(btnBook);
            UIHelper.MakeRounded(btnBook, 12);

            // History Page
            TabPage tpHistory = new TabPage("My Appointments Status");
            tpHistory.BackColor = Color.White;
            _historyPanel = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoScroll = true, Padding = new Padding(10) };
            tpHistory.Controls.Add(_historyPanel);

            tabs.TabPages.Add(tpBook);
            tabs.TabPages.Add(tpHistory);
            this.Controls.Add(tabs);
            
            tabs.SelectedIndexChanged += (s, e) => { if (tabs.SelectedIndex == 1) LoadHistory(); };
        }

        private void CreateLabel(TabPage p, string text, ref int y)
        {
            var lbl = new Label { Text = text, Location = new Point(30, y), AutoSize = true, Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = Color.FromArgb(64, 64, 64) };
            p.Controls.Add(lbl);
            y += 25;
        }

        private void LoadData()
        {
            var counselors = _db.Counselors.ToList();
            _cmbCounselor.DataSource = counselors;
            _cmbCounselor.DisplayMember = "Name";
            _cmbCounselor.ValueMember = "CounselorId";
        }

        private void BookSession()
        {
            if (_cmbCounselor.SelectedValue == null) return;

            var app = new Appointment {
                StudentId = _session.UserId,
                CounselorId = (int)_cmbCounselor.SelectedValue,
                Date = _dtpDate.Value.Date,
                Time = DateTime.Parse(_cmbTime.Text).TimeOfDay,
                Status = "Pending",
                CreatedAt = DateTime.Now
            };

            _db.Appointments.Add(app);
            _db.SaveChanges();
            MessageBox.Show("Your appointment request has been sent! Check the history tab for status updates.", "Success");
            LoadHistory();
        }

        private void LoadHistory()
        {
            _historyPanel.Controls.Clear();
            var history = _db.Appointments
                .Include(a => a.Counselor)
                .Where(a => a.StudentId == _session.UserId)
                .OrderByDescending(a => a.Date)
                .ToList();

            foreach (var app in history)
            {
                Panel card = new Panel { Size = new Size(680, 70), BackColor = Color.FromArgb(245, 245, 245), Margin = new Padding(0, 0, 0, 10) };
                UIHelper.MakeRounded(card, 12);
                
                var lblInfo = new Label {
                    Text = $"{app.Date:MMM dd, yyyy} at {app.Time} - with {app.Counselor.Name}",
                    Font = new Font("Segoe UI", 10),
                    Location = new Point(15, 25),
                    AutoSize = true
                };
                
                var lblStatus = new Label {
                    Text = app.Status,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    ForeColor = GetStatusColor(app.Status),
                    Location = new Point(550, 25),
                    Width = 100,
                    TextAlign = ContentAlignment.TopRight
                };

                card.Controls.Add(lblInfo);
                card.Controls.Add(lblStatus);
                _historyPanel.Controls.Add(card);
            }
        }

        private Color GetStatusColor(string status) => status switch {
            "Approved" => Color.FromArgb(46, 125, 50),
            "Pending" => Color.FromArgb(230, 126, 34),
            "Declined" => Color.FromArgb(192, 57, 43),
            "Cancelled" => Color.Gray,
            _ => Color.Black
        };

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.Name = "AppointmentControl";
            this.Size = new System.Drawing.Size(800, 600);
            this.ResumeLayout(false);
        }
    }
}
