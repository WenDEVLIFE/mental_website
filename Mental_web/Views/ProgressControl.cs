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
    public class ProgressControl : UserControl
    {
        private UserSession _session;
        private MentalHealthContext _db;
        private FlowLayoutPanel _historyPanel;

        public ProgressControl(UserSession session)
        {
            _session = session;
            _db = Program.ServiceProvider.GetRequiredService<MentalHealthContext>();
            SetupUI();
            LoadData();
        }

        private void SetupUI()
        {
            this.BackColor = AppTheme.BackgroundColor;
            this.Dock = DockStyle.Fill;

            var lblTitle = new Label {
                Text = "Mental Health Progress",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = AppTheme.PrimaryColor,
                Location = new Point(30, 30),
                AutoSize = true
            };
            this.Controls.Add(lblTitle);

            var lblSub = new Label {
                Text = "Tracking your assessment scores over time.",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.Gray,
                Location = new Point(32, 70),
                AutoSize = true
            };
            this.Controls.Add(lblSub);

            _historyPanel = new FlowLayoutPanel {
                Location = new Point(30, 110),
                Size = new Size(740, 420),
                AutoScroll = true,
                BackColor = Color.Transparent
            };
            this.Controls.Add(_historyPanel);
        }

        private void LoadData()
        {
            _historyPanel.Controls.Clear();
            var assessments = _db.SelfAssessments
                .Where(s => s.StudentId == _session.UserId)
                .OrderByDescending(s => s.DateTaken)
                .ToList();

            if (!assessments.Any())
            {
                var lblEmpty = new Label { Text = "No assessment history found. Take your first test!", Font = new Font("Segoe UI", 12), ForeColor = Color.Gray, Size = new Size(700, 100), TextAlign = ContentAlignment.MiddleCenter };
                _historyPanel.Controls.Add(lblEmpty);
                return;
            }

            foreach (var ass in assessments)
            {
                CreateProgressCard(ass);
            }
        }

        private void CreateProgressCard(SelfAssessment ass)
        {
            Panel card = new Panel {
                Size = new Size(710, 80),
                BackColor = Color.White,
                Margin = new Padding(0, 0, 0, 15)
            };
            UIHelper.MakeRounded(card, 15);

            var lblDate = new Label {
                Text = ass.DateTaken?.ToShortDateString() ?? "N/A",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(20, 30),
                AutoSize = true
            };

            var lblResult = new Label {
                Text = ass.Result ?? "Completed",
                Font = new Font("Segoe UI", 11),
                ForeColor = AppTheme.PrimaryColor,
                Location = new Point(150, 28),
                AutoSize = true
            };

            var lblScore = new Label {
                Text = $"Score: {ass.Score}%",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.DimGray,
                Location = new Point(550, 26),
                AutoSize = true,
                TextAlign = ContentAlignment.TopRight
            };

            var indicator = new Panel {
                Size = new Size(100, 10),
                Location = new Point(550, 55),
                BackColor = Color.FromArgb(240, 240, 240)
            };
            var bar = new Panel {
                Size = new Size((int)(ass.Score ?? 0), 10),
                Location = new Point(0, 0),
                BackColor = AppTheme.PrimaryColor
            };
            indicator.Controls.Add(bar);

            card.Controls.Add(lblDate);
            card.Controls.Add(lblResult);
            card.Controls.Add(lblScore);
            card.Controls.Add(indicator);
            _historyPanel.Controls.Add(card);
        }
    }
}
