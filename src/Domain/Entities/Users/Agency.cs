namespace Yoli.Core.Domain.Entities
{
    public class Agency : IUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Email Email { get; set; }
        public string AgencyName { get; set; }
        public Person Representant { get; set; }
        public IEnumerable<Person> Integrants { get; set; }
        public AgencyContactInfo ContactInformation { get; set; }
    }
}