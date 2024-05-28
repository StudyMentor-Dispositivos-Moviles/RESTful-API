using _3._Data.Model;
using _3._Data;
using NSubstitute;
namespace _2._Domain._Tests;

public class ReviewDomainTest
{
    [Theory]
        [InlineData(4, "!Buen tutor", 1, 2)]
        public void Create_ValidReview_ReturnsTrue(int rating, string textMessage, int studentId, int tutorId)
        {
            // Arrange
            var review = new Review
            {
                Rating = rating,
                TextMessagge = textMessage,
                StudentId = studentId,
                TutorId = tutorId
            };

            var reviewDataMock = Substitute.For<IReviewData>();
            reviewDataMock.Create(review).Returns(true);

            var studentDataMock = Substitute.For<IStudentData>();
            var tutorDataMock = Substitute.For<ITutorData>();

            var reviewDomain = new ReviewDomain(reviewDataMock, studentDataMock, tutorDataMock);

            // Act
            var result = reviewDomain.Create(review);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData(0, "!Mal tutor", 1, 2)]
        [InlineData(6, "!Buen tutor", 1, 2)]
        public void Create_InvalidReview_ReturnsFalse(int rating, string textMessage, int studentId, int tutorId)
        {
            // Arrange
            var review = new Review
            {
                Rating = rating,
                TextMessagge = textMessage,
                StudentId = studentId,
                TutorId = tutorId
            };

            var reviewDataMock = Substitute.For<IReviewData>();
            var studentDataMock = Substitute.For<IStudentData>();
            var tutorDataMock = Substitute.For<ITutorData>();

            var reviewDomain = new ReviewDomain(reviewDataMock, studentDataMock, tutorDataMock);

            // Act
            var result = reviewDomain.Create(review);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData(1, "Updated review", 4, 5)]
        public void Update_ValidReview_ReturnsTrue(int reviewId, string updatedTextMessage, int updatedRating, int studentId)
        {
            // Arrange
            var existingReview = new Review
            {
                Id = reviewId,
                TextMessagge = "Original review",
                Rating = 3,
                StudentId = 3
            };

            var updatedReview = new Review
            {
                Id = reviewId,
                TextMessagge = updatedTextMessage,
                Rating = updatedRating,
                StudentId = studentId
            };

            var reviewDataMock = Substitute.For<IReviewData>();
            reviewDataMock.GetById(reviewId).Returns(existingReview);
            reviewDataMock.Update(Arg.Any<Review>(), reviewId).Returns(true);

            var studentDataMock = Substitute.For<IStudentData>();
            var tutorDataMock = Substitute.For<ITutorData>();

            var reviewDomain = new ReviewDomain(reviewDataMock, studentDataMock, tutorDataMock);

            // Act
            var result = reviewDomain.Update(updatedReview, reviewId);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData(1, "Updated review", 4, 5)]
        public void Update_NonExistingReview_ReturnsFalse(int reviewId, string updatedTextMessage, int updatedRating, int studentId)
        {
            // Arrange
            var updatedReview = new Review
            {
                Id = reviewId,
                TextMessagge = updatedTextMessage,
                Rating = updatedRating,
                StudentId = studentId
            };

            var reviewDataMock = Substitute.For<IReviewData>();
            reviewDataMock.GetById(reviewId).Returns((Review)null);

            var studentDataMock = Substitute.For<IStudentData>();
            var tutorDataMock = Substitute.For<ITutorData>();

            var reviewDomain = new ReviewDomain(reviewDataMock, studentDataMock, tutorDataMock);

            // Act
            var result = reviewDomain.Update(updatedReview, reviewId);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Delete_ValidId_ReturnsTrue(int reviewId)
        {
            // Arrange
            var reviewDataMock = Substitute.For<IReviewData>();
            reviewDataMock.Delete(reviewId).Returns(true);

            var studentDataMock = Substitute.For<IStudentData>();
            var tutorDataMock = Substitute.For<ITutorData>();

            var reviewDomain = new ReviewDomain(reviewDataMock, studentDataMock, tutorDataMock);

            // Act
            var result = reviewDomain.Delete(reviewId);

            // Assert
            Assert.True(result);
        }
}