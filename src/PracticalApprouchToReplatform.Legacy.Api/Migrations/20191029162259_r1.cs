using Microsoft.EntityFrameworkCore.Migrations;

namespace PracticalApprouchToReplatform.Legacy.Api.Migrations
{
    public partial class r1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Barcode",
                table: "Packages",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Destination",
                table: "Packages",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Packages_Barcode",
                table: "Packages",
                column: "Barcode",
                unique: true,
                filter: "[Barcode] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Packages_Barcode",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "Destination",
                table: "Packages");

            migrationBuilder.AlterColumn<string>(
                name: "Barcode",
                table: "Packages",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
