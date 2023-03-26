using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodDeliveryWebApp.Migrations
{
    /// <inheritdoc />
    public partial class MakeSellerAndCustomerBaseModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Customers_CustomerId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Sellers_SellerId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerOrderProducts_Customers_CustomerId",
                table: "CustomerOrderProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_AspNetUsers_Id",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Sellers_AspNetUsers_Id",
                table: "Sellers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sellers",
                table: "Sellers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "SellerId",
                table: "Addresses",
                newName: "SellerUserId");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Addresses",
                newName: "CustomerUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_SellerId",
                table: "Addresses",
                newName: "IX_Addresses_SellerUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_CustomerId",
                table: "Addresses",
                newName: "IX_Addresses_CustomerUserId");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Sellers",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Sellers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Customers",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Customers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CustomerOrderProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sellers",
                table: "Sellers",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Customers_CustomerUserId",
                table: "Addresses",
                column: "CustomerUserId",
                principalTable: "Customers",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Sellers_SellerUserId",
                table: "Addresses",
                column: "SellerUserId",
                principalTable: "Sellers",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerOrderProducts_Customers_CustomerId",
                table: "CustomerOrderProducts",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_AspNetUsers_UserId",
                table: "Customers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sellers_AspNetUsers_UserId",
                table: "Sellers",
                column: "UserId",
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
                name: "FK_Addresses_Sellers_SellerUserId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerOrderProducts_Customers_CustomerId",
                table: "CustomerOrderProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_AspNetUsers_UserId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Sellers_AspNetUsers_UserId",
                table: "Sellers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sellers",
                table: "Sellers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CustomerOrderProducts");

            migrationBuilder.RenameColumn(
                name: "SellerUserId",
                table: "Addresses",
                newName: "SellerId");

            migrationBuilder.RenameColumn(
                name: "CustomerUserId",
                table: "Addresses",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_SellerUserId",
                table: "Addresses",
                newName: "IX_Addresses_SellerId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_CustomerUserId",
                table: "Addresses",
                newName: "IX_Addresses_CustomerId");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Sellers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Customers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sellers",
                table: "Sellers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Customers_CustomerId",
                table: "Addresses",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Sellers_SellerId",
                table: "Addresses",
                column: "SellerId",
                principalTable: "Sellers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerOrderProducts_Customers_CustomerId",
                table: "CustomerOrderProducts",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_AspNetUsers_Id",
                table: "Customers",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sellers_AspNetUsers_Id",
                table: "Sellers",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
