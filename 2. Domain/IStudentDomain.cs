using _3._Data.Model;

namespace _2._Domain;

public interface IStudentDomain
{
    bool Create(Student student);
    bool Update(Student student, int id);
    bool Delete(int id);
}