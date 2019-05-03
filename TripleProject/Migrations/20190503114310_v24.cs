using Microsoft.EntityFrameworkCore.Migrations;

namespace TripleProject.Migrations
{
    public partial class v24 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertisements_FileUploads_ImageId",
                table: "Advertisements");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_FileUploads_ImageId",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "ImageId",
                table: "Products",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ImageId",
                table: "Advertisements",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisements_FileUploads_ImageId",
                table: "Advertisements",
                column: "ImageId",
                principalTable: "FileUploads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_FileUploads_ImageId",
                table: "Products",
                column: "ImageId",
                principalTable: "FileUploads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertisements_FileUploads_ImageId",
                table: "Advertisements");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_FileUploads_ImageId",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "ImageId",
                table: "Products",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ImageId",
                table: "Advertisements",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisements_FileUploads_ImageId",
                table: "Advertisements",
                column: "ImageId",
                principalTable: "FileUploads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_FileUploads_ImageId",
                table: "Products",
                column: "ImageId",
                principalTable: "FileUploads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
