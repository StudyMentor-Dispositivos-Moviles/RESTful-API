using _3._Data;
using _3._Data.Model;

namespace _2._Domain
{
    public class ScoreDomain : IScoreDomain
    {
        private IScoreData _scoreData;
        private IStudentData _studentData;
        private ITutorData _tutorData;

        public ScoreDomain(IScoreData scoreData, IStudentData studentData, ITutorData tutorData)
        {
            _scoreData = scoreData;
            _studentData = studentData;
            _tutorData = tutorData;
        }

        public bool Create(Score score)
        {
            
            if (IsScoreInvalid(score))
            {
                return false;
            }

            
            var existingStudent = _studentData.GetById(score.StudentId);
            if (existingStudent == null)
            {
                Console.WriteLine($"El ID del estudiante {score.StudentId} no existe.");
                return false;
            }

            
            var existingTutor = _tutorData.GetById(score.TutorId);
            if (existingTutor == null)
            {
                Console.WriteLine($"El ID del tutor {score.TutorId} no existe.");
                return false;
            }

            try
            {
                
                Console.WriteLine($"Debug: StudentId: {score.StudentId}, TutorId: {score.TutorId}");

                
                score.StudentId = existingStudent.Id;
                score.TutorId = existingTutor.Id;
                
                return _scoreData.Create(score);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear la puntuación: {ex.Message}");
                return false;
            }
        }
        private bool IsScoreInvalid(Score score)
        {
            return string.IsNullOrWhiteSpace(score.Type) ||
                   score.Date == DateTime.MinValue ||
                   string.IsNullOrWhiteSpace(score.Status) ||
                   string.IsNullOrWhiteSpace(score.ScoreValue);
        }
        public bool Update(Score score, int id)
        {
            var userScore = _scoreData.GetById(score.Id);
            if (userScore== null) {return _scoreData.Update(score, id);}
            else
            {
                return false;
            }
        }
        public bool Delete(int id)
        {

            return _scoreData.Delete(id);

        }
    }
   
}