using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Mental_web.Components
{
    public class CircularProgressBar : Control
    {
        private int _value = 85;
        private int _maxValue = 100;
        private Color _progressColor = Color.FromArgb(45, 90, 39);
        private Color _backColor = Color.FromArgb(232, 245, 233);
        private int _strokeWidth = 10;

        public int Value { get => _value; set { _value = value; Invalidate(); } }
        public Color ProgressColor { get => _progressColor; set { _progressColor = value; Invalidate(); } }

        public CircularProgressBar()
        {
            this.DoubleBuffered = true;
            this.Size = new Size(100, 100);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(_strokeWidth / 2, _strokeWidth / 2, 
                                         Width - _strokeWidth, Height - _strokeWidth);

            // Draw Background Circle
            using (Pen backPen = new Pen(_backColor, _strokeWidth))
            {
                e.Graphics.DrawEllipse(backPen, rect);
            }

            // Draw Progress Arc
            float sweepAngle = (float)_value / _maxValue * 360;
            using (Pen progressPen = new Pen(_progressColor, _strokeWidth))
            {
                progressPen.StartCap = LineCap.Round;
                progressPen.EndCap = LineCap.Round;
                e.Graphics.DrawArc(progressPen, rect, -90, sweepAngle);
            }

            // Draw Text
            string text = _value + "%";
            Font font = new Font("Segoe UI", (float)Width / 5, FontStyle.Bold);
            Size textSize = TextRenderer.MeasureText(text, font);
            e.Graphics.DrawString(text, font, new SolidBrush(_progressColor), 
                                 (Width - textSize.Width) / 2, (Height - textSize.Height) / 2);
        }
    }
}
