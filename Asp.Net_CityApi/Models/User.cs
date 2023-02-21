﻿namespace Asp.Net_CityApi.Models
{
	public class User
	{
		public User()
		{
			Cities=new List<City>();
		}
		public int Id { get; set; }

		public string Username { get; set; }

		public byte[] PasswordHash { get; set; }

		public byte[] PasswordSalt { get; set; }

		public List<City> Cities { get; set; }
	}
}
