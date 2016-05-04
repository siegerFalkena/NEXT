using Microsoft.Data.Entity;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NEXT.API.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Product> products { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<User> users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().Property(category => category.categoryID).IsRequired();
            modelBuilder.Entity<Product>().Property(product => product.productID).IsRequired();
            modelBuilder.Entity<User>().Property(user => user.userID).IsRequired();
        }
    }
}
