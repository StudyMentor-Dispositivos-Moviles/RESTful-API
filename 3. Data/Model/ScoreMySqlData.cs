using _3._Data.Context;
using _3._Data.Model;
using Microsoft.EntityFrameworkCore;

namespace _3._Data;

public class ScoreMySqlData: IScoreData
{
    private StudyMentorDB _studyMentorDb;

    public ScoreMySqlData(StudyMentorDB studyMentorDb)
    {
        _studyMentorDb = studyMentorDb;
    }
    public Score GetById(int id)
    {
        
        return _studyMentorDb.Scores.Where(t => t.Id == id ).First();
    }
    public async Task<List<Score>> GetByStudentId(int studentId)
    {
        return await _studyMentorDb.Scores.Where(t => t.StudentId==studentId).ToListAsync();
    }

    public async Task<List<Score>> GetAllAsync()
    {
        
        return await _studyMentorDb.Scores.ToListAsync();
    }

    

    public bool Update(Score score, int id)
    {
        try
        {
            var scoreToUpdate = _studyMentorDb.Scores.FirstOrDefault(s => s.Id == id);
        
            if (scoreToUpdate != null)
            {
                scoreToUpdate.Type = score.Type;
                scoreToUpdate.Date = score.Date;
                scoreToUpdate.Status = score.Status;
                scoreToUpdate.ScoreValue = score.ScoreValue;
                scoreToUpdate.StudentId = score.StudentId;
                scoreToUpdate.TutorId = score.TutorId;

                _studyMentorDb.Scores.Update(scoreToUpdate);
                _studyMentorDb.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception error)
        {
            return false;
        }
    }
    public bool Create(Score score)
    {
        try
        {
            _studyMentorDb.Scores.Add(score);
            _studyMentorDb.SaveChanges();
            return true;
        }
        catch (Exception error)
        {
            
            return false;
        }
    }
    
    public bool Delete(int id)
    {
        try
        {
            var scoreToUpdate = _studyMentorDb.Scores.Where(s => s.Id == id).First();
   
            _studyMentorDb.Scores.Remove(scoreToUpdate);
            _studyMentorDb.SaveChanges();
                
            return true;
        }
        catch (Exception error)
        {
            return false;
        }
    }
}