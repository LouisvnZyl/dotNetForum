namespace Domain.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Age { get; set; } = string.Empty;

        public Car? Car { get; set; }

        public void AssignCar(Car newCar)
        {
            this.Car = newCar;
        }

        public void UpdateCredentials(string name, string surname)
        {
            this.Name = name;
            this.Surname = surname;
        }
    }
}
