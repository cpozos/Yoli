namespace Yoli.Core.Domain.ValueObjects
{
    public class EmailAddress
    {
        public string Email { get; set; }
        public bool IsVerified { get; set; }

        public EmailAddress() { }
        public EmailAddress(string email, bool isVerified = false)
        {
            Email = email;
            IsVerified = false;
        }
    }
}