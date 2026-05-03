using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Mental_web.UI;
using Mental_web.Data;

namespace Mental_web
{
    public partial class MainForm : Form
    {
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();



        public MainForm()
        {
            InitializeComponent();
            this.titleBar.MouseDown += TitleBar_MouseDown;
            this.btnExit.Click += (s, e) => Application.Exit();
            
            // Wire up navigation
            this.btnDashboard.Click += (s, e) => {
                if (_currentUser?.Role == "Admin") ShowView(new Views.Admin.AdminDashboardControl(), "Admin Dashboard");
                else if (_currentUser?.Role == "Counselor") ShowView(new Views.Admin.AppointmentQueueControl(), "My Appointments");
                else ShowView(new Views.DashboardControl(_currentUser!), "Dashboard");
            };
            this.btnAssessment.Click += (s, e) => {
                if (_currentUser?.Role == "Admin") ShowView(new Views.Admin.StudentManagementControl(), "Student Management");
                else ShowView(new Views.AssessmentControl(), "Assessment");
            };
            this.btnAppointment.Click += (s, e) => {
                if (_currentUser?.Role == "Admin" || _currentUser?.Role == "Counselor") ShowView(new Views.Admin.AppointmentQueueControl(), "Appointment Queue");
                else ShowView(new Views.AppointmentControl(), "Appointments");
            };
            this.btnResources.Click += (s, e) => ShowView(new Views.ResourcesControl(), "Resources");
            this.btnProgress.Click += (s, e) => ShowView(new Views.ProgressControl(_currentUser), "Progress Tracking");
            this.btnProfile.Click += (s, e) => ShowView(new Views.ProfileControl(_currentUser), "My Profile");

            // Repurpose Admin mode button to Logout
            btnAdminMode.Text = "   Logout";
            btnAdminMode.Click += (s, e) => {
                _currentUser = null!;
                PerformLogin();
            };

            // Hover effects
            SetupHover(btnDashboard);
            SetupHover(btnAssessment);
            SetupHover(btnAppointment);
            SetupHover(btnResources);
            SetupHover(btnProgress);
            SetupHover(btnProfile);
            SetupHover(btnAdminMode);

            // Optimization
            UIHelper.SetDoubleBuffered(this);
            UIHelper.SetDoubleBuffered(contentPanel);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            PerformLogin();
        }

        private void PerformLogin()
        {
            this.Hide();
            using var loginForm = new LoginForm();
            if (loginForm.ShowDialog(this) == DialogResult.OK && loginForm.AuthenticatedUser != null)
            {
                SetUserSession(loginForm.AuthenticatedUser);
                this.Show();
            }
            else
            {
                Application.Exit();
            }
        }

        private UserSession _currentUser = null!;

        public void SetUserSession(UserSession session)
        {
            _currentUser = session;
            lblTitle.Text = $"Welcome, {session.Username} ({session.Role})";
            btnAdminMode.Visible = true; // Logout button is always visible
            
            if (session.Role == "Admin")
            {
                btnDashboard.Text = "   Admin Dashboard";
                btnAssessment.Text = "   Manage Students";
                btnAssessment.Visible = true;
                btnAppointment.Text = "   Appointment Queue";
                btnResources.Visible = false;
                btnProgress.Visible = false;
                btnProfile.Visible = false;
                ShowView(new Views.Admin.AdminDashboardControl(), "Admin Dashboard");
            }
            else if (session.Role == "Counselor")
            {
                btnDashboard.Text = "   My Appointments";
                btnAssessment.Visible = false;
                btnAppointment.Text = "   Schedule settings";
                btnResources.Visible = false;
                btnProgress.Visible = false;
                btnProfile.Visible = false;
                ShowView(new Views.Admin.AppointmentQueueControl(), "My Appointments");
            }
            else
            {
                btnDashboard.Text = "   Dashboard";
                btnAssessment.Text = "   Assessment";
                btnAssessment.Visible = true;
                btnAppointment.Text = "   Appointments";
                btnResources.Visible = true;
                btnProgress.Visible = true;
                btnProfile.Visible = true;
                ShowView(new Views.DashboardControl(_currentUser), "Dashboard");
            }
        }

        private void SetupHover(Button btn)
        {
            btn.MouseEnter += (s, e) => btn.BackColor = Color.FromArgb(60, 110, 54); // Lighter green
            btn.MouseLeave += (s, e) => btn.BackColor = AppTheme.PrimaryColor;
        }

        private void TitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, 0xA1, 0x2, 0);
            }
        }

        private void ShowView(UserControl? view, string title)
        {
            lblTitle.Text = title;
            contentPanel.Controls.Clear();
            
            if (view != null)
            {
                view.Dock = DockStyle.Fill;
                contentPanel.Controls.Add(view);
            }
            else
            {
                var label = new Label {
                    Text = title + " View Placeholder",
                    Font = AppTheme.HeaderFont,
                    ForeColor = AppTheme.PrimaryColor,
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter
                };
                contentPanel.Controls.Add(label);
            }
        }
    }
}
