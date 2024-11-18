using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketOrderFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initialupdate4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MarketProductStock");

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

            migrationBuilder.CreateTable(
                name: "MarketModelProductModel",
                columns: table => new
                {
                    MarketsId = table.Column<long>(type: "bigint", nullable: false),
                    ProductsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketModelProductModel", x => new { x.MarketsId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_MarketModelProductModel_Markets_MarketsId",
                        column: x => x.MarketsId,
                        principalTable: "Markets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MarketModelProductModel_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "MarketProductStocks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    GlobalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketProductStocks", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_MarketProductStockModelId",
                table: "Products",
                column: "MarketProductStockModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Markets_MarketProductStockModelId",
                table: "Markets",
                column: "MarketProductStockModelId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketModelProductModel_ProductsId",
                table: "MarketModelProductModel",
                column: "ProductsId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Markets_MarketProductStocks_MarketProductStockModelId",
                table: "Markets");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_MarketProductStocks_MarketProductStockModelId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "MarketModelProductModel");

            migrationBuilder.DropTable(
                name: "MarketProductStocks");

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

            migrationBuilder.CreateTable(
                name: "MarketProductStock",
                columns: table => new
                {
                    MarketId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketProductStock", x => new { x.MarketId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_MarketProductStock_Markets_MarketId",
                        column: x => x.MarketId,
                        principalTable: "Markets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MarketProductStock_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MarketProductStock_ProductId",
                table: "MarketProductStock",
                column: "ProductId");
        }
    }
}
