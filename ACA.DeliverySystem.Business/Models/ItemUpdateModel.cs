namespace ACA.DeliverySystem.Business.Models
{
    public class ItemUpdateModel
    {
        public int OrderId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
    }
}
