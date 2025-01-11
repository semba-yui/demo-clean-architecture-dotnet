using System.Net.Mime;
using DemoCompany.DemoCleanArchitecture.Api.Forms.Responses.V1.User.GetUser;
using DemoCompany.DemoCleanArchitecture.Application.Services.Users;
using DemoCompany.DemoCleanArchitecture.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoCompany.DemoCleanArchitecture.Api.Controllers.V1;

/// <summary>
///     ユーザーコントローラー
/// </summary>
public sealed class UserController(GetUserService getUserService, DeleteUserService deleteUserService)
    : DemoCleanArchitectureBaseController
{
    [Authorize(Policy = PermissionNames.UserRead)]
    [HttpGet("{id:int}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType<GetUserResponse>(StatusCodes.Status200OK)]
    public async Task<ActionResult<GetUserResponse>> GetUser(int id)
    {
        var user = await getUserService.ExecuteAsync(id);

        var response = new GetUserResponse
        {
            userName = user.userName,
            email = user.email,
            emailConfirmed = user.emailConfirmed,
            twoFactorEnabled = user.twoFactorEnabled,
            accessFailedCount = user.accessFailedCount,
            lockoutEnd = user.lockoutEnd,
            isDeleted = user.isDeleted
        };

        return Ok(response);
    }

    /// <summary>
    ///     ユーザー削除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize(Policy = PermissionNames.UserDelete)]
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> Delete(int id)
    {
        await deleteUserService.ExecuteAsync(id);

        // 204 No Content
        return NoContent();
    }
}
