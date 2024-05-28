using _1._API.Request;
using _1._API.Response;
using _2._Domain;
using _3._Data;
using _3._Data.Model;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _1._API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TutorController : ControllerBase
{
    
    private ITutorData _tutorData;
    private ITutorDomain _tutorDomain;
    private IMapper _mapper;

    public TutorController(ITutorData tutorData, ITutorDomain tutorDomain, IMapper mapper)
    {
        _tutorData = tutorData;
        _tutorDomain = tutorDomain;
        _mapper = mapper;
    }
    
            // GET: api/Tutor
            [HttpGet]
            public async Task<List<TutorResponse>> GetAllTutors()
            {
                var tutors = await _tutorData.GetAll();
                var tutorResponses = _mapper.Map<List<Tutor>, List<TutorResponse>>(tutors);
                return tutorResponses;
            }
            
            
            // GET: api/Tutor/id
            [HttpGet("{id}", Name = "GetTutorById")]
            public Tutor GetById(int id)
            {
                return _tutorData.GetById(id);
            }
            
    
    
            // POST: api/Tutor
            [HttpPost]
            public IActionResult Post([FromBody] TutorRequest request)
            {
                
                if (ModelState.IsValid)
                {
                    var tutor = _mapper.Map<TutorRequest, Tutor>(request);
                    return Ok(_tutorData.Create(tutor));
                }
                else
                {
                    return BadRequest();
                }
            }
    
            // PUT: api/Tutor/id
            [HttpPut("{id}")]
            public bool Put(int id, [FromBody] TutorRequest request)
            {
                Tutor tutor = new Tutor()
                {
                    Name = request.Name,
                    Lastname = request.Lastname,
                    Email = request.Email,
                    Password = request.Password,
                    Specialty = request.Specialty,
                    Cost = request.Cost,
                    Cellphone = request.Cellphone,
                    Image = request.Image,
                };
                
                return _tutorData.Update(tutor, id);
                
            }
    
            // DELETE: api/Tutor/id
            [HttpDelete("{id}")]
            public bool Delete(int id)
            {
                return _tutorDomain.Delete(id);
            }
}
