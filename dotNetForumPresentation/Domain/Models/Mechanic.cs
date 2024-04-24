namespace Domain.Models
{
    public class Mechanic
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public DateTimeOffset DateOfBirth { get; set; }
    }
}
