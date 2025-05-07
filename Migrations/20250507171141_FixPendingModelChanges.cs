using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeManagement.Migrations
{
    /// <inheritdoc />
    public partial class FixPendingModelChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$d5Xo9oyEBBeEZwY6/qn88OVpRmu1fkcXEV.MPbyXIf4BdQrN35TJu");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$A7MgJirF5c.tFR.uV0QkM.DTPPdvKxIlqFuKPMDr1jbTt22LvUQcW");
        }
    }
}
