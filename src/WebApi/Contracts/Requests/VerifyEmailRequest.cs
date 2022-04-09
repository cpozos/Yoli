namespace Yoli.Core.WebApi.Requests
{
    public class VerifyEmailRequest
    {
        public string userId { get; set; }
        public string code { get; set; }
    }
}
