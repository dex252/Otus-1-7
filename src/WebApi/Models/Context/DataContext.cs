using Microsoft.EntityFrameworkCore;

namespace WebApi.Models.Context
{
    /// <summary>
    /// Для миграций
    /// </summary>
    public class DataContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
    }
}
