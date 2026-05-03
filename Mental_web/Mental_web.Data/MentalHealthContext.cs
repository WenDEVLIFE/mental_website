using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mental_web.Data;

public class Admin
{
    [Key]
    [Column("admin_id")]
    public int AdminId { get; set; }
    [Column("username")]
    public string Username { get; set; } = null!;
    [Column("password_hash")]
    public string PasswordHash { get; set; } = null!;
    [Column("role")]
    public string? Role { get; set; }
}

public class Student
{
    [Key]
    [Column("student_id")]
    public int StudentId { get; set; }
    [Column("first_name")]
    public string FirstName { get; set; } = null!;
    [Column("last_name")]
    public string LastName { get; set; } = null!;
    [Column("course")]
    public string? Course { get; set; }
    [Column("year_level")]
    public int? YearLevel { get; set; }
    [Column("email")]
    public string Email { get; set; } = null!;
    [Column("contact_number")]
    public string? ContactNumber { get; set; }
    [Column("password_hash")]
    public string? PasswordHash { get; set; }
    [Column("created_at")]
    public DateTime? CreatedAt { get; set; }

    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    public ICollection<SelfAssessment> SelfAssessments { get; set; } = new List<SelfAssessment>();
    public ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
    public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}

public class Counselor
{
    [Key]
    [Column("counselor_id")]
    public int CounselorId { get; set; }
    [Column("name")]
    public string Name { get; set; } = null!;
    [Column("specialization")]
    public string? Specialization { get; set; }
    [Column("email")]
    public string Email { get; set; } = null!;
    [Column("contact_number")]
    public string? ContactNumber { get; set; }
    [Column("password_hash")]
    public string? PasswordHash { get; set; }

    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}

public class Appointment
{
    [Key]
    [Column("appointment_id")]
    public int AppointmentId { get; set; }
    [Column("student_id")]
    public int StudentId { get; set; }
    [Column("counselor_id")]
    public int CounselorId { get; set; }
    [Column("date", TypeName = "date")]
    public DateTime Date { get; set; }
    [Column("time", TypeName = "time")]
    public TimeSpan Time { get; set; }
    [Column("status")]
    public string? Status { get; set; }
    [Column("created_at")]
    public DateTime? CreatedAt { get; set; }

    [ForeignKey(nameof(StudentId))]
    public Student Student { get; set; } = null!;

    [ForeignKey(nameof(CounselorId))]
    public Counselor Counselor { get; set; } = null!;
    
    public ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
}

public class Schedule
{
    [Key]
    [Column("schedule_id")]
    public int ScheduleId { get; set; }
    [Column("counselor_id")]
    public int CounselorId { get; set; }
    [Column("available_date", TypeName = "date")]
    public DateTime AvailableDate { get; set; }
    [Column("available_time", TypeName = "time")]
    public TimeSpan AvailableTime { get; set; }

    [ForeignKey(nameof(CounselorId))]
    public Counselor Counselor { get; set; } = null!;
}

public class SelfAssessment
{
    [Key]
    [Column("assessment_id")]
    public int AssessmentId { get; set; }
    [Column("student_id")]
    public int StudentId { get; set; }
    [Column("score")]
    public int? Score { get; set; }
    [Column("result")]
    public string? Result { get; set; }
    [Column("date_taken")]
    public DateTime? DateTaken { get; set; }

    [ForeignKey(nameof(StudentId))]
    public Student Student { get; set; } = null!;
}

public class Feedback
{
    [Key]
    [Column("feedback_id")]
    public int FeedbackId { get; set; }
    [Column("student_id")]
    public int StudentId { get; set; }
    [Column("appointment_id")]
    public int? AppointmentId { get; set; }
    [Column("message")]
    public string? Message { get; set; }
    [Column("rating")]
    public int? Rating { get; set; }
    [Column("date")]
    public DateTime? Date { get; set; }

    [ForeignKey(nameof(StudentId))]
    public Student Student { get; set; } = null!;

    [ForeignKey(nameof(AppointmentId))]
    public Appointment? Appointment { get; set; }
}

public class Notification
{
    [Key]
    [Column("notification_id")]
    public int NotificationId { get; set; }
    [Column("student_id")]
    public int StudentId { get; set; }
    [Column("message")]
    public string Message { get; set; } = null!;
    [Column("status")]
    public string? Status { get; set; }
    [Column("created_at")]
    public DateTime? CreatedAt { get; set; }

    [ForeignKey(nameof(StudentId))]
    public Student Student { get; set; } = null!;
}

public class AssessmentQuestion
{
    [Key]
    [Column("question_id")]
    public int QuestionId { get; set; }
    [Column("question_text")]
    public string QuestionText { get; set; } = null!;
    [Column("question_type")]
    public string? QuestionType { get; set; }
    [Column("created_at")]
    public DateTime? CreatedAt { get; set; }
}

public class Resource
{
    [Key]
    [Column("resource_id")]
    public int ResourceId { get; set; }
    [Column("title")]
    public string Title { get; set; } = null!;
    [Column("content_type")]
    public string? ContentType { get; set; }
    [Column("content_body")]
    public string ContentBody { get; set; } = null!;
    [Column("author_id")]
    public int? AuthorId { get; set; }
    [Column("created_at")]
    public DateTime? CreatedAt { get; set; }
}

public class ActivityLog
{
    [Key]
    [Column("log_id")]
    public int LogId { get; set; }
    [Column("user_id")]
    public int UserId { get; set; }
    [Column("role")]
    public string Role { get; set; } = null!;
    [Column("action")]
    public string Action { get; set; } = null!;
    [Column("timestamp")]
    public DateTime? Timestamp { get; set; }
}

public class MentalHealthContext : DbContext
{
    public MentalHealthContext(DbContextOptions<MentalHealthContext> options) : base(options)
    {
    }

    public MentalHealthContext()
    {
    }

    public DbSet<Admin> Admins { get; set; } = null!;
    public DbSet<Student> Students { get; set; } = null!;
    public DbSet<Counselor> Counselors { get; set; } = null!;
    public DbSet<Appointment> Appointments { get; set; } = null!;
    public DbSet<Schedule> Schedules { get; set; } = null!;
    public DbSet<SelfAssessment> SelfAssessments { get; set; } = null!;
    public DbSet<Feedback> Feedbacks { get; set; } = null!;
    public DbSet<Notification> Notifications { get; set; } = null!;
    public DbSet<AssessmentQuestion> AssessmentQuestions { get; set; } = null!;
    public DbSet<Resource> Resources { get; set; } = null!;
    public DbSet<ActivityLog> ActivityLogs { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>().ToTable("admins");
        modelBuilder.Entity<Student>().ToTable("students");
        modelBuilder.Entity<Counselor>().ToTable("counselors");
        modelBuilder.Entity<Appointment>().ToTable("appointments");
        modelBuilder.Entity<Schedule>().ToTable("schedules");
        modelBuilder.Entity<SelfAssessment>().ToTable("self_assessments");
        modelBuilder.Entity<Feedback>().ToTable("feedbacks");
        modelBuilder.Entity<Notification>().ToTable("notifications");
        modelBuilder.Entity<AssessmentQuestion>().ToTable("assessment_questions");
        modelBuilder.Entity<Resource>().ToTable("resources");
        modelBuilder.Entity<ActivityLog>().ToTable("activity_logs");
    }
}
