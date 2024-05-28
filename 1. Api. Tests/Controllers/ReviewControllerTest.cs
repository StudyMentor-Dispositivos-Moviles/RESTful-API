using _1._API.Request;
using _1._API.Controllers;
using _2._Domain;
using _3._Data;
using _3._Data.Model;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
namespace _1.API._Tests.Controller;

public class ReviewControllerTest
{
    [Fact]
        public void GetAllReviews_ReturnsOkResult()
        {
            // Arrange
            var reviewDataMock = Substitute.For<IReviewData>();
            var reviewDomainMock = Substitute.For<IReviewDomain>();
            var mapperMock = Substitute.For<IMapper>();

            var controller = new ReviewController(reviewDomainMock, reviewDataMock, mapperMock);

            // Act
            var result = controller.GetAllReviews();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
        
        [Fact]
        public void GetReviewById_WithInvalidId_ReturnsNotFoundResult()
        {
            // Arrange
            var reviewDataMock = Substitute.For<IReviewData>();
            var reviewDomainMock = Substitute.For<IReviewDomain>();
            var mapperMock = Substitute.For<IMapper>();

            var controller = new ReviewController(reviewDomainMock, reviewDataMock, mapperMock);

            var invalidReviewId = 999;

            // Act
            var result = controller.GetReviewById(invalidReviewId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void CreateReview_WithInvalidData_ReturnsBadRequestResult()
        {
            // Arrange
            var reviewDataMock = Substitute.For<IReviewData>();
            var reviewDomainMock = Substitute.For<IReviewDomain>();
            var mapperMock = Substitute.For<IMapper>();

            var controller = new ReviewController(reviewDomainMock, reviewDataMock, mapperMock);

            // Configura el ModelState para simular un error de validación
            controller.ModelState.AddModelError("PropertyName", "Error message");

            var request = new ReviewRequest
            {
                // Proporciona datos inválidos para activar el ModelState error
            };

            // Act
            var result = controller.CreateReview(request);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
        
        [Fact]
        public void UpdateReview_WithInvalidId_ReturnsNotFoundResult()
        {
            // Arrange
            var reviewDataMock = Substitute.For<IReviewData>();
            var reviewDomainMock = Substitute.For<IReviewDomain>();
            var mapperMock = Substitute.For<IMapper>();

            var controller = new ReviewController(reviewDomainMock, reviewDataMock, mapperMock);

            var invalidReviewId = 999;
            var reviewRequest = new ReviewRequest
            {
                // Populate with valid data
            };

            // Act
            var result = controller.UpdateReview(invalidReviewId, reviewRequest);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }


        [Fact]
        public void DeleteReview_WithInvalidId_ReturnsNotFoundResult()
        {
            // Arrange
            var reviewDataMock = Substitute.For<IReviewData>();
            var reviewDomainMock = Substitute.For<IReviewDomain>();
            var mapperMock = Substitute.For<IMapper>();

            var controller = new ReviewController(reviewDomainMock, reviewDataMock, mapperMock);

            var invalidReviewId = 999;

            // Act
            var result = controller.DeleteReview(invalidReviewId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
}