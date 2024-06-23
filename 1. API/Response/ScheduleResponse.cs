namespace _1._API.Response;

public class ScheduleResponse
{
    public int Id { get; set; }
    public DateOnly Day { get; set; }
    public int TutorHours { get; set; }
    public TimeOnly StartingHour { get; set; }
    public double Price { get; set; }
    public bool IsAvailable { get; set; }
    public int TutorId { get; set; }
}