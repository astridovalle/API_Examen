using API_Examen.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_Examen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        public UserManager<User> userManager;
        public RoleManager<IdentityRole> roleManager;
        private IConfiguration _config;
        public AuthController(UserManager<User> _userManager, IConfiguration config, RoleManager<IdentityRole> _roleManager)
        {
            this.userManager = _userManager;
            this._config = config;
            this.roleManager = _roleManager;
        }

        [HttpPost("GetToken")]
        public async Task<IActionResult> Index(string UserName, string Password)
        {
            var user = await userManager.FindByNameAsync(UserName);

            if (user is null || !await userManager.CheckPasswordAsync(user, Password))
            {
                return StatusCode(403, "Acceso denegado.");
            }

            var roles = await userManager.GetRolesAsync(user);

            // Generamos un token según los claims
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Sid, user.Id),
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(ClaimTypes.GivenName, $"{user.FirstName} {user.LastName}")
    };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(720),
                signingCredentials: credentials);

            var jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

            return Ok(new
            {
                AccessToken = jwt
            });
        }
    }
}

