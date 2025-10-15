using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyKhoApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClaimsRole");

            migrationBuilder.AddColumn<int>(
                name: "ClaimsId",
                table: "Role",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Role_ClaimsId",
                table: "Role",
                column: "ClaimsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Role_Claims_ClaimsId",
                table: "Role",
                column: "ClaimsId",
                principalTable: "Claims",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Role_Claims_ClaimsId",
                table: "Role");

            migrationBuilder.DropIndex(
                name: "IX_Role_ClaimsId",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "ClaimsId",
                table: "Role");

            migrationBuilder.CreateTable(
                name: "ClaimsRole",
                columns: table => new
                {
                    ClaimsId = table.Column<int>(type: "integer", nullable: false),
                    RolesId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaimsRole", x => new { x.ClaimsId, x.RolesId });
                    table.ForeignKey(
                        name: "FK_ClaimsRole_Claims_ClaimsId",
                        column: x => x.ClaimsId,
                        principalTable: "Claims",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClaimsRole_Role_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClaimsRole_RolesId",
                table: "ClaimsRole",
                column: "RolesId");
        }
    }
}
