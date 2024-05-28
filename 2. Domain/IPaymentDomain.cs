using _3._Data.Model;

namespace _2._Domain;

public interface IPaymentDomain
{
    public bool Create(Payment payment);
}