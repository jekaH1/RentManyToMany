using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRent.Migrations
{
    public partial class TotalViewCountPostEdited : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalCommentCount",
                table: "BlogPostComments");

            migrationBuilder.DropColumn(
                name: "TotalViewCount",
                table: "BlogPostComments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalCommentCount",
                table: "BlogPostComments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalViewCount",
                table: "BlogPostComments",
                type: "int",
                nullable: true);
        }
    }
}
