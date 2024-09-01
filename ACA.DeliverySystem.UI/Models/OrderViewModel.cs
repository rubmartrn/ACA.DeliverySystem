namespace ACA.DeliverySystem.UI.Models
{
    public class OrderViewModel
    {
        public string? Name { get; set; }
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateOnly Date { get; set; }

        public decimal PaidAmount { get; set; }
        public decimal AmountToPay { get; set; }

        public ProgressEnum ProgressEnum { get; set; }
        public List<ItemViewModel> Items { get; set; } = new List<ItemViewModel>();

    }
}
