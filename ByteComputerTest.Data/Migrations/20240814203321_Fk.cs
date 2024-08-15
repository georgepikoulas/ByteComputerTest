using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ByteComputerTest.Data.Migrations
{
    /// <inheritdoc />
    public partial class Fk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Degrees_Candidates_CandidateId",
                table: "Degrees");

            migrationBuilder.AlterColumn<int>(
                name: "CandidateId",
                table: "Degrees",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Degrees_Candidates_CandidateId",
                table: "Degrees",
                column: "CandidateId",
                principalTable: "Candidates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Degrees_Candidates_CandidateId",
                table: "Degrees");

            migrationBuilder.AlterColumn<int>(
                name: "CandidateId",
                table: "Degrees",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Degrees_Candidates_CandidateId",
                table: "Degrees",
                column: "CandidateId",
                principalTable: "Candidates",
                principalColumn: "Id");
        }
    }
}
