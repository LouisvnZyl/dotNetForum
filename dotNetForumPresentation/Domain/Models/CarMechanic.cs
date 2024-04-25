namespace Domain.Models
{
    public class CarMechanic
    {
        public int CarId { get; set; }
        public int MechanicId { get; set; }
        public bool Completed { get; set; }

        public Car Car { get; set; } = null!;
        public Mechanic Mechanic { get; set; } = null!;
    }
}
