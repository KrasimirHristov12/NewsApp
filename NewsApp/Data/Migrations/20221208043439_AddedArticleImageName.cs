using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsApp.Data.Migrations
{
    public partial class AddedArticleImageName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Articles");
        }
    }
}
