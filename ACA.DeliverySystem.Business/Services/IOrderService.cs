using ACA.DeliverySystem.Business.Models;
using ACA.DeliverySystem.Data;
using ACA.DeliverySystem.Data.Models;

namespace ACA.DeliverySystem.Business.Services
{
    public interface IOrderService
    {
        Task<OperationResult> AddItemInOrder(int orderId, int itemId, CancellationToken token);
        Task CancelOrder(int orderId, CancellationToken token);
        Task<Order> CreateOrder(OrderAddModel order, CancellationToken token);
        Task<OperationResult> Delete(int id, CancellationToken token);
        Task<OrderViewModel> Get(int id, CancellationToken token);
        Task<IEnumerable<OrderViewModel>> GetAll(CancellationToken token);
        Task OrderCompleted(int orderId, CancellationToken token);
        Task PayForOrder(int orderId, decimal amount, CancellationToken token);
        Task RemoveItemFromOrder(int orderId, int itemId, CancellationToken token);
        Task<bool> Update(int id, Order model, CancellationToken token);
    }
}