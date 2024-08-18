﻿using ACA.DeliverySystem.Business.Models;
using ACA.DeliverySystem.Data.Models;

namespace ACA.DeliverySystem.Business.Services
{
    public interface IOrderService
    {
        Task AddItemInOrder(int orderId, int itemId, CancellationToken token);
        Task<Order> CreateOrder(OrderAddModel order, CancellationToken token);
        Task Delete(int id, CancellationToken token);
        Task<OrderViewModel> Get(int id, CancellationToken token);
        Task<IEnumerable<OrderViewModel>> GetAll(CancellationToken token);
        Task<bool> Update(int id, Order model, CancellationToken token);
    }
}