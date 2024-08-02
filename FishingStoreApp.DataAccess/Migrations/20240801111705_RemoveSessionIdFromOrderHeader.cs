using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FishingStoreApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSessionIdFromOrderHeader : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sessionId",
                table: "OrderHeaders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "sessionId",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
