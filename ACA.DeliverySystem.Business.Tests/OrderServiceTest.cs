//using ACA.DeliverySystem.Business.Models;
//using ACA.DeliverySystem.Business.Services;
//using ACA.DeliverySystem.Data;
//using ACA.DeliverySystem.Data.Models;
//using ACA.DeliverySystem.Data.Repository;
//using AutoMapper;
//using Moq;



//namespace ACA.DeliverySystem.Business.Tests
//{
//    public class OrderServiceTest
//    {
//        private Mock<IOrderRepository> _iOrderRepositoryMock = new Mock<IOrderRepository>();
//        private Mock<IItemRepository> _iItemRepositoryMock = new Mock<IItemRepository>();
//        private Mock<IUnitOfWork> _uowMock = new Mock<IUnitOfWork>();
//        private Mock<IMapper> _mapperMock = new Mock<IMapper>();
//        private IOrderService service => new OrderService(_uowMock.Object, _mapperMock.Object);
//        private Mock<List<Item>> _mockItems = new Mock<List<Item>>();
//        private Mock<List<ItemViewModel>> _mockItemViewModels = new Mock<List<ItemViewModel>>();
//        private Mock<User> _mockUsers = new Mock<User>();

//        [Fact]

//        public async Task CreateOrder_Success()
//        {
//            //Arrange
//            var orders = new List<Order>();
//            var orderAddModel = new OrderAddModel() { Name = "Busines Order" };
//            var order = new Order() { Id = 1, UserId = 1, PaidAmount = 25, ProgressEnum = ProgressEnum.Created, OrderItems = _mockItems.Object, User = _mockUsers.Object };

//            _iOrderRepositoryMock.Setup(e => e.GetAll(It.IsAny<CancellationToken>())).ReturnsAsync(orders);
//            _uowMock.Setup(u => u.OrderRepository).Returns(_iOrderRepositoryMock.Object);
//            _mapperMock.Setup(m => m.Map<Order>(orderAddModel)).Returns(order);

//            //Act
//            await service.CreateOrder(orderAddModel, CancellationToken.None);

//            //Assert
//            _iOrderRepositoryMock.Verify(m => m.Add(It.Is<Order>(c => c == order), It.IsAny<CancellationToken>()), Times.Once);

//        }

//        [Fact]
//        public async Task Delete_Success()
//        {
//            //Arrange
//            var orders = new List<Order>
//            {
//                new Order() { Id = 1, UserId = 1,  PaidAmount = 25, ProgressEnum = ProgressEnum.Created, OrderItems = _mockItems.Object, User = _mockUsers.Object },
//                new Order() { Id = 2, UserId = 2,  PaidAmount = 15, ProgressEnum = ProgressEnum.Completed, OrderItems = _mockItems.Object, User = _mockUsers.Object }
//            };

//            _iOrderRepositoryMock.Setup(e => e.GetAll(It.IsAny<CancellationToken>())).ReturnsAsync(orders);
//            _iOrderRepositoryMock.Setup(e => e.Delete(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(OperationResult.Ok);
//            _uowMock.Setup(u => u.OrderRepository).Returns(_iOrderRepositoryMock.Object);

//            //Act
//            await service.Delete(1, CancellationToken.None);

//            //Assert
//            _iOrderRepositoryMock.Verify(m => m.Delete(It.Is<int>(c => c == 1), It.IsAny<CancellationToken>()), Times.Once());
//        }

