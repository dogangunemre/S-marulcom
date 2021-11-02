using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using vericekme.Models;

namespace vericekme.Context
{
    public class ProductContext:DbContext
    {
       
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Unit> Unit { get; set; }
        public DbSet<ProductAddress> ProductAddresses { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-K0NK5TM\SQLEXPRESS;Database=ProductPoolDb;Trusted_Connection=True;");
        }
    }
}
