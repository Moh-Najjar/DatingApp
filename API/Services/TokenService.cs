
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using API.Interfaces;
using API.Entities;
using System.Text;

namespace API.Services;

public class TokenService(IConfiguration configuration) : ITokenService
{
  public string CreateToken(AppUser user)
  {
    var tokenKey = configuration["TokenKey"] ?? throw new Exception("Cannot acess tokenKey app appsettings.json");
    if (tokenKey.Length < 64) throw new Exception("TokenKey must be at least 64 characters long");

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

    var claims = new List<Claim>
      {
        new(ClaimTypes.Name, user.UserName)
      };

    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Subject = new ClaimsIdentity(claims),
      Expires = DateTime.UtcNow.AddDays(7),
      SigningCredentials = creds
    };

    var tokenHandler = new JwtSecurityTokenHandler();

    var token = tokenHandler.CreateToken(tokenDescriptor);

    return tokenHandler.WriteToken(token);
  }
}
