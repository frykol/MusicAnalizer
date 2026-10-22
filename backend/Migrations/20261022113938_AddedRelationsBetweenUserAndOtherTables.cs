using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AddedRelationsBetweenUserAndOtherTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Tracks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "AudioFiles",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Artists",
                type: "integer",
                nullable: false,
                defaultValue: 0);

                migrationBuilder.Sql("""
                UPDATE "Artists"
                SET "UserId" = 1
                WHERE "UserId" = 0;
            """);

            migrationBuilder.Sql("""
                UPDATE "AudioFiles"
                SET "UserId" = 1
                WHERE "UserId" = 0;
            """);

            migrationBuilder.Sql("""
                UPDATE "Tracks"
                SET "UserId" = 1
                WHERE "UserId" = 0;
            """);

            migrationBuilder.CreateIndex(
                name: "IX_Tracks_UserId",
                table: "Tracks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AudioFiles_UserId",
                table: "AudioFiles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Artists_UserId",
                table: "Artists",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Artists_Users_UserId",
                table: "Artists",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AudioFiles_Users_UserId",
                table: "AudioFiles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tracks_Users_UserId",
                table: "Tracks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artists_Users_UserId",
                table: "Artists");

            migrationBuilder.DropForeignKey(
                name: "FK_AudioFiles_Users_UserId",
                table: "AudioFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Tracks_Users_UserId",
                table: "Tracks");

            migrationBuilder.DropIndex(
                name: "IX_Tracks_UserId",
                table: "Tracks");

            migrationBuilder.DropIndex(
                name: "IX_AudioFiles_UserId",
                table: "AudioFiles");

            migrationBuilder.DropIndex(
                name: "IX_Artists_UserId",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Tracks");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AudioFiles");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Artists");
        }
    }
}
