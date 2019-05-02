using Microsoft.EntityFrameworkCore.Migrations;

namespace TripleProject.Migrations
{
    public partial class v22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GalleryId1",
                table: "Products",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_GalleryId1",
                table: "Products",
                column: "GalleryId1");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ImageId",
                table: "Products",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_FileUploads_GalleryId1",
                table: "Products",
                column: "GalleryId1",
                principalTable: "FileUploads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_FileUploads_ImageId",
                table: "Products",
                column: "ImageId",
                principalTable: "FileUploads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_FileUploads_GalleryId1",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_FileUploads_ImageId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_GalleryId1",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ImageId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "GalleryId1",
                table: "Products");
        }
    }
}
