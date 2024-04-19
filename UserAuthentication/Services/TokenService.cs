using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserAuthentication.Interfaces;

namespace UserAuthentication.Services;

public class TokenService : IToken
{
    private readonly IConfiguration _configuration;
    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <summary>
    ///  Takes the required params and return JWT token
    /// </summary>
    /// <param name="key"></param>
    /// <param name="issuer"></param>
    /// <param name="audience"></param>
    /// <param name="userName"></param>
    /// <param name="userId"></param>
    /// <returns>JWT token</returns>
    public string BuildToken(string key, string issuer, IEnumerable<string> audience, string userName, long userId)
    {
        var claims = new List<Claim>
            {
                new Claim("userName", !string.IsNullOrWhiteSpace(userName) ? userName : string.Empty),
                new Claim("userId", !string.IsNullOrWhiteSpace(userId.ToString()) ? userId.ToString() : string.Empty),
            };

        claims.AddRange(audience.Select(aud => new Claim(JwtRegisteredClaimNames.Aud, aud)));

        var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new JwtSecurityToken(issuer, issuer, claims, notBefore: DateTime.Now, expires: DateTime.Now.Add(new TimeSpan(24, 00, 00)),
            signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }
}
