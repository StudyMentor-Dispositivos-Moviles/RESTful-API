using System.ComponentModel.DataAnnotations;


namespace _1._API.Request
{
    public class ScoreRequest
    {
        [Required(ErrorMessage = "Type is required.")]
        public string Type { get; set; }

        [Required(ErrorMessage = "El campo Date es obligatorio")]
        
        public DateTime Date { get; set; }

        
        [RegularExpression(@"^\d+/\d+$", ErrorMessage = "Invalid score format. Use X/Y format.")]
        public string ScoreValue { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [EnumDataType(typeof(ScoreStatus), ErrorMessage = "Invalid status.")]
        public string Status { get; set; }

        [Required(ErrorMessage = "StudentId is required.")]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "TutorId is required.")]
        public int TutorId { get; set; }
    }

   
    public enum ScoreStatus
    {
        NOTSUBMITTED,
        SUBMITTED,
        GRADED
    }
   
}