namespace Domain.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Brand { get; set; } = string.Empty;
        public Engine Engine { get; set; } = null!;
        public Manufacturer Manufacturer { get; set; } = null!;
    }
}
