using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Mental_web.UI;

namespace Mental_web.Views
{
    public partial class AssessmentControl : UserControl
    {
        private int _currentIndex = 0;
        private List<string> _questions = new List<string> {
            "How often have you been bothered by feeling down, depressed, or hopeless?",
            "How often have you had little interest or pleasure in doing things?",
            "How often have you had trouble falling or staying asleep, or sleeping too much?",
            "How often have you felt tired or had little energy?",
            "How often have you had a poor appetite or been overeating?"
        };
        private List<int> _answers = new List<int>();

        private Label _lblQuestion = null!;
        private Panel _optionsPanel = null!;
        private Button _btnNext = null!;
        private Panel _progressPanel = null!;

        public AssessmentControl()
        {
            InitializeComponent();
            SetupUI();
            ShowQuestion();
        }

        private void SetupUI()
        {
            this.BackColor = AppTheme.BackgroundColor;
            this.Dock = DockStyle.Fill;

            _lblQuestion = new Label {
                Text = "Question text goes here...",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = AppTheme.PrimaryColor,
                Location = new Point(50, 80),
                Size = new Size(700, 100),
                TextAlign = ContentAlignment.TopLeft
            };
            this.Controls.Add(_lblQuestion);

            _optionsPanel = new Panel {
                Location = new Point(50, 180),
                Size = new Size(700, 300)
            };
            this.Controls.Add(_optionsPanel);

            _btnNext = new Button {
                Text = "Next Question",
                Size = new Size(200, 50),
                Location = new Point(550, 480),
                BackColor = AppTheme.PrimaryColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = AppTheme.ButtonFont
            };
            _btnNext.FlatAppearance.BorderSize = 0;
            _btnNext.Click += BtnNext_Click;
            this.Controls.Add(_btnNext);

            _progressPanel = new Panel {
                Location = new Point(50, 50),
                Size = new Size(700, 10),
                BackColor = AppTheme.SecondaryColor
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
                    CreateOptionButton(options[i], i, 0 + (i * 60));
                }

                // Update Progress Bar
                var progressBar = new Panel {
                    Width = (int)(((_currentIndex + 1) / (float)_questions.Count) * 700),
                    Height = 10,
                    BackColor = AppTheme.PrimaryColor
                };
                _progressPanel.Controls.Clear();
                _progressPanel.Controls.Add(progressBar);
                
                _btnNext.Enabled = false;
                _btnNext.BackColor = Color.Gray;
            }
            else
            {
                ShowResults();
            }
        }

        private void CreateOptionButton(string text, int score, int top)
        {
            Panel pnl = new Panel {
                Size = new Size(600, 50),
                Location = new Point(0, top),
                BackColor = Color.White,
                Cursor = Cursors.Hand,
                Tag = score
            };
            Label lbl = new Label {
                Text = text,
                Font = new Font("Segoe UI", 11),
                Location = new Point(15, 15),
                AutoSize = true,
                Enabled = false // Allow click to pass to panel
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

        private void BtnNext_Click(object sender, EventArgs e)
        {
            _answers.Add((int)_btnNext.Tag);
            _currentIndex++;
            ShowQuestion();
        }

        private void ShowResults()
        {
            this.Controls.Clear();
            int totalScore = 0;
            foreach (int s in _answers) totalScore += s;
            int percentage = (int)((totalScore / (float)(_questions.Count * 3)) * 100);

            var lblFinish = new Label {
                Text = "Assessment Complete!",
                Font = AppTheme.HeaderFont,
                ForeColor = AppTheme.PrimaryColor,
                Location = new Point(0, 50),
                Width = 800,
                TextAlign = ContentAlignment.TopCenter
            };
            this.Controls.Add(lblFinish);

            var chart = new Components.CircularProgressBar {
                Location = new Point(300, 150),
                Size = new Size(200, 200),
                Value = 100 - percentage, // Reverse for "Well-being" score
                ProgressColor = percentage > 50 ? Color.Orange : AppTheme.PrimaryColor
            };
            this.Controls.Add(chart);

            var lblResult = new Label {
                Text = percentage > 50 ? "We recommend speaking with a counselor." : "You're doing great! Keep tracking your progress.",
                Font = new Font("Segoe UI", 12),
                Location = new Point(0, 380),
                Width = 800,
                TextAlign = ContentAlignment.TopCenter
            };
            this.Controls.Add(lblResult);
            
            var btnDone = new Button {
                Text = "Back to Dashboard",
                Size = new Size(200, 50),
                Location = new Point(300, 450),
                BackColor = AppTheme.PrimaryColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnDone.Click += (s, e) => {
                // This would normally go back via MainForm navigation
                if (this.Parent != null)
                {
                    this.Parent.Controls.Clear();
                    this.Parent.Controls.Add(new DashboardControl());
                }
            };
            this.Controls.Add(btnDone);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.Name = "AssessmentControl";
            this.Size = new System.Drawing.Size(800, 550);
            this.ResumeLayout(false);
        }
    }
}
