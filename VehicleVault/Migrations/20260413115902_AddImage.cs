using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleVault.Migrations
{
    /// <inheritdoc />
    public partial class AddImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Mileage",
                table: "Vehicles",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Vehicles");

            migrationBuilder.AlterColumn<float>(
                name: "Mileage",
                table: "Vehicles",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
