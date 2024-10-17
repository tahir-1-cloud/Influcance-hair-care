using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace InfluanceHairCare.models.DataContext
{
    public class ApplicationDataContext : DbContext
    {
        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options) : base(options)
        {

        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<SaleRep> SaleReps { get; set; }
        public DbSet<Broadcast> Broadcasts { get; set; }
        public DbSet<Products> Products { get; set; }

        public DbSet<Orders> Orders { get; set; }

        public DbSet<OrderProducts> OrderProducts { get; set; }
        public DbSet<OrderPayment> OrderPayments { get; set; }

        public DbSet<ProductRating> ProductRatings{ get; set; }
        public DbSet<SaleRepOrderPayments> SaleRepsOrderPayments { get; set; }
        public DbSet<CustomerfavoriteProducts> CustomerfavoritesProducts { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // base.OnModelCreating(builder);

            builder.Entity<Login>()
            .HasOne<Customer>(s => s.Customer)
            .WithOne(ad => ad.Login)
            .HasForeignKey<Customer>(ad => ad.LoginId);

            builder.Entity<Login>()
            .HasOne<SaleRep>(s => s.SaleRep)
            .WithOne(ad => ad.Login)
            .HasForeignKey<SaleRep>(ad => ad.LoginId);


            builder.Entity<Customer>()
            .HasOne<SaleRep>(s => s.SaleRep)
            .WithMany(g => g.SaleRepCustomer)
            .HasForeignKey(s => s.SaleRepID);


            builder.Entity<Orders>()
            .HasOne<Customer>(s => s.Customer)
            .WithMany(g => g.CustomerOrders)
            .HasForeignKey(s => s.CustomerId);

            builder.Entity<OrderProducts>().HasKey(op => new { op.OrderId, op.ProductId });
            builder.Entity<OrderProducts>()
                .HasOne(op => op.Order)
                .WithMany(o => o.Products)
                .HasForeignKey(op => op.OrderId);
            builder.Entity<OrderProducts>()
                .HasOne(op => op.Product)
                .WithMany(p => p.Orders)
                .HasForeignKey(op => op.ProductId);

            builder.Entity<CustomerfavoriteProducts>().HasKey(op => new { op.CustomerId, op.ProductId });
            builder.Entity<CustomerfavoriteProducts>()
                .HasOne(op => op.Customer)
                .WithMany(o => o.FavoriteProducts)
                .HasForeignKey(op => op.CustomerId);
            builder.Entity<CustomerfavoriteProducts>()
                .HasOne(op => op.Product)
                .WithMany(p => p.CustomerFavorites)
                .HasForeignKey(op => op.ProductId);
        }
    }
}
