using _3._Data.Context;
using _3._Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _3._Data
{
    public class ReviewMySqlData : IReviewData
    {
        private StudyMentorDB _studyMentorDb;

        public ReviewMySqlData(StudyMentorDB studyMentorDb)
        {
            _studyMentorDb = studyMentorDb;
        }

        public Review GetById(int id)
        {
            return _studyMentorDb.Reviews.FirstOrDefault(r => r.Id == id && r.IsActive);
        }

        public async Task<List<Review>> GetAllAsync()
        {
            return await _studyMentorDb.Reviews.ToListAsync();
        }

        public async Task<List<Review>> GetByRating(int Rating)
        {
            return await _studyMentorDb.Reviews.Where(t => t.Rating == Rating).ToListAsync();
        }

        public bool Create(Review review)
        {
            try
            {
                _studyMentorDb.Reviews.Add(review);
                _studyMentorDb.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                
                return false;
            }
        }

        public bool Update(Review review, int id)
        {
            var existingReview = GetById(id);
            if (existingReview == null)
            {
                return false; 
            }

            
            existingReview.TextMessagge = review.TextMessagge;
            existingReview.Rating = review.Rating;
            existingReview.StudentId = review.StudentId;

            try
            {
                _studyMentorDb.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                
                return false;
            }
        }

        public bool Delete(int id)
        {
            var existingReview = GetById(id);
            if (existingReview == null)
            {
                return false; 
            }

            try
            {
                existingReview.IsActive = false; 
                _studyMentorDb.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                
                return false;
            }
        }
    }
}
