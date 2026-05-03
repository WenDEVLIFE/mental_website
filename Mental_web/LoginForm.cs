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

        public UserSession? AuthenticatedUser { get; private set; }

        public LoginForm()
        {
            Text = "Login";
            Size = new Size(400, 300);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            BackColor = AppTheme.BackgroundColor;

            var lblTitle = new Label { Text = "Login to System", Font = AppTheme.HeaderFont, ForeColor = AppTheme.PrimaryColor, Dock = DockStyle.Top, TextAlign = ContentAlignment.MiddleCenter, Height = 50 };
            
            var panel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20) };
            
            var lblUser = new Label { Text = "Email / Username:", Location = new Point(50, 60), AutoSize = true, ForeColor = AppTheme.SecondaryColor };
            txtUsername = new TextBox { Location = new Point(50, 80), Width = 280, Font = AppTheme.BodyFont };
            
            var lblPass = new Label { Text = "Password:", Location = new Point(50, 120), AutoSize = true, ForeColor = AppTheme.SecondaryColor };
            txtPassword = new TextBox { Location = new Point(50, 140), Width = 280, Font = AppTheme.BodyFont, UseSystemPasswordChar = true };

            btnLogin = new Button { Text = "Login", Location = new Point(50, 180), Width = 280, Height = 40, BackColor = AppTheme.PrimaryColor, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.Click += BtnLogin_Click;

            lblError = new Label { ForeColor = Color.Red, Location = new Point(50, 230), Width = 280, TextAlign = ContentAlignment.MiddleCenter };

            panel.Controls.Add(lblUser);
            panel.Controls.Add(txtUsername);
            panel.Controls.Add(lblPass);
            panel.Controls.Add(txtPassword);
            panel.Controls.Add(btnLogin);
            panel.Controls.Add(lblError);

            Controls.Add(panel);
            Controls.Add(lblTitle);
        }

        private async void BtnLogin_Click(object? sender, EventArgs e)
        {
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
                    lblError.Text = "Invalid credentials";
                    btnLogin.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "Error connecting to database.";
                btnLogin.Enabled = true;
            }
        }
    }
}
