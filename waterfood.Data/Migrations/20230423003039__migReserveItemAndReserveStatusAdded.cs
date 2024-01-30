using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace waterfood.Data.Migrations
{
    public partial class _migReserveItemAndReserveStatusAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReserveItemStatus",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReserveItemStatus", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "ReserveStatus",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReserveStatus", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "Reserve",
                columns: table => new
                {
                    ReserveId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserRef = table.Column<int>(type: "int", nullable: false),
                    StatusRef = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reserve", x => x.ReserveId);
                    table.ForeignKey(
                        name: "FK_Reserve_ReserveStatus_StatusRef",
                        column: x => x.StatusRef,
                        principalTable: "ReserveStatus",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reserve_Users_UserRef",
                        column: x => x.UserRef,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReserveItem",
                columns: table => new
                {
                    ReserveItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReserveRef = table.Column<int>(type: "int", nullable: false),
                    StatusRef = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReserveItem", x => x.ReserveItemId);
                    table.ForeignKey(
                        name: "FK_ReserveItem_Reserve_ReserveRef",
                        column: x => x.ReserveRef,
                        principalTable: "Reserve",
                        principalColumn: "ReserveId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReserveItem_ReserveItemStatus_StatusRef",
                        column: x => x.StatusRef,
                        principalTable: "ReserveItemStatus",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reserve_StatusRef",
                table: "Reserve",
                column: "StatusRef");

            migrationBuilder.CreateIndex(
                name: "IX_Reserve_UserRef",
                table: "Reserve",
                column: "UserRef");

            migrationBuilder.CreateIndex(
                name: "IX_ReserveItem_ReserveRef",
                table: "ReserveItem",
                column: "ReserveRef");

            migrationBuilder.CreateIndex(
                name: "IX_ReserveItem_StatusRef",
                table: "ReserveItem",
                column: "StatusRef");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReserveItem");

            migrationBuilder.DropTable(
                name: "Reserve");

            migrationBuilder.DropTable(
                name: "ReserveItemStatus");

            migrationBuilder.DropTable(
                name: "ReserveStatus");
        }
    }
}
