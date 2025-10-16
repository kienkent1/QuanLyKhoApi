using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace QuanLyKhoApi.Migrations
{
    /// <inheritdoc />
    public partial class update_ThongKe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "GiaXuat",
                table: "PhieuXuat",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "GiaNhap",
                table: "PhieuNhap",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Stt",
                table: "HinhAnhHH",
                type: "integer",
                nullable: true);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ThongKe");

            migrationBuilder.DropColumn(
                name: "GiaXuat",
                table: "PhieuXuat");

            migrationBuilder.DropColumn(
                name: "GiaNhap",
                table: "PhieuNhap");

            migrationBuilder.DropColumn(
                name: "Stt",
                table: "HinhAnhHH");
        }
    }
}
