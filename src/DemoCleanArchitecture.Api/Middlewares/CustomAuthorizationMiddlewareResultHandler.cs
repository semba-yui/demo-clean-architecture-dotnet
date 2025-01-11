using DemoCompany.DemoCleanArchitecture.Api.Forms.Responses.V1;
using DemoCompany.DemoCleanArchitecture.Application.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;

namespace DemoCompany.DemoCleanArchitecture.Api.Middlewares;

public sealed class CustomAuthorizationMiddlewareResultHandler
    : IAuthorizationMiddlewareResultHandler
{
    private readonly AuthorizationMiddlewareResultHandler _defaultHandler
        = new();

    public async Task HandleAsync(
        RequestDelegate next,
        HttpContext context,
        AuthorizationPolicy policy,
        PolicyAuthorizationResult authorizeResult)
    {
        // デフォルトの認可結果を処理
        await _defaultHandler.HandleAsync(next, context, policy, authorizeResult);

        // 認可失敗だったらレスポンス書き換え
        if (!authorizeResult.Succeeded)
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            context.Response.ContentType = "application/json";

            var result = new ErrorResponse
            {
                Code = ErrorCodes.Unauthorized, Message = "You are not authorized to access this resource."
            };
            await context.Response.WriteAsJsonAsync(result);
        }
    }
}
