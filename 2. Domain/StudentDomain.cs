using _3._Data;
using _3._Data.Model;
namespace _2._Domain;

public class StudentDomain : IStudentDomain
{
    private IStudentData _studentData;

    public StudentDomain(IStudentData studentData)
    {
        _studentData = studentData;
    }
    
    public bool Create(Student student)
    {
        if (string.IsNullOrWhiteSpace(student.Name) || string.IsNullOrWhiteSpace(student.Lastname) || string.IsNullOrWhiteSpace(student.Email) ||
            string.IsNullOrWhiteSpace(student.Password) || student.Birthday == DateTime.MinValue || string.IsNullOrWhiteSpace(student.Cellphone))
        {
            return false; 
        }
        
        var userStudent = _studentData.GetByEmail(student.Email);
        if (userStudent == null) {return _studentData.Create(student);}
        else
        {
            return false;
        }
    }

    public bool Update(Student student, int id)
    {
        var userStudent = _studentData.GetByEmail(student.Email);
        if (userStudent == null) {return _studentData.Update(student, id);}
        else
        {
            return false;
        }
    }

    public bool Delete(int id)
    {

        return _studentData.Delete(id);

    }
}