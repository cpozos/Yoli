namespace Domain.Entities
{
    public class Experience
    {
        public string Name { get; set; }
        public Journey Outward { get; set; }
        public Journey Return { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}