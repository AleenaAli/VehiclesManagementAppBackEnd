using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAppVega1.Migrations
{
    public partial class vehicleIdInPhoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Vehicles_VehicleId",
                table: "Photos");

            migrationBuilder.RenameColumn(
                name: "VehicleId",
                table: "Photos",
                newName: "vehicleId");

            migrationBuilder.RenameIndex(
                name: "IX_Photos_VehicleId",
                table: "Photos",
                newName: "IX_Photos_vehicleId");

            migrationBuilder.AlterColumn<int>(
                name: "vehicleId",
                table: "Photos",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Vehicles_vehicleId",
                table: "Photos",
                column: "vehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Vehicles_vehicleId",
                table: "Photos");

            migrationBuilder.RenameColumn(
                name: "vehicleId",
                table: "Photos",
                newName: "VehicleId");

            migrationBuilder.RenameIndex(
                name: "IX_Photos_vehicleId",
                table: "Photos",
                newName: "IX_Photos_VehicleId");

            migrationBuilder.AlterColumn<int>(
                name: "VehicleId",
                table: "Photos",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Vehicles_VehicleId",
                table: "Photos",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
