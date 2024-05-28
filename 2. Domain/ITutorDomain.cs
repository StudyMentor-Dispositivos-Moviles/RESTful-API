using _3._Data.Model;

namespace _2._Domain;

public interface ITutorDomain
{
    bool Create(Tutor tutor);
    bool Update(Tutor tutor, int id);
    bool Delete(int id);
}