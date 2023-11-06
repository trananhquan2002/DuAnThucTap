using Microsoft.EntityFrameworkCore;

namespace QuanLyDatDoAnAPI.Entities
{
    public class AppDbContext : DbContext
    {
        public DbSet<Account> Account { get; set; }
        public DbSet<CartItem> CartItem { get; set; }
        public DbSet<Carts> Carts { get; set; }
        public DbSet<Decentralization> Decentralization { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<OrderStatus> OrderStatus { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductImage> ProductImage { get; set; }
        public DbSet<ProductReview> ProductReview { get; set; }
        public DbSet<ProductType> ProductType { get; set; }
        public DbSet<PaymentResponse> VNPay { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer($"Server = DESKTOP-4UOSLV5\\QUAN; Database = QuanLyDatDoAnChay; Trusted_Connection = True; Encrypt=true; TrustServerCertificate = true;");
        }
    }
}