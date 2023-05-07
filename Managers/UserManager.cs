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
			Container = context;
		}

		public void Add(User user)
		{
			Container.Users.Add(user);
			Container.SaveChanges();
		}

		public User? GetByEmail(string email)
		{
			return Container.Users.FirstOrDefault(u => u.Email == email);
		}

        private AssetAmyContext Container { get; set; }
    }
}

