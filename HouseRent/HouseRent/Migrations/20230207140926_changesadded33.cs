using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRent.Migrations
{
    public partial class changesadded33 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApartmentFeatures_Apartments_ApartmentId",
                table: "ApartmentFeatures");

            migrationBuilder.DropIndex(
                name: "IX_ApartmentFeatures_ApartmentId",
                table: "ApartmentFeatures");

            migrationBuilder.DropColumn(
                name: "ApartmentId",
                table: "ApartmentFeatures");

            migrationBuilder.AddColumn<int>(
                name: "ApartmentFeatureID",
                table: "Apartments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "FeatureName",
                table: "ApartmentFeatures",
                type: "nvarchar(90)",
                maxLength: 90,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "ApartmentApartmentFeature",
                columns: table => new
                {
                    ApartmentFeaturesId = table.Column<int>(type: "int", nullable: false),
                    ApartmentsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApartmentApartmentFeature", x => new { x.ApartmentFeaturesId, x.ApartmentsId });
                    table.ForeignKey(
                        name: "FK_ApartmentApartmentFeature_ApartmentFeatures_ApartmentFeaturesId",
                        column: x => x.ApartmentFeaturesId,
                        principalTable: "ApartmentFeatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApartmentApartmentFeature_Apartments_ApartmentsId",
                        column: x => x.ApartmentsId,
                        principalTable: "Apartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApartmentApartmentFeature_ApartmentsId",
                table: "ApartmentApartmentFeature",
                column: "ApartmentsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApartmentApartmentFeature");

            migrationBuilder.DropColumn(
                name: "ApartmentFeatureID",
                table: "Apartments");

            migrationBuilder.AlterColumn<string>(
                name: "FeatureName",
                table: "ApartmentFeatures",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(90)",
                oldMaxLength: 90,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApartmentId",
                table: "ApartmentFeatures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ApartmentFeatures_ApartmentId",
                table: "ApartmentFeatures",
                column: "ApartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApartmentFeatures_Apartments_ApartmentId",
                table: "ApartmentFeatures",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
