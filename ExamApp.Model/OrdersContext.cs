using Microsoft.EntityFrameworkCore;

namespace ExamApp.Models
{
    public class OrdersContext : DbContext
    {
        public OrdersContext(DbContextOptions<OrdersContext> options) : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<BillingAddresse> BillingAddresses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Payment> Payments { get; set; }
    }
}
