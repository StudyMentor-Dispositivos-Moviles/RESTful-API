using _3._Data.Context;
using Microsoft.EntityFrameworkCore;
namespace _3._Data.Model;

public class ScheduleSQLData : IScheduleData
{
    private StudyMentorDB _studyMentorDb;

    public ScheduleSQLData(StudyMentorDB studyMentorDb)
    {
        _studyMentorDb = studyMentorDb;
    }
    
    public Schedule GetById(int id)
    {
        return _studyMentorDb.Schedules.Where(t => t.Id == id).First();
    }
    public async Task<List<Schedule>> GetByTutorId(int id)
    {
        return await _studyMentorDb.Schedules
            .Where(s => s.TutorId == id)
            .ToListAsync();
    }
    public async Task<List<Schedule>> GetAll()
    {
        return await _studyMentorDb.Schedules.ToListAsync();
    }
    public bool Update(Schedule schedule, int id)
    {
        try
        {
            var scheduleToUpdate = _studyMentorDb.Schedules.Where(s => s.Id == id).FirstOrDefault();

            if (scheduleToUpdate != null)
            {
                scheduleToUpdate.Day = schedule.Day;
                scheduleToUpdate.TutorHours = schedule.TutorHours;
                scheduleToUpdate.StartingHour = schedule.StartingHour;
                scheduleToUpdate.Price = schedule.Price;
                scheduleToUpdate.IsAvailable = schedule.IsAvailable;
                scheduleToUpdate.TutorId = schedule.TutorId;

                _studyMentorDb.Schedules.Update(scheduleToUpdate);
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
            // log
            return false;
        }
    }

    
    
    public bool Create(Schedule schedule)
    {
        try
        {
            _studyMentorDb.Schedules.Add(schedule);
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
            var scheduleToDelete = _studyMentorDb.Schedules.FirstOrDefault(s => s.Id == id);
            if (scheduleToDelete != null)
            {
                _studyMentorDb.Schedules.Remove(scheduleToDelete);
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

    
}