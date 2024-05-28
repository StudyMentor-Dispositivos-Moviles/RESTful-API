using _3._Data.Model;

namespace _3._Data;

public interface ITutorData
{
    Tutor GetById(int id);
    Task<List<Tutor>> GetAll();
    Tutor GetByEmail(string email);
    bool Create(Tutor tutor);
    bool Update(Tutor tutor, int id);
    bool Delete(int id);
}