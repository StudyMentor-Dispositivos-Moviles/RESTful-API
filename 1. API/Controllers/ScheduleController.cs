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
    public class ScheduleController : ControllerBase
    {
        private IScheduleData _scheduleData;
        private IScheduleDomain _scheduleDomain;
        private IMapper _mapper;

        public ScheduleController(IScheduleData scheduleData, IScheduleDomain scheduleDomain, IMapper mapper)
        {
            _scheduleData = scheduleData;
            _scheduleDomain = scheduleDomain;
            _mapper = mapper;
        }
        
        // GET: api/Schedule
        [HttpGet]
        public async Task<List<ScheduleResponse>> GetAllSchedules()
        {
            var schedules = await _scheduleData.GetAll();
            var scheduleResponses = _mapper.Map<List<Schedule>, List<ScheduleResponse>>(schedules);
            return scheduleResponses;
        }
        
        // GET: api/Schedule/ByTutor/{id}
        [HttpGet("ByTutor/{id}", Name = "GetSchedulesByTutorId")]
        public async Task<List<ScheduleResponse>> GetSchedulesByTutorId(int id)
        {
            var schedules = await _scheduleData.GetByTutorId(id);
            var scheduleResponses = _mapper.Map<List<Schedule>, List<ScheduleResponse>>(schedules);
            return scheduleResponses;
        }
        
        [HttpGet("ByStudent/{id}", Name = "GetSchedulesByStudentId")]
        public async Task<List<ScheduleResponse>> GetSchedulesByStudentId(int id)
        {
            var schedules = await _scheduleData.GetByStudentId(id);
            var scheduleResponses = _mapper.Map<List<Schedule>, List<ScheduleResponse>>(schedules);
            return scheduleResponses;
        }

        // GET: api/Schedule/id
        [HttpGet("{id}", Name = "GetScheduleById")]
        public Schedule GetById(int id)
        {
            return _scheduleData.GetById(id);
        }

        // POST: api/Schedule
        [HttpPost]
        public IActionResult Post([FromBody] ScheduleRequest request)
        {
            if (ModelState.IsValid)
            {
                var schedule = _mapper.Map<ScheduleRequest, Schedule>(request);
                return Ok(_scheduleData.Create(schedule));
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ScheduleRequest scheduleRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var scheduleModel = _mapper.Map<Schedule>(scheduleRequest);

            if (_scheduleData.Update(scheduleModel, id))
            {
                return Ok(new { message = "Schedule updated successfully" });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while updating the schedule" });
            }
        }


        // DELETE: api/Schedule/id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _scheduleDomain.Delete(id);

            if (result)
            {
                return Ok(new { Message = "Schedule deleted successfully." });
            }
            else
            {
                return NotFound(new { Message = "Schedule not found." });
            }
        }
    }
}
