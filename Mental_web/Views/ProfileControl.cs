using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Mental_web.UI;
using Mental_web.Data;

namespace Mental_web.Views
{
    public class ProfileControl : UserControl
    {
        private UserSession _session;
        private TextBox txtFirst, txtLast, txtContact, txtCourse;
        private NumericUpDown numYear;

        public ProfileControl(UserSession session)
        {
            _session = session;
            SetupUI();
            LoadData();
        }

        private void SetupUI()
        {
            this.BackColor = AppTheme.BackgroundColor;
            this.Dock = DockStyle.Fill;

            var lblTitle = new Label {
                Text = "My Profile",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = AppTheme.PrimaryColor,
                Location = new Point(30, 30),
                AutoSize = true
            };
            this.Controls.Add(lblTitle);

            var panel = new Panel {
                Location = new Point(30, 90),
                Size = new Size(500, 450),
                BackColor = Color.White,
                Padding = new Padding(30)
            };
            UIHelper.MakeRounded(panel, 25);

            int y = 30;
            CreateField(panel, "First Name:", ref txtFirst, ref y);
            CreateField(panel, "Last Name:", ref txtLast, ref y);
            CreateField(panel, "Contact Number:", ref txtContact, ref y);
            CreateField(panel, "Course:", ref txtCourse, ref y);

            var lblYear = new Label { Text = "Year Level:", Location = new Point(30, y), AutoSize = true, Font = new Font("Segoe UI", 9, FontStyle.Bold) };
            numYear = new NumericUpDown { Location = new Point(30, y + 25), Width = 440, Minimum = 1, Maximum = 5, Font = new Font("Segoe UI", 11) };
            panel.Controls.Add(lblYear);
            panel.Controls.Add(numYear);

            var btnSave = new Button {
                Text = "Save Profile Changes",
                Location = new Point(30, y + 80),
                Width = 440,
                Height = 45,
                BackColor = AppTheme.PrimaryColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnSave.Click += (s, e) => SaveProfile();
            panel.Controls.Add(btnSave);
            UIHelper.MakeRounded(btnSave, 12);

            this.Controls.Add(panel);
        }

        private void CreateField(Panel p, string label, ref TextBox tb, ref int y)
        {
            var lbl = new Label { Text = label, Location = new Point(30, y), AutoSize = true, Font = new Font("Segoe UI", 9, FontStyle.Bold) };
            tb = new TextBox { Location = new Point(30, y + 25), Width = 440, Font = new Font("Segoe UI", 11), BorderStyle = BorderStyle.FixedSingle };
            p.Controls.Add(lbl);
            p.Controls.Add(tb);
            y += 75;
        }

        private void LoadData()
        {
            try
            {
                using (var db = CreateContext())
                {
                    var student = db.Students.Find(_session.UserId);
                    if (student != null)
                    {
                        txtFirst.Text = student.FirstName;
                        txtLast.Text = student.LastName;
                        txtContact.Text = student.ContactNumber;
                        txtCourse.Text = student.Course;
                        numYear.Value = (decimal)(student.YearLevel ?? 1);
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error loading profile: " + ex.Message); }
        }

        private void SaveProfile()
        {
            try
            {
                using (var db = CreateContext())
                {
                    var student = db.Students.Find(_session.UserId);
                    if (student != null)
                    {
                        student.FirstName = txtFirst.Text;
                        student.LastName = txtLast.Text;
                        student.ContactNumber = txtContact.Text;
                        student.Course = txtCourse.Text;
                        student.YearLevel = (int)numYear.Value;

                        db.SaveChanges();
                        MessageBox.Show("Profile updated successfully!", "Success");
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error saving profile: " + ex.Message); }
        }

        private MentalHealthContext CreateContext()
        {
            return new MentalHealthContext(Program.ServiceProvider.GetRequiredService<DbContextOptions<MentalHealthContext>>());
        }
    }
}
