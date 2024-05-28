using System.ComponentModel.DataAnnotations;

namespace _1._API.Request
{
    public class ReviewRequest
    {
        [Required(ErrorMessage = "El campo TextMessage es obligatorio")]
        public string TextMessagge { get; set; }

        [Required(ErrorMessage = "El campo Rating es obligatorio")]
        [Range(1, 5, ErrorMessage = "El rating debe estar entre 1 y 5")]
        public int Rating { get; set; }

        [Required(ErrorMessage = "El campo StudentId es obligatorio")]
        public int StudentId { get; set; }
        
        [Required(ErrorMessage = "El campo Date es obligatorio")]
        
        public DateTime Date { get; set; }
        
        [Required(ErrorMessage = "TutorId is required.")]
        public int TutorId { get; set; }
    }
}