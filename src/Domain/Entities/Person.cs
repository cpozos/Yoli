namespace Yoli.Core.Domain
{
    public class Person : User
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public DateOnly BirthDay { get; set; }
    }
}