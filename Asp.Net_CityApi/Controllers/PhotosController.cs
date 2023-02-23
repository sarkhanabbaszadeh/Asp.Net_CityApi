﻿using Asp.Net_CityApi.Data;
using Asp.Net_CityApi.Dtos;
using Asp.Net_CityApi.Helpers;
using Asp.Net_CityApi.Models;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace Asp.Net_CityApi.Controllers
{
	[Route("api/cities/{cityId}/[controller]")]
	[ApiController]
	public class PhotosController : ControllerBase
	{
		private IAppRepository _appRepository;
		private IMapper _mapper;
		IOptions<CloudinarySettings> _cloudinaryConfig;

		private Cloudinary _cloudinary; 

		public PhotosController(IAppRepository appRepository, IMapper mapper, IOptions<CloudinarySettings> cloudinaryConfig)
		{
			_appRepository = appRepository;
			_mapper = mapper;
			_cloudinaryConfig = cloudinaryConfig;

			Account account = new Account(
				_cloudinaryConfig.Value.CloudName,
				_cloudinaryConfig.Value.ApiKey,
				_cloudinaryConfig.Value.ApiSecret);

			_cloudinary = new Cloudinary(account);
		}

		[HttpPost]
		public ActionResult AddPhotoForCity(int cityId, [FromBody]PhotoForCreationDto photoForCreationDto)
		{
			var city= _appRepository.GetCityById(cityId);
			if (city == null)
			{
				return BadRequest("Could not find the city bro :(");
			}

			var currenUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

			if(currenUserId !=city.UserId)
			{
				return Unauthorized();
			}

			var file = photoForCreationDto.File;
			var uploadResult = new ImageUploadResult();

			if(file.Length > 0)
			{
				using (var stream=file.OpenReadStream())
				{
					var uploadParams = new ImageUploadParams
					{
						File = new FileDescription(file.Name, stream)
					};

					uploadResult= _cloudinary.Upload(uploadParams);
				}
			}

			photoForCreationDto.Url = uploadResult.Url.ToString();
			photoForCreationDto.PuplicId = uploadResult.PublicId;

			var photo = _mapper.Map<Photo>(photoForCreationDto);
			photo.City = city;
			if(!city.Photos.Any(p=>p.IsMain))
			{
				photo.IsMain= true;
			}

			city.Photos.Add(photo);
			if(_appRepository.SaveAll())
			{
				var photoToReturn = _mapper.Map<PhotoForReturnDto>(photo);
				return CreatedAtRoute("GetPhoto", new {id=photo.id}, photoToReturn);
			}
			return BadRequest("Could not add the photo bro :(");
		}
	}
}
