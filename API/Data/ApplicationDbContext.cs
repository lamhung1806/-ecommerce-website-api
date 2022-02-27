using API.Data.EntityBase;
using API.Data.EntityBase.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace API.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<CartDetails> CartDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                        .HasMany<Product>(c => c.Products)
                        .WithOne(p => p.Category)
                        .HasForeignKey(p => p.CategoryId)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>().Property(p => p.Sold).HasDefaultValue(0);

            modelBuilder.Entity<OrderDetail>().HasKey(pt => new { pt.OrderId, pt.ProductId });

            modelBuilder.Entity<CartDetails>().HasKey(pt => new { pt.CartId, pt.ProductId });

            modelBuilder.Entity<AppUser>()
                        .HasOne<Cart>(u=>u.Cart)
                        .WithOne(c=>c.AppUser)
                        .HasForeignKey<Cart>(c=>c.UserId)
                        .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            BeforSaveChanges();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            BeforSaveChanges();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void BeforSaveChanges()
        {
            var entities = ChangeTracker.Entries();
            foreach (var entity in entities)
            {
                var now = DateTime.Now;
                if (entity.Entity is IEntityBase asEntity)
                {
                    if (entity.State == EntityState.Added)
                    {
                        asEntity.CreatedOn = now;
                        asEntity.UpdatedOn = now;
                    }
                    if (entity.State == EntityState.Modified)
                    {
                        asEntity.UpdatedOn = now;
                    }
                }
            }
        }
    }
}