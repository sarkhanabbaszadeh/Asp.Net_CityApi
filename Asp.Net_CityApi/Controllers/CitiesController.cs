using Asp.Net_CityApi.Data;
using Asp.Net_CityApi.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Asp.Net_CityApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CitiesController : ControllerBase
	{
		private IAppRepository _appRepository;

		public CitiesController(IAppRepository appRepository)
		{
			_appRepository = appRepository;
		}

		public ActionResult GetCities()
		{
			//var cities = _appRepository.GetCities()
			//	.Select(c => 
			//	new CityForListDto { Id = c.Id, PhotoUrl = c.Photos.FirstOrDefault(p => p.IsMain == true).Url, Description = c.Description, Name = c.Name }).ToList();
			var cities=_appRepository.GetCities();
			var citiesToReturn = _mapper.Map<List<CityForListDto>>(cities);
			return Ok(cities);
		}
	}
}
