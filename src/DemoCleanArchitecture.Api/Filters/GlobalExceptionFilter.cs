using System.Net;
using DemoCompany.DemoCleanArchitecture.Api.Forms.Responses.V1;
using DemoCompany.DemoCleanArchitecture.Application.Exceptions;
using DemoCompany.DemoCleanArchitecture.Application.Models;
using DemoCompany.DemoCleanArchitecture.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace DemoCompany.DemoCleanArchitecture.Api.Filters;

/// <inheritdoc />
public sealed class GlobalExceptionFilter : IExceptionFilter
{
    /// <summary>
    ///     デフォルトのエラーコード
    /// </summary>
    private const string DefaultErrorCode = ErrorCodes.InternalError;

    /// <summary>
    ///     デフォルトのエラーメッセージ
    /// </summary>
    private const string DefaultErrorMessage = "Internal Server Error";

    /// <summary>
    ///     デフォルトのステータスコード
    /// </summary>
    private const HttpStatusCode DefaultStatusCode = HttpStatusCode.InternalServerError;

    /// <inheritdoc />
    public void OnException(ExceptionContext context)
    {
        var ex = context.Exception;
        var errorCode = DefaultErrorCode;
        var statusCode = DefaultStatusCode;
        var message = DefaultErrorMessage;

        switch (ex)
        {
            case AppException appEx:
                errorCode = appEx.ErrorCode;
                message = appEx.Message;
                statusCode = MapToHttpStatus(errorCode);
                break;
            case DomainException domainEx:
                message = domainEx.Message;
                statusCode = MapToHttpStatus(errorCode);
                break;
        }

        Log.Warning(ex, message);

        context.HttpContext.Response.StatusCode = (int)statusCode;
        context.Result = new JsonResult(new ErrorResponse { Code = errorCode, Message = message });
        context.ExceptionHandled = true;
    }

    /// <summary>
    ///     エラーコードをHTTPステータスコードにマッピング
    /// </summary>
    /// <param name="errorCode"></param>
    /// <returns></returns>
    private static HttpStatusCode MapToHttpStatus(string errorCode)
    {
        return errorCode switch
        {
            ErrorCodes.InvalidParameter => HttpStatusCode.BadRequest,
            ErrorCodes.Unauthorized => HttpStatusCode.Unauthorized,
            ErrorCodes.AccessDenied => HttpStatusCode.Forbidden,
            ErrorCodes.ResourceNotFound => HttpStatusCode.NotFound,
            ErrorCodes.ResourceConflict => HttpStatusCode.Conflict,
            ErrorCodes.ResourceLocked => HttpStatusCode.Locked,
            ErrorCodes.InternalError => HttpStatusCode.InternalServerError,
            _ => HttpStatusCode.InternalServerError
        };
    }
}
