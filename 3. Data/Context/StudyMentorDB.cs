using _3._Data.Model;
using Microsoft.EntityFrameworkCore;

namespace _3._Data.Context;

public class StudyMentorDB : DbContext
{
    public StudyMentorDB()
    {
        
    }
    
    public StudyMentorDB(DbContextOptions<StudyMentorDB> options) : base(options){}
    
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Tutor> Tutors { get; set; }
    public DbSet<Student> Students { get; set; }
    
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Score> Scores { get; set; }
    // db set reviews
    
    public DbSet<Schedule> Schedules { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
            optionsBuilder.UseMySql("Server=sql10.freemysqlhosting.net,3306;Uid=sql10663775;Pwd=xHa8RELc7L;Database=sql10663775;", serverVersion);
        }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // nombrar tablas
        builder.Entity<Payment>().ToTable("Payment");
        // has key definir llave primaria
        builder.Entity<Payment>().HasKey(p => p.Id);
        // definir campos requeridos
        builder.Entity<Payment>().Property(p => p.CardNumber).IsRequired().HasPrecision(16);
        builder.Entity<Payment>().Property(p => p.Cvv).IsRequired().HasPrecision(3);
        // obligatorio
        builder.Entity<Payment>().Property(p => p.DateCreated).HasDefaultValue(DateTime.Now);
        builder.Entity<Payment>().Property(p => p.IsActive).HasDefaultValue(true);
        
        //REVIEWS
        builder.Entity<Review>().ToTable("Review");
        builder.Entity<Review>().HasKey(p => p.Id);
        builder.Entity<Review>().Property(p => p.TextMessagge).IsRequired().HasPrecision(200);
        builder.Entity<Review>().Property(p => p.Rating).IsRequired().HasPrecision(5);
        builder.Entity<Review>().Property(p => p.DateCreated).HasDefaultValue(DateTime.Now);
        builder.Entity<Review>().Property(p => p.IsActive).HasDefaultValue(true);
        
        // Schedule
        builder.Entity<Schedule>().ToTable("Schedule");
        builder.Entity<Schedule>().HasKey(s => s.Id);
        builder.Entity<Schedule>().Property(s => s.TutorName).IsRequired().HasMaxLength(45);
        builder.Entity<Schedule>().Property(s => s.Days).IsRequired().HasMaxLength(45);
        builder.Entity<Schedule>().Property(s => s.Time).IsRequired().HasMaxLength(45);
        builder.Entity<Schedule>().Property(s => s.Price).IsRequired().HasMaxLength(45);
        builder.Entity<Schedule>().Property(s => s.idTutor).IsRequired();
        
        //Students
        builder.Entity<Student>().ToTable("Student");
        builder.Entity<Student>().HasKey(p => p.Id);
        builder.Entity<Student>().Property(c => c.Name).IsRequired().HasMaxLength(45);
        builder.Entity<Student>().Property(q => q.Lastname).IsRequired().HasMaxLength(45);
        builder.Entity<Student>().Property(q => q.Email).IsRequired().HasMaxLength(45);
        builder.Entity<Student>().Property(q => q.Password).IsRequired().HasMaxLength(45);
        builder.Entity<Student>().OwnsOne(q => q.Genre, genre =>
        {
            genre.Property(g => g.NameGenre).IsRequired().HasMaxLength(10);
            genre.Property(g => g.Code).IsRequired().HasMaxLength(1);
        });
        builder.Entity<Student>().Property(q => q.Birthday).IsRequired().HasColumnType("date");
        builder.Entity<Student>().Property(q => q.Cellphone).IsRequired().HasMaxLength(9);
        builder.Entity<Student>().Property(q => q.Image).IsRequired().HasMaxLength(248);
        
        //Tutors
        builder.Entity<Tutor>().ToTable("Tutor");
        builder.Entity<Tutor>().HasKey(p => p.Id);
        builder.Entity<Tutor>().Property(c => c.Name).IsRequired().HasMaxLength(45);
        builder.Entity<Tutor>().Property(q => q.Lastname).IsRequired().HasMaxLength(45);
        builder.Entity<Tutor>().Property(q => q.Email).IsRequired().HasMaxLength(45);
        builder.Entity<Tutor>().Property(q => q.Password).IsRequired().HasMaxLength(45);
        builder.Entity<Tutor>().Property(q => q.Cellphone).IsRequired().HasMaxLength(9);
        builder.Entity<Tutor>().Property(q => q.Specialty).IsRequired().HasMaxLength(45);
        builder.Entity<Tutor>().Property(q => q.Cost).IsRequired();
        builder.Entity<Tutor>().Property(q => q.Image).IsRequired().HasMaxLength(248);
        
        //Scores
        builder.Entity<Score>().ToTable("Score");

        builder.Entity<Score>().HasKey(p => p.Id);

        builder.Entity<Score>().Property(p => p.Type).IsRequired();
        builder.Entity<Score>().Property(p => p.Date).IsRequired().HasColumnType("date");
        builder.Entity<Score>().Property(p => p.ScoreValue).IsRequired().HasMaxLength(5); 
        builder.Entity<Score>().Property(p => p.Status).IsRequired();

    }
}