using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using demo.Data;
using demo.Models;
using Microsoft.EntityFrameworkCore;

namespace demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly string key = "THIS_IS_A_VERY_LONG_SECRET_KEY_1234567890!";
        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        // ================= REGISTER =================
        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }

        // ================= LOGIN =================
        [HttpPost("login")]
        public async Task<IActionResult> Login(User login)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Username == login.Username
                                       && x.Password == login.Password);

            if (user == null)
                return Unauthorized("Sai tài khoản hoặc mật khẩu");

            var tokenHandler = new JwtSecurityTokenHandler();
            var keyBytes = Encoding.UTF8.GetBytes(key);

            var token = new JwtSecurityToken(
                claims: new[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role ?? "user")
                },
                expires: DateTime.Now.AddHours(1),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(keyBytes),
                    SecurityAlgorithms.HmacSha256Signature)
            );

            return Ok(new
            {
                token = tokenHandler.WriteToken(token)
            });
        }
    }
}