namespace ACA.DeliverySystem.Business.Models
{
    public class OrderViewModelDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ItemId { get; set; }
        public DateOnly Date { get; set; }

        public decimal PaidAmount { get; set; }

        public ProgressEnum ProgressEnum { get; set; }
        public List<ItemViewModelDTO> Items { get; set; } = new List<ItemViewModelDTO>();

    }
}
