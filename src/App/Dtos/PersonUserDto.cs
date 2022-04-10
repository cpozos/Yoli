using Domain.ValueObjects;

namespace Yoli.Core.App.Dtos
{
    public class PersonUserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public BirthDay BirthDay { get; set; }
    }
}
