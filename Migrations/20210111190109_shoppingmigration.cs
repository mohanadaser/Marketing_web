using Microsoft.EntityFrameworkCore.Migrations;

namespace Marketing.Migrations
{
    public partial class shoppingmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "productTypes",
                columns: table => new
                {
                    typeid = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    proytpe = table.Column<string>(maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productTypes", x => x.typeid);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(maxLength: 250, nullable: false),
                    email = table.Column<string>(maxLength: 250, nullable: false),
                    password = table.Column<string>(maxLength: 100, nullable: false),
                    confirmpassword = table.Column<string>(maxLength: 100, nullable: false),
                    phone = table.Column<string>(maxLength: 250, nullable: false),
                    chkemail = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    pro_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    proname = table.Column<string>(maxLength: 250, nullable: false),
                    proimg = table.Column<string>(maxLength: 250, nullable: true),
                    prodesc = table.Column<string>(maxLength: 700, nullable: true),
                    price = table.Column<decimal>(maxLength: 50, nullable: false),
                    typeid = table.Column<int>(nullable: false),
                    productTypetypeid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.pro_id);
                    table.ForeignKey(
                        name: "FK_products_productTypes_productTypetypeid",
                        column: x => x.productTypetypeid,
                        principalTable: "productTypes",
                        principalColumn: "typeid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_products_productTypetypeid",
                table: "products",
                column: "productTypetypeid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "productTypes");
        }
    }
}
