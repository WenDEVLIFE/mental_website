using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using Mental_web.UI;
using Mental_web.Data;

namespace Mental_web.Views
{
    public partial class ResourcesControl : UserControl
    {
        private FlowLayoutPanel _resourcePanel = null!;
        private TextBox _txtSearch = null!;
        private MentalHealthContext _db;

        public ResourcesControl()
        {
            InitializeComponent();
            _db = Program.ServiceProvider.GetRequiredService<MentalHealthContext>();
            SetupUI();
            LoadResources();
        }

        private void SetupUI()
        {
            this.BackColor = AppTheme.BackgroundColor;
            this.Dock = DockStyle.Fill;

            var lblTitle = new Label {
                Text = "Mental Health Resources",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = AppTheme.PrimaryColor,
                Location = new Point(30, 30),
                AutoSize = true
            };
            this.Controls.Add(lblTitle);

            // Search Bar
            _txtSearch = new TextBox {
                Location = new Point(470, 40),
                Width = 300,
                Font = new Font("Segoe UI", 11),
                PlaceholderText = "Search resources..."
            };
            _txtSearch.TextChanged += (s, e) => LoadResources(_txtSearch.Text);
            this.Controls.Add(_txtSearch);

            _resourcePanel = new FlowLayoutPanel {
                Location = new Point(30, 100),
                Size = new Size(740, 450),
                AutoScroll = true,
                BackColor = Color.Transparent
            };
            this.Controls.Add(_resourcePanel);
        }

        private void LoadResources(string search = "")
        {
            _resourcePanel.Controls.Clear();
            var query = _db.Resources.AsQueryable();
            
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(r => r.Title.Contains(search) || r.ContentType.Contains(search));
            }

            var resources = query.ToList();

            if (!resources.Any())
            {
                var lblEmpty = new Label { Text = "No resources found.", Font = new Font("Segoe UI", 12), ForeColor = Color.Gray, Size = new Size(700, 100), TextAlign = ContentAlignment.MiddleCenter };
                _resourcePanel.Controls.Add(lblEmpty);
                return;
            }

            foreach (var r in resources)
            {
                CreateResourceCard(r);
            }
        }

        private void CreateResourceCard(Resource res)
        {
            Panel card = new Panel {
                Size = new Size(345, 180),
                BackColor = Color.White,
                Margin = new Padding(0, 0, 20, 20)
            };
            UIHelper.MakeRounded(card, 20);

            var lblType = new Label {
                Text = res.ContentType?.ToUpper() ?? "ARTICLE",
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                ForeColor = Color.Gray,
                Location = new Point(20, 20),
                AutoSize = true
            };

            Label lblTitle = new Label {
                Text = res.Title,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = AppTheme.PrimaryColor,
                Location = new Point(18, 45),
                Size = new Size(300, 50),
                AutoEllipsis = true
            };

            Button btnRead = new Button {
                Text = "Read More",
                Size = new Size(130, 40),
                Location = new Point(20, 120),
                BackColor = AppTheme.SecondaryColor,
                ForeColor = AppTheme.PrimaryColor,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnRead.FlatAppearance.BorderSize = 0;
            btnRead.Click += (s, e) => MessageBox.Show(res.ContentBody ?? "No content available.", res.Title);
            UIHelper.MakeRounded(btnRead, 10);

            card.Controls.Add(lblType);
            card.Controls.Add(lblTitle);
            card.Controls.Add(btnRead);
            _resourcePanel.Controls.Add(card);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.Name = "ResourcesControl";
            this.Size = new System.Drawing.Size(800, 600);
            this.ResumeLayout(false);
        }
    }
}
