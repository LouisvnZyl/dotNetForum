namespace Domain.Models
{
    public class Engine
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;
        public string FeulEfficiency { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
    }
}
