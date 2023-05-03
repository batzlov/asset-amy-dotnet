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

        private AssetAmyContext Container { get; set; }
    }
}

