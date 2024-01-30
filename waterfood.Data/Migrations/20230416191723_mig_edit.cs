using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace waterfood.Data.Migrations
{
    public partial class mig_edit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Distance",
                table: "Centers",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PickupTime",
                table: "Centers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Rate",
                table: "Centers",
                type: "float",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Distance",
                table: "Centers");

            migrationBuilder.DropColumn(
                name: "PickupTime",
                table: "Centers");

            migrationBuilder.DropColumn(
                name: "Rate",
                table: "Centers");
        }
    }
}
