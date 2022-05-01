namespace Yoli.App.Entities
{
    public class YoliAuthenticationResult
    {
        public bool Succeeded { get; }
        public string Token { get; }
        public YoliAuthenticationResult(string token = null)
        {
            Token = token ?? string.Empty;
            Succeeded = !string.IsNullOrEmpty(token);
        }
    }
}