namespace ACA.DeliverySystem.Business.Models
{
    public class ItemViewModelDTO
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
    }
}
