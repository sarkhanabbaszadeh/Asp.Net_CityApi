﻿using Asp.Net_CityApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Asp.Net_CityApi.Data
{
	public class AppRepository : IAppRepository
	{
		private DataContext _context;

		public AppRepository(DataContext context)
		{
			_context = context;
		}

		public void Add<T>(T entity) where T : class
		{
			_context.Add(entity);
		}

		public void Delete<T>(T entity) where T : class
		{
			_context.Remove(entity);
		}

		public List<City> GetCities()
		{
			var cities=_context.Cities.Include(c=>c.Photos).ToList();
			return cities;
		}

		public City GetCityById(int cityId)
		{
			var city=_context.Cities.Include(c=>c.Photos).FirstOrDefault(c=>c.Id== cityId);
			return city;
		}

		public Photo GetPhoto(int id)
		{
			var photo = _context.Photos.FirstOrDefault(p => p.id == id);
			return photo;
		}

		public List<Photo> GetPhotosByCity(int cityId)
		{
			var photo = _context.Photos.Where(p => p.CityId == cityId).ToList();
			return photo;
		}

		public bool SaveAll()
		{
			return _context.SaveChanges() > 0;
		}
	}
}
