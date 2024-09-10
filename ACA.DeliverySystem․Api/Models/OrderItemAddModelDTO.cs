using ACA.DeliverySystem.Business.Models;

namespace ACA.DeliverySystem_Api.Models
{
    public class OrderItemAddModelDTO
    {
        public int ItemId { get; set; }
        public string? ItemName { get; set; }
        public int OrderId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public ItemAddModelDTO? Item { get; set; }
        public OrderAddModelDTO? Order { get; set; }
    }

}
