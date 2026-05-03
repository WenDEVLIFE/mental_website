using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Mental_web.UI;
using Mental_web.Data;

namespace Mental_web.Views.Admin
{
    public partial class StudentManagementControl : UserControl
    {
        private FlowLayoutPanel _studentPanel = null!;
        private TextBox _txtSearch = null!;

        public StudentManagementControl()
        {
            InitializeComponent();
            SetupUI();
            LoadStudents();
        }

        private void SetupUI()
        {
            this.BackColor = AppTheme.BackgroundColor;
            this.Dock = DockStyle.Fill;

            var lblTitle = new Label {
                Text = "Student Management",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = AppTheme.PrimaryColor,
                Location = new Point(30, 30),
                AutoSize = true
            };
            this.Controls.Add(lblTitle);

            _txtSearch = new TextBox {
                Location = new Point(470, 35),
                Width = 300,
                Font = new Font("Segoe UI", 11),
                PlaceholderText = "Search by name or email..."
            };
            _txtSearch.TextChanged += (s, e) => LoadStudents(_txtSearch.Text);
            this.Controls.Add(_txtSearch);

            var btnAdd = new Button {
                Text = "+ Add New Student",
                Location = new Point(30, 85),
                Size = new Size(180, 40),
                BackColor = AppTheme.PrimaryColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnAdd.Click += (s, e) => AddStudent();
            this.Controls.Add(btnAdd);
            UIHelper.MakeRounded(btnAdd, 10);

            _studentPanel = new FlowLayoutPanel {
                Location = new Point(30, 140),
                Size = new Size(740, 390),
                AutoScroll = true,
                BackColor = Color.Transparent
            };
            this.Controls.Add(_studentPanel);
        }

        private void LoadStudents(string search = "")
        {
            _studentPanel.Controls.Clear();
            try
            {
                using (var db = CreateContext())
                {
                    var query = db.Students.AsQueryable();
                    if (!string.IsNullOrWhiteSpace(search))
                    {
                        query = query.Where(s => s.FirstName.Contains(search) || s.LastName.Contains(search) || s.Email.Contains(search));
                    }

                    var students = query.ToList();
                    foreach (var s in students) CreateStudentCard(s);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading students: " + ex.Message);
            }
        }

        private void CreateStudentCard(Student s)
        {
            Panel card = new Panel { Size = new Size(710, 60), BackColor = Color.White, Margin = new Padding(0, 0, 0, 10) };
            UIHelper.MakeRounded(card, 12);

            var lblName = new Label { Text = $"{s.FirstName} {s.LastName}", Font = new Font("Segoe UI", 10, FontStyle.Bold), Location = new Point(15, 20), AutoSize = true };
            var lblEmail = new Label { Text = s.Email, Font = new Font("Segoe UI", 9), ForeColor = Color.Gray, Location = new Point(250, 22), AutoSize = true };
            
            var btnEdit = new Button { Text = "Edit", Location = new Point(520, 15), Size = new Size(80, 30), FlatStyle = FlatStyle.Flat, ForeColor = AppTheme.PrimaryColor };
            btnEdit.Click += (sender, e) => EditStudent(s);
            
            var btnDel = new Button { Text = "Delete", Location = new Point(610, 15), Size = new Size(80, 30), FlatStyle = FlatStyle.Flat, ForeColor = Color.Red };
            btnDel.Click += (sender, e) => DeleteStudent(s);

            card.Controls.Add(lblName);
            card.Controls.Add(lblEmail);
            card.Controls.Add(btnEdit);
            card.Controls.Add(btnDel);
            _studentPanel.Controls.Add(card);
        }

        private void AddStudent()
        {
            using (var dialog = new StudentDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (var db = CreateContext())
                        {
                            db.Students.Add(dialog.Student);
                            db.SaveChanges();
                            LoadStudents();
                        }
                    }
                    catch (Exception ex) { MessageBox.Show("Error adding student: " + ex.Message); }
                }
            }
        }

        private void EditStudent(Student s)
        {
            using (var dialog = new StudentDialog(s))
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (var db = CreateContext())
                        {
                            db.Students.Update(dialog.Student);
                            db.SaveChanges();
                            LoadStudents();
                        }
                    }
                    catch (Exception ex) { MessageBox.Show("Error updating student: " + ex.Message); }
                }
            }
        }

        private void DeleteStudent(Student s)
        {
            if (MessageBox.Show("Delete this student permanently?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    using (var db = CreateContext())
                    {
                        db.Students.Remove(s);
                        db.SaveChanges();
                        LoadStudents();
                    }
                }
                catch (Exception ex) { MessageBox.Show("Error deleting student: " + ex.Message); }
            }
        }

        private MentalHealthContext CreateContext()
        {
            return new MentalHealthContext(Program.ServiceProvider.GetRequiredService<DbContextOptions<MentalHealthContext>>());
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.Name = "StudentManagementControl";
            this.Size = new System.Drawing.Size(800, 600);
            this.ResumeLayout(false);
        }
    }
}
