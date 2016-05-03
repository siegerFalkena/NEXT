using Microsoft.Data.Entity;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NEXT.API.Model
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Product> products { get; set; }
        public DbSet<Category> categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().Property(c => c.id).IsRequired();
            modelBuilder.Entity<Product>().Property(p => p.ID).IsRequired();
        }
    }
}
