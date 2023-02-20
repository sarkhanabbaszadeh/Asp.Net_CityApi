﻿namespace Asp.Net_CityApi.Models
{
	public class City
	{
		public City()
		{
			Photos=new List<Photo>();
		}
		public int Id { get; set; }
		public int UserID { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public List<Photo> Photos { get; set; }
		public User User { get; set; }
	}
}
