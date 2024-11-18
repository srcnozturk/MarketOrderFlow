using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketOrderFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initialupdate5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Markets_MarketProductStocks_MarketProductStockModelId",
                table: "Markets");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_MarketProductStocks_MarketProductStockModelId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_MarketProductStockModelId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Markets_MarketProductStockModelId",
                table: "Markets");

            migrationBuilder.DropColumn(
                name: "MarketProductStockModelId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "MarketProductStockModelId",
                table: "Markets");

            migrationBuilder.AddColumn<long>(
                name: "MarketId",
                table: "MarketProductStocks",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ProductId",
                table: "MarketProductStocks",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_MarketProductStocks_MarketId",
                table: "MarketProductStocks",
                column: "MarketId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketProductStocks_ProductId",
                table: "MarketProductStocks",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_MarketProductStocks_Markets_MarketId",
                table: "MarketProductStocks",
                column: "MarketId",
                principalTable: "Markets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MarketProductStocks_Products_ProductId",
                table: "MarketProductStocks",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MarketProductStocks_Markets_MarketId",
                table: "MarketProductStocks");

            migrationBuilder.DropForeignKey(
                name: "FK_MarketProductStocks_Products_ProductId",
                table: "MarketProductStocks");

            migrationBuilder.DropIndex(
                name: "IX_MarketProductStocks_MarketId",
                table: "MarketProductStocks");

            migrationBuilder.DropIndex(
                name: "IX_MarketProductStocks_ProductId",
                table: "MarketProductStocks");

            migrationBuilder.DropColumn(
                name: "MarketId",
                table: "MarketProductStocks");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "MarketProductStocks");

            migrationBuilder.AddColumn<long>(
                name: "MarketProductStockModelId",
                table: "Products",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "MarketProductStockModelId",
                table: "Markets",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_MarketProductStockModelId",
                table: "Products",
                column: "MarketProductStockModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Markets_MarketProductStockModelId",
                table: "Markets",
                column: "MarketProductStockModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Markets_MarketProductStocks_MarketProductStockModelId",
                table: "Markets",
                column: "MarketProductStockModelId",
                principalTable: "MarketProductStocks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_MarketProductStocks_MarketProductStockModelId",
                table: "Products",
                column: "MarketProductStockModelId",
                principalTable: "MarketProductStocks",
                principalColumn: "Id");
        }
    }
}
