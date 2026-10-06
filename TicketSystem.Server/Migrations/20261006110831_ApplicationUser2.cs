using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketSystem.Server.Migrations
{
    /// <inheritdoc />
    public partial class ApplicationUser2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Department_departmentid",
                schema: "user",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceAgreement_Department_departmentid",
                schema: "user",
                table: "ServiceAgreement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Department",
                schema: "user",
                table: "Department");

            migrationBuilder.RenameTable(
                name: "Department",
                schema: "user",
                newName: "department",
                newSchema: "user");

            migrationBuilder.AddPrimaryKey(
                name: "PK_department",
                schema: "user",
                table: "department",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_department_departmentid",
                schema: "user",
                table: "AspNetUsers",
                column: "departmentid",
                principalSchema: "user",
                principalTable: "department",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceAgreement_department_departmentid",
                schema: "user",
                table: "ServiceAgreement",
                column: "departmentid",
                principalSchema: "user",
                principalTable: "department",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_department_departmentid",
                schema: "user",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceAgreement_department_departmentid",
                schema: "user",
                table: "ServiceAgreement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_department",
                schema: "user",
                table: "department");

            migrationBuilder.RenameTable(
                name: "department",
                schema: "user",
                newName: "Department",
                newSchema: "user");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Department",
                schema: "user",
                table: "Department",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Department_departmentid",
                schema: "user",
                table: "AspNetUsers",
                column: "departmentid",
                principalSchema: "user",
                principalTable: "Department",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceAgreement_Department_departmentid",
                schema: "user",
                table: "ServiceAgreement",
                column: "departmentid",
                principalSchema: "user",
                principalTable: "Department",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
