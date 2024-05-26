using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_API_ASP.NET_NinjaTurtles.Migrations
{
    /// <inheritdoc />
    public partial class addedimageentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Products_ProductsProductId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ProductsProductId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ProductsProductId",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "ImgPath",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_FKProductId",
                table: "Orders",
                column: "FKProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Products_FKProductId",
                table: "Orders",
                column: "FKProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Products_FKProductId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_FKProductId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ImgPath",
                table: "Products");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductsProductId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ProductsProductId",
                table: "Orders",
                column: "ProductsProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Products_ProductsProductId",
                table: "Orders",
                column: "ProductsProductId",
                principalTable: "Products",
                principalColumn: "ProductId");
        }
    }
}
