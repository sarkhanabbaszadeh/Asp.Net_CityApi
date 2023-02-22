using Asp.Net_CityApi.Models;

namespace Asp.Net_CityApi.Data
{
	public interface IAuthRepository
	{
		Task<User> Register(User user, string password);

		Task<User> Login(string userName, string password);

		Task<bool> UserExists(string userName);
	}
}
