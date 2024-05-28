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
public class ReviewController : ControllerBase
{
    private IReviewDomain _reviewDomain;
    private IReviewData _reviewData;
    private IMapper _mapper;
        
    public ReviewController(IReviewDomain reviewDomain, IReviewData reviewData, IMapper mapper)
    {
        _reviewDomain = reviewDomain;
        _reviewData = reviewData;
        _mapper = mapper;
    }
        
    // GET: api/Review
    [HttpGet]
    public IActionResult GetAllReviews()
    {
        var reviews = _reviewData.GetAllAsync().Result; 
        var reviewResponses = _mapper.Map<List<Review>, List<ReviewResponse>>(reviews);
        return Ok(reviewResponses);
    }

    // GET: api/Review/5
    [HttpGet("{id}", Name = "GetReviewById")]
    public IActionResult GetReviewById(int id)
    {
        var review = _reviewData.GetById(id); 
        if (review == null)
        {
            return NotFound(); 
        }

        var reviewResponse = _mapper.Map<Review, ReviewResponse>(review);
        return Ok(reviewResponse);
    }

    // POST: api/Review
    [HttpPost]
    public IActionResult CreateReview([FromBody] ReviewRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); 
        }

        var review = _mapper.Map<ReviewRequest, Review>(request);
        var success = _reviewDomain.Create(review);
        if (!success)
        {
            return StatusCode(StatusCodes.Status500InternalServerError); 
        }

        return StatusCode(StatusCodes.Status201Created); 
    }

    // PUT: api/Review/5
    [HttpPut("{id}")]
    public IActionResult UpdateReview(int id, [FromBody] ReviewRequest request)
    {
        var existingReview = _reviewData.GetById(id);
        if (existingReview == null)
        {
            return NotFound(); 
        }

        var updatedReview = _mapper.Map(request, existingReview);
        
        var success = _reviewDomain.Update(updatedReview,id);
        if (!success)
        {
            return StatusCode(StatusCodes.Status500InternalServerError); 
        }

        return NoContent();
    }

    // DELETE: api/Review/5
    [HttpDelete("{id}")]
    public IActionResult DeleteReview(int id)
    {
        var review = _reviewData.GetById(id);
        if (review == null)
        {
            return NotFound(); 
        }

        var success = _reviewDomain.Delete(id);
        if (!success)
        {
            return StatusCode(StatusCodes.Status500InternalServerError); 
        }

        return NoContent();
    }
    
}

}
