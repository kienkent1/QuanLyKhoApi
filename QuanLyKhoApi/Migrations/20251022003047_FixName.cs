using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace QuanLyKhoApi.Migrations
{
    /// <inheritdoc />
    public partial class FixName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietXuat_PhieuXuat_MaChiTietXuat",
                table: "ChiTietXuat");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChiTietXuat",
                table: "ChiTietXuat");

            migrationBuilder.DropIndex(
                name: "IX_ChiTietXuat_MaChiTietXuat",
                table: "ChiTietXuat");

            migrationBuilder.AlterColumn<int>(
                name: "MaChiTietXuat",
                table: "ChiTietXuat",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "MaPhieuXuat",
                table: "ChiTietXuat",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChiTietXuat",
                table: "ChiTietXuat",
                column: "MaChiTietXuat");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietXuat_MaPhieuXuat",
                table: "ChiTietXuat",
                column: "MaPhieuXuat");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietXuat_PhieuXuat_MaPhieuXuat",
                table: "ChiTietXuat",
                column: "MaPhieuXuat",
                principalTable: "PhieuXuat",
                principalColumn: "MaPhieuXuat",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietXuat_PhieuXuat_MaPhieuXuat",
                table: "ChiTietXuat");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChiTietXuat",
                table: "ChiTietXuat");

            migrationBuilder.DropIndex(
                name: "IX_ChiTietXuat_MaPhieuXuat",
                table: "ChiTietXuat");

            migrationBuilder.AlterColumn<int>(
                name: "MaPhieuXuat",
                table: "ChiTietXuat",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "MaChiTietXuat",
                table: "ChiTietXuat",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChiTietXuat",
                table: "ChiTietXuat",
                column: "MaPhieuXuat");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietXuat_MaChiTietXuat",
                table: "ChiTietXuat",
                column: "MaChiTietXuat");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietXuat_PhieuXuat_MaChiTietXuat",
                table: "ChiTietXuat",
                column: "MaChiTietXuat",
                principalTable: "PhieuXuat",
                principalColumn: "MaPhieuXuat",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
