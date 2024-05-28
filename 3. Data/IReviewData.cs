using _3._Data.Model;

namespace _3._Data;

public interface IReviewData
{
    Review GetById(int id);
    Task<List<Review>> GetAllAsync();
    Task<List<Review>> GetByRating(int rating);
    bool Create(Review review);
    bool Update(Review review, int id);
    bool Delete(int id);

}

