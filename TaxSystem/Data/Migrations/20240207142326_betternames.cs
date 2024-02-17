using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaxSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class betternames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RequiredHours",
                table: "Services",
                newName: "RequiredMinutes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RequiredMinutes",
                table: "Services",
                newName: "RequiredHours");
        }
    }
}
