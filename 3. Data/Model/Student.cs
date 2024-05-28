namespace _3._Data.Model;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Genres Genre { get; set; }
    public DateTime Birthday { get; set; }
    public string Cellphone { get; set; }
    public string Image { get; set; }
}

public class Genres
{
    public string NameGenre { get; set; }
    public string Code { get; set; }
}