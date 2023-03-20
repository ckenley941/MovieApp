using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MovieApp.Api.Common;
using MovieApp.Api.Data.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MovieApp.Api.Controllers
{
    /// <summary>
    /// Authentication Controller with Login functionality to turn a user name and password into a JWT token 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public AuthenticationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLogin user)
        {
            if (user is null)
            {
                return BadRequest(AppConstants.Messages.InvalidUser);
            }

            //Hard coding login username and password - ultimately would want to check against some Identity Server
            if (user.UserName == "Admin" && user.Password == "Admin")
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration[AppConstants.Configurations.JWTSecret]));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(
                    issuer: _configuration[AppConstants.Configurations.JWTValidIssue],
                    audience: _configuration[AppConstants.Configurations.JWTValidAudience],
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(60),
                    signingCredentials: signinCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(new TokenResponse { Token = tokenString });
            }
            return Unauthorized();
        }
    }
}
