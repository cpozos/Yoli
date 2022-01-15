namespace Yoli.Core.Domain.Entities
{
    public class User : Person, IUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public Email Email { get; set; }
        public PersonContactInfo ContactInformation { get; set; }
    }
}