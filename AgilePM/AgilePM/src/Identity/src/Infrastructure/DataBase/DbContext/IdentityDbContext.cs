using AgilePM.Identity.Domain.Tenant;
using AgilePM.Identity.Domain.User;
using Microsoft.EntityFrameworkCore;

namespace AgilePM.Identity.DataBase.DbContext
{
    public class IdentityDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<UserMemento> Users { get; set; }
        public IdentityDbContext(DbContextOptions options)
                : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // OwnsOne
            // OwnsMany

            modelBuilder.Ignore<TenantId>();

            modelBuilder.Entity<Tenant>().HasKey(s => s.Id);
            
            modelBuilder.Entity<UserMemento>()
                .HasKey(um => new
                {
                    um.TenantId,
                    um.UserName
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}