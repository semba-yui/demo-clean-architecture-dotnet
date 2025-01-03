namespace DemoCompany.DemoCleanArchitecture.Application.Models;

/// <summary>
///     システム全体で利用するエラーコードを集約するクラス
/// </summary>
public static class ErrorCodes
{
    /// <summary>
    ///     パラメータが不正
    /// </summary>
    public const string InvalidParameter = "INVALID_PARAMETER";

    /// <summary>
    ///     リソースの重複・衝突
    /// </summary>
    public const string ResourceConflict = "RESOURCE_CONFLICT";

    /// <summary>
    ///     リソースが見つからない
    /// </summary>
    public const string ResourceNotFound = "RESOURCE_NOT_FOUND";

    /// <summary>
    ///     リソースがロックされている
    /// </summary>
    public const string ResourceLocked = "RESOURCE_LOCKED";

    /// <summary>
    ///     認可エラー(権限不足)
    /// </summary>
    public const string AccessDenied = "ACCESS_DENIED";

    /// <summary>
    ///     認証エラー(未ログイン/トークン無効)
    /// </summary>
    public const string Unauthorized = "UNAUTHORIZED";

    /// <summary>
    ///     サーバー内部エラー
    /// </summary>
    public const string InternalError = "INTERNAL_ERROR";

    /// <summary>
    ///     登録失敗
    /// </summary>
    public const string RegistrationFailed = "REGISTRATION_FAILED";
}
