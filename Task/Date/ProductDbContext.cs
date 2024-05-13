using Microsoft.EntityFrameworkCore;
using Task.Models;

namespace Task.Date
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions options)
            : base(options)
        {

        }
        public DbSet<Product> Product { get; set; }



    }
}