using ACA.DeliverySystem.Business.Models;
using ACA.DeliverySystem.Business.Services;
using ACA.DeliverySystem.Data;
using ACA.DeliverySystem.Data.Models;
using ACA.DeliverySystem.Data.Repository;
using AutoMapper;
using Moq;


namespace ACA.DeliverySystem.Business.Tests
{
    public class UserServiceTest
    {

        private Mock<IUserRepository> _iUserRepositoryMock = new Mock<IUserRepository>();
        private Mock<IOrderRepository> _iOrderRepositoryMock = new Mock<IOrderRepository>();
        private Mock<IUnitOfWork> _uowMock = new Mock<IUnitOfWork>();
        private Mock<IMapper> _mapperMock = new Mock<IMapper>();
        private IUserService service => new UserService(_uowMock.Object, _mapperMock.Object);
        private Mock<List<Order>> _mockOrders = new Mock<List<Order>>();
        private Mock<List<Item>> _mockItems = new Mock<List<Item>>();
        private Mock<List<ItemViewModel>> _mockItemViewModels = new Mock<List<ItemViewModel>>();
        private Mock<User> _mockUsers = new Mock<User>();

        [Fact]
        public async Task CreateUser_Success()
        {
            //Arrange
            var users = new List<User>();
            var user = new User { Id = 1, OrderId = 1, Name = "Artur", SureName = "Nikoxosyan", Orders = _mockOrders.Object, Email = "art56@gmail.com" };
            var userAddModel = new UserAddModel { Name = "Artur", SureName = "Nikoxosyan", Email = "art56@gmail.com" };

            _iUserRepositoryMock.Setup(e => e.GetAll(It.IsAny<CancellationToken>())).ReturnsAsync(users);
            _uowMock.Setup(u => u.UserRepository).Returns(_iUserRepositoryMock.Object);
            _mapperMock.Setup(m => m.Map<User>(userAddModel)).Returns(user);

            //Act
            await service.Create(userAddModel, CancellationToken.None);

            //Assert
            _iUserRepositoryMock.Verify(m => m.Add(It.Is<User>(c => c == user), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Delete_Success()
        {
            //Arrange
            var users = new List<User>
            {
                new User { Id = 1, OrderId = 1, Name = "Artur", SureName = "Nikoxosyan", Orders = _mockOrders.Object, Email="art56@gmail.com" },
                new User { Id = 2, OrderId = 2, Name = "Nara", SureName = "Hovhannisyan", Orders = _mockOrders.Object, Email="narush28@gmail.com" }
            };

            _iUserRepositoryMock.Setup(e => e.GetAll(It.IsAny<CancellationToken>())).ReturnsAsync(users);
            _iUserRepositoryMock.Setup(e => e.Delete(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(OperationResult.Ok);
            _uowMock.Setup(u => u.UserRepository).Returns(_iUserRepositoryMock.Object);

            //Act
            await service.Delete(1, CancellationToken.None);

            //Assert
            _iUserRepositoryMock.Verify(m => m.Delete(It.Is<int>(c => c == 1), It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact]
        public async Task GetAll_Success()
        {
            //Arrange
            var users = new List<User>
            {
                new User { Id = 1, OrderId = 1, Name = "Artur", SureName = "Nikoxosyan", Orders = _mockOrders.Object, Email="art56@gmail.com" },
                new User { Id = 2, OrderId = 2, Name = "Nara", SureName = "Hovhannisyan", Orders = _mockOrders.Object, Email="narush28@gmail.com" }
            };
            var userViewModels = new List<UserViewModel>
            {
                new UserViewModel { Id = 1,  Name = "Artur", SureName = "Nikoxosyan",  Email="art56@gmail.com" },
                new UserViewModel { Id = 2,  Name = "Nara", SureName = "Hovhannisyan", Email="narush28@gmail.com" }
            };

            _iUserRepositoryMock.Setup(e => e.GetAll(It.IsAny<CancellationToken>())).ReturnsAsync(users);
            _uowMock.Setup(u => u.UserRepository).Returns(_iUserRepositoryMock.Object);
            _mapperMock.Setup(m => m.Map<IEnumerable<UserViewModel>>(users)).Returns(userViewModels);

            //Act
            var result = await service.GetAll(CancellationToken.None);

            //Assert
            Assert.Equal(userViewModels.Count, result.Count());
        }

        [Fact]
        public async Task GetById_Success()
        {
            //Arrange
            var user = new User { Id = 1, OrderId = 1, Name = "Artur", SureName = "Nikoxosyan", Orders = _mockOrders.Object, Email = "art56@gmail.com" };

            _iUserRepositoryMock.Setup(e => e.GetById(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(user);
            _uowMock.Setup(u => u.UserRepository).Returns(_iUserRepositoryMock.Object);

            // Act
            await service.GetById(1, CancellationToken.None);

            //Assert
            _iUserRepositoryMock.Verify(m => m.GetById(It.Is<int>(c => c == 1), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Update_Success()
        {
            //Arrange
            int userId = 1;
            var users = new List<User>
            {
                new User { Id = 1, OrderId = 1, Name = "Artur", SureName = "Nikoxosyan", Orders = _mockOrders.Object, Email="art56@gmail.com" },
                new User { Id = 2, OrderId = 2, Name = "Nara", SureName = "Hovhannisyan", Orders = _mockOrders.Object, Email="narush28@gmail.com" }
            };
            var userUpdateModel = new UserUpdateModel { Name = "Gagik", SureName = "Sargsyan" };
            var currentUser = new User { Id = 3, OrderId = 3, Name = "Gagik", SureName = "Sargsyan", Orders = _mockOrders.Object, Email = "gags8@gmail.com" };

            _iUserRepositoryMock.Setup(e => e.GetById(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(currentUser);
            _iUserRepositoryMock.Setup(e => e.GetAll(It.IsAny<CancellationToken>())).ReturnsAsync(users);
            _uowMock.Setup(u => u.UserRepository).Returns(_iUserRepositoryMock.Object);

            //Act
            await service.Update(userId, userUpdateModel, CancellationToken.None);

            //Assert
            _iUserRepositoryMock.Verify(m => m.Update(It.Is<User>(c => c == currentUser), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GetUserOrders_Success()
        {
            //Arrange
            var orders = new List<Order>
            {
                new Order() { Id = 1, UserId = 1, PaidAmount = 25, ProgressEnum = ProgressEnum.Created, Items = _mockItems.Object, User = _mockUsers.Object },
                new Order() { Id = 2, UserId = 2, PaidAmount = 15, ProgressEnum = ProgressEnum.Completed, Items = _mockItems.Object, User = _mockUsers.Object }
            };
            var orderViewModel = new List<OrderViewModel>
            {
                new OrderViewModel { Id = 1, UserId = 1, Name = "fast food",  Date = new DateOnly(), PaidAmount=45,  ProgressEnum = ProgressEnum.Created, Items = _mockItemViewModels.Object },
                new OrderViewModel { Id = 2, UserId = 2, Name = "Donar", Date = new DateOnly(), PaidAmount=25,  ProgressEnum = ProgressEnum.Canceled, Items = _mockItemViewModels.Object },
            };
            var currentUser = new User { Id = 3, OrderId = 3, Name = "Gagik", SureName = "Sargsyan", Orders = _mockOrders.Object, Email = "gags8@gmail.com" };

            _iUserRepositoryMock.Setup(e => e.GetById(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(currentUser);
            _iUserRepositoryMock.Setup(e => e.GetUserOrders(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(orders);
            _uowMock.Setup(u => u.UserRepository).Returns(_iUserRepositoryMock.Object);
            _mapperMock.Setup(m => m.Map<IEnumerable<OrderViewModel>>(orders)).Returns(orderViewModel);

            //Act
            var result = await service.GetUserOrders(3, CancellationToken.None);

            //Assert
            Assert.Equal(orderViewModel.Count, result.Count());
        }

        [Fact]
        public async Task AddOrderInUser_Success()
        {
            //Arrange
            var orderAddModel = new OrderAddModel() { Name = "Busines Order" };
            var order = new Order() { Id = 1, Name = "Busines Order", UserId = 1, PaidAmount = 25, ProgressEnum = ProgressEnum.Created, Items = _mockItems.Object, User = _mockUsers.Object };
            var currentUser = new User { Id = 1, OrderId = 1, Name = "Gagik", SureName = "Sargsyan", Orders = _mockOrders.Object, Email = "gags8@gmail.com" };

            _iUserRepositoryMock.Setup(e => e.GetById(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(currentUser);
            _uowMock.Setup(u => u.UserRepository).Returns(_iUserRepositoryMock.Object);
            _iOrderRepositoryMock.Setup(e => e.Add(It.IsAny<Order>(), It.IsAny<CancellationToken>())).ReturnsAsync(order);
            _uowMock.Setup(u => u.OrderRepository).Returns(_iOrderRepositoryMock.Object);

            //Act
            await service.AddOrderInUser(1, orderAddModel, CancellationToken.None);

            //Assert
            _iUserRepositoryMock.Verify(m => m.AddOrderInUser(It.Is<int>(c => c == 1), It.Is<Order>(m => m == order), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
