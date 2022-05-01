namespace Yoli.Domain.ValueObjects
{
    public class YoliToken
    {
        public string Sub { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
