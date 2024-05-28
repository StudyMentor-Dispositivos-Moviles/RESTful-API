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
    public class ScoreController : ControllerBase
    {
        private IScoreDomain _scoreDomain;
        private IScoreData _scoreData;
        private IMapper _mapper;
        
        public ScoreController(IScoreDomain scoreDomain, IScoreData scoreData, IMapper mapper)
        {
            _scoreDomain = scoreDomain;
            _scoreData = scoreData;
            _mapper = mapper;
          
        }

        // GET: api/Score
        [HttpGet]
        public async Task<List<ScoreResponse>> GetAll()
        {
            var scores = await _scoreData.GetAllAsync();
            var scoreResponses = _mapper.Map<List<Score>, List<ScoreResponse>>(scores);
            return scoreResponses;
        }
     
        // GET: api/Score/student/1
        [HttpGet("scores/student/{studentId}")]
        public async Task<List<ScoreResponse>> GetScoresByStudent(int studentId)
        {
            var scores = await _scoreData.GetByStudentId(studentId);
            var scoreResponses = _mapper.Map<List<Score>, List<ScoreResponse>>(scores);
            return scoreResponses;
        }
        // GET: api/Score/5
        [HttpGet("{id}", Name = "GetScore")]
        public Score Get(int id)
        {
            return _scoreData.GetById(id);
        }
        // POST: api/Score
        [HttpPost]
        public IActionResult Post([FromBody] ScoreRequest request)
        {
            
            if (ModelState.IsValid)
            {
                var score = _mapper.Map<ScoreRequest, Score>(request);
                
                return Ok(_scoreData.Create(score));
            }
            else
            {
                return BadRequest();
            }
        }
       
     
      
        // PUT: api/Score/5
        [HttpPut("{id}")]
        public bool Put(int id, [FromBody] ScoreRequest request)
        {
            Score score = new Score()
            {
                Type = request.Type,
                Date = request.Date,
                ScoreValue = request.ScoreValue,
                Status = request.Status,
                
               
            };
            
            return _scoreData.Update(score, id);
            
        }

        // DELETE: api/Score/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return _scoreDomain.Delete(id);
        }
       
        
        
    }
}
