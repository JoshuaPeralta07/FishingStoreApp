using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FishingStoreApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryAndProductToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    ProductCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Stocks = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "DisplayOrder", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Fishing Rods" },
                    { 2, 2, "Fishing Lines" },
                    { 3, 3, "Lures" },
                    { 4, 4, "Storage" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Description", "Price", "ProductCode", "ProductName", "Stocks" },
                values: new object[,]
                {
                    { 1, "A true all-around performer with the features and feel associated with rods four times the cost.", 369.99000000000001, "ECHOCBXL690", "Echo Carbon XL Medium/Fast Fly Rod", 10 },
                    { 2, "Despite the deceptively airy feel, the Abu Garcia Veritas 4.0 Spinning Surf Rod is robust, responsive and comfortable to fish with from dusk to dawn.", 229.99000000000001, "1547516", "Abu Garcia Veritas 2 Piece Surf Rod", 10 },
                    { 3, "A good reliable braid makes the difference between an average fishing trip and an exceptional one. We have proved just how reliable this top quality braid is and thoroughly recommend it. The strongest, most abrasion resistant superline in its class.", 239.99000000000001, "119730-V", "Berkley Fireline Original Bulk 1300m Braid", 5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
