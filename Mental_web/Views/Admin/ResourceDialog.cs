using System;
using System.Drawing;
using System.Windows.Forms;
using Mental_web.UI;

namespace Mental_web.Views.Admin
{
    public class ResourceDialog : Form
    {
        public string ResourceTitle { get; private set; } = "";
        public string ResourceContent { get; private set; } = "";

        private TextBox _txtTitle;
        private TextBox _txtContent;

        public ResourceDialog(string initialTitle = "", string initialContent = "")
        {
            this.Text = string.IsNullOrEmpty(initialTitle) ? "Add Resource" : "Edit Resource";
            this.Size = new Size(500, 500);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.White;

            var lbl1 = new Label { Text = "Resource Title:", Location = new Point(20, 20), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            _txtTitle = new TextBox { Location = new Point(20, 45), Width = 440, Font = new Font("Segoe UI", 11), Text = initialTitle };
            
            var lbl2 = new Label { Text = "Content:", Location = new Point(20, 100), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            _txtContent = new TextBox { Location = new Point(20, 125), Width = 440, Height = 250, Multiline = true, Font = new Font("Segoe UI", 10), Text = initialContent, ScrollBars = ScrollBars.Vertical };

            var btnSave = new Button {
                Text = "Save Resource",
                Location = new Point(320, 400),
                Size = new Size(140, 45),
                BackColor = AppTheme.PrimaryColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                DialogResult = DialogResult.OK
            };
            btnSave.Click += (s, e) => {
                ResourceTitle = _txtTitle.Text;
                ResourceContent = _txtContent.Text;
            };

            this.Controls.Add(lbl1);
            this.Controls.Add(_txtTitle);
            this.Controls.Add(lbl2);
            this.Controls.Add(_txtContent);
            this.Controls.Add(btnSave);
        }
    }
}
