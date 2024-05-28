using _3._Data;
using _3._Data.Model;

namespace _2._Domain
{
    public class ReviewDomain : IReviewDomain
    {
        private IReviewData _reviewData;
        private IStudentData _studentData;
        private ITutorData _tutorData;

        public ReviewDomain(IReviewData reviewData, IStudentData studentData, ITutorData tutorData)
        {
            _reviewData = reviewData;
            _studentData = studentData;
            _tutorData = tutorData;
        }
        
        public bool Create(Review review)
        {
            if (review.Rating < 1 || review.Rating > 5)
            {
                return false;
            }

            return _reviewData.Create(review);
        }

        public bool Update(Review review)
        {
            throw new NotImplementedException(); 
        }

        public bool Update(Review review, int id)
        {
            var existingReview = _reviewData.GetById(id);
            if (existingReview == null)
            {
                return false; 
            }
            
            existingReview.TextMessagge = review.TextMessagge;
            existingReview.Rating = review.Rating;
            existingReview.StudentId = review.StudentId;

            return _reviewData.Update(existingReview, id); 
        }

        public bool Delete(int id)
        {
            return _reviewData.Delete(id);
        }
    }
}