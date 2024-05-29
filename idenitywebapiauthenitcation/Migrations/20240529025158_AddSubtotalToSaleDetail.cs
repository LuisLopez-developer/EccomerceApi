using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EccomerceApi.Migrations
{
    /// <inheritdoc />
    public partial class AddSubtotalToSaleDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Subtotal",
                table: "SaleDetails",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Subtotal",
                table: "SaleDetails");
        }
    }
}
