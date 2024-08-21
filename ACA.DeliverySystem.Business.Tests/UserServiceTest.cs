using ACA.DeliverySystem.Business.Services;
using ACA.DeliverySystem.Data;
using ACA.DeliverySystem.Data.Models;
using ACA.DeliverySystem.Data.Repository;
using Moq;

namespace ACA.DeliverySystem.Business.Tests
{
    public class UserServiceTest
    {
        //private Mock<Order> _mockOrder = new Mock<Order>();
        //private Mock<IUserRepository> _iUserRepositoryMock = new Mock<IUserRepository>();
        //private Mock<IUnitOfWork> _uowMock = new Mock<IUnitOfWork>();
        //private IUserService service => new UserService(_uowMock.Object);

        //[Fact]
        //public async Task CreateUser_Success()
        //{
        //    //Arrange
        //    var users = new List<User>();
        //    var user = new User { Id = 1, OrderId = 1, Name = "Artur", SureName = "Nikoxosyan", Order = _mockOrder.Object, Email = "art56@gmail.com" };
        //    _iUserRepositoryMock.Setup(e => e.GetAll(It.IsAny<CancellationToken>())).ReturnsAsync(users);
        //    _uowMock.Setup(u => u.UserRepository).Returns(_iUserRepositoryMock.Object);
        //    //Act
        //    await service.Create(user, CancellationToken.None);
        //    //Assert
        //    _iUserRepositoryMock.Verify(m => m.Add(It.Is<User>(c => c == user), It.IsAny<CancellationToken>()), Times.Once);
        //}

        //[Fact]
        //public async Task Delete_Success()
        //{
        //    //Arrange
        //    var users = new List<User>
        //    {
        //        new User { Id = 1, OrderId = 1, Name = "Artur", SureName = "Nikoxosyan", Order = _mockOrder.Object, Email="art56@gmail.com" },
        //        new User { Id = 2, OrderId = 2, Name = "Nara", SureName = "Hovhannisyan", Order = _mockOrder.Object, Email="narush28@gmail.com" }
        //    };
        //    _iUserRepositoryMock.Setup(e => e.GetAll(It.IsAny<CancellationToken>())).ReturnsAsync(users);
        //    _uowMock.Setup(u => u.UserRepository).Returns(_iUserRepositoryMock.Object);
        //    //Act
        //    await service.Delete(1, CancellationToken.None);
        //    //Assert
        //    _iUserRepositoryMock.Verify(m => m.Delete(It.Is<int>(c => c == 1), It.IsAny<CancellationToken>()), Times.Once());
        //}
        //[Fact]
        //public async Task GetAll_Success()
        //{
        //    //Arrange
        //    var users = new List<User>
        //    {
        //        new User { Id = 1, OrderId = 1, Name = "Artur", SureName = "Nikoxosyan", Order = _mockOrder.Object, Email="art56@gmail.com" },
        //        new User { Id = 2, OrderId = 2, Name = "Nara", SureName = "Hovhannisyan", Order = _mockOrder.Object, Email="narush28@gmail.com" }
        //    };
        //    _iUserRepositoryMock.Setup(e => e.GetAll(It.IsAny<CancellationToken>())).ReturnsAsync(users);
        //    _uowMock.Setup(u => u.UserRepository).Returns(_iUserRepositoryMock.Object);
        //    //Act
        //    var result = await service.GetAll(CancellationToken.None);
        //    //Assert
        //    Assert.Equal(2, result.Count());
        //}
        //[Fact]
        //public async Task GetById_Success()
        //{
        //    //Arrange
        //    var user = new User { Id = 1, OrderId = 1, Name = "Artur", SureName = "Nikoxosyan", Order = _mockOrder.Object, Email = "art56@gmail.com" };
        //    _iUserRepositoryMock.Setup(e => e.GetById(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(user);

        //    _uowMock.Setup(u => u.UserRepository).Returns(_iUserRepositoryMock.Object);
        //    // Act

        //    await service.GetById(1, CancellationToken.None);

        //    //Assert
        //    _iUserRepositoryMock.Verify(m => m.GetById(It.Is<int>(c => c == 1), It.IsAny<CancellationToken>()), Times.Once);
        //}

        //[Fact]
        //public async Task Update()
        //{
        //    //Arrange
        //    var users = new List<User>
        //    {
        //        new User { Id = 1, OrderId = 1, Name = "Artur", SureName = "Nikoxosyan", Order = _mockOrder.Object, Email="art56@gmail.com" },
        //        new User { Id = 2, OrderId = 2, Name = "Nara", SureName = "Hovhannisyan", Order = _mockOrder.Object, Email="narush28@gmail.com" }
        //    };
        //    _iUserRepositoryMock.Setup(e => e.GetAll(It.IsAny<CancellationToken>())).ReturnsAsync(users);
        //    _uowMock.Setup(u => u.UserRepository).Returns(_iUserRepositoryMock.Object);
        //    var currentUser = await service.GetById(1, CancellationToken.None);
        //    //Act

        //    await service.Update(currentUser, CancellationToken.None);
        //    //Assert
        //    _iUserRepositoryMock.Verify(m => m.Update(It.Is<User>(c => c == currentUser), It.IsAny<CancellationToken>()), Times.Once);

        //}


    }
}
