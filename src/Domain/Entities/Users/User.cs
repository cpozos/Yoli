namespace Yoli.Core.Domain.Entities
{
    public class User : Person, IUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public PersonContactInfo ContactInformation { get; set; }
    }
}