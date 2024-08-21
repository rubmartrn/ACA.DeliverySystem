using ACA.DeliverySystem.Business.Models;
using ACA.DeliverySystem.Business.Services;
using ACA.DeliverySystem.Data;
using ACA.DeliverySystem.Data.Models;
using ACA.DeliverySystem.Data.Repository;
using AutoMapper;
using Moq;

namespace ACA.DeliverySystem.Business.Tests
{
    public class ItemServiceTest
    {
        private Mock<Order> _mockOrder = new Mock<Order>();
        private Mock<IItemRepository> _iItemRepositoryMock = new Mock<IItemRepository>();
        private Mock<IUnitOfWork> _uowMock = new Mock<IUnitOfWork>();
        private Mock<IMapper> _mapperMock = new Mock<IMapper>();
        private IItemService service => new ItemService(_uowMock.Object, _mapperMock.Object);

        [Fact]

        public async Task CreateItem_Success()
        {
            //Arrange
            var items = new List<Item>();
            var itemAddModel = new ItemAddModel{Name = "fast food", Description = "5 pies", Price = 25};
            var item = new Item { Id = 1, OrderId = 1, Name = "fast food", Description = "5 pies", Price = 25, Order = _mockOrder.Object };

            _iItemRepositoryMock.Setup(e => e.GetAll(It.IsAny<CancellationToken>())).ReturnsAsync(items);
            _uowMock.Setup(u => u.ItemRepository).Returns(_iItemRepositoryMock.Object);
            _mapperMock.Setup(m => m.Map<Item>(itemAddModel)).Returns(item);

            //Act
            await service.CreateItem(itemAddModel, CancellationToken.None);
            //Assert
            _iItemRepositoryMock.Verify(m => m.Add(It.Is<Item>(c => c == item), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Delete_Success()
        {
            //Arrange
            int itemId = 1;
            var items = new List<Item>
            {
                new Item { Id = 1, OrderId = 1, Name = "fast food", Description = "5 pies", Price = 25, Order = _mockOrder.Object },
                new Item { Id = 2, OrderId = 1, Name = "Donar", Description = "6 pies", Price = 35, Order = _mockOrder.Object },
            };
           
            _iItemRepositoryMock.Setup(e => e.GetAll(It.IsAny<CancellationToken>())).ReturnsAsync(items);
            _iItemRepositoryMock.Setup(e => e.Delete(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(OperationResult.Ok);
            _uowMock.Setup(u => u.ItemRepository).Returns(_iItemRepositoryMock.Object);
            //Act
            var result = await service.Delete(itemId, CancellationToken.None);
            //Assert
            Assert.True(result.Success);
        }

        [Fact]
        public async Task GetAllItems_Success()
        {
            //Arrange
            var items = new List<Item>
            {
                new Item { Id = 1, OrderId = 1, Name = "fast food", Description = "5 pies", Price = 25, Order = _mockOrder.Object },
                new Item { Id = 2, OrderId = 1, Name = "Donar", Description = "6 pies", Price = 35, Order = _mockOrder.Object },
            };
            var itemViewModels = new List<ItemViewModel>
            {
                new ItemViewModel { Id = 1, OrderId = 1, Name = "fast food", Description = "5 pies", Price = 25 },
                new ItemViewModel { Id = 2, OrderId = 1, Name = "Donar", Description = "6 pies", Price = 35 },
            };

            _iItemRepositoryMock.Setup(e => e.GetAll(It.IsAny<CancellationToken>())).ReturnsAsync(items);
            _uowMock.Setup(u => u.ItemRepository).Returns(_iItemRepositoryMock.Object);
            _mapperMock.Setup(m => m.Map<IEnumerable<ItemViewModel>>(items)).Returns(itemViewModels);

            //Act
            var result = await service.GetAll(CancellationToken.None);
            //Assert
            Assert.Equal(itemViewModels.Count, result.Count());
            Assert.Equal(itemViewModels[0].Id, result.First().Id);
            Assert.Equal(itemViewModels[1].Name, result.Last().Name);

        }

        [Fact]
        public async Task GetItemById_Success()
        {
            //Arrange
            var item = new Item { Id = 1, OrderId = 1, Name = "fast food", Description = "5 pies", Price = 25, Order = _mockOrder.Object };
            
            _iItemRepositoryMock.Setup(e => e.GetById(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(item);
            _uowMock.Setup(u => u.ItemRepository).Returns(_iItemRepositoryMock.Object);
            // Act

            await service.GetById(1, CancellationToken.None);

            //Assert
            _iItemRepositoryMock.Verify(m => m.GetById(It.Is<int>(c => c == 1), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Update_Success()
        {
            //Arrange
            int itemId = 1;
            var items = new List<Item>
            {
                new Item { Id = 1, OrderId = 1, Name = "fast food", Description = "5 pies", Price = 25, Order = _mockOrder.Object },
                new Item { Id = 2, OrderId = 1, Name = "Donar", Description = "6 pies", Price = 35, Order = _mockOrder.Object },
            };
            var itemUpdateModel = new ItemUpdateModel { Name = "fast foodik", Description = "12 pies", Price = 45  };
            var currentItem = new Item { Id = 3, OrderId = 3, Name = "fast foodik", Description = "12 pies", Price = 45, Order = _mockOrder.Object };
            
            _iItemRepositoryMock.Setup(e => e.GetById(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(currentItem);
            _iItemRepositoryMock.Setup(e => e.GetAll(It.IsAny<CancellationToken>())).ReturnsAsync(items);
            _uowMock.Setup(u => u.ItemRepository).Returns(_iItemRepositoryMock.Object);
            
            //Act

            await service.Update(itemId, itemUpdateModel, CancellationToken.None);
            //Assert
            _iItemRepositoryMock.Verify(m => m.Update(It.Is<Item>(c => c == currentItem), It.IsAny<CancellationToken>()), Times.Once);

        }
    }
}
