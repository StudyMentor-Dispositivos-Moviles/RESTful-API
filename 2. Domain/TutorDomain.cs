using _3._Data;
using _3._Data.Model;

namespace _2._Domain;

public class TutorDomain : ITutorDomain
{
    private ITutorData _tutorData;

    public TutorDomain(ITutorData tutorData)
    {
        _tutorData = tutorData;
    }
    
    public bool Create(Tutor tutor)
    {
        if (string.IsNullOrWhiteSpace(tutor.Name) || string.IsNullOrWhiteSpace(tutor.Lastname) || string.IsNullOrWhiteSpace(tutor.Email) ||
            string.IsNullOrWhiteSpace(tutor.Password) || string.IsNullOrWhiteSpace(tutor.Specialty) || string.IsNullOrWhiteSpace(tutor.Cellphone) ||
            tutor.Cost <= 0)
        {
            return false; 
        }
        
        var userTutor = _tutorData.GetByEmail(tutor.Email);
        if (userTutor == null) {return _tutorData.Create(tutor);}
        else
        {
            return false;
        }
    }

    public bool Update(Tutor tutor, int id)
    {
        var userTutor = _tutorData.GetByEmail(tutor.Email);
        if (userTutor == null) {return _tutorData.Update(tutor, id);}
        else
        {
            return false;
        }    }

    public bool Delete(int id)
    {
        return _tutorData.Delete(id);
    }
}