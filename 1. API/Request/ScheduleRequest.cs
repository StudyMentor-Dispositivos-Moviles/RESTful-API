using System.ComponentModel.DataAnnotations;

namespace _1._API.Request;

public class ScheduleRequest
{
    [Required(ErrorMessage = "El campo TutorName es obligatorio")]
    public string TutorName { get; set; }

    [Required(ErrorMessage = "El campo Days es obligatorio")]
    public string Days { get; set; }

    [Required(ErrorMessage = "El campo Time es obligatorio")]
    public string Time { get; set; }

    [Required(ErrorMessage = "El campo Price es obligatorio")]
    public string Price { get; set; }

    [Required(ErrorMessage = "El campo idTutor es obligatorio y debe ser un valor positivo")]
    [Range(1, int.MaxValue, ErrorMessage = "El campo idTutor debe ser un valor positivo")]
    public int idTutor { get; set; }
}