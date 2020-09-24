using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAppVega1.Migrations
{
    public partial class AddVehicle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Models_Makes_MakeId",
                table: "Models");

            migrationBuilder.RenameColumn(
                name: "MakeId",
                table: "Models",
                newName: "MakeID");

            migrationBuilder.RenameIndex(
                name: "IX_Models_MakeId",
                table: "Models",
                newName: "IX_Models_MakeID");

            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    FeatureId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FeatureName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.FeatureId);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ModelId = table.Column<int>(nullable: false),
                    IsRegistered = table.Column<bool>(nullable: false),
                    ContactName = table.Column<string>(maxLength: 255, nullable: true),
                    ContactEmail = table.Column<string>(maxLength: 255, nullable: true),
                    ContactPhone = table.Column<string>(maxLength: 255, nullable: true),
                    LastUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicles_Models_ModelId",
                        column: x => x.ModelId,
                        principalTable: "Models",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VehicleFeatures",
                columns: table => new
                {
                    VehicleId = table.Column<int>(nullable: false),
                    FeatureId = table.Column<int>(nullable: false),
                    FeatureVehicleId = table.Column<int>(nullable: true),
                    FeatureId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleFeatures", x => new { x.VehicleId, x.FeatureId });
                    table.UniqueConstraint("AK_VehicleFeatures_FeatureId_VehicleId", x => new { x.FeatureId, x.VehicleId });
                    table.ForeignKey(
                        name: "FK_VehicleFeatures_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VehicleFeatures_VehicleFeatures_FeatureVehicleId_FeatureId1",
                        columns: x => new { x.FeatureVehicleId, x.FeatureId1 },
                        principalTable: "VehicleFeatures",
                        principalColumns: new[] { "VehicleId", "FeatureId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VehicleFeatures_FeatureVehicleId_FeatureId1",
                table: "VehicleFeatures",
                columns: new[] { "FeatureVehicleId", "FeatureId1" });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_ModelId",
                table: "Vehicles",
                column: "ModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Models_Makes_MakeID",
                table: "Models",
                column: "MakeID",
                principalTable: "Makes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Models_Makes_MakeID",
                table: "Models");

            migrationBuilder.DropTable(
                name: "Features");

            migrationBuilder.DropTable(
                name: "VehicleFeatures");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.RenameColumn(
                name: "MakeID",
                table: "Models",
                newName: "MakeId");

            migrationBuilder.RenameIndex(
                name: "IX_Models_MakeID",
                table: "Models",
                newName: "IX_Models_MakeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Models_Makes_MakeId",
                table: "Models",
                column: "MakeId",
                principalTable: "Makes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
