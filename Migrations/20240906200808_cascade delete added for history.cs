using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HishabNikash.Migrations
{
    /// <inheritdoc />
    public partial class cascadedeleteaddedforhistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_History_Hishabs_HishabID",
                table: "History");

            migrationBuilder.AlterColumn<int>(
                name: "HishabID",
                table: "History",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_History_Hishabs_HishabID",
                table: "History",
                column: "HishabID",
                principalTable: "Hishabs",
                principalColumn: "HishabID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_History_Hishabs_HishabID",
                table: "History");

            migrationBuilder.AlterColumn<int>(
                name: "HishabID",
                table: "History",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_History_Hishabs_HishabID",
                table: "History",
                column: "HishabID",
                principalTable: "Hishabs",
                principalColumn: "HishabID");
        }
    }
}
