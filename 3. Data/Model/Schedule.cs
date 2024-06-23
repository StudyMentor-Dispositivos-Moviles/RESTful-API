namespace _3._Data.Model;

public class Schedule
{
    public int Id { get; set; }
    public DateOnly Day { get; set; }
    public int TutorHours { get; set; }
    public TimeOnly StartingHour { get; set; }
    public double Price { get; set; }
    public bool IsAvailable { get; set; }
    // Foreign Key
    public int TutorId { get; set; }

    // Navigation Property
    public Tutor Tutor { get; set; }
}