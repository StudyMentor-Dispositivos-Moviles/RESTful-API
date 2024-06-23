using _3._Data.Model;

namespace _2._Domain;

public interface IScheduleDomain
{
    bool Create(Schedule schedule);
    bool Update(Schedule schedule, int id);
    bool Delete(int id);
}