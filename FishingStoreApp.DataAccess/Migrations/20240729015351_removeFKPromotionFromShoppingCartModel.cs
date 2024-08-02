using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FishingStoreApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class removeFKPromotionFromShoppingCartModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_Promotion_PromotionId",
                table: "ShoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCarts_PromotionId",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "PromotionId",
                table: "ShoppingCarts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PromotionId",
                table: "ShoppingCarts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_PromotionId",
                table: "ShoppingCarts",
                column: "PromotionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_Promotion_PromotionId",
                table: "ShoppingCarts",
                column: "PromotionId",
                principalTable: "Promotion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
