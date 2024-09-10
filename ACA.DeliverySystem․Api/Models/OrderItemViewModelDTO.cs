using ACA.DeliverySystem.Business.Models;

namespace ACA.DeliverySystem_Api.Models
{
    public class OrderItemViewModelDTO
    {
        public int ItemId { get; set; }
        public string? ItemName { get; set; }
        public int OrderId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public ItemViewModelDTO? Item { get; set; }
    }
}
