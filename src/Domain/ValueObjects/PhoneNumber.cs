namespace Yoli.Domain.ValueObjects
{
    public class PhoneNumber
    {
        public string Number { get; set; }
        public bool IsAvailable { get; set; }
        public int UserId { get; set; }
    }
}