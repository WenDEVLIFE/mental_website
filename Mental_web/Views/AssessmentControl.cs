using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Mental_web.UI;
using Mental_web.Data;

namespace Mental_web.Views
{
    public partial class AssessmentControl : UserControl
    {
        private UserSession _session;
        private int _currentIndex = 0;
        private List<string> _questions = new List<string> {
            "How often have you been bothered by feeling down, depressed, or hopeless?",
            "How often have you had little interest or pleasure in doing things?",
            "How often have you had trouble falling or staying asleep, or sleeping too much?",
            "How often have you felt tired or had little energy?",
            "How often have you had a poor appetite or been overeating?",
            "How often have you felt bad about yourself - or that you are a failure?",
            "How often have you had trouble concentrating on things?"
        };
        private List<int> _answers = new List<int>();

        private Label _lblQuestion = null!;
        private Panel _optionsPanel = null!;
        private Button _btnNext = null!;
        private Panel _progressPanel = null!;

        public AssessmentControl(UserSession session)
        {
            _session = session;
            InitializeComponent();
            SetupUI();
            ShowQuestion();
        }

        public AssessmentControl() : this(new UserSession()) { }

        private void SetupUI()
        {
            this.BackColor = AppTheme.BackgroundColor;
            this.Dock = DockStyle.Fill;

            var lblTitle = new Label {
                Text = "Self-Assessment",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = AppTheme.PrimaryColor,
                Location = new Point(50, 20),
                AutoSize = true
            };
            this.Controls.Add(lblTitle);

            _lblQuestion = new Label {
                Text = "Question text goes here...",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 64, 64),
                Location = new Point(50, 100),
                Size = new Size(700, 100),
                TextAlign = ContentAlignment.TopLeft
            };
            this.Controls.Add(_lblQuestion);

            _optionsPanel = new Panel {
                Location = new Point(50, 200),
                Size = new Size(700, 300)
            };
            this.Controls.Add(_optionsPanel);

            _btnNext = new Button {
                Text = "Next Question",
                Size = new Size(200, 50),
                Location = new Point(550, 500),
                BackColor = AppTheme.PrimaryColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            _btnNext.FlatAppearance.BorderSize = 0;
            _btnNext.Click += (s, e) => {
                if (_btnNext.Tag != null) {
                    _answers.Add((int)_btnNext.Tag);
                    _currentIndex++;
                    ShowQuestion();
                }
            };
            this.Controls.Add(_btnNext);

            _progressPanel = new Panel {
                Location = new Point(50, 70),
                Size = new Size(700, 8),
                BackColor = Color.FromArgb(230, 230, 230)
            };
            this.Controls.Add(_progressPanel);
        }

        private void ShowQuestion()
        {
            if (_currentIndex < _questions.Count)
            {
                _lblQuestion.Text = $"{_currentIndex + 1}. {_questions[_currentIndex]}";
                _optionsPanel.Controls.Clear();
                
                string[] options = { "Not at all", "Several days", "More than half the days", "Nearly every day" };
                for (int i = 0; i < options.Length; i++)
                {
                    CreateOptionButton(options[i], i, i * 65);
                }

                Panel progressBar = new Panel {
                    Width = (int)(((_currentIndex + 1) / (float)_questions.Count) * 700),
                    Height = 8,
                    BackColor = AppTheme.PrimaryColor
                };
                _progressPanel.Controls.Clear();
                _progressPanel.Controls.Add(progressBar);
                
                _btnNext.Enabled = false;
                _btnNext.BackColor = Color.LightGray;
            }
            else
            {
                SaveAndShowResults();
            }
        }

        private void CreateOptionButton(string text, int score, int top)
        {
            Panel pnl = new Panel {
                Size = new Size(650, 55),
                Location = new Point(0, top),
                BackColor = Color.White,
                Cursor = Cursors.Hand
            };
            UIHelper.MakeRounded(pnl, 12);

            Label lbl = new Label {
                Text = text,
                Font = new Font("Segoe UI", 11),
                Location = new Point(20, 15),
                AutoSize = true,
                Enabled = false
            };
            pnl.Controls.Add(lbl);

            pnl.Click += (s, e) => {
                foreach (Control c in _optionsPanel.Controls) c.BackColor = Color.White;
                pnl.BackColor = AppTheme.SecondaryColor;
                _btnNext.Tag = score;
                _btnNext.Enabled = true;
                _btnNext.BackColor = AppTheme.PrimaryColor;
            };
            _optionsPanel.Controls.Add(pnl);
        }

        private void SaveAndShowResults()
        {
            int totalScore = _answers.Sum();
            int maxScore = _questions.Count * 3;
            int percentage = (int)((totalScore / (float)maxScore) * 100);
            
            string resultStatus = "Normal";
            if (percentage > 70) resultStatus = "High Support Needed";
            else if (percentage > 40) resultStatus = "Moderate Stress";

            try 
            {
                using (var db = new MentalHealthContext(Program.ServiceProvider.GetRequiredService<DbContextOptions<MentalHealthContext>>()))
                {
                    var studentExists = db.Students.Any(s => s.StudentId == _session.UserId);
                    if (studentExists)
                    {
                        var assessment = new SelfAssessment {
                            StudentId = _session.UserId,
                            DateTaken = DateTime.Now,
                            Score = 100 - percentage,
                            Result = resultStatus
                        };
                        db.SelfAssessments.Add(assessment);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Result saved locally only. Database sync failed.", "Database Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            this.Controls.Clear();
            
            // Centered Results UI
            var lblFinish = new Label {
                Text = "Assessment Complete",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = AppTheme.PrimaryColor,
                Location = new Point(0, 120), // Moved down to avoid header cutoff
                Size = new Size(800, 60),
                TextAlign = ContentAlignment.TopCenter
            };
            this.Controls.Add(lblFinish);

            var chart = new Components.CircularProgressBar {
                Location = new Point(300, 200),
                Size = new Size(200, 200),
                Value = 100 - percentage,
                ProgressColor = percentage > 40 ? Color.FromArgb(230, 126, 34) : AppTheme.PrimaryColor,
                InnerColor = Color.White
            };
            this.Controls.Add(chart);

            var lblResult = new Label {
                Text = $"Your Well-being Score: {100 - percentage}%\nStatus: {resultStatus}",
                Font = new Font("Segoe UI", 13),
                Location = new Point(0, 420),
                Size = new Size(800, 60),
                TextAlign = ContentAlignment.TopCenter,
                ForeColor = Color.FromArgb(100, 100, 100)
            };
            this.Controls.Add(lblResult);
            
            var btnDone = new Button {
                Text = "Return to Dashboard",
                Size = new Size(280, 50),
                Location = new Point(260, 500), // Better centered (800-280)/2 = 260
                BackColor = AppTheme.PrimaryColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            UIHelper.MakeRounded(btnDone, 12);
            btnDone.Click += (s, e) => {
                var parent = this.Parent;
                if (parent != null)
                {
                    parent.Controls.Clear();
                    var dash = new DashboardControl(_session);
                    dash.Dock = DockStyle.Fill;
                    parent.Controls.Add(dash);
                }
            };
            this.Controls.Add(btnDone);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.Name = "AssessmentControl";
            this.Size = new System.Drawing.Size(800, 600);
            this.ResumeLayout(false);
        }
    }
}
