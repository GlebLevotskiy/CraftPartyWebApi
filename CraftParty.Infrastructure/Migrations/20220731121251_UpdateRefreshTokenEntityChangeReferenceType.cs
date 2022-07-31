using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CraftParty.Infrastructure.Migrations
{
    public partial class UpdateRefreshTokenEntityChangeReferenceType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RefreshTokens_IdentityUserId",
                table: "RefreshTokens");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_IdentityUserId",
                table: "RefreshTokens",
                column: "IdentityUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RefreshTokens_IdentityUserId",
                table: "RefreshTokens");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_IdentityUserId",
                table: "RefreshTokens",
                column: "IdentityUserId",
                unique: true);
        }
    }
}
