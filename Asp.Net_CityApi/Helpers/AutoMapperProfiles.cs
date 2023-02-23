using Asp.Net_CityApi.Dtos;
using Asp.Net_CityApi.Models;
using AutoMapper;

namespace Asp.Net_CityApi.Helpers
{
	public class AutoMapperProfiles:Profile
	{
		public AutoMapperProfiles()
		{
			CreateMap<City, CityForListDto>().ForMember(dest => dest.PhotoUrl, opt =>
			{
				opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
			});

			CreateMap<City, CityForDetailDto>();
			CreateMap<Photo, PhotoForCreationDto>();
			CreateMap<PhotoForReturnDto, Photo>();
		} 
	}
}
