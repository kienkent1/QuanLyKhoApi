using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyKhoApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateChitietphieuNhap : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietNhap_PhieuNhap_PhieuNhapMaPhieuNhap",
                table: "ChiTietNhap");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietXuat_PhieuXuat_PhieuXuatMaPhieuXuat",
                table: "ChiTietXuat");

            migrationBuilder.DropIndex(
                name: "IX_ChiTietXuat_PhieuXuatMaPhieuXuat",
                table: "ChiTietXuat");

            migrationBuilder.DropIndex(
                name: "IX_ChiTietNhap_PhieuNhapMaPhieuNhap",
                table: "ChiTietNhap");

            migrationBuilder.DropColumn(
                name: "PhieuXuatMaPhieuXuat",
                table: "ChiTietXuat");

            migrationBuilder.DropColumn(
                name: "PhieuNhapMaPhieuNhap",
                table: "ChiTietNhap");

            migrationBuilder.AddColumn<int>(
                name: "MaChiTietXuat",
                table: "ChiTietXuat",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaPhieuNhap",
                table: "ChiTietNhap",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietXuat_MaChiTietXuat",
                table: "ChiTietXuat",
                column: "MaChiTietXuat");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietNhap_MaPhieuNhap",
                table: "ChiTietNhap",
                column: "MaPhieuNhap");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietNhap_PhieuNhap_MaPhieuNhap",
                table: "ChiTietNhap",
                column: "MaPhieuNhap",
                principalTable: "PhieuNhap",
                principalColumn: "MaPhieuNhap",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietXuat_PhieuXuat_MaChiTietXuat",
                table: "ChiTietXuat",
                column: "MaChiTietXuat",
                principalTable: "PhieuXuat",
                principalColumn: "MaPhieuXuat",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietNhap_PhieuNhap_MaPhieuNhap",
                table: "ChiTietNhap");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietXuat_PhieuXuat_MaChiTietXuat",
                table: "ChiTietXuat");

            migrationBuilder.DropIndex(
                name: "IX_ChiTietXuat_MaChiTietXuat",
                table: "ChiTietXuat");

            migrationBuilder.DropIndex(
                name: "IX_ChiTietNhap_MaPhieuNhap",
                table: "ChiTietNhap");

            migrationBuilder.DropColumn(
                name: "MaChiTietXuat",
                table: "ChiTietXuat");

            migrationBuilder.DropColumn(
                name: "MaPhieuNhap",
                table: "ChiTietNhap");

            migrationBuilder.AddColumn<int>(
                name: "PhieuXuatMaPhieuXuat",
                table: "ChiTietXuat",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PhieuNhapMaPhieuNhap",
                table: "ChiTietNhap",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietXuat_PhieuXuatMaPhieuXuat",
                table: "ChiTietXuat",
                column: "PhieuXuatMaPhieuXuat");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietNhap_PhieuNhapMaPhieuNhap",
                table: "ChiTietNhap",
                column: "PhieuNhapMaPhieuNhap");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietNhap_PhieuNhap_PhieuNhapMaPhieuNhap",
                table: "ChiTietNhap",
                column: "PhieuNhapMaPhieuNhap",
                principalTable: "PhieuNhap",
                principalColumn: "MaPhieuNhap");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietXuat_PhieuXuat_PhieuXuatMaPhieuXuat",
                table: "ChiTietXuat",
                column: "PhieuXuatMaPhieuXuat",
                principalTable: "PhieuXuat",
                principalColumn: "MaPhieuXuat");
        }
    }
}
