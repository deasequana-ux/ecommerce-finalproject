using ecommerce_finalproject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce_finalproject.Data
{
    public class AppDbContext:IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
          
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) //not use
        {
            base.OnModelCreating(modelBuilder);
        }

       public DbSet<Products> Products { get; set; }
       
       //Orders related tables
       public DbSet<Order> Orders { get; set; }
       public DbSet<OrderItem> OrderItems { get;set; }
       public DbSet<Category> Categories { get;set; }
       public DbSet<CategoryProduct> CategoryProducts { get;set; }
       public DbSet<ShoppingCartItem> ShoppingCartItems { get;set; }
    
    }
}
