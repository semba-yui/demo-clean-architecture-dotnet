namespace DemoCompany.DemoCleanArchitecture.Application.Services;

public class LoginService
{
    /// <summary>
    ///     6桁の数字をランダム生成する (000000 ~ 999999)
    /// </summary>
    private static string Generate6DigitCode()
    {
        var number = new Random().Next(0, 1000000);
        return number.ToString("D6");
    }
}
