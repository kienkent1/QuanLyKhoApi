using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QuanLyKhoApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Claims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Quyen = table.Column<string>(type: "text", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Claims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Loai",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenLoai = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    MoTa = table.Column<string>(type: "text", nullable: true),
                    HinhAnh = table.Column<string>(type: "text", nullable: true),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loai", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NhaCungCap",
                columns: table => new
                {
                    MaNCC = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenNCC = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    DiaChi = table.Column<Dictionary<string, object>>(type: "jsonb", nullable: true, defaultValueSql: "'{}'::jsonb"),
                    DienThoai = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true),
                    HinhAnh = table.Column<string>(type: "text", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhaCungCap", x => x.MaNCC);
                });

            migrationBuilder.CreateTable(
                name: "NhanVien",
                columns: table => new
                {
                    IdNhanVien = table.Column<Guid>(type: "uuid", nullable: false),
                    TenNhanVien = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    sdt = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    diaChi = table.Column<Dictionary<string, object>>(type: "jsonb", nullable: true, defaultValueSql: "'{}'::jsonb"),
                    ngaySinh = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    gioiTinh = table.Column<string>(type: "text", nullable: false),
                    chucVu = table.Column<string>(type: "text", nullable: false),
                    trangthai = table.Column<bool>(type: "boolean", nullable: false),
                    UrlHinh = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanVien", x => x.IdNhanVien);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    VaiTro = table.Column<string>(type: "text", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ThongKe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    Month = table.Column<int>(type: "integer", nullable: false),
                    SoPhieuluongNhap = table.Column<int>(type: "integer", nullable: false),
                    SoPhieuluongXuat = table.Column<int>(type: "integer", nullable: false),
                    TongGiaNhap = table.Column<decimal>(type: "numeric", nullable: false),
                    TongGiaXuat = table.Column<decimal>(type: "numeric", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThongKe", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrangThaiPhieu",
                columns: table => new
                {
                    MaTrangThai = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenTrangThai = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    MoTa = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrangThaiPhieu", x => x.MaTrangThai);
                });

            migrationBuilder.CreateTable(
                name: "HangHoa",
                columns: table => new
                {
                    MaHH = table.Column<Guid>(type: "uuid", nullable: false),
                    Model = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    MoTa = table.Column<string>(type: "text", nullable: true),
                    DonViTinh = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    NhaCungCapId = table.Column<int>(type: "integer", nullable: false),
                    SoLuongTon = table.Column<int>(type: "integer", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IdLoai = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HangHoa", x => x.MaHH);
                    table.ForeignKey(
                        name: "FK_HangHoa_Loai_IdLoai",
                        column: x => x.IdLoai,
                        principalTable: "Loai",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HangHoa_NhaCungCap_NhaCungCapId",
                        column: x => x.NhaCungCapId,
                        principalTable: "NhaCungCap",
                        principalColumn: "MaNCC",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaiKhoan",
                columns: table => new
                {
                    IdNhanVien = table.Column<Guid>(type: "uuid", nullable: false),
                    TenDangNhap = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Password = table.Column<string>(type: "text", nullable: true),
                    GoogleId = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaiKhoan", x => x.IdNhanVien);
                    table.ForeignKey(
                        name: "FK_TaiKhoan_NhanVien_IdNhanVien",
                        column: x => x.IdNhanVien,
                        principalTable: "NhanVien",
                        principalColumn: "IdNhanVien",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => new { x.RoleId, x.ClaimId });
                    table.ForeignKey(
                        name: "FK_RoleClaims_Claims_ClaimId",
                        column: x => x.ClaimId,
                        principalTable: "Claims",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CauHinh",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MaHH = table.Column<Guid>(type: "uuid", nullable: false),
                    GiaBan = table.Column<decimal>(type: "numeric", nullable: false),
                    SoLuongTon = table.Column<int>(type: "integer", nullable: false),
                    MoTa = table.Column<string>(type: "text", nullable: true),
                    MauSac = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ColorCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Ram = table.Column<string>(type: "text", nullable: true),
                    Rom = table.Column<string>(type: "text", nullable: true),
                    SoLuongHidden = table.Column<int>(type: "integer", nullable: true),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CauHinh", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CauHinh_HangHoa_MaHH",
                        column: x => x.MaHH,
                        principalTable: "HangHoa",
                        principalColumn: "MaHH",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhieuNhap",
                columns: table => new
                {
                    MaPhieuNhap = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NgayNhap = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MaNV = table.Column<Guid>(type: "uuid", nullable: false),
                    MaNCC = table.Column<string>(type: "text", nullable: false),
                    GiaNhap = table.Column<decimal>(type: "numeric", nullable: true),
                    MaHH = table.Column<Guid>(type: "uuid", nullable: false),
                    MaTrangThai = table.Column<int>(type: "integer", nullable: false),
                    GhiChu = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhieuNhap", x => x.MaPhieuNhap);
                    table.ForeignKey(
                        name: "FK_PhieuNhap_HangHoa_MaHH",
                        column: x => x.MaHH,
                        principalTable: "HangHoa",
                        principalColumn: "MaHH",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhieuNhap_NhanVien_MaNV",
                        column: x => x.MaNV,
                        principalTable: "NhanVien",
                        principalColumn: "IdNhanVien",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhieuNhap_TrangThaiPhieu_MaTrangThai",
                        column: x => x.MaTrangThai,
                        principalTable: "TrangThaiPhieu",
                        principalColumn: "MaTrangThai",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhieuXuat",
                columns: table => new
                {
                    MaPhieuXuat = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NgayXuat = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MaNV = table.Column<Guid>(type: "uuid", nullable: false),
                    MaTrangThai = table.Column<int>(type: "integer", nullable: false),
                    GhiChu = table.Column<string>(type: "text", nullable: true),
                    GiaXuat = table.Column<decimal>(type: "numeric", nullable: true),
                    MaHH = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhieuXuat", x => x.MaPhieuXuat);
                    table.ForeignKey(
                        name: "FK_PhieuXuat_HangHoa_MaHH",
                        column: x => x.MaHH,
                        principalTable: "HangHoa",
                        principalColumn: "MaHH",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhieuXuat_NhanVien_MaNV",
                        column: x => x.MaNV,
                        principalTable: "NhanVien",
                        principalColumn: "IdNhanVien",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhieuXuat_TrangThaiPhieu_MaTrangThai",
                        column: x => x.MaTrangThai,
                        principalTable: "TrangThaiPhieu",
                        principalColumn: "MaTrangThai",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComfirmAccounts",
                columns: table => new
                {
                    IdTaiKhoan = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComfirmAccounts", x => x.IdTaiKhoan);
                    table.ForeignKey(
                        name: "FK_ComfirmAccounts_TaiKhoan_IdTaiKhoan",
                        column: x => x.IdTaiKhoan,
                        principalTable: "TaiKhoan",
                        principalColumn: "IdNhanVien",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaiKhoanRoles",
                columns: table => new
                {
                    TaiKhoanId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaiKhoanRoles", x => new { x.TaiKhoanId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_TaiKhoanRoles_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaiKhoanRoles_TaiKhoan_TaiKhoanId",
                        column: x => x.TaiKhoanId,
                        principalTable: "TaiKhoan",
                        principalColumn: "IdNhanVien",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaiKhoanToken",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ExpiryTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IdTaiKhoan = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaiKhoanToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaiKhoanToken_TaiKhoan_IdTaiKhoan",
                        column: x => x.IdTaiKhoan,
                        principalTable: "TaiKhoan",
                        principalColumn: "IdNhanVien",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HinhAnhHH",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CauHinhId = table.Column<Guid>(type: "uuid", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false),
                    Stt = table.Column<int>(type: "integer", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HinhAnhHH", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HinhAnhHH_CauHinh_CauHinhId",
                        column: x => x.CauHinhId,
                        principalTable: "CauHinh",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietNhap",
                columns: table => new
                {
                    MaChiTietNhap = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MaCauHinh = table.Column<Guid>(type: "uuid", nullable: false),
                    SoLuong = table.Column<int>(type: "integer", nullable: false),
                    DonGia = table.Column<decimal>(type: "numeric", nullable: false),
                    PhieuNhapMaPhieuNhap = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietNhap", x => x.MaChiTietNhap);
                    table.ForeignKey(
                        name: "FK_ChiTietNhap_CauHinh_MaCauHinh",
                        column: x => x.MaCauHinh,
                        principalTable: "CauHinh",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietNhap_PhieuNhap_PhieuNhapMaPhieuNhap",
                        column: x => x.PhieuNhapMaPhieuNhap,
                        principalTable: "PhieuNhap",
                        principalColumn: "MaPhieuNhap");
                });

            migrationBuilder.CreateTable(
                name: "ChiTietXuat",
                columns: table => new
                {
                    MaPhieuXuat = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MaCauHinh = table.Column<Guid>(type: "uuid", nullable: false),
                    SoLuong = table.Column<int>(type: "integer", nullable: false),
                    DonGia = table.Column<decimal>(type: "numeric", nullable: false),
                    PhieuXuatMaPhieuXuat = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietXuat", x => x.MaPhieuXuat);
                    table.ForeignKey(
                        name: "FK_ChiTietXuat_CauHinh_MaCauHinh",
                        column: x => x.MaCauHinh,
                        principalTable: "CauHinh",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietXuat_PhieuXuat_PhieuXuatMaPhieuXuat",
                        column: x => x.PhieuXuatMaPhieuXuat,
                        principalTable: "PhieuXuat",
                        principalColumn: "MaPhieuXuat");
                });

            migrationBuilder.InsertData(
                table: "Claims",
                columns: new[] { "Id", "Category", "Quyen" },
                values: new object[,]
                {
                    { 1, null, "User" },
                    { 2, null, "Admin" },
                    { 3, "NhanVien", "ThemNhanVien" },
                    { 4, "NhanVien", "XoaNhanVien" },
                    { 5, "NhanVien", "SuaNhanVien" },
                    { 6, "NhanVien", "XemNhanVien" }
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

            migrationBuilder.CreateIndex(
                name: "IX_CauHinh_MaHH",
                table: "CauHinh",
                column: "MaHH");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietNhap_MaCauHinh",
                table: "ChiTietNhap",
                column: "MaCauHinh");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietNhap_PhieuNhapMaPhieuNhap",
                table: "ChiTietNhap",
                column: "PhieuNhapMaPhieuNhap");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietXuat_MaCauHinh",
                table: "ChiTietXuat",
                column: "MaCauHinh");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietXuat_PhieuXuatMaPhieuXuat",
                table: "ChiTietXuat",
                column: "PhieuXuatMaPhieuXuat");

            migrationBuilder.CreateIndex(
                name: "IX_HangHoa_IdLoai",
                table: "HangHoa",
                column: "IdLoai");

            migrationBuilder.CreateIndex(
                name: "IX_HangHoa_NhaCungCapId",
                table: "HangHoa",
                column: "NhaCungCapId");

            migrationBuilder.CreateIndex(
                name: "IX_HinhAnhHH_CauHinhId",
                table: "HinhAnhHH",
                column: "CauHinhId");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuNhap_MaHH",
                table: "PhieuNhap",
                column: "MaHH");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuNhap_MaNV",
                table: "PhieuNhap",
                column: "MaNV");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuNhap_MaTrangThai",
                table: "PhieuNhap",
                column: "MaTrangThai");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuXuat_MaHH",
                table: "PhieuXuat",
                column: "MaHH");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuXuat_MaNV",
                table: "PhieuXuat",
                column: "MaNV");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuXuat_MaTrangThai",
                table: "PhieuXuat",
                column: "MaTrangThai");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_ClaimId",
                table: "RoleClaims",
                column: "ClaimId");

            migrationBuilder.CreateIndex(
                name: "IX_TaiKhoanRoles_RoleId",
                table: "TaiKhoanRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_TaiKhoanToken_IdTaiKhoan",
                table: "TaiKhoanToken",
                column: "IdTaiKhoan");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietNhap");

            migrationBuilder.DropTable(
                name: "ChiTietXuat");

            migrationBuilder.DropTable(
                name: "ComfirmAccounts");

            migrationBuilder.DropTable(
                name: "HinhAnhHH");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "TaiKhoanRoles");

            migrationBuilder.DropTable(
                name: "TaiKhoanToken");

            migrationBuilder.DropTable(
                name: "ThongKe");

            migrationBuilder.DropTable(
                name: "PhieuNhap");

            migrationBuilder.DropTable(
                name: "PhieuXuat");

            migrationBuilder.DropTable(
                name: "CauHinh");

            migrationBuilder.DropTable(
                name: "Claims");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "TaiKhoan");

            migrationBuilder.DropTable(
                name: "TrangThaiPhieu");

            migrationBuilder.DropTable(
                name: "HangHoa");

            migrationBuilder.DropTable(
                name: "NhanVien");

            migrationBuilder.DropTable(
                name: "Loai");

            migrationBuilder.DropTable(
                name: "NhaCungCap");
        }
    }
}
