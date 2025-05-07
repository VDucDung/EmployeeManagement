using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeManagement.Migrations
{
    /// <inheritdoc />
    public partial class FixDynamicSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 1,
                columns: new[] { "HireDate", "Password" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "$2a$11$A7MgJirF5c.tFR.uV0QkM.DTPPdvKxIlqFuKPMDr1jbTt22LvUQcW" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 1,
                columns: new[] { "HireDate", "Password" },
                values: new object[] { new DateTime(2025, 5, 8, 0, 3, 33, 397, DateTimeKind.Local).AddTicks(6080), "$2a$11$ZQSMvdyvah1xh8clrxWt4u4aSCkScUUxjxcgUJ4sqZt/riGfLFRXK" });
        }
    }
}
