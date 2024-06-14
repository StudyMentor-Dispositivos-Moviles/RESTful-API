using _3._Data;
using _3._Data.Model;

namespace _2._Domain;

public class AuthDomain : IAuthDomain
{
    private readonly IStudentData _studentData;
    private readonly ITutorData _tutorData;

    public AuthDomain(IStudentData studentData, ITutorData tutorData)
    {
        _studentData = studentData;
        _tutorData = tutorData;
    }

    public Student AuthenticateStudent(string email, string password)
    {
        var student = _studentData.GetByEmail(email);
        if (student != null && student.Password == password)
        {
            return student;
        }

        return null;
    }

    public Tutor AuthenticateTutor(string email, string password)
    {
        var tutor = _tutorData.GetByEmail(email);
        if (tutor != null && tutor.Password == password)
        {
            return tutor;
        }

        return null;
    }
}