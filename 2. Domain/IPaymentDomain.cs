using _3._Data.Model;

namespace _2._Domain;

public interface IPaymentDomain
{
    public bool Create(Payment payment);
    bool Update(Payment payment, int id);
    bool Delete(int id);
}