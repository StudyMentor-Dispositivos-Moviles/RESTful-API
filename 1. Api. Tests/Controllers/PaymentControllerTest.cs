using NSubstitute;
using AutoMapper;
using _1._API.Controllers;
using _1._API.Request;
using _1._API.Response;
using _2._Domain;
using _3._Data;
using _3._Data.Model;
using Microsoft.AspNetCore.Mvc;

namespace _1.API._Tests.Controller;

public class PaymentControllerTest
{
    [Fact]
        public async Task GetAsync_ReturnsPaymentResponses()
        {
            // Arrange
            var paymentDataMock = Substitute.For<IPaymentData>();
            var paymentDomainMock = Substitute.For<IPaymentDomain>();
            var mapperMock = Substitute.For<IMapper>();

            var controller = new PaymentController(paymentDomainMock, paymentDataMock, mapperMock);

            var payments = new List<Payment> { /* Agrega pagos de ejemplo aquí */ };
            var paymentResponses = new List<PaymentResponse> { /* Agrega respuestas de pago de ejemplo aquí */ };

            paymentDataMock.GetAllAsync().Returns(Task.FromResult(payments));
            mapperMock.Map<List<Payment>, List<PaymentResponse>>(payments).Returns(paymentResponses);

            // Act
            var result = await controller.GetAsync();

            // Assert
            Assert.Equal(paymentResponses, result);
        }

        [Fact]
        public void GetById_ReturnsPayment()
        {
            // Arrange
            var paymentDataMock = Substitute.For<IPaymentData>();
            var paymentDomainMock = Substitute.For<IPaymentDomain>();
            var mapperMock = Substitute.For<IMapper>();

            var controller = new PaymentController(paymentDomainMock, paymentDataMock, mapperMock);

            var paymentId = 1;
            var payment = new Payment { /* Agrega un pago de ejemplo aquí */ };

            paymentDataMock.GetById(paymentId).Returns(payment);

            // Act
            var result = controller.Get(paymentId);

            // Assert
            Assert.Equal(payment, result);
        }

        [Fact]
        public void Post_ValidPayment_ReturnsOkResult()
        {
            // Arrange
            var paymentDataMock = Substitute.For<IPaymentData>();
            var paymentDomainMock = Substitute.For<IPaymentDomain>();
            var mapperMock = Substitute.For<IMapper>();

            var controller = new PaymentController(paymentDomainMock, paymentDataMock, mapperMock);

            var request = new PaymentRequest { /* Agrega una solicitud de pago de ejemplo aquí */ };
            var payment = new Payment { /* Agrega un pago de ejemplo aquí */ };

            mapperMock.Map<PaymentRequest, Payment>(request).Returns(payment);
            paymentDomainMock.Create(payment).Returns(true);

            // Act
            var result = controller.Post(request);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Post_InvalidPayment_ReturnsBadRequest()
        {
            // Arrange
            var paymentDataMock = Substitute.For<IPaymentData>();
            var paymentDomainMock = Substitute.For<IPaymentDomain>();
            var mapperMock = Substitute.For<IMapper>();

            var controller = new PaymentController(paymentDomainMock, paymentDataMock, mapperMock);
            controller.ModelState.AddModelError("PropertyName", "Error Message");

            var request = new PaymentRequest { /* Agrega una solicitud de pago de ejemplo aquí */ };

            // Act
            var result = controller.Post(request);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        
}