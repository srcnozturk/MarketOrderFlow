using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketOrderFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Identifier",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Identifier",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Identifier",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "Identifier",
                table: "Markets");

            migrationBuilder.DropColumn(
                name: "Identifier",
                table: "LogisticsCenters");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Identifier",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Identifier",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Identifier",
                table: "OrderItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Identifier",
                table: "Markets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Identifier",
                table: "LogisticsCenters",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
