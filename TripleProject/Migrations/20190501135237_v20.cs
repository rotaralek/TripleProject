using Microsoft.EntityFrameworkCore.Migrations;

namespace TripleProject.Migrations
{
    public partial class v20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GalleryId",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Products",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GalleryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Products");
        }
    }
}
