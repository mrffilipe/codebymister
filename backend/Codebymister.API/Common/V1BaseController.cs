using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Codebymister.API.Common;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public abstract class V1BaseController : ControllerBase
{
}
