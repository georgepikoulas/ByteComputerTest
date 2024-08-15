using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ByteComputerTest.Data.Migrations
{
    /// <inheritdoc />
    public partial class SelectedDegre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Degrees_Candidates_CandidateId",
                table: "Degrees");

            migrationBuilder.DropIndex(
                name: "IX_Degrees_CandidateId",
                table: "Degrees");

            migrationBuilder.DropColumn(
                name: "CandidateId",
                table: "Degrees");

            migrationBuilder.AddColumn<string>(
                name: "SelectedDegree",
                table: "Candidates",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SelectedDegree",
                table: "Candidates");

            migrationBuilder.AddColumn<int>(
                name: "CandidateId",
                table: "Degrees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Degrees_CandidateId",
                table: "Degrees",
                column: "CandidateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Degrees_Candidates_CandidateId",
                table: "Degrees",
                column: "CandidateId",
                principalTable: "Candidates",
                principalColumn: "Id");
        }
    }
}
