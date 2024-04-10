using Microsoft.AspNetCore.Mvc;

namespace UserAuthentication.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name ="Login")]
    public async Task<IActionResult> Login()
    {
        await Task.Delay(1000);
        return Ok();
    }
}
