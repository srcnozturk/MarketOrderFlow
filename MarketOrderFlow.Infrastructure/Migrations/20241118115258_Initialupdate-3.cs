using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketOrderFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initialupdate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MarketProductStock_Markets_MarketsId",
                table: "MarketProductStock");

            migrationBuilder.DropForeignKey(
                name: "FK_MarketProductStock_Products_ProductsId",
                table: "MarketProductStock");

            migrationBuilder.RenameColumn(
                name: "ProductsId",
                table: "MarketProductStock",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "MarketsId",
                table: "MarketProductStock",
                newName: "MarketId");

            migrationBuilder.RenameIndex(
                name: "IX_MarketProductStock_ProductsId",
                table: "MarketProductStock",
                newName: "IX_MarketProductStock_ProductId");

            migrationBuilder.AddColumn<int>(
                name: "StockQuantity",
                table: "MarketProductStock",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_MarketProductStock_Markets_MarketId",
                table: "MarketProductStock",
                column: "MarketId",
                principalTable: "Markets",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_MarketProductStock_Products_ProductId",
                table: "MarketProductStock",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MarketProductStock_Markets_MarketId",
                table: "MarketProductStock");

            migrationBuilder.DropForeignKey(
                name: "FK_MarketProductStock_Products_ProductId",
                table: "MarketProductStock");

            migrationBuilder.DropColumn(
                name: "StockQuantity",
                table: "MarketProductStock");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "MarketProductStock",
                newName: "ProductsId");

            migrationBuilder.RenameColumn(
                name: "MarketId",
                table: "MarketProductStock",
                newName: "MarketsId");

            migrationBuilder.RenameIndex(
                name: "IX_MarketProductStock_ProductId",
                table: "MarketProductStock",
                newName: "IX_MarketProductStock_ProductsId");

            migrationBuilder.AddForeignKey(
                name: "FK_MarketProductStock_Markets_MarketsId",
                table: "MarketProductStock",
                column: "MarketsId",
                principalTable: "Markets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MarketProductStock_Products_ProductsId",
                table: "MarketProductStock",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
