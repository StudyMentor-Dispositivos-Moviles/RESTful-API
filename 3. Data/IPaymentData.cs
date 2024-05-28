using _3._Data.Model;

namespace _3._Data;

public interface IPaymentData
{
    Payment GetById(int id);
    Task<List<Payment>> GetAllAsync();
    Payment GetByCardNumber(string cardNumber);
    bool Create(Payment payment);
}