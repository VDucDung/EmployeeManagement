using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeManagement.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminUserChargePassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$gjJa3l5cToZfPNXiJpEkkubTFxAQjecPIzkL2YAgJ0umYW.Cxbkyq");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$eIXs/bXJ/NPI0Rn3a3CJUuwEgfWWsQZk9HNS4YbZ4Zo.WVHYhKdXK");
        }
    }
}
