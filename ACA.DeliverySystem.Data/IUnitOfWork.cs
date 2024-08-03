using ACA.DeliverySystem.Data.Repository;

namespace ACA.DeliverySystem.Data
{
    public interface IUnitOfWork
    {
        IItemRepository ItemRepository { get; set; }
        IOrderRepository OrderRepository { get; set; }

        Task Save(CancellationToken token);
    }
}