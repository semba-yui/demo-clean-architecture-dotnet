namespace DemoCompany.DemoCleanArchitecture.Domain.Constants;

/// <summary>
///     定数クラス
/// </summary>
public static class DemoCleanArchitectureConstants
{
    /// <summary>
    ///     一時的に発行される Role
    /// </summary>
    public const string LimitedAccessRole = "LimitedAccess";

    /// <summary>
    ///     2段階認証が完了しているかどうかを示す Claim
    /// </summary>
    public const string TwoFaVerifiedClaim = "twofa_verified";
}
