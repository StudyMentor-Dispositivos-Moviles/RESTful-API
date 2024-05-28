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

public class StudentControllerTest
{
    [Fact]
        public async Task GetAll_ReturnsListOfStudents()
        {
            // Arrange
            var studentDataMock = Substitute.For<IStudentData>();
            var studentDomainMock = Substitute.For<IStudentDomain>();
            var mapperMock = Substitute.For<IMapper>();

            var controller = new StudentController(studentDataMock, studentDomainMock, mapperMock);

            var students = new List<Student> { new Student() }; 
            var studentResponses = new List<StudentResponse> { new StudentResponse() }; 

            studentDataMock.GetAll().Returns(students);
            mapperMock.Map<List<Student>, List<StudentResponse>>(students).Returns(studentResponses);

            // Act
            var result = await controller.GetAll();

            // Assert
            Assert.IsType<List<StudentResponse>>(result);
            Assert.Equal(studentResponses, result);
        }

        [Fact]
        public void GetById_ReturnsStudent()
        {
            // Arrange
            var studentId = 1;
            var studentDataMock = Substitute.For<IStudentData>();
            var studentDomainMock = Substitute.For<IStudentDomain>();
            var mapperMock = Substitute.For<IMapper>();

            var controller = new StudentController(studentDataMock, studentDomainMock, mapperMock);

            var student = new Student { Id = studentId };

            studentDataMock.GetById(studentId).Returns(student);

            // Act
            var result = controller.GetById(studentId);

            // Assert
            Assert.IsType<Student>(result);
            Assert.Equal(student, result);
        }

        [Fact]
        public void Post_ValidStudentRequest_ReturnsOkResult()
        {
            // Arrange
            var studentDataMock = Substitute.For<IStudentData>();
            var studentDomainMock = Substitute.For<IStudentDomain>();
            var mapperMock = Substitute.For<IMapper>();

            var controller = new StudentController(studentDataMock, studentDomainMock, mapperMock);

            var request = new StudentRequest(); 
            var student = new Student(); 

            mapperMock.Map<StudentRequest, Student>(request).Returns(student);

            // Act
            var result = controller.Post(request);

            // Assert
            Assert.True(((OkObjectResult)result).StatusCode == 200);
            studentDataMock.Received(1).Create(student);
        }

        [Fact]
        public void Post_InvalidStudentRequest_ReturnsBadRequest()
        {
            // Arrange
            var studentDataMock = Substitute.For<IStudentData>();
            var studentDomainMock = Substitute.For<IStudentDomain>();
            var mapperMock = Substitute.For<IMapper>();

            var controller = new StudentController(studentDataMock, studentDomainMock, mapperMock);

            controller.ModelState.AddModelError("Name", "The Name field is required.");

            // Act
            var result = controller.Post(new StudentRequest());

            // Assert
            Assert.IsType<BadRequestResult>(result);
            studentDataMock.DidNotReceive().Create(Arg.Any<Student>());
        }

        [Fact]
        public void Put_ValidStudent_ReturnsTrue()
        {
            // Arrange
            var studentId = 1;
            var studentDataMock = Substitute.For<IStudentData>();
            var studentDomainMock = Substitute.For<IStudentDomain>();
            var mapperMock = Substitute.For<IMapper>();

            var controller = new StudentController(studentDataMock, studentDomainMock, mapperMock);

            var request = new StudentRequest
            {
                Name = "Juan",
                Lastname = "Perez",
                Email = "juan@gmail.com",
                Password = "password123",
                Birthday = DateTime.Now.AddYears(-20),
                Cellphone = "123456789",
                Genre = new Genres()
                {
                    NameGenre = "Male",
                    Code = "M"
                },
                Image = "string"
            };

            studentDataMock.Update(Arg.Any<Student>(), studentId).Returns(true);
            // Act
            var result = controller.Put(studentId, request);

            // Assert
            Assert.True(result);
            studentDataMock.Received(1).Update(Arg.Any<Student>(), studentId);
        }

        [Fact]
        public void Delete_ReturnsFalse()
        {
            // Arrange
            var studentId = 1;
            var studentDataMock = Substitute.For<IStudentData>();
            var studentDomainMock = Substitute.For<IStudentDomain>();
            var mapperMock = Substitute.For<IMapper>();

            var controller = new StudentController(studentDataMock, studentDomainMock, mapperMock);

            // Act
            var result = controller.Delete(studentId);

            // Assert
            Assert.False(result);
            studentDomainMock.Received(1).Delete(studentId);
        }
}