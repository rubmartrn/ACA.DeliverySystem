namespace ACA.DeliverySystem.Business.Models
{
    public class OrderItemUpdateModel
    {
        public int ItemId { get; set; }
        public string? ItemName { get; set; }
        public int OrderId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public ItemUpdateModel? Item { get; set; }
    }
}
