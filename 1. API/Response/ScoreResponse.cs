namespace _1._API.Response;

public class ScoreResponse
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int TutorId { get; set; }
    public string Type { get; set; }
   
    public DateTime Date { get; set; }

    public string ScoreValue { get; set; }

    public string Status { get; set; }
    
    
}