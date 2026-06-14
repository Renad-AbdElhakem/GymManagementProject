using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexForEmployeeAttendanceAndFKForMemberPlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EmployeeAttendance_EmployeeId",
                table: "EmployeeAttendance");

            migrationBuilder.UpdateData(
                table: "Employee",
                keyColumn: "Id",
                keyValue: 1,
                column: "HireDate",
                value: new DateOnly(2026, 6, 11));

            migrationBuilder.CreateIndex(
                name: "IX_Members_MemberPlanId",
                table: "Members",
                column: "MemberPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAttendance_EmployeeId_Date",
                table: "EmployeeAttendance",
                columns: new[] { "EmployeeId", "Date" },
                unique: true,
                filter: "[EmployeeId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_SubscriptionTypes_MemberPlanId",
                table: "Members",
                column: "MemberPlanId",
                principalTable: "SubscriptionTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_SubscriptionTypes_MemberPlanId",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_MemberPlanId",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeAttendance_EmployeeId_Date",
                table: "EmployeeAttendance");

            migrationBuilder.UpdateData(
                table: "Employee",
                keyColumn: "Id",
                keyValue: 1,
                column: "HireDate",
                value: new DateOnly(2026, 6, 1));

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAttendance_EmployeeId",
                table: "EmployeeAttendance",
                column: "EmployeeId");
        }
    }
}
