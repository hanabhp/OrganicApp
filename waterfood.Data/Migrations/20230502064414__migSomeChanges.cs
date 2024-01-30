using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace waterfood.Data.Migrations
{
    public partial class _migSomeChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReserveItems_Items_itemRef",
                table: "ReserveItems");

            migrationBuilder.RenameColumn(
                name: "itemRef",
                table: "ReserveItems",
                newName: "ItemRef");

            migrationBuilder.RenameIndex(
                name: "IX_ReserveItems_itemRef",
                table: "ReserveItems",
                newName: "IX_ReserveItems_ItemRef");

            migrationBuilder.AddColumn<int>(
                name: "CenterRef",
                table: "Reserves",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "FavoriteCenters",
                columns: table => new
                {
                    FavoriteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserRef = table.Column<int>(type: "int", nullable: false),
                    CenterRef = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteCenters", x => x.FavoriteId);
                    table.ForeignKey(
                        name: "FK_FavoriteCenters_Centers_CenterRef",
                        column: x => x.CenterRef,
                        principalTable: "Centers",
                        principalColumn: "CenterId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FavoriteCenters_Users_UserRef",
                        column: x => x.UserRef,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reserves_CenterRef",
                table: "Reserves",
                column: "CenterRef");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteCenters_CenterRef",
                table: "FavoriteCenters",
                column: "CenterRef");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteCenters_UserRef",
                table: "FavoriteCenters",
                column: "UserRef");

            migrationBuilder.AddForeignKey(
                name: "FK_ReserveItems_Items_ItemRef",
                table: "ReserveItems",
                column: "ItemRef",
                principalTable: "Items",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reserves_Centers_CenterRef",
                table: "Reserves",
                column: "CenterRef",
                principalTable: "Centers",
                principalColumn: "CenterId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReserveItems_Items_ItemRef",
                table: "ReserveItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Reserves_Centers_CenterRef",
                table: "Reserves");

            migrationBuilder.DropTable(
                name: "FavoriteCenters");

            migrationBuilder.DropIndex(
                name: "IX_Reserves_CenterRef",
                table: "Reserves");

            migrationBuilder.DropColumn(
                name: "CenterRef",
                table: "Reserves");

            migrationBuilder.RenameColumn(
                name: "ItemRef",
                table: "ReserveItems",
                newName: "itemRef");

            migrationBuilder.RenameIndex(
                name: "IX_ReserveItems_ItemRef",
                table: "ReserveItems",
                newName: "IX_ReserveItems_itemRef");

            migrationBuilder.AddForeignKey(
                name: "FK_ReserveItems_Items_itemRef",
                table: "ReserveItems",
                column: "itemRef",
                principalTable: "Items",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
