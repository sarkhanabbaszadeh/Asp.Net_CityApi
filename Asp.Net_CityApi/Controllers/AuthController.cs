using Asp.Net_CityApi.Data;
using Asp.Net_CityApi.Dtos;
using Asp.Net_CityApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

			if(ModelState.IsValid)
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
	}
}
