using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EccomerceApi.Migrations
{
    /// <inheritdoc />
    public partial class AddPeopleUserRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PeopleId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Peoples",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Peoples", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PeopleId",
                table: "AspNetUsers",
                column: "PeopleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Peoples_PeopleId",
                table: "AspNetUsers",
                column: "PeopleId",
                principalTable: "Peoples",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Peoples_PeopleId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Peoples");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PeopleId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PeopleId",
                table: "AspNetUsers");
        }
    }
}
