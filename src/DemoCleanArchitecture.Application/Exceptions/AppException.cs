namespace DemoCompany.DemoCleanArchitecture.Application.Exceptions;

/// <summary>
/// アプリケーション層で発生する例外の基底クラス
/// </summary>
[Serializable]
public class AppException : Exception
{
    /// <summary>
    /// プレゼンテーション層が返却するエラーコード
    /// </summary>
    public string ErrorCode { get; }

    public AppException(string errorCode, string message)
        : base(message)
    {
        ErrorCode = errorCode;
    }

    public AppException(string errorCode, string message, Exception innerException)
        : base(message, innerException)
    {
        ErrorCode = errorCode;
    }
}
