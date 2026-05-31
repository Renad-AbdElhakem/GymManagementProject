using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManagement.Migrations
{
    /// <inheritdoc />
    public partial class AllowNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_SubscriptionTypes_MembershipPlansId",
                table: "Members");

            migrationBuilder.AlterColumn<int>(
                name: "MembershipPlansId",
                table: "Members",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Employee",
                keyColumn: "Id",
                keyValue: 1,
                column: "HireDate",
                value: new DateOnly(2026, 5, 31));

            migrationBuilder.AddForeignKey(
                name: "FK_Members_SubscriptionTypes_MembershipPlansId",
                table: "Members",
                column: "MembershipPlansId",
                principalTable: "SubscriptionTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_SubscriptionTypes_MembershipPlansId",
                table: "Members");

            migrationBuilder.AlterColumn<int>(
                name: "MembershipPlansId",
                table: "Members",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Employee",
                keyColumn: "Id",
                keyValue: 1,
                column: "HireDate",
                value: new DateOnly(2026, 5, 11));

            migrationBuilder.AddForeignKey(
                name: "FK_Members_SubscriptionTypes_MembershipPlansId",
                table: "Members",
                column: "MembershipPlansId",
                principalTable: "SubscriptionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
