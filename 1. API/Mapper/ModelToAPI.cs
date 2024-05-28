using _1._API.Request;
using _1._API.Response;
using _3._Data.Model;
using AutoMapper;

namespace _1._API.Mapper;

public class ModelToAPI: Profile
{
    public ModelToAPI()
    {
        CreateMap<Payment, PaymentRequest>();
        CreateMap<Payment, PaymentResponse>();
        CreateMap<Student, StudentRequest>();
        CreateMap<Student, StudentResponse>();
        CreateMap<Tutor, TutorRequest>();
        CreateMap<Tutor, TutorResponse>();
        CreateMap<Review, ReviewResponse>();
        CreateMap<Review, ReviewRequest>();
        CreateMap<Score, ScoreRequest>();
        CreateMap<Score, ScoreResponse>();
        CreateMap<Schedule, ScheduleRequest>();
        CreateMap<Schedule, ScheduleResponse>();
    }
}