using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Mental_web.UI;

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
            this.btnDashboard.Click += (s, e) => ShowView(new Views.DashboardControl(), "Dashboard");
            this.btnAssessment.Click += (s, e) => ShowView(null, "Assessment");
            this.btnAppointment.Click += (s, e) => ShowView(null, "Appointments");
            this.btnResources.Click += (s, e) => ShowView(null, "Resources");

            // Hover effects
            SetupHover(btnDashboard);
            SetupHover(btnAssessment);
            SetupHover(btnAppointment);
            SetupHover(btnResources);

            // Initial view
            ShowView(new Views.DashboardControl(), "Dashboard");
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
