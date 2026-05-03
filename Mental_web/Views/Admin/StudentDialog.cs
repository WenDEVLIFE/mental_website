using System;
using System.Drawing;
using System.Windows.Forms;
using Mental_web.UI;
using Mental_web.Data;

namespace Mental_web.Views.Admin
{
    public class StudentDialog : Form
    {
        public Student Student { get; private set; }
        private TextBox txtFirst, txtLast, txtEmail, txtCourse, txtContact, txtPass;
        private NumericUpDown numYear;

        public StudentDialog(Student s = null)
        {
            Student = s ?? new Student { CreatedAt = DateTime.Now };
            this.Text = s == null ? "Add New Student" : "Edit Student";
            this.Size = new Size(450, 600);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.White;

            int y = 20;
            CreateField("First Name:", ref txtFirst, Student.FirstName, ref y);
            CreateField("Last Name:", ref txtLast, Student.LastName, ref y);
            CreateField("Email:", ref txtEmail, Student.Email, ref y);
            CreateField("Course:", ref txtCourse, Student.Course, ref y);
            CreateField("Contact:", ref txtContact, Student.ContactNumber, ref y);
            CreateField("Password:", ref txtPass, "", ref y);
            txtPass.PasswordChar = '*';

            var lblYear = new Label { Text = "Year Level:", Location = new Point(30, y), AutoSize = true, Font = new Font("Segoe UI", 9, FontStyle.Bold) };
            numYear = new NumericUpDown { Location = new Point(30, y + 25), Width = 370, Minimum = 1, Maximum = 5, Value = (decimal)(Student.YearLevel ?? 1) };
            this.Controls.Add(lblYear);
            this.Controls.Add(numYear);

            var btnSave = new Button {
                Text = "Save Student Data",
                Location = new Point(30, y + 80),
                Width = 370,
                Height = 45,
                BackColor = AppTheme.PrimaryColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                DialogResult = DialogResult.OK
            };
            btnSave.Click += BtnSave_Click;
            this.Controls.Add(btnSave);
            UIHelper.MakeRounded(btnSave, 12);
        }

        private void CreateField(string label, ref TextBox tb, string value, ref int y)
        {
            var lbl = new Label { Text = label, Location = new Point(30, y), AutoSize = true, Font = new Font("Segoe UI", 9, FontStyle.Bold) };
            tb = new TextBox { Location = new Point(30, y + 25), Width = 370, Font = new Font("Segoe UI", 10), Text = value };
            this.Controls.Add(lbl);
            this.Controls.Add(tb);
            y += 65;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Student.FirstName = txtFirst.Text;
            Student.LastName = txtLast.Text;
            Student.Email = txtEmail.Text;
            Student.Course = txtCourse.Text;
            Student.ContactNumber = txtContact.Text;
            Student.YearLevel = (int)numYear.Value;
            
            if (!string.IsNullOrWhiteSpace(txtPass.Text))
            {
                Student.PasswordHash = txtPass.Text; // In a real app, hash this
            }
        }
    }
}
