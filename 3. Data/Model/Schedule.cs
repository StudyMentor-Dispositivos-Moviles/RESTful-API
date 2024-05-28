namespace _3._Data.Model;

public class Schedule
{
    public int Id { get; set; }
    public string TutorName { get; set; }
    public string Days { get; set; }
    public string Time { get; set; }
    public string Price { get; set; }
    public int idTutor { get; set; }
}