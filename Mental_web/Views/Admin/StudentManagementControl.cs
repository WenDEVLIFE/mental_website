using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Mental_web.UI;
using Mental_web.Data;

namespace Mental_web.Views.Admin
{
    public partial class StudentManagementControl : UserControl
    {
        private DataGridView _grid = null!;
        private TextBox _txtSearch = null!;
        private MentalHealthContext _db = null!;

        public StudentManagementControl()
        {
            InitializeComponent();
            _db = Program.ServiceProvider.GetRequiredService<MentalHealthContext>();
            SetupUI();
            LoadData();
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

            // Search Bar
            _txtSearch = new TextBox {
                Location = new Point(30, 80),
                Width = 400,
                Font = new Font("Segoe UI", 12),
                PlaceholderText = "Search by name or email..."
            };
            _txtSearch.TextChanged += (s, e) => LoadData(_txtSearch.Text);
            this.Controls.Add(_txtSearch);

            // Add Button
            var btnAdd = new Button {
                Text = "+ Add Student",
                Location = new Point(620, 80),
                Width = 150,
                Height = 35,
                BackColor = AppTheme.PrimaryColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.Click += (s, e) => ShowAddDialog();
            this.Controls.Add(btnAdd);
            UIHelper.MakeRounded(btnAdd, 10);

            // Grid
            _grid = new DataGridView {
                Location = new Point(30, 130),
                Size = new Size(740, 380),
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AllowUserToAddRows = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                ColumnHeadersHeight = 45,
                EnableHeadersVisualStyles = false,
                RowTemplate = { Height = 40 }
            };

            _grid.ColumnHeadersDefaultCellStyle.BackColor = AppTheme.PrimaryColor;
            _grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            _grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            _grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            _grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(232, 245, 233);
            _grid.DefaultCellStyle.SelectionForeColor = AppTheme.PrimaryColor;
            _grid.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            
            this.Controls.Add(_grid);

            // Action Buttons below grid
            var btnEdit = new Button { Text = "Edit Selected", Location = new Point(480, 520), Width = 140, Height = 30, FlatStyle = FlatStyle.Flat, ForeColor = Color.Blue };
            btnEdit.Click += (s, e) => EditSelected();
            this.Controls.Add(btnEdit);

            var btnDelete = new Button { Text = "Delete Selected", Location = new Point(630, 520), Width = 140, Height = 30, FlatStyle = FlatStyle.Flat, ForeColor = Color.Red };
            btnDelete.Click += (s, e) => DeleteSelected();
            this.Controls.Add(btnDelete);
        }

        private void LoadData(string search = "")
        {
            var query = _db.Students.AsQueryable();
            
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(s => s.FirstName.Contains(search) || s.LastName.Contains(search) || s.Email.Contains(search));
            }

            var list = query.Select(s => new {
                s.StudentId,
                Name = s.FirstName + " " + s.LastName,
                s.Email,
                s.Course,
                Year = s.YearLevel
            }).ToList();

            _grid.DataSource = list;
        }

        private void ShowAddDialog()
        {
            using (var dialog = new StudentDialog(new Student()))
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    _db.Students.Add(dialog.Student);
                    _db.SaveChanges();
                    LoadData();
                }
            }
        }

        private void EditSelected()
        {
            if (_grid.SelectedRows.Count > 0)
            {
                int id = (int)_grid.SelectedRows[0].Cells[0].Value;
                var student = _db.Students.Find(id);
                if (student != null)
                {
                    using (var dialog = new StudentDialog(student))
                    {
                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            _db.SaveChanges();
                            LoadData();
                        }
                    }
                }
            }
        }

        private void DeleteSelected()
        {
            if (_grid.SelectedRows.Count > 0)
            {
                int id = (int)_grid.SelectedRows[0].Cells[0].Value;
                if (MessageBox.Show("Are you sure you want to delete this student?", "Confirm Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    var student = _db.Students.Find(id);
                    if (student != null)
                    {
                        _db.Students.Remove(student);
                        _db.SaveChanges();
                        LoadData();
                    }
                }
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.Name = "StudentManagementControl";
            this.Size = new System.Drawing.Size(800, 600);
            this.ResumeLayout(false);
        }
    }

    // Quick Dialog for adding/editing students
    public class StudentDialog : Form
    {
        public Student Student { get; private set; }
        private TextBox txtFirst, txtLast, txtEmail, txtCourse;
        private NumericUpDown numYear;

        public StudentDialog(Student student)
        {
            this.Student = student;
            this.Text = student.StudentId == 0 ? "Add Student" : "Edit Student";
            this.Size = new Size(400, 500);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            int y = 30;
            CreateField("First Name:", ref txtFirst, student.FirstName, ref y);
            CreateField("Last Name:", ref txtLast, student.LastName, ref y);
            CreateField("Email:", ref txtEmail, student.Email, ref y);
            CreateField("Course:", ref txtCourse, student.Course, ref y);

            var lblYear = new Label { Text = "Year Level:", Location = new Point(30, y), AutoSize = true };
            numYear = new NumericUpDown { Location = new Point(30, y + 25), Width = 320, Minimum = 1, Maximum = 5, Value = (decimal)(student.YearLevel ?? 1) };
            this.Controls.Add(lblYear);
            this.Controls.Add(numYear);

            var btnSave = new Button { Text = "Save", Location = new Point(180, 400), Width = 100, Height = 40, BackColor = AppTheme.PrimaryColor, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnSave.Click += (s, e) => {
                Student.FirstName = txtFirst.Text;
                Student.LastName = txtLast.Text;
                Student.Email = txtEmail.Text;
                Student.Course = txtCourse.Text;
                Student.YearLevel = (int)numYear.Value;
                if (Student.StudentId == 0) Student.CreatedAt = DateTime.Now;
                this.DialogResult = DialogResult.OK;
                this.Close();
            };
            this.Controls.Add(btnSave);

            var btnCancel = new Button { Text = "Cancel", Location = new Point(290, 400), Width = 80, Height = 40, FlatStyle = FlatStyle.Flat };
            btnCancel.Click += (s, e) => this.Close();
            this.Controls.Add(btnCancel);
        }

        private void CreateField(string label, ref TextBox tb, string val, ref int y)
        {
            var lbl = new Label { Text = label, Location = new Point(30, y), AutoSize = true };
            tb = new TextBox { Text = val, Location = new Point(30, y + 25), Width = 320, Font = new Font("Segoe UI", 10) };
            this.Controls.Add(lbl);
            this.Controls.Add(tb);
            y += 70;
        }
    }
}
