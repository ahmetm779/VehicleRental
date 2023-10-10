using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleRentalProject.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class UpdateVehicleRental : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rental_AspNetUsers_UserId",
                table: "Rental");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Rental",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Rental_UserId",
                table: "Rental",
                newName: "IX_Rental_ApplicationUserId");

            migrationBuilder.AddColumn<decimal>(
                name: "DailyRate",
                table: "Vehicles",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "Rental",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Rental_AspNetUsers_ApplicationUserId",
                table: "Rental",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rental_AspNetUsers_ApplicationUserId",
                table: "Rental");

            migrationBuilder.DropColumn(
                name: "DailyRate",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "Rental");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Rental",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Rental_ApplicationUserId",
                table: "Rental",
                newName: "IX_Rental_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rental_AspNetUsers_UserId",
                table: "Rental",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
