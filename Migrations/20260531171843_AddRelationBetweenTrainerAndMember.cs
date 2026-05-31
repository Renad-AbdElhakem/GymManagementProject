using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationBetweenTrainerAndMember : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPrivateMember",
                table: "Members",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PrivateTrainerId",
                table: "Members",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Members_PrivateTrainerId",
                table: "Members",
                column: "PrivateTrainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Employee_PrivateTrainerId",
                table: "Members",
                column: "PrivateTrainerId",
                principalTable: "Employee",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Employee_PrivateTrainerId",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_PrivateTrainerId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "IsPrivateMember",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "PrivateTrainerId",
                table: "Members");
        }
    }
}
