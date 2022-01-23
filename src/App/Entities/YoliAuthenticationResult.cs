namespace Yoli.Core.App.Entities
{
    public class YoliAuthenticationResult
    {
        public bool Succeeded { get; }
        public string Token { get; }
        public YoliAuthenticationResult(bool succeeded, string token)
        {
            ArgumentNullException.ThrowIfNull(token);
            Succeeded = succeeded;
            Token = token;
        }
    }
}