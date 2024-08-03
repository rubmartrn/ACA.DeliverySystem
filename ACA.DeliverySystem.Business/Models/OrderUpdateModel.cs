namespace ACA.DeliverySystem.Business.Models
{
    public class OrderUpdateModel
    {
        public int UserId { get; set; }
        public int ItemId { get; set; }
        public ProgressEnum ProgressEnum { get; set; }
    }
}
