using Microsoft.EntityFrameworkCore.Migrations;

namespace PBS.Database.Migrations
{
    public partial class removeslotpricefromparkinglot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HourlyRate",
                table: "ParkingLots");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "HourlyRate",
                table: "ParkingLots",
                type: "decimal(2,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
