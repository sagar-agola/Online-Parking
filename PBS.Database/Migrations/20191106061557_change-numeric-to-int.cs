using Microsoft.EntityFrameworkCore.Migrations;

namespace PBS.Database.Migrations
{
    public partial class changenumerictoint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "HourlyRate",
                table: "Slots",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(decimal),
                oldType: "numeric(2,2)",
                oldDefaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "HourlyRate",
                table: "Slots",
                type: "numeric(2,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(int),
                oldDefaultValue: 0);
        }
    }
}
