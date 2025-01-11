using System.Net.Mime;
using DemoCompany.DemoCleanArchitecture.Api.Forms.Requests.V1.Permission.PostPermission;
using DemoCompany.DemoCleanArchitecture.Api.Forms.Responses.V1.Permission.GetPermission;
using DemoCompany.DemoCleanArchitecture.Application.Services.Permissions;
using DemoCompany.DemoCleanArchitecture.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoCompany.DemoCleanArchitecture.Api.Controllers.V1;

/// <summary>
///     権限コントローラー
/// </summary>
/// <param name="addPermissionService"></param>
/// <param name="getPermissionService"></param>
/// <param name="deletePermissionService"></param>
/// <param name="updatePermissionService"></param>
public sealed class PermissionController(
    AddPermissionService addPermissionService,
    GetPermissionService getPermissionService,
    DeletePermissionService deletePermissionService,
    UpdatePermissionService updatePermissionService) : DemoCleanArchitectureBaseController
{
    /// <summary>
    ///     権限取得
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize(Policy = PermissionNames.PermissionRead)]
    [HttpGet("{id:int}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType<GetPermissionResponse>(StatusCodes.Status200OK)]
    public async Task<ActionResult<GetPermissionResponse>> Get(int id)
    {
        var permission = await getPermissionService.ExecuteAsync(id);

        var response = new GetPermissionResponse
        {
            PermissionName = permission.permissionName, Description = permission.description
        };

        return Ok(response);
    }

    /// <summary>
    ///     権限登録
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [Authorize(Policy = PermissionNames.PermissionCreate)]
    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult> Post([FromBody] PostPermissionRequest request)
    {
        var permissionId = await addPermissionService.ExecuteAsync(request.PermissionName, request.Description);

        // 201 Created
        return CreatedAtAction(nameof(Get), new { id = permissionId }, null);
    }

    /// <summary>
    ///     権限更新
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [Authorize(Policy = PermissionNames.PermissionUpdate)]
    [HttpPatch("{id:int}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> Patch(int id, [FromBody] PatchPermissionRequest request)
    {
        await updatePermissionService.ExecuteAsync(id, request.PermissionName, request.Description);

        // 204 No Content
        return NoContent();
    }

    /// <summary>
    ///     権限削除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize(Policy = PermissionNames.PermissionDelete)]
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> Delete(int id)
    {
        await deletePermissionService.ExecuteAsync(id);

        // 204 No Content
        return NoContent();
    }
}
