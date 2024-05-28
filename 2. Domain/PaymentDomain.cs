using _3._Data;
using _3._Data.Model;

namespace _2._Domain;

public class PaymentDomain: IPaymentDomain
{
    private IPaymentData _paymentData;

    public PaymentDomain(IPaymentData paymentData)
    {
        _paymentData = paymentData;
    }
    public bool Create(Payment payment)
    {
        var creditCard = _paymentData.GetByCardNumber(payment.CardNumber);
        if (creditCard == null) return _paymentData.Create(payment);
        return false;
    }
}