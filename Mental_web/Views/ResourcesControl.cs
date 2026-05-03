using System;
using System.Drawing;
using System.Windows.Forms;
using Mental_web.UI;

namespace Mental_web.Views
{
    public partial class ResourcesControl : UserControl
    {
        private FlowLayoutPanel _resourcePanel = null!;
        private TextBox _txtSearch = null!;

        public ResourcesControl()
        {
            InitializeComponent();
            SetupUI();
            LoadResources();
        }

        private void SetupUI()
        {
            this.BackColor = AppTheme.BackgroundColor;
            this.Dock = DockStyle.Fill;

            var lblTitle = new Label {
                Text = "Mental Health Resources",
                Font = AppTheme.HeaderFont,
                ForeColor = AppTheme.PrimaryColor,
                Location = new Point(30, 30),
                AutoSize = true
            };
            this.Controls.Add(lblTitle);

            // Search Bar
            Panel pnlSearch = new Panel {
                Size = new Size(300, 40),
                Location = new Point(470, 35),
                BackColor = Color.White,
            };
            _txtSearch = new TextBox {
                Text = "Search resources...",
                ForeColor = Color.Gray,
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 11),
                Location = new Point(10, 10),
                Size = new Size(280, 25)
            };
            _txtSearch.Enter += (s, e) => { if (_txtSearch.Text == "Search resources...") { _txtSearch.Text = ""; _txtSearch.ForeColor = Color.Black; } };
            _txtSearch.Leave += (s, e) => { if (string.IsNullOrWhiteSpace(_txtSearch.Text)) { _txtSearch.Text = "Search resources..."; _txtSearch.ForeColor = Color.Gray; } };
            
            pnlSearch.Controls.Add(_txtSearch);
            this.Controls.Add(pnlSearch);

            _resourcePanel = new FlowLayoutPanel {
                Location = new Point(30, 100),
                Size = new Size(740, 420),
                AutoScroll = true,
                BackColor = Color.Transparent
            };
            this.Controls.Add(_resourcePanel);
        }

        private void LoadResources()
        {
            _resourcePanel.Controls.Clear();
            
            string[] titles = { "Understanding Anxiety", "Depression Recovery", "Mindfulness 101", "Sleep Hygiene", "Managing Stress at School", "Building Resilience" };
            string[] descriptions = { 
                "Learn about the symptoms and how to manage daily anxiety.", 
                "Steps and resources to help guide you through recovery.",
                "Basic exercises to stay present and calm in your daily life.",
                "How to improve your sleep quality for better mental health.",
                "Practical tips for balancing academic pressure and well-being.",
                "Strengthening your mental toughness and emotional balance."
            };

            for (int i = 0; i < titles.Length; i++)
            {
                CreateResourceCard(titles[i], descriptions[i]);
            }
        }

        private void CreateResourceCard(string title, string desc)
        {
            Panel card = new Panel {
                Size = new Size(340, 180),
                BackColor = Color.White,
                Margin = new Padding(0, 0, 20, 20)
            };

            Label lblTitle = new Label {
                Text = title,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = AppTheme.PrimaryColor,
                Location = new Point(15, 15),
                AutoSize = true
            };

            Label lblDesc = new Label {
                Text = desc,
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(100, 100, 100),
                Location = new Point(15, 50),
                Size = new Size(310, 60)
            };

            Button btnRead = new Button {
                Text = "Read Article",
                Size = new Size(120, 35),
                Location = new Point(15, 130),
                BackColor = AppTheme.SecondaryColor,
                ForeColor = AppTheme.PrimaryColor,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            btnRead.FlatAppearance.BorderSize = 0;

            card.Controls.Add(lblTitle);
            card.Controls.Add(lblDesc);
            card.Controls.Add(btnRead);
            _resourcePanel.Controls.Add(card);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.Name = "ResourcesControl";
            this.Size = new System.Drawing.Size(800, 550);
            this.ResumeLayout(false);
        }
    }
}
