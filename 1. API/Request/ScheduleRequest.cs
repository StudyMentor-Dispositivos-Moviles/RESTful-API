using System.ComponentModel.DataAnnotations;

namespace _1._API.Request;

public class ScheduleRequest
{
    [Required(ErrorMessage = "El campo Day es obligatorio")]
    public DateOnly Day { get; set; }

    [Required(ErrorMessage = "El campo TutorHours es obligatorio")]
    public int TutorHours { get; set; }

    [Required(ErrorMessage = "El campo StartingHour es obligatorio")]
    public TimeOnly StartingHour { get; set; }

    [Required(ErrorMessage = "El campo Price es obligatorio")]
    public double Price { get; set; }

    [Required(ErrorMessage = "El campo IsAvailable es obligatorio")]
    public bool IsAvailable { get; set; }
        
    [Required(ErrorMessage = "El campo TutorId es obligatorio y debe ser un valor positivo")]
    public int TutorId { get; set; }
    
    [Required(ErrorMessage = "El campo TutorId es obligatorio y debe ser un valor positivo")]
    public int StudentId { get; set; }
}