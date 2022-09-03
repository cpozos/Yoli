using Microsoft.AspNetCore.Mvc;
using Yoli.Shared.Constants;
using Yoli.Domain.Entities;
using Yoli.WebApi.Authorization;

namespace Yoli.WebApi.Extensions;

public static class ControllerExtensions
{
    public static IUser? GetCurrentUser(this ControllerBase controller)
    {
        bool isAuthorized =
            controller.ControllerContext.ActionDescriptor.MethodInfo
                .GetCustomAttributes(typeof(YoliAuthorizeAttribute), false).Any();

        if (isAuthorized)
            return controller.HttpContext.Items[HttpContextItems.YoliUser] as IUser;

        return controller?.HttpContext?.Items[HttpContextItems.YoliUser] as IUser;
    }
}