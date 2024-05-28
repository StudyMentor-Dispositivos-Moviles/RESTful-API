namespace _3._Data.Model
{
    public class Review:ModelBase
    {
        public int Id { get; set; }
        public string TextMessagge { get; set; }
        public int Rating { get; set; }
        public int StudentId { get; set; }
        public bool IsActive { get; set; }
        public int TutorId { get; set; }
        public DateTime Date { get; set; }
    }
}