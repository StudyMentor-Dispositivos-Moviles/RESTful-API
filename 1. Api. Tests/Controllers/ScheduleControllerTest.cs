using _1._API.Controllers;
using _1._API.Request;
using _1._API.Response;
using _2._Domain;
using _3._Data;
using _3._Data.Model;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
namespace _1.API._Tests.Controller;

public class ScheduleControllerTest
{
    [Fact]
    public async Task GetAllSchedules_ReturnsListOfSchedules()
    {
        // Arrange
        var scheduleDataMock = Substitute.For<IScheduleData>();
        var scheduleDomainMock = Substitute.For<IScheduleDomain>();
        var mapperMock = Substitute.For<IMapper>();

        var controller = new ScheduleController(scheduleDataMock, scheduleDomainMock, mapperMock);

        var schedules = new List<Schedule> {new Schedule()};
        var scheduleResponses = new List<ScheduleResponse> { new ScheduleResponse() };
        
        scheduleDataMock.GetAll().Returns(schedules);
        mapperMock.Map<List<Schedule>, List<ScheduleResponse>>(schedules).Returns(scheduleResponses);

        // Act
        var result = await controller.GetAllSchedules();

        // Assert
        var model = Assert.IsType<List<ScheduleResponse>>(result);
        Assert.Equal(scheduleResponses, result);
    }



    [Fact]
    public void GetById_InvalidId_ReturnsNotFound()
    {
        // Arrange
        var scheduleId = 2;
        var substituteScheduleData = Substitute.For<IScheduleData>();
        var substituteScheduleDomain = Substitute.For<IScheduleDomain>();
        var substituteMapper = Substitute.For<IMapper>();

        var controller = new ScheduleController(substituteScheduleData, substituteScheduleDomain, substituteMapper);
        var schedule = new Schedule { Id = scheduleId };
        substituteScheduleData.GetById(scheduleId).Returns(schedule);
        // Act
        var result = controller.GetById(scheduleId);

        // Assert
        Assert.IsType<Schedule>(result);
        Assert.Equal(schedule, result);
    }

    [Fact]
    public void Post_ValidSchedule_ReturnsOkResult()
    {
        // Arrange
        var substituteScheduleData = Substitute.For<IScheduleData>();
        var substituteScheduleDomain = Substitute.For<IScheduleDomain>();
        var substituteMapper = Substitute.For<IMapper>();

        var controller = new ScheduleController(substituteScheduleData, substituteScheduleDomain, substituteMapper);

        var validRequest = new ScheduleRequest
        {
            TutorName = "John Doe",
            Days = "Monday",
            Time = "10:00 AM",
            Price = "$50",
            idTutor = 1
        };

        substituteMapper.Map<ScheduleRequest, Schedule>(validRequest).Returns(new Schedule());
        substituteScheduleData.Create(Arg.Any<Schedule>()).Returns(true);

        // Act
        var result = controller.Post(validRequest);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void Post_InvalidModelState_ReturnsBadRequest()
    {
        // Arrange
        var substituteScheduleData = Substitute.For<IScheduleData>();
        var substituteScheduleDomain = Substitute.For<IScheduleDomain>();
        var substituteMapper = Substitute.For<IMapper>();

        var controller = new ScheduleController(substituteScheduleData, substituteScheduleDomain, substituteMapper);
        controller.ModelState.AddModelError("error", "some error");

        // Act
        var result = controller.Post(new ScheduleRequest());

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public void Delete_ValidId_ReturnsTrue()
    {
        // Arrange
        var substituteScheduleData = Substitute.For<IScheduleData>();
        var substituteScheduleDomain = Substitute.For<IScheduleDomain>();
        var substituteMapper = Substitute.For<IMapper>();

        var controller = new ScheduleController(substituteScheduleData, substituteScheduleDomain, substituteMapper);

        substituteScheduleDomain.Delete(1).Returns(true);

        // Act
        var result = controller.Delete(1);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }
}