using _3._Data.Model;

namespace _2._Domain;

public interface IScoreDomain
{
    public bool Create(Score score);
    bool Update(Score score, int id);
    bool Delete(int id);
}