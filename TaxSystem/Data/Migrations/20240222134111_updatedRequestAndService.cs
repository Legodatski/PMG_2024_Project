using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaxSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatedRequestAndService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RequiredMinutes",
                table: "Services",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "Requests",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Time",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Requests");

            migrationBuilder.AlterColumn<double>(
                name: "RequiredMinutes",
                table: "Services",
                type: "float",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
