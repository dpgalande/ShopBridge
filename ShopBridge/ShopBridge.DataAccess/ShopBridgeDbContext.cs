using ShopBridge.DataAccess.Configuration;
using ShopBridge.Entities;
using System.Data.Entity;

namespace ShopBridge.DataAccess
{
   public class ShopBridgeDbContext: DbContext
    {
        public ShopBridgeDbContext()
            : base("name=ShopBridgeDbContext")
        {
        }

        public virtual DbSet<ProductDetails> ProductDetails { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ProductDetailMap());
        }

    }
}
