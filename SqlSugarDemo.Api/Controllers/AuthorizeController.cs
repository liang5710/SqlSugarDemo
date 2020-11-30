using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SqlSugarDemo.Api.JwtAuth;

namespace SqlSugarDemo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        private readonly ILogger<AuthorizeController> _logger;
        private readonly JwtOptions _jwtOptions;

        public AuthorizeController(ILogger<AuthorizeController> logger, IOptions<JwtOptions> jwtOptions) 
        {
            _logger = logger;
            _jwtOptions = jwtOptions?.Value;
        }
        [HttpPost]
        public IActionResult Auth([FromForm] string username, [FromForm] string password)
        {
            // check account
            if (!username.StartsWith("Wilson") || password != "123456") return BadRequest("username or password invalide");

            //define claim 
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Email, $"{username}@github.com"),
                new Claim(ClaimTypes.Role, username == "WilsonPan" ? "Admin" : "Reader"),
                new Claim(ClaimTypes.Hash, JwtHashHelper.GetHashString($"{username}:{password}:{System.DateTime.Now.Ticks}")),
            };

            //define JwtSecurityToken
            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: System.DateTime.Now.AddMinutes(5),
                signingCredentials: _jwtOptions.SigningCredentials
            );

            // generate token
            var result = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(result);
        }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               
    }
}
