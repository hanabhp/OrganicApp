using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using waterfood.Data.Entities.Centers;
using waterfood.Data.Entities.Generals;
using waterfood.Data.Entities.Items;
using waterfood.Data.Entities.Reserves;
using waterfood.Data.Entities.Users;

namespace waterfood.Data.Context
{
    public class WaterFoodContext : DbContext
    {
        public WaterFoodContext(DbContextOptions<WaterFoodContext> options)
            : base(options)
        {

        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<Center> Centers { get; set; } = null!;
        public DbSet<Item> Items { get; set; } = null!;
        public DbSet<Menu> Menus { get; set; } = null!;
        public DbSet<MenuItem> MenuItems { get; set; } = null!;
        public DbSet<Location> Locations { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Reserve> Reserves { get; set; } = null!;
        public DbSet<ReserveItem> ReserveItems { get; set; } = null!;
        public DbSet<ReserveStatus> ReserveStatuses { get; set; } = null!;
        public DbSet<ReserveItemStatus> ReserveItemStatuses { get; set; } = null!;
        public DbSet<FavoriteCenter> FavoriteCenters { get; set; } = null!;
        

        public void DetachAllEntities()
        {
            var changedEntriesCopy = this.ChangeTracker.Entries()
                .Where(e => e.State is EntityState.Added or EntityState.Modified or EntityState.Deleted)
                .ToList();

            foreach (var entry in changedEntriesCopy)
                entry.State = EntityState.Detached;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }


            #region Indexes



            #endregion


            #region Clearify Active Entities


            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
