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
            Container = context;
        }

        public Expense[] GetAllForUser(int userId)
        {
            return Container.Expenses.Where(e => e.BelongsToId == userId).ToArray();
        }

        private AssetAmyContext Container { get; set; }
    }
}