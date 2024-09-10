namespace ACA.DeliverySystem.Business.Models
{
    public class OrderUpdateModelDTO
    {
        public int UserId { get; set; }
        public int OrderItemId { get; set; }
        public ProgressEnum ProgressEnum { get; set; }
    }
}
