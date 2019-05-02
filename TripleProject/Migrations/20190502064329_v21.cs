using Microsoft.EntityFrameworkCore.Migrations;

namespace TripleProject.Migrations
{
    public partial class v21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GalleryId1",
                table: "Advertisements",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_GalleryId1",
                table: "Advertisements",
                column: "GalleryId1");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_ImageId",
                table: "Advertisements",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisements_FileUploads_GalleryId1",
                table: "Advertisements",
                column: "GalleryId1",
                principalTable: "FileUploads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisements_FileUploads_ImageId",
                table: "Advertisements",
                column: "ImageId",
                principalTable: "FileUploads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertisements_FileUploads_GalleryId1",
                table: "Advertisements");

            migrationBuilder.DropForeignKey(
                name: "FK_Advertisements_FileUploads_ImageId",
                table: "Advertisements");

            migrationBuilder.DropIndex(
                name: "IX_Advertisements_GalleryId1",
                table: "Advertisements");

            migrationBuilder.DropIndex(
                name: "IX_Advertisements_ImageId",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "GalleryId1",
                table: "Advertisements");
        }
    }
}
