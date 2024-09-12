using ACA.DeliverySystem.Api.Models;

namespace ACA.DeliverySystem_Api.Models
{
    public class OrderItemUpdateModelDTO
    {
        public int ItemId { get; set; }
        public string? ItemName { get; set; }
        public int OrderId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public ItemUpdateModelDTO? Item { get; set; }
    }
}
