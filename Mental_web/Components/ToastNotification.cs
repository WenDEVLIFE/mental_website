using System;
using System.Drawing;
using System.Windows.Forms;
using Mental_web.UI;

namespace Mental_web.Components
{
    public class ToastNotification : Form
    {
        private System.Windows.Forms.Timer _timer;
        private int _counter = 0;

        public ToastNotification(string message, Color? bgColor = null)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.Manual;
            this.ShowInTaskbar = false;
            this.Size = new Size(300, 60);
            this.BackColor = bgColor ?? AppTheme.PrimaryColor;
            this.TopMost = true;

            Label lbl = new Label {
                Text = message,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.Controls.Add(lbl);

            UIHelper.MakeRounded(this, 20);

            _timer = new System.Windows.Forms.Timer { Interval = 20 };
            _timer.Tick += (s, e) => {
                _counter++;
                if (_counter > 150) this.Opacity -= 0.05;
                if (this.Opacity <= 0) this.Close();
            };
        }

        public static void Show(string message, Form parent)
        {
            var toast = new ToastNotification(message);
            toast.Location = new Point(parent.Location.X + (parent.Width - toast.Width) / 2, parent.Location.Y + 50);
            toast.Show();
            toast._timer.Start();
        }
    }
}
