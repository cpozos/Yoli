using Yoli.Domain.Entities;

namespace Yoli.WebApi.Responses
{
    public class SigninResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public SigninResponse(IUser user)
        {
            Id = user.Id;
            Name = user.Name;
        }
    }
}