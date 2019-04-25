using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TripleProject.Migrations
{
    public partial class v11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertisements_Attribute_AttributeId",
                table: "Advertisements");

            migrationBuilder.DropTable(
                name: "Attribute");

            migrationBuilder.AddColumn<int>(
                name: "AttributeId",
                table: "Products",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AdvertisementAttributes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvertisementAttributes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductAttributes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttributes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_AttributeId",
                table: "Products",
                column: "AttributeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisements_AdvertisementAttributes_AttributeId",
                table: "Advertisements",
                column: "AttributeId",
                principalTable: "AdvertisementAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductAttributes_AttributeId",
                table: "Products",
                column: "AttributeId",
                principalTable: "ProductAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertisements_AdvertisementAttributes_AttributeId",
                table: "Advertisements");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductAttributes_AttributeId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "AdvertisementAttributes");

            migrationBuilder.DropTable(
                name: "ProductAttributes");

            migrationBuilder.DropIndex(
                name: "IX_Products_AttributeId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "AttributeId",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "Attribute",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attribute", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisements_Attribute_AttributeId",
                table: "Advertisements",
                column: "AttributeId",
                principalTable: "Attribute",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
