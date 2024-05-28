using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _1._API.Request;
using _1._API.Response;
using _2._Domain;
using _3._Data;
using _3._Data.Model;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _1._API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private IStudentData _studentData;
        private IStudentDomain _studentDomain;
        private IMapper _mapper;

        public StudentController(IStudentData studentData, IStudentDomain studentDomain, IMapper mapper)
        {
            _studentData = studentData;
            _studentDomain = studentDomain;
            _mapper = mapper;
        }
        
        // GET: api/Student
        [HttpGet]
        public async Task<List<StudentResponse>> GetAll()
        {
            var students = await _studentData.GetAll();
            var studentResponses = _mapper.Map<List<Student>, List<StudentResponse>>(students);
            return studentResponses;
        }
        
        
        // GET: api/Student/5
        [HttpGet("{id}", Name = "GetById")]
        public Student GetById(int id)
        {
            return _studentData.GetById(id);
        }
        


        // POST: api/Student
        [HttpPost]
        public IActionResult Post([FromBody] StudentRequest request)
        {
            
            if (ModelState.IsValid)
            {
                var student = _mapper.Map<StudentRequest, Student>(request);
                return Ok(_studentData.Create(student));
            }
            else
            {
                return BadRequest();
            }
        }

        // PUT: api/Student/5
        [HttpPut("{id}")]
        public bool Put(int id, [FromBody] StudentRequest request)
        {
            Student student = new Student()
            {
                Name = request.Name,
                Lastname = request.Lastname,
                Email = request.Email,
                Password = request.Password,
                Birthday = request.Birthday,
                Cellphone = request.Cellphone,
                Genre = new Genres
                {
                    NameGenre = request.Genre.NameGenre,
                    Code = request.Genre.Code
                },
                Image = request.Image,
            };
            
            return _studentData.Update(student, id);
            
        }

        // DELETE: api/Student/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return _studentDomain.Delete(id);
        }
    }
}
