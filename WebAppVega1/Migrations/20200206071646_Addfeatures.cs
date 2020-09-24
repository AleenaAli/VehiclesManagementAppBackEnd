using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAppVega1.Migrations
{
    public partial class Addfeatures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ContactName",
                table: "Vehicles",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 255,
                oldNullable: true);


            migrationBuilder.Sql("INSERT INTO Features (FeatureName) VALUES ('Airbags')");
            migrationBuilder.Sql("INSERT INTO Features (FeatureName) VALUES ('Brakes')");
            migrationBuilder.Sql("INSERT INTO Features (FeatureName) VALUES ('Camera')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ContactName",
                table: "Vehicles",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255);
        }
    }
}
