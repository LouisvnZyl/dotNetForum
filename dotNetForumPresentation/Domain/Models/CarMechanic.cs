namespace Domain.Models
{
    public class CarMechanic
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public int MechanicId { get; set; }

        public Car Car { get; set; } = null!;
        public Mechanic Mechanic { get; set; } = null!;
    }
}
