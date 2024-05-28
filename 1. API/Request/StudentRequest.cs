using System.ComponentModel.DataAnnotations;

namespace _1._API.Request;

public class StudentRequest
{
    [Required(ErrorMessage = "El campo Name es obligatorio")]
    public string Name { get; set; }
    [Required(ErrorMessage = "El campo Lastname es obligatorio")]
    public string Lastname { get; set; }
    [Required(ErrorMessage = "El campo Email es obligatorio")]
    public string Email { get; set; }
    [Required(ErrorMessage = "El campo Password es obligatorio y tiene que tener 8 caracteres como minimo")]
    [MinLength(8)]
    public string Password { get; set; }
    [Required(ErrorMessage = "El campo Genre es obligatorio")]
    public _3._Data.Model.Genres Genre { get; set; }
    [Required(ErrorMessage = "El campo Birthday es obligatorio")]
    public DateTime Birthday { get; set; }
    [Required(ErrorMessage = "El campo Cellphone es obligatorio y tiene que tener 9 dígitos")]
    [StringLength(9)]
    public string Cellphone { get; set; }
    
    public string Image { get; set; }
}