using Microsoft.EntityFrameworkCore.Migrations;

namespace TripleProject.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attributes_Catalogs_ParentId",
                table: "Attributes");

            migrationBuilder.DropForeignKey(
                name: "FK_Catalogs_Attributes_AttributeId",
                table: "Catalogs");

            migrationBuilder.DropIndex(
                name: "IX_Catalogs_AttributeId",
                table: "Catalogs");

            migrationBuilder.DropColumn(
                name: "AttributeId",
                table: "Catalogs");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Advertisements",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_UserId",
                table: "Products",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_UserId",
                table: "Advertisements",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisements_AspNetUsers_UserId",
                table: "Advertisements",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Attributes_Attributes_ParentId",
                table: "Attributes",
                column: "ParentId",
                principalTable: "Attributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_UserId",
                table: "Products",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertisements_AspNetUsers_UserId",
                table: "Advertisements");

            migrationBuilder.DropForeignKey(
                name: "FK_Attributes_Attributes_ParentId",
                table: "Attributes");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_UserId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_UserId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Advertisements_UserId",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Advertisements");

            migrationBuilder.AddColumn<int>(
                name: "AttributeId",
                table: "Catalogs",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Catalogs_AttributeId",
                table: "Catalogs",
                column: "AttributeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attributes_Catalogs_ParentId",
                table: "Attributes",
                column: "ParentId",
                principalTable: "Catalogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Catalogs_Attributes_AttributeId",
                table: "Catalogs",
                column: "AttributeId",
                principalTable: "Attributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
