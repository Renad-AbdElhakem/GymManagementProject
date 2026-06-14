using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManagement.Migrations
{
    /// <inheritdoc />
    public partial class AlterMemberAttendancRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberAttendances_SubscriptionTypes_MembershipPlansId",
                table: "MemberAttendances");

            migrationBuilder.DropIndex(
                name: "IX_MemberAttendances_MembershipPlansId",
                table: "MemberAttendances");

            migrationBuilder.DropColumn(
                name: "MembershipPlansId",
                table: "MemberAttendances");

            migrationBuilder.UpdateData(
                table: "Employee",
                keyColumn: "Id",
                keyValue: 1,
                column: "HireDate",
                value: new DateOnly(2026, 6, 1));

            migrationBuilder.CreateIndex(
                name: "IX_MemberAttendances_MemberPlansId",
                table: "MemberAttendances",
                column: "MemberPlansId");

            migrationBuilder.AddForeignKey(
                name: "FK_MemberAttendances_SubscriptionTypes_MemberPlansId",
                table: "MemberAttendances",
                column: "MemberPlansId",
                principalTable: "SubscriptionTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberAttendances_SubscriptionTypes_MemberPlansId",
                table: "MemberAttendances");

            migrationBuilder.DropIndex(
                name: "IX_MemberAttendances_MemberPlansId",
                table: "MemberAttendances");

            migrationBuilder.AddColumn<int>(
                name: "MembershipPlansId",
                table: "MemberAttendances",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Employee",
                keyColumn: "Id",
                keyValue: 1,
                column: "HireDate",
                value: new DateOnly(2026, 5, 31));

            migrationBuilder.CreateIndex(
                name: "IX_MemberAttendances_MembershipPlansId",
                table: "MemberAttendances",
                column: "MembershipPlansId");

            migrationBuilder.AddForeignKey(
                name: "FK_MemberAttendances_SubscriptionTypes_MembershipPlansId",
                table: "MemberAttendances",
                column: "MembershipPlansId",
                principalTable: "SubscriptionTypes",
                principalColumn: "Id");
        }
    }
}
