using Microsoft.EntityFrameworkCore.Migrations;

namespace Marketing.Migrations
{
    public partial class cntactmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_productTypes_productTypetypeid",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_products_productTypetypeid",
                table: "products");

            migrationBuilder.DropColumn(
                name: "productTypetypeid",
                table: "products");

            migrationBuilder.CreateTable(
                name: "contacts",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: false),
                    message = table.Column<string>(maxLength: 100, nullable: false),
                    phone_number = table.Column<string>(maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contacts", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_products_typeid",
                table: "products",
                column: "typeid");

            migrationBuilder.AddForeignKey(
                name: "FK_products_productTypes_typeid",
                table: "products",
                column: "typeid",
                principalTable: "productTypes",
                principalColumn: "typeid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_productTypes_typeid",
                table: "products");

            migrationBuilder.DropTable(
                name: "contacts");

            migrationBuilder.DropIndex(
                name: "IX_products_typeid",
                table: "products");

            migrationBuilder.AddColumn<int>(
                name: "productTypetypeid",
                table: "products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_products_productTypetypeid",
                table: "products",
                column: "productTypetypeid");

            migrationBuilder.AddForeignKey(
                name: "FK_products_productTypes_productTypetypeid",
                table: "products",
                column: "productTypetypeid",
                principalTable: "productTypes",
                principalColumn: "typeid",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
