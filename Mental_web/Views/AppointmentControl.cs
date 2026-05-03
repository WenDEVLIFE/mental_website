using System;
using System.Drawing;
using System.Windows.Forms;
using Mental_web.UI;

namespace Mental_web.Views
{
    public partial class AppointmentControl : UserControl
    {
        private FlowLayoutPanel _timeSlotPanel;
        private TableLayoutPanel _calendarGrid;
        private Label _lblMonthYear;
        private DateTime _currentDate;

        public AppointmentControl()
        {
            InitializeComponent();
            _currentDate = DateTime.Now;
            SetupUI();
            LoadCalendar();
        }

        private void SetupUI()
        {
            this.BackColor = AppTheme.BackgroundColor;
            this.Dock = DockStyle.Fill;

            var lblTitle = new Label {
                Text = "Book a Counseling Session",
                Font = AppTheme.HeaderFont,
                ForeColor = AppTheme.PrimaryColor,
                Location = new Point(30, 30),
                AutoSize = true
            };
            this.Controls.Add(lblTitle);

            // Calendar Section
            Panel pnlCalendar = new Panel {
                Size = new Size(350, 400),
                Location = new Point(30, 90),
                BackColor = Color.White
            };
            
            _lblMonthYear = new Label {
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(0, 10),
                Width = 350,
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnlCalendar.Controls.Add(_lblMonthYear);

            _calendarGrid = new TableLayoutPanel {
                ColumnCount = 7,
                RowCount = 6,
                Location = new Point(10, 50),
                Size = new Size(330, 330),
            };
            for (int i = 0; i < 7; i++) _calendarGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.28f));
            for (int i = 0; i < 6; i++) _calendarGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 16.66f));
            pnlCalendar.Controls.Add(_calendarGrid);
            this.Controls.Add(pnlCalendar);

            // Time Slot Section
            var lblTime = new Label {
                Text = "Select Available Time",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(410, 90),
                AutoSize = true
            };
            this.Controls.Add(lblTime);

            _timeSlotPanel = new FlowLayoutPanel {
                Location = new Point(410, 130),
                Size = new Size(350, 300),
                AutoScroll = true
            };
            this.Controls.Add(_timeSlotPanel);

            var btnBook = new Button {
                Text = "Confirm Appointment",
                Size = new Size(350, 50),
                Location = new Point(410, 440),
                BackColor = AppTheme.PrimaryColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = AppTheme.ButtonFont
            };
            btnBook.FlatAppearance.BorderSize = 0;
            btnBook.Click += BtnBook_Click;
            this.Controls.Add(btnBook);
        }

        private void LoadCalendar()
        {
            _calendarGrid.Controls.Clear();
            _lblMonthYear.Text = _currentDate.ToString("MMMM yyyy");

            DateTime firstDay = new DateTime(_currentDate.Year, _currentDate.Month, 1);
            int startOffset = (int)firstDay.DayOfWeek;
            int daysInMonth = DateTime.DaysInMonth(_currentDate.Year, _currentDate.Month);

            // Header for Days of Week
            string[] days = { "S", "M", "T", "W", "T", "F", "S" };
            // Note: Grid implementation for days
            for (int i = 0; i < 42; i++)
            {
                int dayNum = i - startOffset + 1;
                if (dayNum > 0 && dayNum <= daysInMonth)
                {
                    Button btnDay = new Button {
                        Text = dayNum.ToString(),
                        Dock = DockStyle.Fill,
                        FlatStyle = FlatStyle.Flat,
                        Margin = new Padding(2),
                        BackColor = (dayNum == DateTime.Now.Day && _currentDate.Month == DateTime.Now.Month) ? AppTheme.SecondaryColor : Color.White
                    };
                    btnDay.FlatAppearance.BorderSize = 0;
                    btnDay.Click += (s, e) => {
                        foreach (Control c in _calendarGrid.Controls) c.BackColor = Color.White;
                        btnDay.BackColor = AppTheme.SecondaryColor;
                        LoadTimeSlots();
                    };
                    _calendarGrid.Controls.Add(btnDay);
                }
                else
                {
                    _calendarGrid.Controls.Add(new Label());
                }
            }
        }

        private void LoadTimeSlots()
        {
            _timeSlotPanel.Controls.Clear();
            string[] slots = { "08:00 AM", "09:30 AM", "11:00 AM", "01:30 PM", "03:00 PM", "04:30 PM" };
            foreach (var slot in slots)
            {
                Button btnSlot = new Button {
                    Text = slot,
                    Size = new Size(100, 40),
                    FlatStyle = FlatStyle.Flat,
                    BackColor = Color.White,
                    Margin = new Padding(5)
                };
                btnSlot.FlatAppearance.BorderColor = AppTheme.PrimaryColor;
                btnSlot.Click += (s, e) => {
                    foreach (Control c in _timeSlotPanel.Controls) c.BackColor = Color.White;
                    btnSlot.BackColor = AppTheme.SecondaryColor;
                };
                _timeSlotPanel.Controls.Add(btnSlot);
            }
        }

        private void BtnBook_Click(object sender, EventArgs e)
        {
            // Show custom success dialog
            MessageBox.Show("Appointment Requested!\n\nYour session with the counselor has been booked. You will receive a notification once it is approved.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.Name = "AppointmentControl";
            this.Size = new System.Drawing.Size(800, 550);
            this.ResumeLayout(false);
        }
    }
}
