using System;
using System.Drawing;
using System.Windows.Forms;
using Mental_web.Data;
using Mental_web.UI;
using Microsoft.Extensions.DependencyInjection;

namespace Mental_web
{
    public class LoginForm : Form
    {
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;
        private Label lblError;
        private Panel leftPanel;
        private Panel rightPanel;

        public UserSession? AuthenticatedUser { get; private set; }

        public LoginForm()
        {
            // Form Setup
            this.Text = "Mental Health System - Login";
            this.Size = new Size(950, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None; // Frameless for premium look
            this.BackColor = Color.White;

            // Allow dragging the frameless form
            this.MouseDown += (s, e) => {
                if (e.Button == MouseButtons.Left) {
                    MainForm.ReleaseCapture();
                    MainForm.SendMessage(Handle, 0xA1, 0x2, 0);
                }
            };

            // Left Panel (Visual/Logo Area)
            leftPanel = new Panel {
                Dock = DockStyle.Left,
                Width = 400,
                BackColor = AppTheme.PrimaryColor,
                Padding = new Padding(40)
            };
            
            var lblBrand = new Label {
                Text = "SMHASS",
                Font = new Font("Segoe UI", 28, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(40, 60),
                AutoSize = true
            };
            var lblWelcome = new Label {
                Text = "Student Mental Health\nAppointment Support\nSystem",
                Font = new Font("Segoe UI", 14),
                ForeColor = Color.FromArgb(200, 255, 255, 255),
                Location = new Point(40, 130),
                Size = new Size(320, 100)
            };
            var lblFooter = new Label {
                Text = "Supporting your mental well-being\nevery step of the way.",
                Font = new Font("Segoe UI", 10, FontStyle.Italic),
                ForeColor = Color.FromArgb(150, 255, 255, 255),
                Location = new Point(40, 500),
                AutoSize = true
            };

            leftPanel.Controls.Add(lblBrand);
            leftPanel.Controls.Add(lblWelcome);
            leftPanel.Controls.Add(lblFooter);

            // Right Panel (Login Form Area)
            rightPanel = new Panel {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(60)
            };

            var btnClose = new Button {
                Text = "✕",
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.Gray,
                Size = new Size(40, 40),
                Location = new Point(500, 10),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, e) => Application.Exit();
            rightPanel.Controls.Add(btnClose);

            var lblLoginTitle = new Label {
                Text = "Welcome Back",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = AppTheme.PrimaryColor,
                Location = new Point(60, 80),
                AutoSize = true
            };
            var lblLoginSub = new Label {
                Text = "Please login to your account",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.Gray,
                Location = new Point(64, 130),
                AutoSize = true
            };

            // Username
            var lblUser = new Label { Text = "EMAIL OR USERNAME", Font = new Font("Segoe UI", 8, FontStyle.Bold), ForeColor = Color.Silver, Location = new Point(60, 190), AutoSize = true };
            txtUsername = new TextBox { 
                Location = new Point(60, 215), 
                Width = 400, 
                Font = new Font("Segoe UI", 12),
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(10)
            };

            // Password
            var lblPass = new Label { Text = "PASSWORD", Font = new Font("Segoe UI", 8, FontStyle.Bold), ForeColor = Color.Silver, Location = new Point(60, 275), AutoSize = true };
            txtPassword = new TextBox { 
                Location = new Point(60, 300), 
                Width = 400, 
                Font = new Font("Segoe UI", 12),
                BorderStyle = BorderStyle.FixedSingle,
                UseSystemPasswordChar = true
            };

            btnLogin = new Button { 
                Text = "Sign In", 
                Location = new Point(60, 380), 
                Width = 400, 
                Height = 50, 
                BackColor = AppTheme.PrimaryColor, 
                ForeColor = Color.White, 
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.Click += BtnLogin_Click;
            UIHelper.MakeRounded(btnLogin, 12);

            lblError = new Label { 
                ForeColor = Color.FromArgb(231, 76, 60), 
                Location = new Point(60, 440), 
                Width = 400, 
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 9)
            };

            rightPanel.Controls.Add(lblLoginTitle);
            rightPanel.Controls.Add(lblLoginSub);
            rightPanel.Controls.Add(lblUser);
            rightPanel.Controls.Add(txtUsername);
            rightPanel.Controls.Add(lblPass);
            rightPanel.Controls.Add(txtPassword);
            rightPanel.Controls.Add(btnLogin);
            rightPanel.Controls.Add(lblError);

            this.Controls.Add(rightPanel);
            this.Controls.Add(leftPanel);

            // Add subtle shadow effect if possible or just rounded corners
            this.Load += (s, e) => UIHelper.MakeRounded(this, 30);
        }

        private async void BtnLogin_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text)) {
                lblError.Text = "Please enter both credentials.";
                return;
            }

            lblError.Text = "Authenticating...";
            btnLogin.Enabled = false;

            try
            {
                var authService = Program.ServiceProvider.GetRequiredService<AuthService>();
                var session = await authService.AuthenticateAsync(txtUsername.Text, txtPassword.Text);

                if (session != null)
                {
                    AuthenticatedUser = session;
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    lblError.Text = "Invalid email or password.";
                    btnLogin.Enabled = true;
                }
            }
            catch (Exception)
            {
                lblError.Text = "Database connection error. Please try again.";
                btnLogin.Enabled = true;
            }
        }
    }
}
