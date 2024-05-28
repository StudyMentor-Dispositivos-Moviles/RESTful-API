using Microsoft.Build.Framework;

namespace _1._API.Request;

public class PaymentRequest
{
    [Required]
    public string CardNumber { get; set; }
    [Required]
    public string ExpirationDate { get; set; }
    [Required]
    public string Owner { get; set; }
    [Required]
    public int Cvv { get; set; }
    // public Student student
    [Required]
    public int StudentId { get; set; }
}