using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace DemoCompany.DemoCleanArchitecture.Api.Controllers.V1;

[ApiVersion("1.0")]
[ApiController]
[Route("api/demo-clean-architecture/v{version:apiVersion}/[controller]")]
public abstract class DemoCleanArchitectureBaseController : ControllerBase;
