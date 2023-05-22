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

        public void Create(Expense expense)
        {
            container.expenses.Add(expense);
            container.SaveChanges();
        }

        public void Update(Expense expense)
        {
            container.expenses.Update(expense);
            container.SaveChanges();
        }

        public void Delete(Expense expense)
        {
            container.expenses.Remove(expense);
            container.SaveChanges();
        }

        public Expense? GetById(int id)
        {
            return container.expenses.FirstOrDefault(e => e.id == id);
        }

        public Expense[] GetAllForUser(int userId)
        {
            return container.expenses.Where(e => e.belongsToId == userId).ToArray();
        }

        private AssetAmyContext container { get; set; }
    }
}