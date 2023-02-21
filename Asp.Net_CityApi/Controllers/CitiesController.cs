using Asp.Net_CityApi.Data;
using Asp.Net_CityApi.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Asp.Net_CityApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CitiesController : ControllerBase
	{
		private IAppRepository _appRepository;
		private IMapper _mapper;

		public CitiesController(IAppRepository appRepository, IMapper mapper)
		{
			_appRepository = appRepository;
			_mapper = mapper;
		}

		public ActionResult GetCities()
		{
			//var cities = _appRepository.GetCities()
			//	.Select(c => 
			//	new CityForListDto { Id = c.Id, PhotoUrl = c.Photos.FirstOrDefault(p => p.IsMain == true).Url, Description = c.Description, Name = c.Name }).ToList();
			var cities=_appRepository.GetCities();
			var citiesToReturn = _mapper.Map<List<CityForListDto>>(cities);
			return Ok(citiesToReturn);
		}
	}
}
