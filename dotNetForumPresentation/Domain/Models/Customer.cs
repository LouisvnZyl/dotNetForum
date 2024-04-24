namespace Domain.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Age { get; set; } = string.Empty;

        public List<Car> Cars { get; set; } = new List<Car>();

        public void AddCar(Car newCar)
        {
            this.Cars.Add(newCar);
        }

        public void UpdateCredentials(string name, string surname)
        {
            this.Name = name;
            this.Surname = surname;
        }
    }
}
