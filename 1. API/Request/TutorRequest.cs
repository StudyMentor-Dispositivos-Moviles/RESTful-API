using System.ComponentModel.DataAnnotations;

namespace _1._API.Request;

public class TutorRequest
{
    [Required(ErrorMessage = "El campo Name es obligatorio")]
    public string Name { get; set; }

    [Required(ErrorMessage = "El campo Lastname es obligatorio")]
    public string Lastname { get; set; }

    [Required(ErrorMessage = "El campo Email es obligatorio")]
    public string Email { get; set; }

    [Required(ErrorMessage = "El campo Password es obligatorio y tiene que tener 8 caracteres como mínimo")]
    [MinLength(8, ErrorMessage = "El campo Password debe tener al menos 8 caracteres")]
    public string Password { get; set; }

    [Required(ErrorMessage = "El campo Specialty es obligatorio")]
    public string Specialty { get; set; }

    [Required(ErrorMessage = "El campo Cost es obligatorio")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El campo Cost debe ser un valor positivo")]
    public decimal Cost { get; set; }

    [Required(ErrorMessage = "El campo Cellphone es obligatorio y debe tener 9 dígitos")]
    [StringLength(9, ErrorMessage = "El campo Cellphone debe tener exactamente 9 dígitos")]
    public string Cellphone { get; set; }

    public string Image { get; set; }
}