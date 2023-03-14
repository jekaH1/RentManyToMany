using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRent.Migrations
{
    public partial class settingsUpdate22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Img",
                table: "SettingsRH",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsImage",
                table: "SettingsRH",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Img",
                table: "SettingsRH");

            migrationBuilder.DropColumn(
                name: "IsImage",
                table: "SettingsRH");
        }
    }
}
