using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TicketSystem.Server.Migrations
{
    /// <inheritdoc />
    public partial class ApplicationUser3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_department_departmentid",
                schema: "user",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ServiceAgreement",
                schema: "user");

            migrationBuilder.DropTable(
                name: "department",
                schema: "user");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_departmentid",
                schema: "user",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "departmentid",
                schema: "user",
                table: "AspNetUsers",
                newName: "DepartmentId");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                schema: "user",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                schema: "user",
                table: "AspNetUsers",
                newName: "departmentid");

            migrationBuilder.AlterColumn<int>(
                name: "departmentid",
                schema: "user",
                table: "AspNetUsers",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateTable(
                name: "department",
                schema: "user",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_department", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceAgreement",
                schema: "user",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    departmentid = table.Column<int>(type: "integer", nullable: false),
                    createdby = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    signedby = table.Column<string>(type: "text", nullable: true),
                    title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceAgreement", x => x.id);
                    table.ForeignKey(
                        name: "FK_ServiceAgreement_department_departmentid",
                        column: x => x.departmentid,
                        principalSchema: "user",
                        principalTable: "department",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_departmentid",
                schema: "user",
                table: "AspNetUsers",
                column: "departmentid");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceAgreement_departmentid",
                schema: "user",
                table: "ServiceAgreement",
                column: "departmentid");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_department_departmentid",
                schema: "user",
                table: "AspNetUsers",
                column: "departmentid",
                principalSchema: "user",
                principalTable: "department",
                principalColumn: "id");
        }
    }
}
