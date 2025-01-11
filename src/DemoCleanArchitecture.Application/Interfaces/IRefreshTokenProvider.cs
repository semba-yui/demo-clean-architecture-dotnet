namespace DemoCompany.DemoCleanArchitecture.Application.Interfaces;

/// <summary>
///     リフレッシュトークンを生成するプロバイダ
/// </summary>
public interface IRefreshTokenProvider
{
    /// <summary>
    ///     リフレッシュトークンを生成する
    /// </summary>
    /// <returns></returns>
    string GenerateRefreshToken();
}
