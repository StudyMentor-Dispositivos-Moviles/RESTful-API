using _2._Domain;
using _3._Data.Model;
using _3._Data;
using NSubstitute;
namespace _2._Domain._Tests;

public class StudentDomainTest
{
        [Theory]
        [InlineData("Juan", "Perez", "juan.perez@gmail.com", "password123", "1990-01-01", "123456789")]
        public void Create_ValidStudent_ReturnsTrue(
            string name, string lastName, string email, string password, string birthday, string cellphone)
        {
            // Arrange
            var student = new Student
            {
                Name = name,
                Lastname = lastName,
                Email = email,
                Password = password,
                Birthday = DateTime.Parse(birthday),
                Cellphone = cellphone
            };

            var studentDataMock = Substitute.For<IStudentData>();
            studentDataMock.GetByEmail(student.Email).Returns((Student)null);
            studentDataMock.Create(student).Returns(true);

            var studentDomain = new StudentDomain(studentDataMock);

            // Act
            var result = studentDomain.Create(student);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData("", "Perez", "juan.perez@gmail.com", "password123", "1990-01-01", "123456789")]
        [InlineData("Juan", "", "juan.perez@gmail.com", "password123", "1990-01-01", "123456789")]
        public void Create_InvalidStudent_ReturnsFalse(
            string name, string lastName, string email, string password, string birthday, string cellphone)
        {
            // Arrange
            var student = new Student
            {
                Name = name,
                Lastname = lastName,
                Email = email,
                Password = password,
                Birthday = DateTime.Parse(birthday),
                Cellphone = cellphone
            };

            var studentDataMock = Substitute.For<IStudentData>();
            var studentDomain = new StudentDomain(studentDataMock);

            // Act
            var result = studentDomain.Create(student);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData("juan.perez@gmail.com")]
        public void Create_ExistingEmail_ReturnsFalse(string existingEmail)
        {
            // Arrange
            var student = new Student
            {
                Email = existingEmail
            };

            var studentDataMock = Substitute.For<IStudentData>();
            studentDataMock.GetByEmail(student.Email).Returns(new Student()); // Simulate an existing student
            var studentDomain = new StudentDomain(studentDataMock);

            // Act
            var result = studentDomain.Create(student);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Update_ValidStudent_ReturnsTrue(int studentId)
        {
            // Arrange
            var student = new Student
            {
                Name = "Juan",
                Lastname = "Perez",
                Email = "juan.perez@gmail.com",
                Password = "password",
                Birthday = DateTime.Parse("1990/01/01"),
                Cellphone = "123456789"
            };

            var studentDataMock = Substitute.For<IStudentData>();
            studentDataMock.GetByEmail(student.Email).Returns((Student)null);
            studentDataMock.Update(student, studentId).Returns(true);

            var studentDomain = new StudentDomain(studentDataMock);

            // Act
            var result = studentDomain.Update(student, studentId);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData("juan.perez@example.com")]
        public void Update_ExistingEmail_ReturnsFalse(string existingEmail)
        {
            // Arrange
            var student = new Student
            {
                Email = existingEmail
            };

            var studentDataMock = Substitute.For<IStudentData>();
            studentDataMock.GetByEmail(student.Email).Returns(new Student()); 
            var studentDomain = new StudentDomain(studentDataMock);

            // Act
            var result = studentDomain.Update(student, 1);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Delete_ValidId_ReturnsTrue(int studentId)
        {
            // Arrange
            var studentDataMock = Substitute.For<IStudentData>();
            studentDataMock.Delete(studentId).Returns(true);

            var studentDomain = new StudentDomain(studentDataMock);

            // Act
            var result = studentDomain.Delete(studentId);

            // Assert
            Assert.True(result);
        }
}