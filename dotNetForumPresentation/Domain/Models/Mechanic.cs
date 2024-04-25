namespace Domain.Models
{
    public class Mechanic
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public DateTimeOffset DateOfBirth { get; set; }

        public List<Car> Cars { get; set; } = new List<Car>();

        //public List<CarMechanic> CarMechanics { get; set; } = new List<CarMechanic>();
    }
}
