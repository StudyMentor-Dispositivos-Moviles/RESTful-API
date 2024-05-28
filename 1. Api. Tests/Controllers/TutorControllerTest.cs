using _1._API.Controllers;
using _1._API.Request;
using _1._API.Response;
using _2._Domain;
using _3._Data;
using _3._Data.Model;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using NSubstitute;
namespace _1.API._Tests.Controller;

public class TutorControllerTest
{
    [Fact]
        public async Task GetAllTutors_ReturnsTutorResponses()
        {
            // Arrange
            var tutorDataMock = Substitute.For<ITutorData>();
            var tutorDomainMock = Substitute.For<ITutorDomain>();
            var mapperMock = Substitute.For<IMapper>();

            var controller = new TutorController(tutorDataMock, tutorDomainMock, mapperMock);

            var tutorList = new List<Tutor> {};
            var tutorResponseList = new List<TutorResponse> {};

            tutorDataMock.GetAll().Returns(tutorList);
            mapperMock.Map<List<Tutor>, List<TutorResponse>>(tutorList).Returns(tutorResponseList);

            // Act
            var result = await controller.GetAllTutors();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(tutorResponseList, result);
        }

        [Fact]
        public void GetById_ReturnsTutor()
        {
            // Arrange
            var tutorDataMock = Substitute.For<ITutorData>();
            var tutorDomainMock = Substitute.For<ITutorDomain>();
            var mapperMock = Substitute.For<IMapper>();

            var controller = new TutorController(tutorDataMock, tutorDomainMock, mapperMock);

            var tutorId = 1;
            var tutor = new Tutor {};

            tutorDataMock.GetById(tutorId).Returns(tutor);

            // Act
            var result = controller.GetById(tutorId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(tutor, result);
        }

        [Fact]
        public void Post_ValidTutorRequest_ReturnsOkResult()
        {
            // Arrange
            var tutorDataMock = Substitute.For<ITutorData>();
            var tutorDomainMock = Substitute.For<ITutorDomain>();
            var mapperMock = Substitute.For<IMapper>();

            var controller = new TutorController(tutorDataMock, tutorDomainMock, mapperMock);

            var request = new TutorRequest {};
            var tutor = new Tutor {};

            mapperMock.Map<TutorRequest, Tutor>(request).Returns(tutor);
            tutorDataMock.Create(tutor).Returns(true);

            // Act
            var result = controller.Post(request);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Put_ValidTutor_ReturnsTrue()
        {
            // Arrange
            var tutorDataMock = Substitute.For<ITutorData>();
            var tutorDomainMock = Substitute.For<ITutorDomain>();
            var mapperMock = Substitute.For<IMapper>();

            var controller = new TutorController(tutorDataMock, tutorDomainMock, mapperMock);

            var tutorId = 1;
    
            var request = new TutorRequest
            {
                Name = "John",
                Lastname = "Doe",
                Email = "john.doe@example.com",
                Password = "password123",
                Specialty = "Math",
                Cost = (decimal)30.0,
                Cellphone = "123456789",
                Image = "profile.jpg"
            };

            tutorDataMock.Update(Arg.Any<Tutor>(), tutorId).Returns(true);

            // Act
            var result = controller.Put(tutorId, request);

            // Assert
            Assert.True(result);
            tutorDataMock.Received(1).Update(Arg.Any<Tutor>(), tutorId);
        }

        [Fact]
        public void Delete_ReturnsTrue()
        {
            // Arrange
            var tutorDataMock = Substitute.For<ITutorData>();
            var tutorDomainMock = Substitute.For<ITutorDomain>();
            var mapperMock = Substitute.For<IMapper>();

            var controller = new TutorController(tutorDataMock, tutorDomainMock, mapperMock);

            var tutorId = 1;
            tutorDomainMock.Delete(tutorId).Returns(true);

            // Act
            var result = controller.Delete(tutorId);

            // Assert
            Assert.True(result);
        }
}