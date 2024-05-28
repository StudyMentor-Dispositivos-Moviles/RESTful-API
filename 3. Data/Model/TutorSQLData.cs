using Microsoft.EntityFrameworkCore;
using _3._Data.Context;

namespace _3._Data.Model;

public class TutorSQLData : ITutorData
{
    private StudyMentorDB _studyMentorDb;

    public TutorSQLData(StudyMentorDB studyMentorDb)
    {
        _studyMentorDb = studyMentorDb;
    }
    
    public Tutor GetById(int id)
    {
        return _studyMentorDb.Tutors.Where(t => t.Id == id).First();
    }

    public async Task<List<Tutor>> GetAll()
    {
        return await _studyMentorDb.Tutors.ToListAsync();
    }

    public Tutor GetByEmail(string email)
    {
        return _studyMentorDb.Tutors.Where(t => t.Email==email).FirstOrDefault();
    }

    public bool Create(Tutor tutor)
    {
        try
        {
            _studyMentorDb.Tutors.Add(tutor);
            _studyMentorDb.SaveChanges();
            return true;
        }
        catch (Exception error)
        {
            //log
            return false;
        }    }

    public bool Update(Tutor tutor, int id)
    {
        try
        {
            var tutorToUpdate = _studyMentorDb.Tutors.Where(t => t.Id == id).FirstOrDefault();
        
            if (tutorToUpdate != null)
            {
                tutorToUpdate.Name = tutor.Name;
                tutorToUpdate.Lastname = tutor.Lastname;
                tutorToUpdate.Email = tutor.Email;
                tutorToUpdate.Password = tutor.Password;
                tutorToUpdate.Cellphone = tutor.Cellphone;
                tutorToUpdate.Specialty = tutor.Specialty;
                tutorToUpdate.Cost = tutor.Cost;
                tutorToUpdate.Image = tutor.Image;

                _studyMentorDb.Tutors.Update(tutorToUpdate);
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
            return false; // Ocurrió un error al intentar actualizar el tutor.
        }    }

    public bool Delete(int id)
    {
        try
        {
            var tutorToDelete = _studyMentorDb.Tutors.Where(s => s.Id == id).First();
   
            _studyMentorDb.Tutors.Remove(tutorToDelete);
            _studyMentorDb.SaveChanges();
                
            return true;
        }
        catch (Exception error)
        {
            return false;
        }    }
}