using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeManagement.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminUserCharge : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$eIXs/bXJ/NPI0Rn3a3CJUuwEgfWWsQZk9HNS4YbZ4Zo.WVHYhKdXK");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$eIXs/bXJ/NPI0Rn3a3CJUuwEgfWWsQZk9HNS4YbZ4Zo.WVHYhKdXK\r\n");
        }
    }
}
