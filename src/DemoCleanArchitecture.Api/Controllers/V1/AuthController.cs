using System.Net.Mime;
using DemoCompany.DemoCleanArchitecture.Api.Forms.Requests.V1.Auth.Login;
using DemoCompany.DemoCleanArchitecture.Api.Forms.Requests.V1.Auth.Register;
using DemoCompany.DemoCleanArchitecture.Api.Forms.Requests.V1.Auth.TwoFactor.Issue;
using DemoCompany.DemoCleanArchitecture.Api.Forms.Requests.V1.Auth.TwoFactor.Verify;
using DemoCompany.DemoCleanArchitecture.Api.Forms.Responses.V1.Auth.Login;
using DemoCompany.DemoCleanArchitecture.Api.Forms.Responses.V1.Auth.Register;
using DemoCompany.DemoCleanArchitecture.Api.Forms.Responses.V1.Auth.TwoFactor.Issue;
using DemoCompany.DemoCleanArchitecture.Api.Forms.Responses.V1.Auth.TwoFactor.Verify;
using DemoCompany.DemoCleanArchitecture.Application.Services.Auths;
using DemoCompany.DemoCleanArchitecture.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoCompany.DemoCleanArchitecture.Api.Controllers.V1;

/// <summary>
///     認証コントローラー
/// </summary>
/// <param name="registerUserService"></param>
/// <param name="loginService"></param>
/// <param name="logoutService"></param>
/// <param name="issueTwoFactorCodeService"></param>
/// <param name="verifyTwoFactorCodeService"></param>
public sealed class AuthController(
    RegisterUserService registerUserService,
    LoginService loginService,
    LogoutService logoutService,
    IssueTwoFactorCodeService issueTwoFactorCodeService,
    VerifyTwoFactorCodeService verifyTwoFactorCodeService)
    : DemoCleanArchitectureBaseController
{
    /// <summary>
    ///     ユーザー登録
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("register")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType<UserRegisterResponse>(StatusCodes.Status200OK)]
    public async Task<ActionResult<UserRegisterResponse>> Register([FromBody] UserRegisterRequest request)
    {
        var userId = await registerUserService.ExecuteAsync(
            request.UserName,
            request.Email,
            request.Password
        );

        // レスポンスオブジェクトを作成
        var response = new UserRegisterResponse() { UserId = userId };

        // 200 OK
        return Ok(response);
    }

    /// <summary>
    ///     ログイン
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("login")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType<LoginResponse>(StatusCodes.Status200OK)]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
    {
        var loginDto = await loginService.ExecuteAsync(
            request.Email,
            request.Password
        );

        // レスポンスオブジェクトを作成
        var response = new LoginResponse
        {
            AccessToken = loginDto.AccessToken,
            RefreshToken = loginDto.RefreshToken,
            IsTwoFactorEnabled = loginDto.IsTwoFaRequired
        };

        // 200 OK
        return Ok(response);
    }

    /// <summary>
    ///     ログアウト
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [HttpPost("logout")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Logout()
    {
        // 200 OK
        return Ok();
    }

    /// <summary>
    ///     2段階認証コードを新たに発行する
    /// </summary>
    [Authorize(Roles = DemoCleanArchitectureConstants.LimitedAccessRole)]
    [HttpPost("two-factor/issue")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType<IssueResponse>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IssueResponse>> TwoFactorIssue([FromBody] IssueRequest request)
    {
        var result = await issueTwoFactorCodeService.ExecuteAsync(
            request.UserId
        );

        var response = new IssueResponse { Code = result.code, ExpiredAt = result.expirationDate };

        // TODO: API レスポンスにコードを含めるかは要検討(セキュリティ)
        // 通常は SMS/Email 送信で済ませ、ここでは 200 OK だけ返すケースも
        return Ok(response);
    }

    /// <summary>
    ///     発行済みの2段階認証コードを検証する
    /// </summary>
    [Authorize(Roles = DemoCleanArchitectureConstants.LimitedAccessRole)]
    [HttpPost("two-factor/verify")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType<VerifyResponse>(StatusCodes.Status200OK)]
    public async Task<ActionResult<VerifyResponse>> TwoFactorVerify([FromBody] VerifyRequest request)
    {
        var result = await verifyTwoFactorCodeService.ExecuteAsync(
            request.UserId,
            request.Code
        );

        var response = new VerifyResponse { AccessToken = result.AccessToken, RefreshToken = result.RefreshToken };

        return Ok(response);
    }
}
