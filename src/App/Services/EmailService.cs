namespace Yoli.Core.App.Services
{
    public class EmailService : IEmailService
    {
        public Task<bool> SendAsync(string mailTo, string subject, string message, bool isHtml = false)
        {
            throw new NotImplementedException();
        }
    }
}
