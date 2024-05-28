using _3._Data;
using _3._Data.Model;
using NSubstitute;
namespace _2._Domain._Tests;

public class ScheduleDomainTest
{
   [Fact]
    public void Create_ValidSchedule_ReturnsTrue()
    {
        // Arrange
        var tutorName = "John Doe";
        var days = "Monday";
        var time = "10:00 AM";
        var price = "$50";
        var idTutor = 1;

        var schedule = new Schedule
        {
            TutorName = tutorName,
            Days = days,
            Time = time,
            Price = price,
            idTutor = idTutor
        };

        var scheduleDataMock = Substitute.For<IScheduleData>();
        scheduleDataMock.GetByTutor(Arg.Any<int>()).Returns((Schedule)null);
        scheduleDataMock.Create(schedule).Returns(true);

        var scheduleDomain = new ScheduleDomain(scheduleDataMock);

        // Act
        var result = scheduleDomain.Create(schedule);

        // Assert
        Assert.True(result);
    }

    [Theory]
    [InlineData("", "Monday", "10:00 AM", "$50", 1)]
    [InlineData("John Doe", "", "10:00 AM", "$50", 1)]
    // Add more invalid test cases as needed
    public void Create_InvalidSchedule_ReturnsFalse(
        string tutorName, string days, string time, string price, int idTutor)
    {
        // Arrange
        var schedule = new Schedule
        {
            TutorName = tutorName,
            Days = days,
            Time = time,
            Price = price,
            idTutor = idTutor
        };

        var scheduleDataMock = Substitute.For<IScheduleData>();
        var scheduleDomain = new ScheduleDomain(scheduleDataMock);

        // Act
        var result = scheduleDomain.Create(schedule);

        // Assert
        Assert.False(result);
    }

    [Theory]
    [InlineData(1)]
    public void Create_ExistingSchedule_ReturnsFalse(int idTutor)
    {
        // Arrange
        var schedule = new Schedule
        {
            idTutor = idTutor
        };

        var scheduleDataMock = Substitute.For<IScheduleData>();
        scheduleDataMock.GetByTutor(Arg.Any<int>()).Returns(new Schedule());
        var scheduleDomain = new ScheduleDomain(scheduleDataMock);

        // Act
        var result = scheduleDomain.Create(schedule);

        // Assert
        Assert.False(result);
    }

    [Theory]
    [InlineData(1)]
    public void Delete_ValidId_ReturnsTrue(int scheduleId)
    {
        // Arrange
        var scheduleDataMock = Substitute.For<IScheduleData>();
        scheduleDataMock.Delete(scheduleId).Returns(true);

        var scheduleDomain = new ScheduleDomain(scheduleDataMock);

        // Act
        var result = scheduleDomain.Delete(scheduleId);

        // Assert
        Assert.True(result);
    }

    [Theory]
    [InlineData(-1)] // Provide an invalid schedule ID
    public void Delete_InvalidId_ReturnsFalse(int invalidScheduleId)
    {
        // Arrange
        var scheduleDataMock = Substitute.For<IScheduleData>();
        scheduleDataMock.Delete(invalidScheduleId).Returns(false);

        var scheduleDomain = new ScheduleDomain(scheduleDataMock);

        // Act
        var result = scheduleDomain.Delete(invalidScheduleId);

        // Assert
        Assert.False(result);
    }
}
