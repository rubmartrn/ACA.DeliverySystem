namespace ACA.DeliverySystem.Data.Models
{
    public class Order
    {
        public Order()
        {
            Date = DateOnly.FromDateTime(DateTime.Now);
        }

        public int Id { get; set; }

        public int? UserId { get; set; }
        public int? ItemId { get; set; }
        public DateOnly Date { get; private set; }

        public decimal PaidAmount { get; set; }

        public ProgressEnum ProgressEnum { get; set; }

        public List<Item> Items { get; set; } = new List<Item>();

        public List<User> Users { get; set; } = new List<User> { };

    }
}
