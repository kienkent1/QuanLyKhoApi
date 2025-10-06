using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyKhoApi.Migrations
{
    /// <inheritdoc />
    public partial class Update_roleClaim : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Claims_Role_RoleId",
                table: "Claims");

            migrationBuilder.DropForeignKey(
                name: "FK_Claims_TaiKhoan_TaiKhoanIdNhanVien",
                table: "Claims");

            migrationBuilder.DropForeignKey(
                name: "FK_Role_TaiKhoan_TaiKhoanIdNhanVien",
                table: "Role");

            migrationBuilder.DropIndex(
                name: "IX_Role_TaiKhoanIdNhanVien",
                table: "Role");

            migrationBuilder.DropIndex(
                name: "IX_Claims_RoleId",
                table: "Claims");

            migrationBuilder.DropIndex(
                name: "IX_Claims_TaiKhoanIdNhanVien",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "TaiKhoanIdNhanVien",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "TaiKhoanIdNhanVien",
                table: "Claims");

            migrationBuilder.CreateTable(
                name: "ClaimsRole",
                columns: table => new
                {
                    ClaimsId = table.Column<int>(type: "integer", nullable: false),
                    RolesId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaimsRole", x => new { x.ClaimsId, x.RolesId });
                    table.ForeignKey(
                        name: "FK_ClaimsRole_Claims_ClaimsId",
                        column: x => x.ClaimsId,
                        principalTable: "Claims",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClaimsRole_Role_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClaimsRole_RolesId",
                table: "ClaimsRole",
                column: "RolesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClaimsRole");

            migrationBuilder.AddColumn<Guid>(
                name: "TaiKhoanIdNhanVien",
                table: "Role",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoleId",
                table: "Claims",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TaiKhoanIdNhanVien",
                table: "Claims",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Role_TaiKhoanIdNhanVien",
                table: "Role",
                column: "TaiKhoanIdNhanVien");

            migrationBuilder.CreateIndex(
                name: "IX_Claims_RoleId",
                table: "Claims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Claims_TaiKhoanIdNhanVien",
                table: "Claims",
                column: "TaiKhoanIdNhanVien");

            migrationBuilder.AddForeignKey(
                name: "FK_Claims_Role_RoleId",
                table: "Claims",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Claims_TaiKhoan_TaiKhoanIdNhanVien",
                table: "Claims",
                column: "TaiKhoanIdNhanVien",
                principalTable: "TaiKhoan",
                principalColumn: "IdNhanVien");

            migrationBuilder.AddForeignKey(
                name: "FK_Role_TaiKhoan_TaiKhoanIdNhanVien",
                table: "Role",
                column: "TaiKhoanIdNhanVien",
                principalTable: "TaiKhoan",
                principalColumn: "IdNhanVien");
        }
    }
}
