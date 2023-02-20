using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRent.Migrations
{
    public partial class blogPostsEdited : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalLikeCount",
                table: "BlogPostComments");

            migrationBuilder.RenameColumn(
                name: "PostDate",
                table: "BlogPosts",
                newName: "BlogPostDate");

            migrationBuilder.RenameColumn(
                name: "Message",
                table: "BlogPosts",
                newName: "Quote");

            migrationBuilder.RenameColumn(
                name: "MessageTime",
                table: "BlogPostComments",
                newName: "UserMessageTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quote",
                table: "BlogPosts",
                newName: "Message");

            migrationBuilder.RenameColumn(
                name: "BlogPostDate",
                table: "BlogPosts",
                newName: "PostDate");

            migrationBuilder.RenameColumn(
                name: "UserMessageTime",
                table: "BlogPostComments",
                newName: "MessageTime");

            migrationBuilder.AddColumn<int>(
                name: "TotalLikeCount",
                table: "BlogPostComments",
                type: "int",
                nullable: true);
        }
    }
}
