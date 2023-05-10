using System;
using Microsoft.EntityFrameworkCore;
using asset_amy.Models;
using asset_amy.DbContext;

namespace asset_amy.Managers
{
    public class ExpenseManager
    {
        public ExpenseManager(AssetAmyContext context)
        {
            container = context;
        }

        public Expense[] GetAllForUser(int userId)
        {
            return container.expenses.Where(e => e.belongsToId == userId).ToArray();
        }

        private AssetAmyContext container { get; set; }
    }
}