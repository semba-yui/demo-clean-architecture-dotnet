using DemoCompany.DemoCleanArchitecture.Domain.Entities;

namespace DemoCompany.DemoCleanArchitecture.Application.Interfaces;

public interface IJwtProvider
{
    /// <summary>
    ///     2段階認証までの一時的な JWT を生成
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    string GeneratePartialToken(UserEntity user);

    /// <summary>
    ///     2段階認証後の最終的な JWT を生成
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    string GenerateFinalToken(UserEntity user);
}
