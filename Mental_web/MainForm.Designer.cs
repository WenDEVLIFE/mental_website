namespace Mental_web
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.sidePanel = new System.Windows.Forms.Panel();
            this.logoPanel = new System.Windows.Forms.Panel();
            this.lblLogo = new System.Windows.Forms.Label();
            this.btnDashboard = new System.Windows.Forms.Button();
            this.btnAssessment = new System.Windows.Forms.Button();
            this.btnAppointment = new System.Windows.Forms.Button();
            this.btnResources = new System.Windows.Forms.Button();
            this.btnProgress = new System.Windows.Forms.Button();
            this.btnProfile = new System.Windows.Forms.Button();
            this.btnAdminMode = new System.Windows.Forms.Button();
            this.titleBar = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.sidePanel.SuspendLayout();
            this.logoPanel.SuspendLayout();
            this.titleBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // sidePanel
            // 
            this.sidePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(90)))), ((int)(((byte)(39)))));
            this.sidePanel.Controls.Add(this.btnAdminMode);
            this.sidePanel.Controls.Add(this.btnProfile);
            this.sidePanel.Controls.Add(this.btnProgress);
            this.sidePanel.Controls.Add(this.btnResources);
            this.sidePanel.Controls.Add(this.btnAppointment);
            this.sidePanel.Controls.Add(this.btnAssessment);
            this.sidePanel.Controls.Add(this.btnDashboard);
            this.sidePanel.Controls.Add(this.logoPanel);
            this.sidePanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidePanel.Location = new System.Drawing.Point(0, 0);
            this.sidePanel.Name = "sidePanel";
            this.sidePanel.Size = new System.Drawing.Size(220, 600);
            this.sidePanel.TabIndex = 0;
            // 
            // logoPanel
            // 
            this.logoPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.logoPanel.Controls.Add(this.lblLogo);
            this.logoPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.logoPanel.Location = new System.Drawing.Point(0, 0);
            this.logoPanel.Name = "logoPanel";
            this.logoPanel.Size = new System.Drawing.Size(220, 80);
            this.logoPanel.TabIndex = 0;
            // 
            // lblLogo
            // 
            this.lblLogo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLogo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblLogo.ForeColor = System.Drawing.Color.White;
            this.lblLogo.Location = new System.Drawing.Point(0, 0);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(220, 80);
            this.lblLogo.TabIndex = 0;
            this.lblLogo.Text = "SMHASS";
            this.lblLogo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDashboard
            // 
            this.btnDashboard.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDashboard.FlatAppearance.BorderSize = 0;
            this.btnDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDashboard.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDashboard.ForeColor = System.Drawing.Color.White;
            this.btnDashboard.Location = new System.Drawing.Point(0, 80);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Size = new System.Drawing.Size(220, 50);
            this.btnDashboard.TabIndex = 1;
            this.btnDashboard.Text = "   Dashboard";
            this.btnDashboard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDashboard.UseVisualStyleBackColor = true;
            // 
            // btnAssessment
            // 
            this.btnAssessment.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAssessment.FlatAppearance.BorderSize = 0;
            this.btnAssessment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAssessment.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAssessment.ForeColor = System.Drawing.Color.White;
            this.btnAssessment.Location = new System.Drawing.Point(0, 130);
            this.btnAssessment.Name = "btnAssessment";
            this.btnAssessment.Size = new System.Drawing.Size(220, 50);
            this.btnAssessment.TabIndex = 2;
            this.btnAssessment.Text = "   Assessment";
            this.btnAssessment.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAssessment.UseVisualStyleBackColor = true;
            // 
            // btnAppointment
            // 
            this.btnAppointment.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAppointment.FlatAppearance.BorderSize = 0;
            this.btnAppointment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAppointment.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAppointment.ForeColor = System.Drawing.Color.White;
            this.btnAppointment.Location = new System.Drawing.Point(0, 180);
            this.btnAppointment.Name = "btnAppointment";
            this.btnAppointment.Size = new System.Drawing.Size(220, 50);
            this.btnAppointment.TabIndex = 3;
            this.btnAppointment.Text = "   Appointments";
            this.btnAppointment.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAppointment.UseVisualStyleBackColor = true;
            // 
            // btnResources
            // 
            this.btnResources.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnResources.FlatAppearance.BorderSize = 0;
            this.btnResources.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResources.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnResources.ForeColor = System.Drawing.Color.White;
            this.btnResources.Location = new System.Drawing.Point(0, 230);
            this.btnResources.Name = "btnResources";
            this.btnResources.Size = new System.Drawing.Size(220, 50);
            this.btnResources.TabIndex = 4;
            this.btnResources.Text = "   Resources";
            this.btnResources.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnResources.UseVisualStyleBackColor = true;
            // 
            // btnProgress
            // 
            this.btnProgress.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnProgress.FlatAppearance.BorderSize = 0;
            this.btnProgress.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProgress.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnProgress.ForeColor = System.Drawing.Color.White;
            this.btnProgress.Location = new System.Drawing.Point(0, 280);
            this.btnProgress.Name = "btnProgress";
            this.btnProgress.Size = new System.Drawing.Size(220, 50);
            this.btnProgress.TabIndex = 5;
            this.btnProgress.Text = "   Progress";
            this.btnProgress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProgress.UseVisualStyleBackColor = true;
            // 
            // btnProfile
            // 
            this.btnProfile.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnProfile.FlatAppearance.BorderSize = 0;
            this.btnProfile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProfile.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnProfile.ForeColor = System.Drawing.Color.White;
            this.btnProfile.Location = new System.Drawing.Point(0, 330);
            this.btnProfile.Name = "btnProfile";
            this.btnProfile.Size = new System.Drawing.Size(220, 50);
            this.btnProfile.TabIndex = 6;
            this.btnProfile.Text = "   My Profile";
            this.btnProfile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProfile.UseVisualStyleBackColor = true;
            // 
            // btnAdminMode
            this.btnAdminMode.Text = "🔒 Admin Mode";
            this.btnAdminMode.FlatStyle = FlatStyle.Flat;
            this.btnAdminMode.FlatAppearance.BorderSize = 0;
            this.btnAdminMode.ForeColor = Color.Gold;
            this.btnAdminMode.Font = Mental_web.UI.AppTheme.ButtonFont;
            this.btnAdminMode.Dock = DockStyle.Bottom;
            this.btnAdminMode.Height = 50;
            this.btnAdminMode.Cursor = Cursors.Hand;

            // titleBar
            // 
            this.titleBar.BackColor = System.Drawing.Color.White;
            this.titleBar.Controls.Add(this.btnExit);
            this.titleBar.Controls.Add(this.lblTitle);
            this.titleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.titleBar.Location = new System.Drawing.Point(220, 0);
            this.titleBar.Name = "titleBar";
            this.titleBar.Size = new System.Drawing.Size(780, 50);
            this.titleBar.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblTitle.Location = new System.Drawing.Point(20, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(86, 21);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Dashboard";
            // 
            // btnExit
            // 
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Location = new System.Drawing.Point(730, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(50, 50);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "X";
            this.btnExit.UseVisualStyleBackColor = true;
            // 
            // contentPanel
            // 
            this.contentPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(220, 50);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Size = new System.Drawing.Size(780, 550);
            this.contentPanel.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.titleBar);
            this.Controls.Add(this.sidePanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.sidePanel.ResumeLayout(false);
            this.logoPanel.ResumeLayout(false);
            this.titleBar.ResumeLayout(false);
            this.titleBar.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel sidePanel;
        private System.Windows.Forms.Panel logoPanel;
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.Button btnDashboard;
        private System.Windows.Forms.Button btnAssessment;
        private System.Windows.Forms.Button btnAppointment;
        private System.Windows.Forms.Button btnResources;
        private System.Windows.Forms.Button btnProgress;
        private System.Windows.Forms.Button btnProfile;
        private System.Windows.Forms.Button btnAdminMode;
        private System.Windows.Forms.Panel titleBar;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Panel contentPanel;
    }
}
