using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyKhoApi.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldForCauHing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TenPhienBan",
                table: "CauHinh",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenPhienBan",
                table: "CauHinh");
        }
    }
}