//        [Fact]
//        public async Task GetAllOrder_Success()
//        {
//            //Arrange
//            var orders = new List<Order>
//            {
//                new Order() { Id = 1, UserId = 1, PaidAmount = 25, ProgressEnum = ProgressEnum.Created, OrderItems = _mockItems.Object, User = _mockUsers.Object },
//                new Order() { Id = 2, UserId = 2, PaidAmount = 15, ProgressEnum = ProgressEnum.Completed, OrderItems = _mockItems.Object, User = _mockUsers.Object }
//            };
//            var orderViewModel = new List<OrderViewModel>
//            {
//                new OrderViewModel { Id = 1, UserId = 1, Name = "fast food",  Date = new DateOnly(), PaidAmount=45,  ProgressEnum = ProgressEnum.Created, Items = _mockItemViewModels.Object },
//                new OrderViewModel { Id = 2, UserId = 2, Name = "Donar", Date = new DateOnly(), PaidAmount=25,  ProgressEnum = ProgressEnum.Canceled, Items = _mockItemViewModels.Object },
//            };

//            _iOrderRepositoryMock.Setup(e => e.GetAll(It.IsAny<CancellationToken>())).ReturnsAsync(orders);
//            _uowMock.Setup(u => u.OrderRepository).Returns(_iOrderRepositoryMock.Object);
//            _mapperMock.Setup(m => m.Map<IEnumerable<OrderViewModel>>(orders)).Returns(orderViewModel);

//            //Act
//            var result = await service.GetAll(CancellationToken.None);

//            //Assert
//            Assert.Equal(orderViewModel.Count, result.Count());            
//        }

//        [Fact]
//        public async Task GetOrderById_Success()
//        {
//            //Arrange
//            var orders = new List<Order>
//            {
//                new Order() { Id = 1, UserId = 1,  PaidAmount = 25, ProgressEnum = ProgressEnum.Created, OrderItems = _mockItems.Object, User = _mockUsers.Object },
//                new Order() { Id = 2, UserId = 2,  PaidAmount = 15, ProgressEnum = ProgressEnum.Completed, OrderItems = _mockItems.Object, User = _mockUsers.Object }
//            };

//            _iOrderRepositoryMock.Setup(e => e.GetAll(It.IsAny<CancellationToken>())).ReturnsAsync(orders);
//            _uowMock.Setup(u => u.OrderRepository).Returns(_iOrderRepositoryMock.Object);

//            //Act
//            await service.Get(1, CancellationToken.None);

//            //Assert
//            _iOrderRepositoryMock.Verify(m => m.GetById(It.Is<int>(c => c == 1), It.IsAny<CancellationToken>()), Times.Once);
//        }

//        [Fact]
//        public async Task Update_Success()
//        {
//            //Arrange
//            var order = new Order() { Id = 1, UserId = 1,  PaidAmount = 25, ProgressEnum = ProgressEnum.Created, OrderItems = _mockItems.Object, User = _mockUsers.Object };
//            _iOrderRepositoryMock.Setup(e => e.GetById(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(order);
//            _uowMock.Setup(u => u.OrderRepository).Returns(_iOrderRepositoryMock.Object);
//            var currentOrder = new Order() { Id = 3, UserId = 3, PaidAmount = 15, ProgressEnum = ProgressEnum.InProgress, OrderItems = _mockItems.Object, User = _mockUsers.Object };

//            //Act
//            await service.Update(2, currentOrder, CancellationToken.None);

//            //Assert
//            _iOrderRepositoryMock.Verify(m => m.Update(It.Is<Order>(c => c == currentOrder), It.IsAny<CancellationToken>()), Times.Once);
//        }

//        [Fact]
//        public async Task AddItemInOrder_Success() 
//        {
//            //Arrange
//            var order = new Order() { Id = 1, UserId = 1, PaidAmount = 25, ProgressEnum = ProgressEnum.Created, OrderItems = _mockItems.Object, User = _mockUsers.Object };
//            var item = new Item { Id = 3, OrderItemId = 3, Name = "fast foodik", Description = "12 pies", Price = 45, Order = order};

//            _iOrderRepositoryMock.Setup(e => e.GetById(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(order);
//            _iItemRepositoryMock.Setup(e => e.GetById(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(item);
//            _iOrderRepositoryMock.Setup(e => e.AddItemInOrder(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(OperationResult.Ok);
//            _uowMock.Setup(u => u.OrderRepository).Returns(_iOrderRepositoryMock.Object);

