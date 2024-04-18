using KheyaShop.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KheyaShop.Data
{
    public class AppDbContext:IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product_Review>().HasKey(am => new
            {
                am.ProductId,
                am.ReviewId
            });
            modelBuilder.Entity<Product_Review>().HasOne(x => x.Product).WithMany(am => am.ProductReviewObj).HasForeignKey(x => x.ProductId);
            modelBuilder.Entity<Product_Review>().HasOne(y=>y.Review).WithMany(am=>am.ProductReviewObject).HasForeignKey(x => x.ReviewId);  
            
            base.OnModelCreating(modelBuilder);
        }

       
        public DbSet <Category> Categories { get; set; }
        public DbSet<ProductUnit> ProductUnit { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        
        public DbSet<ShoppingCartItems> ShoppingCartItems { get; set; }

        public DbSet<Slider> Sliders  { get; set; }

        public DbSet<UserText> Texts { get; set; }
        public DbSet<Product_Review> Product_Reviews { get; set; }
        public DbSet<Review> Reviews { get; set; }




    }
}
