using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_API_ASP.NET_NinjaTurtles.Migrations
{
    /// <inheritdoc />
    public partial class cleanedUppOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderProduct_Orders_OrdersOrderId",
                table: "OrderProduct");

            migrationBuilder.DropColumn(
                name: "FKProductId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "OrdersOrderId",
                table: "OrderProduct",
                newName: "Product");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProduct_Orders_Product",
                table: "OrderProduct",
                column: "Product",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderProduct_Orders_Product",
                table: "OrderProduct");

            migrationBuilder.RenameColumn(
                name: "Product",
                table: "OrderProduct",
                newName: "OrdersOrderId");

            migrationBuilder.AddColumn<Guid>(
                name: "FKProductId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProduct_Orders_OrdersOrderId",
                table: "OrderProduct",
                column: "OrdersOrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
