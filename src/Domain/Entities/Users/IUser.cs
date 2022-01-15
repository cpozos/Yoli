namespace Yoli.Core.Domain.Entities
{
    public interface IUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public Email Email { get; set; }
    }
}