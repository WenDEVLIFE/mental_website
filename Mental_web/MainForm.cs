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
            this.btnDashboard.Click += (s, e) => ShowView("Dashboard");
            this.btnAssessment.Click += (s, e) => ShowView("Assessment");
            this.btnAppointment.Click += (s, e) => ShowView("Appointments");
            this.btnResources.Click += (s, e) => ShowView("Resources");
        }

        private void TitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, 0xA1, 0x2, 0);
            }
        }

        private void ShowView(string title)
        {
            lblTitle.Text = title;
            contentPanel.Controls.Clear();
            
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
