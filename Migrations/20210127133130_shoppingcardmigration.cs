using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Marketing.Migrations
{
    public partial class shoppingcardmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "message",
                table: "contacts",
                maxLength: 600,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.CreateTable(
                name: "shopping_Orders",
                columns: table => new
                {
                    shopping_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    code_order = table.Column<string>(nullable: true),
                    name = table.Column<string>(maxLength: 400, nullable: false),
                    address = table.Column<string>(maxLength: 700, nullable: false),
                    phone = table.Column<string>(nullable: false),
                    time_order = table.Column<DateTime>(nullable: false),
                    pro_id = table.Column<int>(nullable: false),
                    pr_oname = table.Column<string>(nullable: true),
                    pro_image = table.Column<string>(nullable: true),
                    qty = table.Column<int>(nullable: false),
                    price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shopping_Orders", x => x.shopping_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "shopping_Orders");

            migrationBuilder.AlterColumn<string>(
                name: "message",
                table: "contacts",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 600);
        }
    }
}
