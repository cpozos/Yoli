namespace Yoli.Core.Domain
{
    public class Email : IEquatable<string>
    {
        public string Address { get; private set; }

        public Email(string address)
        {
            Address = address;

            //TODO: Validations
        }

        public bool Equals(string? other)
            => Address == other;
    }
}