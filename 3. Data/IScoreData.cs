using _3._Data.Model;

namespace _3._Data;

public interface IScoreData
{ 
   
    Task<List<Score>> GetAllAsync();
    Score GetById(int id);
    Task<List<Score>> GetByStudentId(int studentId);
    bool Update(Score score, int id);
    bool Create(Score score);
    bool Delete(int id);
}