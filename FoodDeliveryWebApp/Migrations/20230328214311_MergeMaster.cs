using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodDeliveryWebApp.Migrations
{
    /// <inheritdoc />
    public partial class MergeMaster : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_PromoCodes_PromoCodeId",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerOrderProducts_Customers_CustomerId",
                table: "CustomerOrderProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerOrderProducts",
                table: "CustomerOrderProducts");

            migrationBuilder.DropIndex(
                name: "IX_CustomerOrderProducts_CustomerId",
                table: "CustomerOrderProducts");

            migrationBuilder.DropIndex(
                name: "IX_Category_PromoCodeId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "MaxSale",
                table: "PromoCodes");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "CustomerOrderProducts");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CustomerOrderProducts");

            migrationBuilder.DropColumn(
                name: "PromoCodeId",
                table: "Category");

            migrationBuilder.AlterColumn<double>(
                name: "Discount",
                table: "PromoCodes",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "SellerId",
                table: "PromoCodes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PromoCodeId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice",
                table: "CustomerOrderProducts",
                type: "money",
                precision: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "CustomerUserId",
                table: "Addresses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerOrderProducts",
                table: "CustomerOrderProducts",
                columns: new[] { "ProductId", "OrderId" });

            migrationBuilder.CreateIndex(
                name: "IX_PromoCodes_SellerId",
                table: "PromoCodes",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_PromoCodeId",
                table: "Products",
                column: "PromoCodeId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Products_PromoCodes_PromoCodeId",
                table: "Products",
                column: "PromoCodeId",
                principalTable: "PromoCodes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PromoCodes_AspNetUsers_SellerId",
                table: "PromoCodes",
                column: "SellerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Customers_CustomerUserId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_PromoCodes_PromoCodeId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_PromoCodes_AspNetUsers_SellerId",
                table: "PromoCodes");

            migrationBuilder.DropIndex(
                name: "IX_PromoCodes_SellerId",
                table: "PromoCodes");

            migrationBuilder.DropIndex(
                name: "IX_Products_PromoCodeId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerOrderProducts",
                table: "CustomerOrderProducts");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_CustomerUserId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "PromoCodes");

            migrationBuilder.DropColumn(
                name: "PromoCodeId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "CustomerOrderProducts");

            migrationBuilder.DropColumn(
                name: "CustomerUserId",
                table: "Addresses");

            migrationBuilder.AlterColumn<int>(
                name: "Discount",
                table: "PromoCodes",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<int>(
                name: "MaxSale",
                table: "PromoCodes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CustomerId",
                table: "CustomerOrderProducts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CustomerOrderProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PromoCodeId",
                table: "Category",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerOrderProducts",
                table: "CustomerOrderProducts",
                columns: new[] { "ProductId", "OrderId", "CustomerId" });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrderProducts_CustomerId",
                table: "CustomerOrderProducts",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_PromoCodeId",
                table: "Category",
                column: "PromoCodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_PromoCodes_PromoCodeId",
                table: "Category",
                column: "PromoCodeId",
                principalTable: "PromoCodes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerOrderProducts_Customers_CustomerId",
                table: "CustomerOrderProducts",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
