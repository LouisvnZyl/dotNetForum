namespace Domain.Models
{
    public class CarMechanic
    {
        public int CarId { get; set; }
        public int MechanicId { get; set; }

        public Car Car { get; set; } = null!;
        public Mechanic MyProperty { get; set; } = null!;
    }
}
