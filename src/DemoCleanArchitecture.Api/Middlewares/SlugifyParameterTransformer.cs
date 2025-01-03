using System.Text.RegularExpressions;

namespace DemoCompany.DemoCleanArchitecture.Api.Middlewares;

/// <summary>
///     パスパラメータをケバブ形式に変換するためのパラメータトランスフォーマー
/// </summary>
public class SlugifyParameterTransformer : IOutboundParameterTransformer
{
    /// <summary>
    ///     パスパラメータをケバブ形式に変換する
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public string? TransformOutbound(object? value)
    {
        if (value is null)
        {
            return null;
        }

        // 信頼できない入力を受け取る場合は、正規表現のタイムアウトを設定する
        return Regex.Replace(
                value.ToString()!,
                "([a-z])([A-Z])",
                "$1-$2",
                RegexOptions.CultureInvariant,
                TimeSpan.FromMilliseconds(100))
            .ToLowerInvariant();
    }
}
