using System;
using Microsoft.EntityFrameworkCore;
using asset_amy.Models;
using asset_amy.DbContext;

namespace asset_amy.Managers
{
	public class UserManager
	{
		public UserManager(AssetAmyContext context)
		{
			container = context;
		}

		public void Add(User user)
		{
			container.users.Add(user);
			container.SaveChanges();
		}

		public void Update(User user)
		{
			container.users.Update(user);
			container.SaveChanges();
		}

		public User? GetByEmail(string email)
		{
			return container.users.FirstOrDefault(u => u.email == email);
		}

		public User? GetByPasswordResetHash(string hash)
		{
			return container.users.FirstOrDefault(u => u.passwordResetHash == hash);
		}

        private AssetAmyContext container { get; set; }
    }
}

