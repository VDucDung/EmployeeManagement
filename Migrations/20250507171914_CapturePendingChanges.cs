using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeManagement.Migrations
{
    /// <inheritdoc />
    public partial class CapturePendingChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "DepartmentId", "Email", "FirstName", "HireDate", "IsActive", "LastName", "Password", "PhoneNumber", "RoleId" },
                values: new object[] { 1, null, "admin@system.com", "Admin", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "System", "$2a$11$bIVJY5qLJbue9/DdAtgAbuWHXW.mYwK.cGP.Y0soFnKp.r81mjq0q", "0123", 1 });
        }
    }
}
