using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CQRSRentACar.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailTrackingToMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AIResponse",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "EmailSent",
                table: "Messages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "EmailSentDate",
                table: "Messages",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AIResponse",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "EmailSent",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "EmailSentDate",
                table: "Messages");
        }
    }
}
