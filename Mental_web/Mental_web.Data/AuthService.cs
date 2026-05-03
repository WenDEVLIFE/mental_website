using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Mental_web.Data;

public class UserSession
{
    public int UserId { get; set; }
    public string Username { get; set; } = null!;
    public string Role { get; set; } = null!; // "Admin", "Student", "Counselor"
}

public class AuthService
{
    private readonly MentalHealthContext _context;

    public AuthService(MentalHealthContext context)
    {
        _context = context;
    }

    public static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }

    public async Task<UserSession?> AuthenticateAsync(string usernameOrEmail, string password)
    {
        var hash = HashPassword(password);

        // 1. Check Admin
        var admin = await _context.Admins.FirstOrDefaultAsync(a => a.Username == usernameOrEmail && a.PasswordHash == hash);
        if (admin != null)
        {
            return new UserSession { UserId = admin.AdminId, Username = admin.Username, Role = admin.Role ?? "Admin" };
        }

        // 2. Check Student
        var student = await _context.Students.FirstOrDefaultAsync(s => s.Email == usernameOrEmail && s.PasswordHash == hash);
        if (student != null)
        {
            return new UserSession { UserId = student.StudentId, Username = student.Email, Role = "Student" };
        }

        // 3. Check Counselor
        var counselor = await _context.Counselors.FirstOrDefaultAsync(c => c.Email == usernameOrEmail && c.PasswordHash == hash);
        if (counselor != null)
        {
            return new UserSession { UserId = counselor.CounselorId, Username = counselor.Email, Role = "Counselor" };
        }

        return null;
    }

    public async Task RegisterStudentAsync(Student student, string plainPassword)
    {
        student.PasswordHash = HashPassword(plainPassword);
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
    }
}
