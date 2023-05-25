using System;
using Microsoft.EntityFrameworkCore;
using asset_amy.Models;
using asset_amy.DbContext;

namespace asset_amy.Managers
{
    public class AssetManager 
    {
        public AssetManager(AssetAmyContext context)
        {
            container = context;
        }

        public void Create(Asset asset)
        {
            container.assets.Add(asset);
            container.SaveChanges();
        }

        public void Update(Asset asset)
        {
            container.assets.Update(asset);
            container.SaveChanges();
        }

        public void Delete(Asset asset)
        {
            container.assets.Remove(asset);
            container.SaveChanges();
        }

        public Asset? GetById(int id)
        {
            return container.assets.FirstOrDefault(a => a.id == id);
        }

        public Asset[] GetAllForUser(int userId)
        {
            return container.assets.Where(a => a.belongsToId == userId).ToArray();
        }

        private AssetAmyContext container { get; set; }
    }
}