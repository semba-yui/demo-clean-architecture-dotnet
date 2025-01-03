using DemoCompany.DemoCleanArchitecture.Api.Forms.Responses.V1;
using DemoCompany.DemoCleanArchitecture.Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace DemoCompany.DemoCleanArchitecture.Api.Filters;

/// <inheritdoc />
public sealed class CustomValidationFilter : ActionFilterAttribute
{
    /// <inheritdoc />
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ModelState.IsValid)
        {
            return;
        }

        var errors = context.ModelState
            .Where(e => e.Value?.Errors.Count > 0)
            .ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value?.Errors.Select(e => e.ErrorMessage).ToArray()
            );

        // Logging errors
        Log.Error("Model validation error: {@Errors}", errors);

        var response = new ErrorResponse { Code = ErrorCodes.InvalidParameter, Message = "Invalid parameter" };

        context.Result = new BadRequestObjectResult(response);
    }
}
