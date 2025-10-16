using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QuanLyKhoApi.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Role_Claims_ClaimsId",
                table: "Role");

            migrationBuilder.DropIndex(
                name: "IX_Role_ClaimsId",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "ClaimsId",
                table: "Role");

            migrationBuilder.InsertData(
                table: "Claims",
                columns: new[] { "Id", "Quyen" },
                values: new object[,]
                {
                    { 1, "User" },
                    { 2, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Deleted", "DeletedAt", "VaiTro" },
                values: new object[,]
                {
                    { "admin", false, null, "Admin" },
                    { "user", false, null, "User" }
                });

            migrationBuilder.InsertData(
                table: "RoleClaims",
                columns: new[] { "ClaimId", "RoleId" },
                values: new object[,]
                {
                    { 2, "admin" },
                    { 1, "user" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumns: new[] { "ClaimId", "RoleId" },
                keyValues: new object[] { 2, "admin" });

            migrationBuilder.DeleteData(
                table: "RoleClaims",
                keyColumns: new[] { "ClaimId", "RoleId" },
                keyValues: new object[] { 1, "user" });

            migrationBuilder.DeleteData(
                table: "Claims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Claims",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "admin");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "user");

            migrationBuilder.AddColumn<int>(
                name: "ClaimsId",
                table: "Role",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Role_ClaimsId",
                table: "Role",
                column: "ClaimsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Role_Claims_ClaimsId",
                table: "Role",
                column: "ClaimsId",
                principalTable: "Claims",
                principalColumn: "Id");
        }
    }
}
