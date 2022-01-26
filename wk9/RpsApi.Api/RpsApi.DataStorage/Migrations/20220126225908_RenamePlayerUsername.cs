using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RpsApi.DataStorage.Migrations
{
    public partial class RenamePlayerUsername : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Players",
                newName: "Username");

            migrationBuilder.RenameIndex(
                name: "IX_Players_Name",
                table: "Players",
                newName: "IX_Players_Username");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Players",
                newName: "Name");

            migrationBuilder.RenameIndex(
                name: "IX_Players_Username",
                table: "Players",
                newName: "IX_Players_Name");
        }
    }
}
