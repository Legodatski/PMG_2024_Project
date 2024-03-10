using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaxSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class betterNames2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Services_ServiceId",
                table: "Requests");

            migrationBuilder.RenameColumn(
                name: "ServiceId",
                table: "Requests",
                newName: "AmenityId");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_ServiceId",
                table: "Requests",
                newName: "IX_Requests_AmenityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Services_AmenityId",
                table: "Requests",
                column: "AmenityId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Services_AmenityId",
                table: "Requests");

            migrationBuilder.RenameColumn(
                name: "AmenityId",
                table: "Requests",
                newName: "ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_AmenityId",
                table: "Requests",
                newName: "IX_Requests_ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Services_ServiceId",
                table: "Requests",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
