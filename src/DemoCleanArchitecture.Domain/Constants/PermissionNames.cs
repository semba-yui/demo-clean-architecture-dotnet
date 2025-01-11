namespace DemoCompany.DemoCleanArchitecture.Domain.Constants;

/// <summary>
///     権限名
/// </summary>
public static class PermissionNames
{
    // --------------------------------------------------
    //  ユーザー
    // --------------------------------------------------
    /// <summary>
    ///     ユーザー作成
    /// </summary>
    public const string UserCreate = "USER_CREATE";

    /// <summary>
    ///     ユーザー読み取り
    /// </summary>
    public const string UserRead = "USER_READ";

    /// <summary>
    ///     ユーザー更新
    /// </summary>
    public const string UserUpdate = "USER_UPDATE";

    /// <summary>
    ///     ユーザー削除
    /// </summary>
    public const string UserDelete = "USER_DELETE";

    // --------------------------------------------------
    //  ロール
    // --------------------------------------------------
    /// <summary>
    ///     ロール作成
    /// </summary>
    public const string RoleCreate = "ROLE_CREATE";

    /// <summary>
    ///     ロール読み取り
    /// </summary>
    public const string RoleRead = "ROLE_READ";

    /// <summary>
    ///     ロール更新
    /// </summary>
    public const string RoleUpdate = "ROLE_UPDATE";

    /// <summary>
    ///     ロール削除
    /// </summary>
    public const string RoleDelete = "ROLE_DELETE";

    // --------------------------------------------------
    //  権限
    // --------------------------------------------------
    /// <summary>
    ///     権限作成
    /// </summary>
    public const string PermissionCreate = "PERMISSION_CREATE";

    /// <summary>
    ///     権限読み取り
    /// </summary>
    public const string PermissionRead = "PERMISSION_READ";

    /// <summary>
    ///     権限更新
    /// </summary>
    public const string PermissionUpdate = "PERMISSION_UPDATE";

    /// <summary>
    ///     権限削除
    /// </summary>
    public const string PermissionDelete = "PERMISSION_DELETE";
}
