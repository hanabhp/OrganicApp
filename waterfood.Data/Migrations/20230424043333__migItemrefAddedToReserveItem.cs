using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace waterfood.Data.Migrations
{
    public partial class _migItemrefAddedToReserveItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reserve_ReserveStatus_StatusRef",
                table: "Reserve");

            migrationBuilder.DropForeignKey(
                name: "FK_Reserve_Users_UserRef",
                table: "Reserve");

            migrationBuilder.DropForeignKey(
                name: "FK_ReserveItem_Reserve_ReserveRef",
                table: "ReserveItem");

            migrationBuilder.DropForeignKey(
                name: "FK_ReserveItem_ReserveItemStatus_StatusRef",
                table: "ReserveItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReserveStatus",
                table: "ReserveStatus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReserveItemStatus",
                table: "ReserveItemStatus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReserveItem",
                table: "ReserveItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reserve",
                table: "Reserve");

            migrationBuilder.RenameTable(
                name: "ReserveStatus",
                newName: "ReserveStatuses");

            migrationBuilder.RenameTable(
                name: "ReserveItemStatus",
                newName: "ReserveItemStatuses");

            migrationBuilder.RenameTable(
                name: "ReserveItem",
                newName: "ReserveItems");

            migrationBuilder.RenameTable(
                name: "Reserve",
                newName: "Reserves");

            migrationBuilder.RenameIndex(
                name: "IX_ReserveItem_StatusRef",
                table: "ReserveItems",
                newName: "IX_ReserveItems_StatusRef");

            migrationBuilder.RenameIndex(
                name: "IX_ReserveItem_ReserveRef",
                table: "ReserveItems",
                newName: "IX_ReserveItems_ReserveRef");

            migrationBuilder.RenameIndex(
                name: "IX_Reserve_UserRef",
                table: "Reserves",
                newName: "IX_Reserves_UserRef");

            migrationBuilder.RenameIndex(
                name: "IX_Reserve_StatusRef",
                table: "Reserves",
                newName: "IX_Reserves_StatusRef");

            migrationBuilder.AddColumn<int>(
                name: "itemRef",
                table: "ReserveItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReserveStatuses",
                table: "ReserveStatuses",
                column: "StatusId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReserveItemStatuses",
                table: "ReserveItemStatuses",
                column: "StatusId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReserveItems",
                table: "ReserveItems",
                column: "ReserveItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reserves",
                table: "Reserves",
                column: "ReserveId");

            migrationBuilder.CreateIndex(
                name: "IX_ReserveItems_itemRef",
                table: "ReserveItems",
                column: "itemRef");

            migrationBuilder.AddForeignKey(
                name: "FK_ReserveItems_Items_itemRef",
                table: "ReserveItems",
                column: "itemRef",
                principalTable: "Items",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReserveItems_ReserveItemStatuses_StatusRef",
                table: "ReserveItems",
                column: "StatusRef",
                principalTable: "ReserveItemStatuses",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReserveItems_Reserves_ReserveRef",
                table: "ReserveItems",
                column: "ReserveRef",
                principalTable: "Reserves",
                principalColumn: "ReserveId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reserves_ReserveStatuses_StatusRef",
                table: "Reserves",
                column: "StatusRef",
                principalTable: "ReserveStatuses",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reserves_Users_UserRef",
                table: "Reserves",
                column: "UserRef",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReserveItems_Items_itemRef",
                table: "ReserveItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ReserveItems_ReserveItemStatuses_StatusRef",
                table: "ReserveItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ReserveItems_Reserves_ReserveRef",
                table: "ReserveItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Reserves_ReserveStatuses_StatusRef",
                table: "Reserves");

            migrationBuilder.DropForeignKey(
                name: "FK_Reserves_Users_UserRef",
                table: "Reserves");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReserveStatuses",
                table: "ReserveStatuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reserves",
                table: "Reserves");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReserveItemStatuses",
                table: "ReserveItemStatuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReserveItems",
                table: "ReserveItems");

            migrationBuilder.DropIndex(
                name: "IX_ReserveItems_itemRef",
                table: "ReserveItems");

            migrationBuilder.DropColumn(
                name: "itemRef",
                table: "ReserveItems");

            migrationBuilder.RenameTable(
                name: "ReserveStatuses",
                newName: "ReserveStatus");

            migrationBuilder.RenameTable(
                name: "Reserves",
                newName: "Reserve");

            migrationBuilder.RenameTable(
                name: "ReserveItemStatuses",
                newName: "ReserveItemStatus");

            migrationBuilder.RenameTable(
                name: "ReserveItems",
                newName: "ReserveItem");

            migrationBuilder.RenameIndex(
                name: "IX_Reserves_UserRef",
                table: "Reserve",
                newName: "IX_Reserve_UserRef");

            migrationBuilder.RenameIndex(
                name: "IX_Reserves_StatusRef",
                table: "Reserve",
                newName: "IX_Reserve_StatusRef");

            migrationBuilder.RenameIndex(
                name: "IX_ReserveItems_StatusRef",
                table: "ReserveItem",
                newName: "IX_ReserveItem_StatusRef");

            migrationBuilder.RenameIndex(
                name: "IX_ReserveItems_ReserveRef",
                table: "ReserveItem",
                newName: "IX_ReserveItem_ReserveRef");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReserveStatus",
                table: "ReserveStatus",
                column: "StatusId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reserve",
                table: "Reserve",
                column: "ReserveId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReserveItemStatus",
                table: "ReserveItemStatus",
                column: "StatusId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReserveItem",
                table: "ReserveItem",
                column: "ReserveItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reserve_ReserveStatus_StatusRef",
                table: "Reserve",
                column: "StatusRef",
                principalTable: "ReserveStatus",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reserve_Users_UserRef",
                table: "Reserve",
                column: "UserRef",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReserveItem_Reserve_ReserveRef",
                table: "ReserveItem",
                column: "ReserveRef",
                principalTable: "Reserve",
                principalColumn: "ReserveId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReserveItem_ReserveItemStatus_StatusRef",
                table: "ReserveItem",
                column: "StatusRef",
                principalTable: "ReserveItemStatus",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
