using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRent.Migrations
{
    public partial class tableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApartmentsAndFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApartmentFeatureId = table.Column<int>(type: "int", nullable: true),
                    ApartmentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApartmentsAndFeatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApartmentsAndFeatures_ApartmentFeatures_ApartmentFeatureId",
                        column: x => x.ApartmentFeatureId,
                        principalTable: "ApartmentFeatures",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApartmentsAndFeatures_Apartments_ApartmentId",
                        column: x => x.ApartmentId,
                        principalTable: "Apartments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApartmentsAndFeatures_ApartmentFeatureId",
                table: "ApartmentsAndFeatures",
                column: "ApartmentFeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_ApartmentsAndFeatures_ApartmentId",
                table: "ApartmentsAndFeatures",
                column: "ApartmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApartmentsAndFeatures");
        }
    }
}
