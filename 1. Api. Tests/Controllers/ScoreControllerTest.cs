using _1._API.Controllers;
using _1._API.Request;
using _1._API.Response;
using _2._Domain;
using Microsoft.AspNetCore.Mvc;
using _3._Data;
using _3._Data.Model;
using AutoMapper;
using NSubstitute;
namespace _1.API._Tests.Controller;

public class ScoreControllerTest
{
    [Fact]
        public async Task GetAll_ReturnsListOfScoreResponses()
        {
            // Arrange
            var scoreDataMock = Substitute.For<IScoreData>();
            var scoreDomainMock = Substitute.For<IScoreDomain>();
            var mapperMock = Substitute.For<IMapper>();

            var controller = new ScoreController(scoreDomainMock, scoreDataMock, mapperMock);

            var scores = new List<Score>
            {
                new Score { Id = 1, Type = "Exam", Date = DateTime.Now, ScoreValue = "90", Status = "Pass" },
                new Score { Id = 2, Type = "Quiz", Date = DateTime.Now, ScoreValue = "85", Status = "Pass" }
            };

            scoreDataMock.GetAllAsync().Returns(scores);
            mapperMock.Map<List<Score>, List<ScoreResponse>>(scores).Returns(new List<ScoreResponse>());

            // Act
            var result = await controller.GetAll();

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetScoresByStudent_ReturnsListOfScoreResponses()
        {
            // Arrange
            var scoreDataMock = Substitute.For<IScoreData>();
            var scoreDomainMock = Substitute.For<IScoreDomain>();
            var mapperMock = Substitute.For<IMapper>();

            var controller = new ScoreController(scoreDomainMock, scoreDataMock, mapperMock);

            var studentId = 1;
            var scores = new List<Score> 
            {
                new Score { Id = 1, Type = "Exam", Date = DateTime.Now, ScoreValue = "90", Status = "Pass", StudentId = studentId },
                new Score { Id = 2, Type = "Quiz", Date = DateTime.Now, ScoreValue = "85", Status = "Pass", StudentId = studentId },
            };

            scoreDataMock.GetByStudentId(studentId).Returns(scores);
            mapperMock.Map<List<Score>, List<ScoreResponse>>(scores).Returns(new List<ScoreResponse>());

            // Act
            var result = await controller.GetScoresByStudent(studentId);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void GetById_ReturnsScore()
        {
            // Arrange
            var scoreDataMock = Substitute.For<IScoreData>();
            var scoreDomainMock = Substitute.For<IScoreDomain>();
            var mapperMock = Substitute.For<IMapper>();

            var controller = new ScoreController(scoreDomainMock, scoreDataMock, mapperMock);

            var scoreId = 1;
            var score = new Score { Id = scoreId, Type = "Exam", Date = DateTime.Now, ScoreValue = "90", Status = "Pass" };

            scoreDataMock.GetById(scoreId).Returns(score);

            // Act
            var result = controller.Get(scoreId);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Post_ValidScoreRequest_ReturnsOkResult()
        {
            // Arrange
            var scoreDataMock = Substitute.For<IScoreData>();
            var scoreDomainMock = Substitute.For<IScoreDomain>();
            var mapperMock = Substitute.For<IMapper>();

            var controller = new ScoreController(scoreDomainMock, scoreDataMock, mapperMock);

            var request = new ScoreRequest { Type = "Exam", Date = DateTime.Now, ScoreValue = "90", Status = "Pass" };
            var score = new Score { Id = 1, Type = "Exam", Date = DateTime.Now, ScoreValue = "90", Status = "Pass" };

            mapperMock.Map<ScoreRequest, Score>(request).Returns(score);
            scoreDataMock.Create(score).Returns(true);

            // Act
            var result = controller.Post(request);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Put_ValidScore_ReturnsTrue()
        {
            // Arrange
            var scoreId = 1;
            var scoreDataMock = Substitute.For<IScoreData>();
            var scoreDomainMock = Substitute.For<IScoreDomain>();
            var mapperMock = Substitute.For<IMapper>();

            var controller = new ScoreController(scoreDomainMock, scoreDataMock, mapperMock);

            var request = new ScoreRequest
            {
                Type = "Exam",
                Date = DateTime.Now,
                ScoreValue = "90",
                Status = "Pass"
            };

            scoreDataMock.Update(Arg.Any<Score>(), scoreId).Returns(true);

            // Act
            var result = controller.Put(scoreId, request);

            // Assert
            Assert.True(result);
            scoreDataMock.Received(1).Update(Arg.Any<Score>(), scoreId);
        }

        [Fact]
        public void Delete_ReturnsTrue()
        {
            // Arrange
            var scoreDataMock = Substitute.For<IScoreData>();
            var scoreDomainMock = Substitute.For<IScoreDomain>();
            var mapperMock = Substitute.For<IMapper>();

            var controller = new ScoreController(scoreDomainMock, scoreDataMock, mapperMock);

            var scoreId = 1;
            scoreDomainMock.Delete(scoreId).Returns(true);

            // Act
            var result = controller.Delete(scoreId);

            // Assert
            Assert.True(result);
        }
}