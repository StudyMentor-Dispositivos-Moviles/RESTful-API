using _3._Data.Model;

namespace _2._Domain
{
    public interface IReviewDomain
    {
        bool Create(Review review);
        bool Update(Review review,int id);
        bool Delete(int id);
    }
}
