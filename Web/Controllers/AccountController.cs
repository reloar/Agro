using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Domain.Interface;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IConfiguration _config;
        private IUserRepository _userRepo;
        public AccountController(IConfiguration config, IUserRepository userRepo)
        {
            _config = config;
            _userRepo = userRepo;
        }

        [HttpGet("register")]
        public IActionResult Register()
        {
            var usertypes = _userRepo.getRegistrationType();

            var values = usertypes.Split(',');

            return Ok(values);
        }
        [HttpPost("register")]

        public async Task<IActionResult> Register([FromBody] UserModel user)
        {
            if (ModelState.IsValid)
            {
                var result = await _userRepo.RegisterUser(user);

                return Ok(result.ToResponse("Successful"));

            }
            return BadRequest(ModelState.ToResponse("Invalid credentials", status: HttpStatusCode.BadRequest));

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserModel model)
        {

            var user = await _userRepo.FindUserbyEmail(model.Email);
            if (user == null)
                return Unauthorized();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:Token").Value);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.userId),
                    new Claim(ClaimTypes.Name,user.UserName),
                    new Claim(ClaimTypes.Role,user.RegistrationType),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.GivenName, user.FirstName)
                }),
                Expires = DateTime.Now.AddDays(1),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { tokenString });
        }


    }
}