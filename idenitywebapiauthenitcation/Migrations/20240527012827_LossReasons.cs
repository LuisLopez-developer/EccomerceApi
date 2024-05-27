using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EccomerceApi.Migrations
{
    /// <inheritdoc />
    public partial class LossReasons : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LossReasonId",
                table: "LostDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "descripcion",
                table: "LostDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "reasonId",
                table: "LostDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "LossReasons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LossReasons", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LostDetails_LossReasonId",
                table: "LostDetails",
                column: "LossReasonId");

            migrationBuilder.AddForeignKey(
                name: "FK_LostDetails_LossReasons_LossReasonId",
                table: "LostDetails",
                column: "LossReasonId",
                principalTable: "LossReasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LostDetails_LossReasons_LossReasonId",
                table: "LostDetails");

            migrationBuilder.DropTable(
                name: "LossReasons");

            migrationBuilder.DropIndex(
                name: "IX_LostDetails_LossReasonId",
                table: "LostDetails");

            migrationBuilder.DropColumn(
                name: "LossReasonId",
                table: "LostDetails");

            migrationBuilder.DropColumn(
                name: "descripcion",
                table: "LostDetails");

            migrationBuilder.DropColumn(
                name: "reasonId",
                table: "LostDetails");
        }
    }
}
