using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QuanLyKhoApi.Migrations
{
    /// <inheritdoc />
    public partial class Update_ThongKe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MoTa",
                table: "TrangThaiPhieu",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "Day",
                table: "ThongKe",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Claims",
                columns: new[] { "Id", "Category", "Quyen" },
                values: new object[,]
                {
                    { 7, "NhaCungCap", "ThemNhaCungCap" },
                    { 8, "NhaCungCap", "XoaNhaCungCap" },
                    { 9, "NhaCungCap", "SuaNhaCungCap" },
                    { 10, "NhaCungCap", "XemNhaCungCap" },
                    { 11, "HangHoa", "ThemHangHoa" },
                    { 12, "HangHoa", "XoaHangHoa" },
                    { 13, "HangHoa", "SuaHangHoa" },
                    { 14, "HangHoa", "XemHangHoa" },
                    { 15, "Loai", "ThemLoai" },
                    { 16, "Loai", "XoaLoai" },
                    { 17, "Loai", "SuaLoai" },
                    { 18, "Loai", "XemLoai" },
                    { 19, "ThongKe", "XemThongKe" },
                    { 20, "Role", "ThemRole" },
                    { 21, "Role", "XoaRole" },
                    { 22, "Role", "SuaRole" },
                    { 23, "Role", "XemRole" },
                    { 24, "PhieuNhap", "ThemPhieuNhap" },
                    { 25, "PhieuNhap", "XemPhieuNhap" },
                    { 26, "PhieuXuat", "ThemPhieuXuat" },
                    { 27, "PhieuXuat", "XemPhieuXuat" },
                    { 28, "TaiKhoan", "ThemTaiKhoan" },
                    { 29, "TaiKhoan", "XoaTaiKhoan" },
                    { 30, "TaiKhoan", "SuaTaiKhoan" },
                    { 31, "TaiKhoan", "XemTaiKhoan" }
                });

            migrationBuilder.InsertData(
                table: "TrangThaiPhieu",
                columns: new[] { "MaTrangThai", "MoTa", "TenTrangThai" },
                values: new object[,]
                {
                    { 1, null, "Đang xử lý" },
                    { 2, null, "Hoàn thành" },
                    { 3, null, "Hủy" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Claims",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Claims",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Claims",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Claims",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Claims",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Claims",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Claims",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Claims",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Claims",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Claims",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Claims",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Claims",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Claims",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Claims",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Claims",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Claims",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Claims",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Claims",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Claims",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Claims",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Claims",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Claims",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Claims",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Claims",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Claims",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "TrangThaiPhieu",
                keyColumn: "MaTrangThai",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TrangThaiPhieu",
                keyColumn: "MaTrangThai",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TrangThaiPhieu",
                keyColumn: "MaTrangThai",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "Day",
                table: "ThongKe");

            migrationBuilder.AlterColumn<string>(
                name: "MoTa",
                table: "TrangThaiPhieu",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
