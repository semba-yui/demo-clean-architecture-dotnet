using System.Net.Mime;
using DemoCompany.DemoCleanArchitecture.Api.Forms.Requests.V1.Role.PostRole;
using DemoCompany.DemoCleanArchitecture.Api.Forms.Responses.V1.Role.GetRole;
using DemoCompany.DemoCleanArchitecture.Application.Services.Roles;
using DemoCompany.DemoCleanArchitecture.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoCompany.DemoCleanArchitecture.Api.Controllers.V1;

/// <summary>
///     ロールコントローラー
/// </summary>
/// <param name="getRoleService"></param>
/// <param name="addRoleService"></param>
/// <param name="deleteRoleService"></param>
/// <param name="updateRoleService"></param>
public sealed class RoleController(
    GetRoleService getRoleService,
    AddRoleService addRoleService,
    DeleteRoleService deleteRoleService,
    UpdateRoleService updateRoleService) : DemoCleanArchitectureBaseController
{
    /// <summary>
    ///     ロール取得
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize(Policy = PermissionNames.RoleRead)]
    [HttpGet("{id:int}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType<GetRoleResponse>(StatusCodes.Status200OK)]
    public async Task<ActionResult<GetRoleResponse>> Get(int id)
    {
        var role = await getRoleService.ExecuteAsync(id);

        var response = new GetRoleResponse { RoleName = role.roleName, Description = role.description };

        return Ok(response);
    }

    /// <summary>
    ///     ロール登録
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [Authorize(Policy = PermissionNames.RoleCreate)]
    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult> Post([FromBody] PostRoleRequest request)
    {
        var permissionId = await addRoleService.ExecuteAsync(request.RoleName, request.Description);

        // 201 Created
        return CreatedAtAction(nameof(Get), new { id = permissionId }, null);
    }

    /// <summary>
    ///     ロール更新
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [Authorize(Policy = PermissionNames.RoleUpdate)]
    [HttpPatch("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> Patch(int id, [FromBody] PatchRoleRequest request)
    {
        await updateRoleService.ExecuteAsync(id, request.RoleName, request.Description);

        // 204 No Content
        return NoContent();
    }

    /// <summary>
    ///     ロール削除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize(Policy = PermissionNames.RoleDelete)]
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> Delete(int id)
    {
        await deleteRoleService.ExecuteAsync(id);

        // 204 No Content
        return NoContent();
    }
}
