using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HishabNikash.Migrations
{
    /// <inheritdoc />
    public partial class keyNameChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Users",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "HistoryID",
                table: "History",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "HishabID",
                table: "Hishabs",
                newName: "ID");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreatedDate",
                value: "10/5/2024 7:02:43 PM");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Users",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "History",
                newName: "HistoryID");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Hishabs",
                newName: "HishabID");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                column: "CreatedDate",
                value: "10/5/2024 6:35:44 PM");
        }
    }
}
