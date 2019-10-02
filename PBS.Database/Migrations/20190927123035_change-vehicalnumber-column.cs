using Microsoft.EntityFrameworkCore.Migrations;

namespace PBS.Database.Migrations
{
    public partial class changevehicalnumbercolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "VehicleNumber",
                table: "Bookings",
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 10);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "VehicleNumber",
                table: "Bookings",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 13);
        }
    }
}
