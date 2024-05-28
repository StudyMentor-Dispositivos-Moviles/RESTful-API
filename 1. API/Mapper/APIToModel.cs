using _1._API.Request;
using _3._Data.Model;
using AutoMapper;

namespace _1._API.Mapper;

public class APIToModel: Profile
{
    public APIToModel()
    {
        CreateMap<PaymentRequest, Payment>();
        CreateMap<StudentRequest, Student>();
        CreateMap<TutorRequest, Tutor>();
        CreateMap<ReviewRequest, Review>();
        CreateMap<ScoreRequest, Score>();
        CreateMap<ScheduleRequest, Schedule>();
    }
}