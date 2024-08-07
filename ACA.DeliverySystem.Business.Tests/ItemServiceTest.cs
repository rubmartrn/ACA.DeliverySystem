using Moq;
using ACA.DeliverySystem.Business.Services;
using ACA.DeliverySystem.Data;
using ACA.DeliverySystem.Data.Repository;
using ACA.DeliverySystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACA.DeliverySystem.Business.Tests
{
    public class ItemServiceTest
    {
        private Mock<Order> _mockOrder = new Mock<Order>();
        private Mock<IItemRepository> _iItemRepositoryMock = new Mock<IItemRepository>();
        private Mock<IUnitOfWork> _uowMock = new Mock<IUnitOfWork>();
        private IItemService service => new ItemService(_uowMock.Object);

        [Fact]

        public async Task CreateItem_Success() 
        {
            //Arrange
            var items = new List<Item>();
            var item = new Item { Id = 1, OrderId = 1, Name = "fast food", Description = "5 pies", Price = 25, Order = _mockOrder.Object };
            _iItemRepositoryMock.Setup(e => e.GetAllItem(It.IsAny<CancellationToken>())).ReturnsAsync(items);
            _uowMock.Setup(u => u.ItemRepository).Returns(_iItemRepositoryMock.Object);
            //Act
            await service.CreateItem(item, CancellationToken.None);
            //Assert
            _iItemRepositoryMock.Verify(m => m.Add(It.Is<Item>(c => c == item), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Delete_Success()
        {
            //Arrange
            var items = new List<Item>
            {
                new Item { Id = 1, OrderId = 1, Name = "fast food", Description = "5 pies", Price = 25, Order = _mockOrder.Object },
                new Item { Id = 2, OrderId = 1, Name = "Donar", Description = "6 pies", Price = 35, Order = _mockOrder.Object },
            };
            _iItemRepositoryMock.Setup(e => e.GetAllItem(It.IsAny<CancellationToken>())).ReturnsAsync(items);
            _uowMock.Setup(u => u.ItemRepository).Returns(_iItemRepositoryMock.Object);
            //Act
            await service.Delete(1, CancellationToken.None);
            _iItemRepositoryMock.Verify(m => m.Delete(It.Is<int>(c => c == 1), It.IsAny<CancellationToken>()), Times.Once());
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
            _iItemRepositoryMock.Setup(e => e.GetAllItem(It.IsAny<CancellationToken>())).ReturnsAsync(items);
            _uowMock.Setup(u => u.ItemRepository).Returns(_iItemRepositoryMock.Object);
            //Act
            var result=await service.GetAllItems(CancellationToken.None);
            //Assert
            Assert.Equal(2, result.Count());

        }

        [Fact]
        public async Task GetItemById_Success()
        {
            //Arrange
            var item = new Item { Id = 1, OrderId = 1, Name = "fast food", Description = "5 pies", Price = 25, Order = _mockOrder.Object };
            _iItemRepositoryMock.Setup(e => e.GetItemById(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(item);

            _uowMock.Setup(u => u.ItemRepository).Returns(_iItemRepositoryMock.Object);
            // Act
            await service.GetItemById(1, CancellationToken.None);

            //Assert
            _iItemRepositoryMock.Verify(m => m.GetItemById(It.Is<int>(c => c == 1), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Update() 
        {
            //Arrange
            var items = new List<Item>
            {
                new Item { Id = 1, OrderId = 1, Name = "fast food", Description = "5 pies", Price = 25, Order = _mockOrder.Object },
                new Item { Id = 2, OrderId = 1, Name = "Donar", Description = "6 pies", Price = 35, Order = _mockOrder.Object },
            };
            _iItemRepositoryMock.Setup(e => e.GetAllItem(It.IsAny<CancellationToken>())).ReturnsAsync(items);
            _uowMock.Setup(u => u.ItemRepository).Returns(_iItemRepositoryMock.Object);
            var currentItem = await service.GetItemById(1,CancellationToken.None);
            //Act

            await service.Update(currentItem, CancellationToken.None);
            //Assert
            _iItemRepositoryMock.Verify(m => m.Update(It.Is<Item>(c => c == currentItem), It.IsAny<CancellationToken>()), Times.Once);

        }
    }
}
