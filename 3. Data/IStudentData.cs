using _3._Data.Model;

namespace _3._Data;

public interface IStudentData
{
    Student GetById(int id);
    Task<List<Student>> GetAll();
    Student GetByEmail(string email);
    bool Create(Student student);
    bool Update(Student student, int id);
    bool Delete(int id);
}