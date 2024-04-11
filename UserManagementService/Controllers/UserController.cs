using Microsoft.AspNetCore.Mvc;
using System.Net;
using UserManagementService.Interface;
using UserManagementService.Models;

namespace UserManagementService.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUser _user;
    public UserController(IUser user)
    {
        this._user = user;
    }

    [HttpPost("Signup")]
    public async Task<IActionResult> SignUp(SignupModel user)
    {
        if (!string.IsNullOrWhiteSpace(user?.UserName) && !string.IsNullOrWhiteSpace(user?.Password))
        {
            bool isUserExist = await _user.IsUserExist(user.UserName);
            if (!isUserExist)
            {
                bool result = await _user.CreateUser(user);
                if (result)
                {
                    return Ok(new CommonResponse
                    {
                        StatusCode = 200,
                        Message = "User Created successfully"
                    });
                }
            }
            return Ok(new CommonResponse
            {
                StatusCode = 200,
                Message = "User alread exist"
            });
        }
        return BadRequest();
    }
}
