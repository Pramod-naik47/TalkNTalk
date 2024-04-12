using Microsoft.AspNetCore.Mvc;
using TokenAuthentication.Models;
using UserAuthentication.Interfaces;
using UserAuthentication.Models;
using UserManagementService.Models;

namespace UserAuthentication.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private  IUser _user { get; }
    private IToken _token { get; }
    private readonly IConfiguration _configuration;
    public UserController(IUser user, IToken token, IConfiguration configuration)
    {
        _user = user;
        _token = token;
        _configuration = configuration;
    }
    /// <summary>
    ///  Takes the user name and password, verify if user exist or not, if exist validate user and generate token.
    /// </summary>
    /// <returns>Token if login sucessfull.</returns>
    [HttpPost("Login")]
    public async Task<IActionResult> Login(Login login)
    {
        User? isUser = await _user.Login(login);
        if (isUser is not null)
        {
            string token = _token.BuildToken(_configuration["Jwt:Key"],
                                            _configuration["Jwt:Issuer"],
                                            new[]
                                            {
                                            _configuration["Jwt:Aud1"],
                                            _configuration["Jwt:Aud2"],
                                            _configuration["Jwt:Aud3"]
                                            },
                                            isUser.UserName,
                                            isUser.UserId);
            var result = new TokenModel
            {
                Token = token,
                IsAuthenticated = true,
                Message = "Login successfull"
            };
            return Ok(result);
        }
        else
        {
            return Ok(new CommonResponse
            {
                StatusCode = 200,
                Message = "User or password incorrect"
            });
        }
    }
}
