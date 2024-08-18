using ACA.DeliverySystem.Data.Models;

namespace ACA.DeliverySystem.Data.Repository
{
    public interface IOrderRepository
    {
        Task<Order> Add(Order order, CancellationToken token);
        Task<OperationResult> AddItemInOrder(int orderId, int itemId, CancellationToken token);
        Task CancelOrder(int orderId, CancellationToken token);
        Task<OperationResult> Delete(int id, CancellationToken token);
        Task<IEnumerable<Order>> GetAll(CancellationToken token);

        Task<Order> GetById(int id, CancellationToken token);
        Task OrderCompleted(int orderId, CancellationToken token);
        Task PayForOrder(int orderId, decimal amount, CancellationToken token);
        Task<OperationResult> RemoveItemFromOrder(int orderId, int itemId, CancellationToken token);
        Task Update(Order order, CancellationToken token);
    }
}