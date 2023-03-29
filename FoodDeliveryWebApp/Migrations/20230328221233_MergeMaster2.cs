using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodDeliveryWebApp.Migrations
{
    /// <inheritdoc />
    public partial class MergeMaster2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerOrderProducts_Orders_OrderId",
                table: "CustomerOrderProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerOrderProducts_Products_ProductId",
                table: "CustomerOrderProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerOrderProducts",
                table: "CustomerOrderProducts");

            migrationBuilder.RenameTable(
                name: "CustomerOrderProducts",
                newName: "OrderProducts");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerOrderProducts_OrderId",
                table: "OrderProducts",
                newName: "IX_OrderProducts_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderProducts",
                table: "OrderProducts",
                columns: new[] { "ProductId", "OrderId" });

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_Orders_OrderId",
                table: "OrderProducts",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_Products_ProductId",
                table: "OrderProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_Orders_OrderId",
                table: "OrderProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_Products_ProductId",
                table: "OrderProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderProducts",
                table: "OrderProducts");

            migrationBuilder.RenameTable(
                name: "OrderProducts",
                newName: "CustomerOrderProducts");

            migrationBuilder.RenameIndex(
                name: "IX_OrderProducts_OrderId",
                table: "CustomerOrderProducts",
                newName: "IX_CustomerOrderProducts_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerOrderProducts",
                table: "CustomerOrderProducts",
                columns: new[] { "ProductId", "OrderId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerOrderProducts_Orders_OrderId",
                table: "CustomerOrderProducts",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerOrderProducts_Products_ProductId",
                table: "CustomerOrderProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
