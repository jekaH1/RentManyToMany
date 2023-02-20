using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRent.Migrations
{
    public partial class aparmentIdforOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApartmentId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ApartmentId",
                table: "Orders",
                column: "ApartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Apartments_ApartmentId",
                table: "Orders",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Apartments_ApartmentId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ApartmentId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ApartmentId",
                table: "Orders");
        }
    }
}
