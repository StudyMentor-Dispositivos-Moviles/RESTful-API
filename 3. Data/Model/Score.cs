namespace _3._Data.Model;

public class Score : ModelBase
{
    

    public int StudentId { get; set; }
    public int TutorId { get; set; }

    public string Type { get; set; }
    public DateTime Date { get; set; }
    public string ScoreValue { get; set; }
    public string Status { get; set; }

    
}