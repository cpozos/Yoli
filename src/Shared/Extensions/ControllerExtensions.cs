using Microsoft.AspNetCore.Mvc;
using Yoli.Shared.Constants;
using Yoli.Domain.Entities;

namespace Yoli.Shared.Extensions
{
    public static class ControllerExtensions
    {
        public static IUser? GetCurrentUser(this ControllerBase controller)
        {
            return controller?.HttpContext?.Items[HttpContextItems.YoliUser] as IUser;
        }
    }
}