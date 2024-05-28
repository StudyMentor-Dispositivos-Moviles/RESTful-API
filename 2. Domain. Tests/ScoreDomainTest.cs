using _3._Data.Model;
using _3._Data;
using NSubstitute;
namespace _2._Domain._Tests;

public class ScoreDomainTest
{
        [Theory]
        [InlineData(1, "Examen", "2023-11-19", "Aprobado", "18", 1, 2)]
        public void Create_ValidScore_ReturnsFalse(
            int scoreId, string type, string date, string status, string scoreValue, int studentId, int tutorId)
        {
            // Arrange
            var score = new Score
            {
                Id = scoreId,
                Type = type,
                Date = DateTime.Parse(date),
                Status = status,
                ScoreValue = scoreValue,
                StudentId = studentId,
                TutorId = tutorId
            };

            var scoreDataMock = Substitute.For<IScoreData>();
            scoreDataMock.Create(Arg.Any<Score>()).Returns(true);
            

            var studentDataMock = Substitute.For<IStudentData>();
            var tutorDataMock = Substitute.For<ITutorData>();

            var scoreDomain = new ScoreDomain(scoreDataMock, studentDataMock, tutorDataMock);

            // Act
            var result = scoreDomain.Create(score);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData(1, "Examen", "2023-11-19", "Aprobado", "18", 1, 2)]
        public void Create_InvalidScore_ReturnsFalse(
            int scoreId, string type, string date, string status, string scoreValue, int studentId, int tutorId)
        {
            // Arrange
            var score = new Score
            {
                Id = scoreId,
                Type = type,
                Date = DateTime.Parse(date),
                Status = status,
                ScoreValue = scoreValue,
                StudentId = studentId,
                TutorId = tutorId
            };

            var scoreDataMock = Substitute.For<IScoreData>();
            var studentDataMock = Substitute.For<IStudentData>();
            var tutorDataMock = Substitute.For<ITutorData>();

            var scoreDomain = new ScoreDomain(scoreDataMock, studentDataMock, tutorDataMock);

            // Act
            var result = scoreDomain.Create(score);

            // Assert
            Assert.False(result);
            scoreDataMock.DidNotReceive().Create(Arg.Any<Score>());
        }

        [Theory]
        [InlineData(1, "UpdatedType", "2023-11-20", "Aprobado", "20", 1, 2)]
        public void Update_NonExistingScore_ReturnsFalse(
            int scoreId, string type, string date, string status, string scoreValue, int studentId, int tutorId)
        {
            // Arrange
            var updatedScore = new Score
            {
                Id = scoreId,
                Type = type,
                Date = DateTime.Parse(date),
                Status = status,
                ScoreValue = scoreValue,
                StudentId = studentId,
                TutorId = tutorId
            };

            var scoreDataMock = Substitute.For<IScoreData>();
            scoreDataMock.GetById(scoreId).Returns((Score)null);

            var studentDataMock = Substitute.For<IStudentData>();
            var tutorDataMock = Substitute.For<ITutorData>();

            var scoreDomain = new ScoreDomain(scoreDataMock, studentDataMock, tutorDataMock);

            // Act
            var result = scoreDomain.Update(updatedScore, scoreId);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData(1)]
        public void Delete_ValidId_ReturnsTrue(int scoreId)
        {
            // Arrange
            var scoreDataMock = Substitute.For<IScoreData>();
            scoreDataMock.Delete(scoreId).Returns(true);

            var studentDataMock = Substitute.For<IStudentData>();
            var tutorDataMock = Substitute.For<ITutorData>();

            var scoreDomain = new ScoreDomain(scoreDataMock, studentDataMock, tutorDataMock);

            // Act
            var result = scoreDomain.Delete(scoreId);

            // Assert
            Assert.True(result);
            scoreDataMock.Received(1).Delete(scoreId);
        }
}