using System;
using Microsoft.EntityFrameworkCore;
using asset_amy.Models;
using asset_amy.DbContext;

namespace asset_amy.Managers
{
    public class RevenueManager
    {
        public RevenueManager(AssetAmyContext context)
        {
            container = context;
        }

        public void Create(Revenue revenue)
        {
            container.revenues.Add(revenue);
            container.SaveChanges();
        }

        public void Update(Revenue revenue)
        {
            container.revenues.Update(revenue);
            container.SaveChanges();
        }

        public void Delete(Revenue revenue)
        {
            container.revenues.Remove(revenue);
            container.SaveChanges();
        }

        public Revenue? GetById(int id)
        {
            return container.revenues.FirstOrDefault(e => e.id == id);
        }

        public Revenue[] GetAllForUser(int userId)
        {
            return container.revenues.Where(e => e.belongsToId == userId).ToArray();
        }

        private AssetAmyContext container { get; set; }
    }
}