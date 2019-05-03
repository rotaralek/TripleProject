using Microsoft.EntityFrameworkCore.Migrations;

namespace TripleProject.Migrations
{
    public partial class v23 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Products",
                maxLength: 10000,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Advertisements",
                maxLength: 10000,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 1000);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Products",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 10000);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Advertisements",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 10000);
        }
    }
}
