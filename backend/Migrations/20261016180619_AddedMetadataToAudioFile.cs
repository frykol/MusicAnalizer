using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AddedMetadataToAudioFile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Bitrate",
                table: "AudioFiles",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChannelLayout",
                table: "AudioFiles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Channels",
                table: "AudioFiles",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Codec",
                table: "AudioFiles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Duration",
                table: "AudioFiles",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "SampleRate",
                table: "AudioFiles",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "Size",
                table: "AudioFiles",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bitrate",
                table: "AudioFiles");

            migrationBuilder.DropColumn(
                name: "ChannelLayout",
                table: "AudioFiles");

            migrationBuilder.DropColumn(
                name: "Channels",
                table: "AudioFiles");

            migrationBuilder.DropColumn(
                name: "Codec",
                table: "AudioFiles");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "AudioFiles");

            migrationBuilder.DropColumn(
                name: "SampleRate",
                table: "AudioFiles");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "AudioFiles");
        }
    }
}
