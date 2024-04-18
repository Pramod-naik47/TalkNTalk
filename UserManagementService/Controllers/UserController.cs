using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagementService.Interface;
using UserManagementService.Models;

namespace UserManagementService.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUser _userService;
    public UserController(IUser user)
    {
        this._userService = user;
    }

    [HttpPost("Signup")]
    public async Task<IActionResult> SignUp(SignupModel user)
    {
        if (!string.IsNullOrWhiteSpace(user?.UserName) && !string.IsNullOrWhiteSpace(user?.Password))
        {
            bool isUserExist = await _userService.IsUserExist(user.UserName);
            if (!isUserExist)
            {
                bool result = await _userService.CreateUser(user);
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

    [Authorize]
    [HttpPut("UpdateUser")]
    public async Task<IActionResult> UpdateUser([FromBody]UserUpdateModel userUpdate)
    {
        if (userUpdate is not null)
        {
            User? user = await _userService.GetUserById(userUpdate.UserId);
            if (user is not null)
            {
                user.Name = userUpdate.Name;
                user.Password = !string.IsNullOrWhiteSpace(userUpdate.Password) ? BCrypt.Net.BCrypt.HashPassword(userUpdate.Password)  : user.Password;
                User? updatedUser = await _userService.UpdateUser(user);

                UserResponse userResponse  = new UserResponse
                {
                    UserId = updatedUser.UserId,
                    UserName = updatedUser.UserName,
                    Name = updatedUser.Name
                };

                return Ok(userResponse);
            } 
        }
        return BadRequest();
    }
}
