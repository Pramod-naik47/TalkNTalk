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

    /// <summary>
    /// Create new user
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userUpdate"></param>
    /// <returns></returns>
    [Authorize]
    [HttpPut("UpdateUser")]
    public async Task<IActionResult> UpdateUser([FromBody]UserUpdateModel userUpdate)
    {
        if (userUpdate is not null)
        {
            //Get the requested user first
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
