namespace _1._API.Response
{
    public class ReviewResponse
    {
        public int Id { get; set; }
        public string TextMessagge { get; set; }
        public int Rating { get; set; }
        public int StudentId { get; set; }
        public int TutorId { get; set; }
        public DateTime Date { get; set; }
        public int Type { get; set; }
        
    }
}