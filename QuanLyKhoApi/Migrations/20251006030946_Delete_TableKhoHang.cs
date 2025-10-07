using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace QuanLyKhoApi.Migrations
{
    /// <inheritdoc />
    public partial class Delete_TableKhoHang : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietNhap_KhoHang_MaKho",
                table: "ChiTietNhap");

            migrationBuilder.DropForeignKey(
                name: "FK_PhieuXuat_KhoHang_MaKho",
                table: "PhieuXuat");

            migrationBuilder.DropTable(
                name: "KhoHang");

            migrationBuilder.DropIndex(
                name: "IX_PhieuXuat_MaKho",
                table: "PhieuXuat");

            migrationBuilder.DropIndex(
                name: "IX_ChiTietNhap_MaKho",
                table: "ChiTietNhap");

            migrationBuilder.DropColumn(
                name: "MaKho",
                table: "PhieuXuat");

            migrationBuilder.DropColumn(
                name: "MaKho",
                table: "HangHoa");

            migrationBuilder.DropColumn(
                name: "MaKho",
                table: "ChiTietNhap");

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Loai",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Loai",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PhieuXuat_MaTrangThai",
                table: "PhieuXuat",
                column: "MaTrangThai");

            migrationBuilder.AddForeignKey(
                name: "FK_PhieuXuat_TrangThaiPhieu_MaTrangThai",
                table: "PhieuXuat",
                column: "MaTrangThai",
                principalTable: "TrangThaiPhieu",
                principalColumn: "MaTrangThai",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhieuXuat_TrangThaiPhieu_MaTrangThai",
                table: "PhieuXuat");

            migrationBuilder.DropIndex(
                name: "IX_PhieuXuat_MaTrangThai",
                table: "PhieuXuat");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Loai");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Loai");

            migrationBuilder.AddColumn<int>(
                name: "MaKho",
                table: "PhieuXuat",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MaKho",
                table: "HangHoa",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "MaKho",
                table: "ChiTietNhap",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "KhoHang",
                columns: table => new
                {
                    MaKho = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DiaChi = table.Column<string>(type: "text", nullable: false),
                    MoTa = table.Column<string>(type: "text", nullable: true),
                    TenKho = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhoHang", x => x.MaKho);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhieuXuat_MaKho",
                table: "PhieuXuat",
                column: "MaKho");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietNhap_MaKho",
                table: "ChiTietNhap",
                column: "MaKho");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietNhap_KhoHang_MaKho",
                table: "ChiTietNhap",
                column: "MaKho",
                principalTable: "KhoHang",
                principalColumn: "MaKho",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhieuXuat_KhoHang_MaKho",
                table: "PhieuXuat",
                column: "MaKho",
                principalTable: "KhoHang",
                principalColumn: "MaKho",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
