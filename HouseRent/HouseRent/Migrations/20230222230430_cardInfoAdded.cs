using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRent.Migrations
{
    public partial class cardInfoAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "OneDayPrice",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<byte>(
                name: "CVV",
                table: "Orders",
                type: "tinyint",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "CardMonth",
                table: "Orders",
                type: "tinyint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CardNum",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "CardYear",
                table: "Orders",
                type: "tinyint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameOncard",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CVV",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CardMonth",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CardNum",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CardYear",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "NameOncard",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "OneDayPrice",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
