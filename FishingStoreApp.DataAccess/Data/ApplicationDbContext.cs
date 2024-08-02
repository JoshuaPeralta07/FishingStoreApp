using FishingStoreApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace FishingStoreApp.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set;}
        public DbSet<ApplicationUser> ApplicationUsers {get; set;} 
        public DbSet<OrderHeader> OrderHeaders { get; set;}
        public DbSet<OrderDetail> OrderDetails { get; set;}
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<Promotion> Promotion { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Fishing Rods", DisplayOrder = 1 },
                new Category { CategoryId = 2, Name = "Fishing Lines", DisplayOrder = 2 },
                new Category { CategoryId = 3, Name = "Lures", DisplayOrder = 3 },
                new Category { CategoryId = 4, Name = "Storage", DisplayOrder = 4 }
                );
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductId = 1,
                    ProductName = "Echo Carbon XL Medium/Fast Fly Rod",
                    Description = "A true all-around performer with the features and feel associated with rods four times the cost.",
                    Price = 369.99,
                    ProductCode = "ECHOCBXL690",
                    Stocks = 10,
                    CategoryId = 1,
                    ImageUrl = ""
                },
                 new Product
                 {
                     ProductId = 2,
                     ProductName = "Abu Garcia Veritas 2 Piece Surf Rod",
                     Description = "Despite the deceptively airy feel, the Abu Garcia Veritas 4.0 Spinning Surf Rod is robust, responsive and comfortable to fish with from dusk to dawn.",
                     Price = 229.99,
                     ProductCode = "1547516",
                     Stocks = 10,
                     CategoryId = 1,
                     ImageUrl = ""
                 },
                 new Product
                 {
                     ProductId = 3,
                     ProductName = "Berkley Fireline Original Bulk 1300m Braid",
                     Description = "A good reliable braid makes the difference between an average fishing trip and an exceptional one. We have proved just how reliable this top quality braid is and thoroughly recommend it. The strongest, most abrasion resistant superline in its class.",
                     Price = 239.99,
                     ProductCode = "119730-V",
                     Stocks = 5,
                     CategoryId = 2,
                     ImageUrl = ""

                 }
                );
        }
    }
}
