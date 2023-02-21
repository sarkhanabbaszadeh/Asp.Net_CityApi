using Asp.Net_CityApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Asp.Net_CityApi.Data
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions options) : base(options)
		{
			
		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("server=SABBASZADA-N\\MSSQLSERVER2;database=CityApi;integrated security=true;encrypt=false");
		}

		public DbSet<City> Cities { get; set; }

		public DbSet<Photo> Photos { get; set; }

		public DbSet<User> Users { get; set; }

	}
}
