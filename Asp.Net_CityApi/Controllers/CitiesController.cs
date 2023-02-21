using Asp.Net_CityApi.Data;
using Asp.Net_CityApi.Dtos;
using Asp.Net_CityApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Asp.Net_CityApi.Controllers
{
	[Produces("application/json")]
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

		[HttpPost]
		[Route("add")]
		public ActionResult Add([FromBody]City city)
		{
			_appRepository.Add(city);
			_appRepository.SaveAll();
			return Ok(city);
		}

		[HttpGet]
		[Route("detail")]
		public ActionResult GetCityById(int id)
		{
			var city=_appRepository.GetCityById(id);
			var cityToReturun = _mapper.Map<CityForDetailDto>(city);
			return Ok(cityToReturun);
		}
	}
}
