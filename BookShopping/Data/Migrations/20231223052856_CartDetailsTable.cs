using Microsoft.EntityFrameworkCore.Migrations;

namespace BookShopping.Data.Migrations
{
    public partial class CartDetailsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UnitPrice",
                table: "CartDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "CartDetail");
        }
    }
}
