using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TicketSystem.Server.Migrations
{
    /// <inheritdoc />
    public partial class ApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "departmentid",
                schema: "user",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Department",
                schema: "user",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceAgreement",
                schema: "user",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    departmentid = table.Column<int>(type: "integer", nullable: false),
                    title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    createdby = table.Column<string>(type: "text", nullable: true),
                    signedby = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceAgreement", x => x.id);
                    table.ForeignKey(
                        name: "FK_ServiceAgreement_Department_departmentid",
                        column: x => x.departmentid,
                        principalSchema: "user",
                        principalTable: "Department",
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
                name: "FK_AspNetUsers_Department_departmentid",
                schema: "user",
                table: "AspNetUsers",
                column: "departmentid",
                principalSchema: "user",
                principalTable: "Department",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Department_departmentid",
                schema: "user",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ServiceAgreement",
                schema: "user");

            migrationBuilder.DropTable(
                name: "Department",
                schema: "user");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_departmentid",
                schema: "user",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "departmentid",
                schema: "user",
                table: "AspNetUsers");
        }
    }
}
