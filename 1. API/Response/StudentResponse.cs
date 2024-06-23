namespace _1._API.Response;

public class StudentResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Lastname { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public DateTime Birthday { get; set; }
    public string Cellphone { get; set; }
    public string Image { get; set; }
    public _3._Data.Model.Genres Genre { get; set; }  
    
}
