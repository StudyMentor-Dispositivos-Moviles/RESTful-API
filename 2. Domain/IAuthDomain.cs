using _3._Data.Model;

namespace _2._Domain;

public interface IAuthDomain
{
    Student AuthenticateStudent(string email, string password);
    Tutor AuthenticateTutor(string email, string password);
}