using Exercice;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net;

namespace Dal
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<Client> Clients { get; set; } = null!;
        public DbSet<Address> Addresses { get; set; } = null!;
        public DbSet<OrderProduct> OrderProducts { get; set; } = null!;

        public ApplicationDbContext()
            : base()
        {

        }

        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {

        }

    }
}
