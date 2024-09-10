using System.Text.Json.Serialization;

namespace ACA.DeliverySystem.Data.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int? OrderId { get; set; }
        public string? ItemName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Item? Item { get; set; }

        [JsonIgnore]
        public Order? Order { get; set; }
    }
}
