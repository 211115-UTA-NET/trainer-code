using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RpsApi.DataStorage.Migrations
{
    public partial class AddSeedMoves : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Moves",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Rock" });

            migrationBuilder.InsertData(
                table: "Moves",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Paper" });

            migrationBuilder.InsertData(
                table: "Moves",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Scissors" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Moves",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Moves",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Moves",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
