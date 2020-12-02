using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SqlSugarDemo.Api.JwtAuth;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SqlSugarDemo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;

        public AuthorizeController(IOptions<JwtSettings> options) 
        {
            _jwtSettings = options.Value;
        }

        [HttpPost]
        public string GetToken(string userName, string password) 
        {
            var claims = new Claim[] { 
                new Claim(ClaimTypes.Name,userName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecurityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                DateTime.Now,
                DateTime.Now.AddMinutes(20),
                creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
