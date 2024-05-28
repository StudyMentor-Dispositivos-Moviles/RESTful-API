namespace _3._Data.Model;

public class Payment: ModelBase
{
    public string CardNumber { get; set; }
    public string ExpirationDate { get; set; }
    public string Owner { get; set; }
    public int Cvv { get; set; }
    // public Student student
    public int StudentId { get; set; }
}