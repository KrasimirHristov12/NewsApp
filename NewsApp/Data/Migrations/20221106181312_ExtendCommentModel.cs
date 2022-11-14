using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsApp.Data.Migrations
{
    public partial class ExtendCommentModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OuterCommentId",
                table: "Comments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_OuterCommentId",
                table: "Comments",
                column: "OuterCommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Comments_OuterCommentId",
                table: "Comments",
                column: "OuterCommentId",
                principalTable: "Comments",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Comments_OuterCommentId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_OuterCommentId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "OuterCommentId",
                table: "Comments");
        }
    }
}
