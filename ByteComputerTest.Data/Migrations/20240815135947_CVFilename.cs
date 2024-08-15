using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ByteComputerTest.Data.Migrations
{
    /// <inheritdoc />
    public partial class CVFilename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CVFilename",
                table: "Candidates",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CVFilename",
                table: "Candidates");
        }
    }
}
