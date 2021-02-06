using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PostsBoard.Migrations
{
    public partial class SecondMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostComment_Posts_PostID",
                table: "PostComment");

            migrationBuilder.DropForeignKey(
                name: "FK_PostComment_Users_UserID",
                table: "PostComment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostComment",
                table: "PostComment");

            migrationBuilder.RenameTable(
                name: "PostComment",
                newName: "Comments");

            migrationBuilder.RenameIndex(
                name: "IX_PostComment_UserID",
                table: "Comments",
                newName: "IX_Comments_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_PostComment_PostID",
                table: "Comments",
                newName: "IX_Comments_PostID");

            migrationBuilder.AddColumn<DateTime>(
                name: "CommentedDate",
                table: "Comments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedDate",
                table: "Comments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments",
                table: "Comments",
                column: "CommentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Posts_PostID",
                table: "Comments",
                column: "PostID",
                principalTable: "Posts",
                principalColumn: "PostID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_UserID",
                table: "Comments",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Posts_PostID",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_UserID",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comments",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "CommentedDate",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "LastUpdatedDate",
                table: "Comments");

            migrationBuilder.RenameTable(
                name: "Comments",
                newName: "PostComment");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_UserID",
                table: "PostComment",
                newName: "IX_PostComment_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_PostID",
                table: "PostComment",
                newName: "IX_PostComment_PostID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostComment",
                table: "PostComment",
                column: "CommentID");

            migrationBuilder.AddForeignKey(
                name: "FK_PostComment_Posts_PostID",
                table: "PostComment",
                column: "PostID",
                principalTable: "Posts",
                principalColumn: "PostID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostComment_Users_UserID",
                table: "PostComment",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
