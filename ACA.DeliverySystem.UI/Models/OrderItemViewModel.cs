using System.Text.Json.Serialization;

namespace ACA.DeliverySystem.UI.Models
{
    public class OrderItemViewModel
    {
        [JsonPropertyName("itemId")]
        public int ItemId { get; set; }

        [JsonPropertyName("itemName")]
        public string? ItemName { get; set; }
        [JsonPropertyName("orderId")]
        public int OrderId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public ItemViewModel? Item { get; set; }
    }
}