//            //Act
//            var result = await service.AddItemInOrder(1, 3, CancellationToken.None);

//            //Assert
//            Assert.True(result.Success);
//        }

//        [Fact]
//        public async Task RemoveItemFromOrder_Success()
//        {
//            //Arrange
//            var order = new Order() { Id = 1, UserId = 1, PaidAmount = 25, ProgressEnum = ProgressEnum.Created, OrderItems = _mockItems.Object, User = _mockUsers.Object };
//            var item = new Item { Id = 3, OrderItemId = 3, Name = "fast foodik", Description = "12 pies", Price = 45, Order = order };

//            _iOrderRepositoryMock.Setup(e => e.GetById(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(order);
//            _iItemRepositoryMock.Setup(e => e.GetById(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(item);
//            _iOrderRepositoryMock.Setup(e => e.RemoveItemFromOrder(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(OperationResult.Ok);
//            _uowMock.Setup(u => u.OrderRepository).Returns(_iOrderRepositoryMock.Object);

//            //Act
//            var result = await service.RemoveItemFromOrder(1, 3, CancellationToken.None);

//            //Assert
//            Assert.True(result.Success);
//        }

//        [Fact]
//        public async Task PayForOrder_Success() 
//        {
//            //Arrange
//            var orderId = 1;
//            var amount = 85;
//            var order = new Order() { Id = 1, UserId = 1, PaidAmount = 25, ProgressEnum = ProgressEnum.Created, OrderItems = _mockItems.Object, User = _mockUsers.Object };

//            _iOrderRepositoryMock.Setup(e => e.GetById(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(order);
//            _iOrderRepositoryMock.Setup(e => e.PayForOrder(It.IsAny<int>(), It.IsAny<decimal>(), It.IsAny<CancellationToken>())).ReturnsAsync(OperationResult.Ok);
//            _uowMock.Setup(u => u.OrderRepository).Returns(_iOrderRepositoryMock.Object);

//            //Act
//            var result = await service.PayForOrder(orderId, amount, CancellationToken.None);

//            //Assert
//            Assert.True(result.Success);
//        }


//        [Fact]
//        public async Task OrderCompleted_Success() 
//        {
//            //Arrange
//            var orderId = 1;
//            var order = new Order() { Id = 1, UserId = 1, PaidAmount = 25, ProgressEnum = ProgressEnum.Created, OrderItems = _mockItems.Object, User = _mockUsers.Object };

//            _iOrderRepositoryMock.Setup(e => e.GetById(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(order);
//            _iOrderRepositoryMock.Setup(e => e.OrderCompleted(It.IsAny<int>(),It.IsAny<CancellationToken>())).ReturnsAsync(OperationResult.Ok);
//            _uowMock.Setup(u => u.OrderRepository).Returns(_iOrderRepositoryMock.Object);

//            //Act
//            var result = await service.OrderCompleted(orderId, CancellationToken.None);

//            //Assert
//            Assert.True(result.Success);
//        }

//        [Fact]
//        public async Task CancelOrderSuccess()
//        {
//            //Arrange
//            var orderId = 1;
//            var order = new Order() { Id = 1, UserId = 1, PaidAmount = 25, ProgressEnum = ProgressEnum.Created, OrderItems = _mockItems.Object, User = _mockUsers.Object };
//            _iOrderRepositoryMock.Setup(e => e.GetById(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(order);
//            _iOrderRepositoryMock.Setup(e => e.CancelOrder(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(OperationResult.Ok);
//            _uowMock.Setup(u => u.OrderRepository).Returns(_iOrderRepositoryMock.Object);

//            //Act
//            var result = await service.CancelOrder(orderId, CancellationToken.None);

//            //Assert
//            Assert.True(result.Success);
//        }

//    }
//}
