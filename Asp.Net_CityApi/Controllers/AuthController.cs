using Asp.Net_CityApi.Data;
using Asp.Net_CityApi.Dtos;
using Asp.Net_CityApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Asp.Net_CityApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private IAuthRepository _authRepository;
		private IConfiguration _configuration;

		public AuthController(IAuthRepository authRepository, IConfiguration configuration)
		{
			_authRepository = authRepository;
			_configuration = configuration;
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody]UserForRegisterDto userForRegisterDto)
		{
			if(await _authRepository.UserExists(userForRegisterDto.UserName))
			{
				ModelState.AddModelError("UserName", "Username already exists");
			}

			if(!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var userToCreate = new User
			{
				Username = userForRegisterDto.UserName,
			};

			var createdUser = await _authRepository.Register(userToCreate, userForRegisterDto.Password);
			return StatusCode(201);
		}

		[HttpPost("login")]
		public async Task<ActionResult> Login([FromBody] UserForLoginDto userForLoginDto)
		{
			var user= await _authRepository.Login(userForLoginDto.UserName, userForLoginDto.Password);

			if(user == null)
			{
				return Unauthorized();
			}

			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
				{
					new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
					new  Claim(ClaimTypes.Name,user.Username)
				}),
				Expires = DateTime.Now.AddDays(1),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
			};

			var token=tokenHandler.CreateToken(tokenDescriptor);
			var tokenString=tokenHandler.WriteToken(token);

			return Ok(token);
		}
	}
}
