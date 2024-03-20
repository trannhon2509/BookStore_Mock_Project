using BookStore_Mock_Project.Model;
using Microsoft.EntityFrameworkCore;

namespace BookStore_Mock_Project.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Đọc chuỗi kết nối từ appsettings.json
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                string connectionString = configuration.GetConnectionString("DefaultConnection");

                optionsBuilder.UseSqlServer(connectionString);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Thiết lập mối quan hệ 1-n giữa Book và BookCategory
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Category)
                .WithMany(bc => bc.Books)
                .HasForeignKey(b => b.CategoryId);

            // Thiết lập mối quan hệ 1-n giữa User và Role
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);

            // Thiết lập mối quan hệ 1-n giữa Order và User
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);

            // Thiết lập mối quan hệ n-n giữa OrderDetail và Book
            modelBuilder.Entity<OrderDetail>()
                .HasKey(od => new { od.OrderId, od.BookId });

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Book)
                .WithMany() // Không cần chỉ định navigation property ở đây vì mối quan hệ là một-n
                .HasForeignKey(od => od.BookId);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId);
        }
    }
}
