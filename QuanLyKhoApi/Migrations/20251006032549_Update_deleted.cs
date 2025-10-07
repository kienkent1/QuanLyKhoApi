using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyKhoApi.Migrations
{
    /// <inheritdoc />
    public partial class Update_deleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Deleted_at",
                table: "NhaCungCap",
                newName: "DeletedAt");

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Role",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Role",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "HangHoa",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "HangHoa",
                type: "timestamp with time zone",
                nullable: true);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComfirmAccounts");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "HangHoa");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "HangHoa");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "NhaCungCap",
                newName: "Deleted_at");
        }
    }
}
