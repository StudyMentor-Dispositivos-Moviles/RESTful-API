using _2._Domain;
using _3._Data.Model;
using _3._Data;
using NSubstitute;
namespace _2._Domain._Tests;

public class TutorDomainTest
{
    [Theory]
        [InlineData("Carlos", "Perez", "carlos@gmail.com", "contraseña", "Math", "123456789", 30.0)]
        public void Create_ValidTutor_ReturnsTrue(
            string name, string lastName, string email, string password, string specialty, string cellphone, decimal cost)
        {
            var tutor = new Tutor
            {
                Name = name,
                Lastname = lastName,
                Email = email,
                Password = password,
                Specialty = specialty,
                Cellphone = cellphone,
                Cost = cost
            };

            var tutorDataMock = Substitute.For<ITutorData>();
            tutorDataMock.GetByEmail(tutor.Email).Returns((Tutor)null);
            tutorDataMock.Create(tutor).Returns(true);

            var tutorDomain = new TutorDomain(tutorDataMock);

            var result = tutorDomain.Create(tutor);

            Assert.True(result);
        }

        [Theory]
        [InlineData("", "Perez", "carlos@gmail.com", "contraseña", "Math", "123456789", 30.0)]
        [InlineData("Carlos", "", "carlos@gmail.com", "contraseña", "Math", "123456789", 30.0)]
        public void Create_InvalidTutor_ReturnsFalse(
            string name, string lastName, string email, string password, string specialty, string cellphone, decimal cost)
        {
            var tutor = new Tutor
            {
                Name = name,
                Lastname = lastName,
                Email = email,
                Password = password,
                Specialty = specialty,
                Cellphone = cellphone,
                Cost = cost
            };

            var tutorDataMock = Substitute.For<ITutorData>();
            var tutorDomain = new TutorDomain(tutorDataMock);

            // Act
            var result = tutorDomain.Create(tutor);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData("carlos@gmail.com")]
        public void Create_ExistingEmail_ReturnsFalse(string existingEmail)
        {
            var tutor = new Tutor
            {
                Email = existingEmail
            };

            var tutorDataMock = Substitute.For<ITutorData>();
            tutorDataMock.GetByEmail(tutor.Email).Returns(new Tutor());
            var tutorDomain = new TutorDomain(tutorDataMock);

            // Act
            var result = tutorDomain.Create(tutor);

            // Assert
            Assert.False(result);
        }
}