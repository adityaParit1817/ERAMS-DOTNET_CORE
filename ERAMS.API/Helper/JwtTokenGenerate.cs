using ERAMS.API.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ERAMS.API.Helper
{
    public class JwtTokenGenerate
    {
        private readonly IConfiguration _config;

        public JwtTokenGenerate(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(User user)
        {
            var Calims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString() ),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["JwtSettings:Key"])
                );
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["JwtSettings:Issuer"],
                audience: _config["JwtSettings:Audience"],
                claims: Calims,
                expires: DateTime.Now.AddMinutes(
                Convert.ToDouble(_config["JwtSettings:DurationInMinutes"])
            ),
                signingCredentials: creds

                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
