namespace Yoli.Core.WebApi.Contracts
{
    public class YoliBasicSigninRequest : BaseRequest
    {
        public string SignInId { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}