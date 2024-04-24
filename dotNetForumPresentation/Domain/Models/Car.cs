namespace Domain.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Brand { get; set; } = string.Empty;
        //public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;


        //public Engine Engine { get; set; } = null!;
        //public Manufacturer Manufacturer { get; set; } = null!;
    }
}
