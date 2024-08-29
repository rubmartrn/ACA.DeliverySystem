/*using Moq;


namespace ACA.DeliverySystem.Data.Test
{
    public class UnitOfWorkTests
    {
        private Mock<DeliveryDbContext> _mockDeliveryDbContext = new Mock<DeliveryDbContext>();

        [Fact]
        public async Task SaveTest ()
        {
            //Arrange
            var _unitOfWork = new UnitOfWork(_mockDeliveryDbContext.Object);
            var token = new CancellationTokenSource().Token;

            // Act
            await _unitOfWork.Save(token);
            

            // Assert
            _mockDeliveryDbContext.Verify(c => c.SaveChangesAsync(token), Times.Once);
        }
    }
}
*/