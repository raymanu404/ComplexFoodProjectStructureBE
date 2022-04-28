using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class ConfigureShoppingCartItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ShoppingItems_ProductId",
                table: "ShoppingItems");

            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "ShoppingItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "ShoppingItems");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingItems_ProductId",
                table: "ShoppingItems",
                column: "ProductId",
                unique: true);
        }
    }
}
