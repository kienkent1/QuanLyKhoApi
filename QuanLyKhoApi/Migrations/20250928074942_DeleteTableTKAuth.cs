using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyKhoApi.Migrations
{
    /// <inheritdoc />
    public partial class DeleteTableTKAuth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaiKhoanAuth");

            migrationBuilder.AddColumn<string>(
                name: "GoogleId",
                table: "TaiKhoan",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "TaiKhoan",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "TaiKhoan",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "TaiKhoan",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GoogleId",
                table: "TaiKhoan");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "TaiKhoan");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "TaiKhoan");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "TaiKhoan");

            migrationBuilder.CreateTable(
                name: "TaiKhoanAuth",
                columns: table => new
                {
                    MaNhanVien = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GoogleId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaiKhoanAuth", x => x.MaNhanVien);
                });
        }
    }
}
