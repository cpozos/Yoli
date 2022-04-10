using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Yoli.Core.App.Services;
using Yoli.Core.Domain.Entities;

namespace Yoli.Core.WebApi.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class YoliAuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
    {
        private readonly ITokenService _tokenService;
        private readonly JsonResult _unauthorizedResult = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
        public YoliAuthorizeAttribute(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.Items["User"] as IUser;
            if (user == null)
            {
                // not logged in
                context.Result = _unauthorizedResult;
                return;
            }

            var auth = context.HttpContext.Request.Headers.Authorization[0];
            if (await ValidateHeaderAsync(auth))
            {
                context.Result = _unauthorizedResult;
                return;
            }

            var token = auth.Split()[1];
            if (await _tokenService.ValidateEmailConfirmationTokenAsync(token))
            {
                context.Result = _unauthorizedResult;
            }
        }

        private async Task<bool> ValidateHeaderAsync(string? authHeaderValue)
        {
            var result = await Task.Run(() =>
            {
                if (string.IsNullOrWhiteSpace(authHeaderValue) || !authHeaderValue.StartsWith("Bearer"))
                    return false;

                var words = authHeaderValue.Split("", StringSplitOptions.None);
                if (words.Count() != 1)
                    return false;

                return true;
            });

            return result;
        }
    }
}