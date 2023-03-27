using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodDeliveryWebApp.Migrations
{
    /// <inheritdoc />
    public partial class AddSaleCols : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "PromoCodes");

            migrationBuilder.DropColumn(
                name: "PromoCodeId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "MaxSale",
                table: "PromoCodes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sale",
                table: "PromoCodes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "HasSale",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Sale",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PromoCodeId",
                table: "Category",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sellers_StoreName",
                table: "Sellers",
                column: "StoreName",
                unique: true);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_PromoCodes_PromoCodeId",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Sellers_StoreName",
                table: "Sellers");

            migrationBuilder.DropIndex(
                name: "IX_Category_PromoCodeId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "MaxSale",
                table: "PromoCodes");

            migrationBuilder.DropColumn(
                name: "Sale",
                table: "PromoCodes");

            migrationBuilder.DropColumn(
                name: "HasSale",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Sale",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PromoCodeId",
                table: "Category");

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

            migrationBuilder.CreateIndex(
                name: "IX_PromoCodes_SellerId",
                table: "PromoCodes",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_PromoCodeId",
                table: "Products",
                column: "PromoCodeId");

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
    }
}
