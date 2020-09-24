using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAppVega1.Migrations
{
    public partial class contactClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleFeatures_VehicleFeatures_FeatureVehicleId_FeatureId1",
                table: "VehicleFeatures");

            migrationBuilder.DropIndex(
                name: "IX_VehicleFeatures_FeatureVehicleId_FeatureId1",
                table: "VehicleFeatures");

            migrationBuilder.DropColumn(
                name: "ContactEmail",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "ContactName",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "ContactPhone",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "FeatureId1",
                table: "VehicleFeatures");

            migrationBuilder.DropColumn(
                name: "FeatureVehicleId",
                table: "VehicleFeatures");

            migrationBuilder.AddColumn<int>(
                name: "ContactId",
                table: "Vehicles",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContactName = table.Column<string>(maxLength: 255, nullable: false),
                    ContactEmail = table.Column<string>(maxLength: 255, nullable: true),
                    ContactPhone = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_ContactId",
                table: "Vehicles",
                column: "ContactId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Contact_ContactId",
                table: "Vehicles",
                column: "ContactId",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Contact_ContactId",
                table: "Vehicles");

            migrationBuilder.DropTable(
                name: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_ContactId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "ContactId",
                table: "Vehicles");

            migrationBuilder.AddColumn<string>(
                name: "ContactEmail",
                table: "Vehicles",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactName",
                table: "Vehicles",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContactPhone",
                table: "Vehicles",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FeatureId1",
                table: "VehicleFeatures",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FeatureVehicleId",
                table: "VehicleFeatures",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VehicleFeatures_FeatureVehicleId_FeatureId1",
                table: "VehicleFeatures",
                columns: new[] { "FeatureVehicleId", "FeatureId1" });

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleFeatures_VehicleFeatures_FeatureVehicleId_FeatureId1",
                table: "VehicleFeatures",
                columns: new[] { "FeatureVehicleId", "FeatureId1" },
                principalTable: "VehicleFeatures",
                principalColumns: new[] { "VehicleId", "FeatureId" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
