using Asp.Net_CityApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Asp.Net_CityApi.Data
{
	public class AuthRepository : IAuthRepository
	{
		private DataContext _dataContext;

		public AuthRepository(DataContext dataContext)
		{
			_dataContext = dataContext;
		}

		public async Task<User> Login(string userName, string password)
		{
			var user = await _dataContext.Users.FirstOrDefaultAsync(x=>x.Username==userName);
			if(user == null)
			{
				return null;
			}
			
			if(!VerifyPasswordHash(password,user.PasswordHash,user.PasswordSalt))
			{
				return null;
			}

			return user;
		}

		private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
		{
			using (var hmac=new System.Security.Cryptography.HMACSHA512(passwordSalt))
			{
				var computerHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
				for(int i=0; i<computerHash.Length; i++)
				{
					if (computerHash[i] != passwordHash[i])
					{
						return false;
					}
				}

				return true;
			}
		}

		public async Task<User> Register(User user, string password)
		{
			byte[] passwordHash, passwordSalt;
			CreatePasswordHash(password, out passwordHash, out passwordSalt);
			user.PasswordSalt = passwordSalt;
			user.PasswordHash = passwordHash;

			await _dataContext.Users.AddAsync(user);
			await _dataContext.SaveChangesAsync();
			return user;
		}


		private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
		{
			using (var hmac = new System.Security.Cryptography.HMACSHA512())
			{
				passwordSalt = hmac.Key;
				passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
			}
		}

		public async Task<bool> UserExists(string userName)
		{
			if(await _dataContext.Users.AnyAsync(x=>x.Username==userName))
			{
				return true;
			}

			return false;
		}
	}
}
