using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RMPortal.WebServer.Helpers;
using RMPortal.WebServer.Models;
using RMPortal.WebServer.Models.Sys;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RMPortal.WebServer.Authorization
{
    public interface IJwtUtils
    {
        public string GenerateJwtToken(User user);
        public int? ValidateJwtToken(string token);
    }
    public class JwtUtils : IJwtUtils
    {
        private readonly AppSettings _appSettings;
        public JwtUtils(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            if (string.IsNullOrEmpty(_appSettings.Secret))
            {
                throw new Exception("JWT secret not configured");
            }
        }
        public string GenerateJwtToken(User user)
        {
            //generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key=Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public int? ValidateJwtToken(string token)
        {
            if (token == null)
                return null;

            var tokenHandler=new JwtSecurityTokenHandler();
            var key=Encoding.ASCII.GetBytes(_appSettings.Secret);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);
                var jwtoken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtoken.Claims.First(x => x.Type == "id").Value);
                return userId;

            }
            catch
            {
                return null;
            }
        }
    }
}
