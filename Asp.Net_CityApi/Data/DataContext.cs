using Asp.Net_CityApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Asp.Net_CityApi.Data
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions options) : base(options)
		{

		}

		public DbSet<Value> Values {get; set;}
	}
}
