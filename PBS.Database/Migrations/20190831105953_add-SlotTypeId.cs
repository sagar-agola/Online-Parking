using Microsoft.EntityFrameworkCore.Migrations;

namespace PBS.Database.Migrations
{
    public partial class addSlotTypeId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slots_SlotTypes_SlotTypeId",
                table: "Slots");

            migrationBuilder.DropColumn(
                name: "SlotId",
                table: "Slots");

            migrationBuilder.AlterColumn<int>(
                name: "SlotTypeId",
                table: "Slots",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Slots_SlotTypes_SlotTypeId",
                table: "Slots",
                column: "SlotTypeId",
                principalTable: "SlotTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slots_SlotTypes_SlotTypeId",
                table: "Slots");

            migrationBuilder.AlterColumn<int>(
                name: "SlotTypeId",
                table: "Slots",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "SlotId",
                table: "Slots",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Slots_SlotTypes_SlotTypeId",
                table: "Slots",
                column: "SlotTypeId",
                principalTable: "SlotTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
