using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketOrderFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initialupdate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MarketProductStock",
                columns: table => new
                {
                    MarketsId = table.Column<long>(type: "bigint", nullable: false),
                    ProductsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketProductStock", x => new { x.MarketsId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_MarketProductStock_Markets_MarketsId",
                        column: x => x.MarketsId,
                        principalTable: "Markets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_MarketProductStock_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MarketProductStock_ProductsId",
                table: "MarketProductStock",
                column: "ProductsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MarketProductStock");
        }
    }
}
