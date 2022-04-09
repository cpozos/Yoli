namespace Yoli.Core.App.Services
{
    public interface IEmailService
    {
        Task<bool> SendAsync(string mailTo, string subject, string message, bool isHtml = false);
    }
}
