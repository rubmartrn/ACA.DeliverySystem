namespace ACA.DeliverySystem_Api
{
    public class AppSettings
    {
        public string Password { get; set; } = default!;
        public string Name { get; set; } = default!;

        public int Version { get; set; } = default!;

        public EntitySettings EntitySettings { get; set; } = default!;
    }
}
