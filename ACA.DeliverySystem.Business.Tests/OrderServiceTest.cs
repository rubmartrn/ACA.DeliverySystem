using ACA.DeliverySystem.Business.Services;
using ACA.DeliverySystem.Data;
using ACA.DeliverySystem.Data.Models;
using ACA.DeliverySystem.Data.Repository;
using Moq;


namespace ACA.DeliverySystem.Business.Tests
{
    public class OrderServiceTest
    {
        private Mock<IOrderRepository> _iOrderRepositoryMock = new Mock<IOrderRepository>();
        private Mock<IUnitOfWork> _uowMock = new Mock<IUnitOfWork>();
        private IOrderService service => new OrderService(_uowMock.Object);
        private Mock<List<Item>> _mockItems = new Mock<List<Item>>();
        private Mock<List<User>> _mockUsers = new Mock<List<User>>();

        [Fact]

        public async Task CreateOrder_Success()
        {
            //Arrange
            var orders = new List<Order>();
            var order = new Order() { Id = 1, UserId = 1, Date = new DateOnly(2022, 1, 6), PaidAmount = 25, ProgressEnum = ProgressEnum.Created, Items = _mockItems.Object, Users = _mockUsers.Object };
            _iOrderRepositoryMock.Setup(e => e.GetAll(It.IsAny<CancellationToken>())).ReturnsAsync(orders);
            _uowMock.Setup(u => u.OrderRepository).Returns(_iOrderRepositoryMock.Object);
            //Act
            await service.CreateOrder(order, CancellationToken.None);
            //Assert
            _iOrderRepositoryMock.Verify(m => m.Add(It.Is<Order>(c => c == order)), Times.Once);

        }

        [Fact]
        public async Task Delete_Success()
        {
            //Arrange
            var orders = new List<Order>
        {
            new Order() { Id = 1, UserId = 1, Date = new DateOnly(2022, 1, 6), PaidAmount = 25, ProgressEnum = ProgressEnum.Created, Items = _mockItems.Object, Users = _mockUsers.Object },
            new Order() { Id = 2, UserId = 2, Date = new DateOnly(2023, 2, 3), PaidAmount = 15, ProgressEnum = ProgressEnum.Done, Items = _mockItems.Object, Users = _mockUsers.Object }
        };
            _iOrderRepositoryMock.Setup(e => e.GetAll(It.IsAny<CancellationToken>())).ReturnsAsync(orders);
            _uowMock.Setup(u => u.OrderRepository).Returns(_iOrderRepositoryMock.Object);
            //Act
            await service.Delete(1, CancellationToken.None);
            //Assert
            _iOrderRepositoryMock.Verify(m => m.Delete(It.Is<int>(c => c == 1), It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact]
        public async Task GetAllOrder_Success()
        {
            //Arrange
            var orders = new List<Order>
        {
            new Order() { Id = 1, UserId = 1, Date = new DateOnly(2022, 1, 6), PaidAmount = 25, ProgressEnum = ProgressEnum.Created, Items = _mockItems.Object, Users = _mockUsers.Object },
            new Order() { Id = 2, UserId = 2, Date = new DateOnly(2023, 2, 3), PaidAmount = 15, ProgressEnum = ProgressEnum.Done, Items = _mockItems.Object, Users = _mockUsers.Object }
        };
            _iOrderRepositoryMock.Setup(e => e.GetAll(It.IsAny<CancellationToken>())).ReturnsAsync(orders);
            _uowMock.Setup(u => u.OrderRepository).Returns(_iOrderRepositoryMock.Object);
            //Act
            var result = await service.GetAll(CancellationToken.None);
            //Assert
            Assert.Equal(2, result.Count());
        }
        [Fact]
        public async Task GetOrderById_Success()
        {
            //Arrange
            var orders = new List<Order>
        {
            new Order() { Id = 1, UserId = 1, Date = new DateOnly(2022, 1, 6), PaidAmount = 25, ProgressEnum = ProgressEnum.Created, Items = _mockItems.Object, Users = _mockUsers.Object },
            new Order() { Id = 2, UserId = 2, Date = new DateOnly(2023, 2, 3), PaidAmount = 15, ProgressEnum = ProgressEnum.Done, Items = _mockItems.Object, Users = _mockUsers.Object }
        };
            _iOrderRepositoryMock.Setup(e => e.GetAll(It.IsAny<CancellationToken>())).ReturnsAsync(orders);
            _uowMock.Setup(u => u.OrderRepository).Returns(_iOrderRepositoryMock.Object);
            //Act
            await service.Get(1, CancellationToken.None);
            //Assert
            _iOrderRepositoryMock.Verify(m => m.GetById(It.Is<int>(c => c == 1), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Update()
        {
            //Arrange
            var order = new Order() { Id = 1, UserId = 1, Date = new DateOnly(2022, 1, 6), PaidAmount = 25, ProgressEnum = ProgressEnum.Created, Items = _mockItems.Object, Users = _mockUsers.Object };
            _iOrderRepositoryMock.Setup(e => e.GetById(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(order);
            _uowMock.Setup(u => u.OrderRepository).Returns(_iOrderRepositoryMock.Object);
            var currentOrder = new Order() { Id = 3, UserId = 3, Date = new DateOnly(2025, 4, 6), PaidAmount = 15, ProgressEnum = ProgressEnum.InProgress, Items = _mockItems.Object, Users = _mockUsers.Object };
            //Act
            await service.Update(2, currentOrder, CancellationToken.None);
            //Assert
            _iOrderRepositoryMock.Verify(m => m.Update(It.Is<Order>(c => c == currentOrder), It.IsAny<CancellationToken>()), Times.Once);

        }
    }
}
