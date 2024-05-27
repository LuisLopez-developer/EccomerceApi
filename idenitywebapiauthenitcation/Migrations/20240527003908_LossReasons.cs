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
                name: "LossReasonid",
                table: "LostDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LossReasons", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LostDetails_LossReasonid",
                table: "LostDetails",
                column: "LossReasonid");

            migrationBuilder.AddForeignKey(
                name: "FK_LostDetails_LossReasons_LossReasonid",
                table: "LostDetails",
                column: "LossReasonid",
                principalTable: "LossReasons",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LostDetails_LossReasons_LossReasonid",
                table: "LostDetails");

            migrationBuilder.DropTable(
                name: "LossReasons");

            migrationBuilder.DropIndex(
                name: "IX_LostDetails_LossReasonid",
                table: "LostDetails");

            migrationBuilder.DropColumn(
                name: "LossReasonid",
                table: "LostDetails");

            migrationBuilder.DropColumn(
                name: "reasonId",
                table: "LostDetails");
        }
    }
}
