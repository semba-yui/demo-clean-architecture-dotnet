using System.Net.Mime;
using DemoCompany.DemoCleanArchitecture.Api.Forms.Requests.V1.Auth.UserRegister;
using DemoCompany.DemoCleanArchitecture.Api.Forms.Responses.V1.Auth.UserRegister;
using DemoCompany.DemoCleanArchitecture.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DemoCompany.DemoCleanArchitecture.Api.Controllers.V1;

public sealed class UserController(RegisterUserService registerUserService) : DemoCleanArchitectureBaseController
{
    [HttpPost("register")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<UserRegisterResponse>> Register([FromBody] UserRegisterRequest request)
    {
        // Application層のサービスを呼び出し、ユーザーを作成
        var userId = await registerUserService.ExecuteAsync(
            userName: request.UserName,
            email: request.Email,
            plainPassword: request.Password
        );

        // レスポンスオブジェクトを作成
        var response = new UserRegisterResponse { UserId = userId };

        // 201 Created
        return CreatedAtAction(nameof(GetUser), new { id = userId }, response);
    }

    [HttpGet("{id:int}")]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> GetUser(int id)
    {
        throw new NotImplementedException();
    }
}
