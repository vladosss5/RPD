using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentalPoint.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemakeIdOnOrderInventoriesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderInventories",
                table: "OrderInventories");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "OrderInventories",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderInventories",
                table: "OrderInventories",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_OrderInventories_OrderId",
                table: "OrderInventories",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderInventories",
                table: "OrderInventories");

            migrationBuilder.DropIndex(
                name: "IX_OrderInventories_OrderId",
                table: "OrderInventories");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "OrderInventories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderInventories",
                table: "OrderInventories",
                columns: new[] { "OrderId", "InventoryId" });
        }
    }
}
