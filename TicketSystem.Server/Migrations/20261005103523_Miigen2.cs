using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketSystem.Server.Migrations
{
    /// <inheritdoc />
    public partial class Miigen2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                schema: "user",
                newName: "AspNetUserTokens",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                schema: "user",
                newName: "AspNetUsers",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                schema: "user",
                newName: "AspNetUserRoles",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "AspNetUserPasskeys",
                schema: "user",
                newName: "AspNetUserPasskeys",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                schema: "user",
                newName: "AspNetUserLogins",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                schema: "user",
                newName: "AspNetUserClaims",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                schema: "user",
                newName: "AspNetRoles",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                schema: "user",
                newName: "AspNetRoleClaims",
                newSchema: "public");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "user");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                schema: "public",
                newName: "AspNetUserTokens",
                newSchema: "user");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                schema: "public",
                newName: "AspNetUsers",
                newSchema: "user");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                schema: "public",
                newName: "AspNetUserRoles",
                newSchema: "user");

            migrationBuilder.RenameTable(
                name: "AspNetUserPasskeys",
                schema: "public",
                newName: "AspNetUserPasskeys",
                newSchema: "user");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                schema: "public",
                newName: "AspNetUserLogins",
                newSchema: "user");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                schema: "public",
                newName: "AspNetUserClaims",
                newSchema: "user");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                schema: "public",
                newName: "AspNetRoles",
                newSchema: "user");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                schema: "public",
                newName: "AspNetRoleClaims",
                newSchema: "user");
        }
    }
}
