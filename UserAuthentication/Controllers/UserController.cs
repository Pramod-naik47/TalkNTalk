using Microsoft.AspNetCore.Mvc;

namespace UserAuthentication.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet(Name ="Login")]
    public async Task<IActionResult> Login()
    {
        await Task.Delay(1000);
        return Ok();
    }
}
