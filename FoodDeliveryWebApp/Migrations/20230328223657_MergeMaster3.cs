using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodDeliveryWebApp.Migrations
{
    /// <inheritdoc />
    public partial class MergeMaster3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Customers_CustomerUserId",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_CustomerUserId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "CustomerUserId",
                table: "Addresses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerUserId",
                table: "Addresses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CustomerUserId",
                table: "Addresses",
                column: "CustomerUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Customers_CustomerUserId",
                table: "Addresses",
                column: "CustomerUserId",
                principalTable: "Customers",
                principalColumn: "UserId");
        }
    }
}
