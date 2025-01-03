using System.Net;
using System.Net.Mime;
using DemoCompany.DemoCleanArchitecture.Api.Forms.Responses.V1;
using DemoCompany.DemoCleanArchitecture.Application.Exceptions;
using DemoCompany.DemoCleanArchitecture.Application.Models;
using Serilog;

namespace DemoCompany.DemoCleanArchitecture.Api.Middlewares;

/// <summary>
///     例外処理を行うミドルウェア
/// </summary>
public class ExceptionHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (InvalidParameterException ipe)
        {
            Log.Error(ipe, ipe.Message);

            if (!context.Response.HasStarted)
            {
                await HandleInvalidParameterExceptionAsync(context: context, errorCode: ipe.ErrorCode,
                    errorMessage: ipe.Message);
            }
        }
        catch (Exception e)
        {
            Log.Error(e, e.Message);

            if (!context.Response.HasStarted)
            {
                await HandleExceptionAsync(context, e.Message);
            }
        }
    }

    /// <summary>
    ///     パラメータが不正な場合の例外を処理
    /// </summary>
    /// <param name="context"></param>
    /// <param name="errorCode"></param>
    /// <param name="errorMessage"></param>
    /// <returns></returns>
    private static Task HandleInvalidParameterExceptionAsync(HttpContext context, string errorCode, string errorMessage)
    {
        context.Response.ContentType = MediaTypeNames.Application.Json;
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

        var response = new ErrorResponse { Code = errorCode, Message = errorMessage };

        return context.Response.WriteAsJsonAsync(response);
    }

    /// <summary>
    ///     その他の例外を処理
    /// </summary>
    /// <param name="context"></param>
    /// <param name="errorMessage"></param>
    /// <returns></returns>
    private static Task HandleExceptionAsync(HttpContext context, string errorMessage)
    {
        context.Response.ContentType = MediaTypeNames.Application.Json;
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = new ErrorResponse { Code = ErrorCodes.InternalError, Message = errorMessage };

        return context.Response.WriteAsJsonAsync(response);
    }
}
