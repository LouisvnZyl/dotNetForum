namespace Domain.Models
{
    public class Car
    {
        // This is how we map if you want to share a primary key value

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }

        public Manufacturer Manufacturer { get; set; } = null!;
        public Customer? Customer { get; set; }

        public int EngineId { get; set; }
        public Engine Engine { get; set; } = null!;

        public List<Mechanic> Mechanics { get; set; } = new List<Mechanic>();
    }
}
