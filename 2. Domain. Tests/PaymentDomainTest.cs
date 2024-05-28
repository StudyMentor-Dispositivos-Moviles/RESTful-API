using _2._Domain;
using _3._Data.Model;
using _3._Data;
using NSubstitute;
namespace _2._Domain._Tests;

public class PaymentDomainTest
{
    [Theory]
    [InlineData("1234567890123456", "Juan Perez", "12/25", 123)]
    public void Create_NewCreditCard_ReturnsTrue(
        string cardNumber, string cardHolderName, string expirationDate, int cvv)
    {
        // Arrange
        var payment = new Payment
        {
            CardNumber = cardNumber,
            Owner = cardHolderName,
            ExpirationDate = expirationDate,
            Cvv = cvv
        };

        var paymentDataMock = Substitute.For<IPaymentData>();
        paymentDataMock.GetByCardNumber(payment.CardNumber).Returns((Payment)null);
        paymentDataMock.Create(payment).Returns(true);

        var paymentDomain = new PaymentDomain(paymentDataMock);

        // Act
        var result = paymentDomain.Create(payment);

        // Assert
        Assert.True(result);
    }

    [Theory]
    [InlineData("1234567890123456", "Juan Perez", "12/25", 123)]
    public void Create_ExistingCreditCard_ReturnsFalse(
        string cardNumber, string cardHolderName, string expirationDate, int cvv)
    {
        // Arrange
        var payment = new Payment
        {
            CardNumber = cardNumber,
            Owner = cardHolderName,
            ExpirationDate = expirationDate,
            Cvv = cvv
        };

        var paymentDataMock = Substitute.For<IPaymentData>();
        paymentDataMock.GetByCardNumber(payment.CardNumber).Returns(payment);

        var paymentDomain = new PaymentDomain(paymentDataMock);

        // Act
        var result = paymentDomain.Create(payment);

        // Assert
        Assert.False(result);
    }
}