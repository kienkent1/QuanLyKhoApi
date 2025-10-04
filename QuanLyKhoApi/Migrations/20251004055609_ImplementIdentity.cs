using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace QuanLyKhoApi.Migrations
{
    /// <inheritdoc />
    public partial class ImplementIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Role_TaiKhoan_IdTaiKhoan",
                table: "Role");

            migrationBuilder.DropIndex(
                name: "IX_Role_IdTaiKhoan",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "IdTaiKhoan",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "Quyen",
                table: "Role");

            migrationBuilder.AddColumn<Guid>(
                name: "TaiKhoanIdNhanVien",
                table: "Role",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Claims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Quyen = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: true),
                    TaiKhoanIdNhanVien = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Claims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Claims_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Claims_TaiKhoan_TaiKhoanIdNhanVien",
                        column: x => x.TaiKhoanIdNhanVien,
                        principalTable: "TaiKhoan",
                        principalColumn: "IdNhanVien");
                });

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
                name: "FK_Role_TaiKhoan_TaiKhoanIdNhanVien",
                table: "Role",
                column: "TaiKhoanIdNhanVien",
                principalTable: "TaiKhoan",
                principalColumn: "IdNhanVien");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Role_TaiKhoan_TaiKhoanIdNhanVien",
                table: "Role");

            migrationBuilder.DropTable(
                name: "Claims");

            migrationBuilder.DropIndex(
                name: "IX_Role_TaiKhoanIdNhanVien",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "TaiKhoanIdNhanVien",
                table: "Role");

            migrationBuilder.AddColumn<Guid>(
                name: "IdTaiKhoan",
                table: "Role",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Quyen",
                table: "Role",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Role_IdTaiKhoan",
                table: "Role",
                column: "IdTaiKhoan");

            migrationBuilder.AddForeignKey(
                name: "FK_Role_TaiKhoan_IdTaiKhoan",
                table: "Role",
                column: "IdTaiKhoan",
                principalTable: "TaiKhoan",
                principalColumn: "IdNhanVien",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
