using Yoli.Domain.Entities;

namespace Yoli.WebApi.Contracts.Responses;

public class SigninResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Token { get; set; }

    public SigninResponse(IUser user, string token)
    {
        Id = user.Id;
        Name = user.Name;
        Token = token;
    }
}