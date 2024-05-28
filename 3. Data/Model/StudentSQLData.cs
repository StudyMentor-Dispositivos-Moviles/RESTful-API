using _3._Data.Context;
using Microsoft.EntityFrameworkCore;

namespace _3._Data.Model;

public class StudentSQLData : IStudentData
{
        private StudyMentorDB _studyMentorDb;

        public StudentSQLData(StudyMentorDB studyMentorDb)
        {
            _studyMentorDb = studyMentorDb;
        }
        public Student GetById(int id)
        {
            // DB-TABLA-
            return _studyMentorDb.Students.Where(t => t.Id == id).First();
        }

        public async Task<List<Student>> GetAll()
        {
            return await _studyMentorDb.Students.ToListAsync();
        }

        public Student GetByEmail(string email)
        {
            return _studyMentorDb.Students.Where(t => t.Email==email).FirstOrDefault();
        }

        public bool Create(Student student)
        {
            try
            {
                _studyMentorDb.Students.Add(student);
                _studyMentorDb.SaveChanges();
                return true;
            }
            catch (Exception error)
            {
                //log
                return false;
            }
        }

        public bool Update(Student student, int id)
        {
            try
            {
                var studentToUpdate = _studyMentorDb.Students.Where(s => s.Id == id).FirstOrDefault();

                if (studentToUpdate != null)
                {
                    studentToUpdate.Name = student.Name;
                    studentToUpdate.Lastname = student.Lastname;
                    studentToUpdate.Email = student.Email;
                    studentToUpdate.Password = student.Password;
                    studentToUpdate.Birthday = student.Birthday;
                    studentToUpdate.Cellphone = student.Cellphone;
                    studentToUpdate.Genre = new Genres
                    {
                        NameGenre = student.Genre.NameGenre,
                        Code = student.Genre.Code
                    };
                    studentToUpdate.Image = student.Image;

                    _studyMentorDb.Students.Update(studentToUpdate);  
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

        public bool Delete(int id)
        {
            try
            {
                var studentToUpdate = _studyMentorDb.Students.FirstOrDefault(s => s.Id == id);

                if (studentToUpdate != null)
                {
                    _studyMentorDb.Students.Remove(studentToUpdate);
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
