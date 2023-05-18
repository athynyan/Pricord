using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pricord.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUnitConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PlayableCharacter_PrefabId",
                table: "PlayableCharacter",
                column: "PrefabId");

            migrationBuilder.CreateIndex(
                name: "IX_Boss_PrefabId",
                table: "Boss",
                column: "PrefabId");

            migrationBuilder.AddForeignKey(
                name: "FK_Boss_Units_PrefabId",
                table: "Boss",
                column: "PrefabId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayableCharacter_Units_PrefabId",
                table: "PlayableCharacter",
                column: "PrefabId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boss_Units_PrefabId",
                table: "Boss");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayableCharacter_Units_PrefabId",
                table: "PlayableCharacter");

            migrationBuilder.DropIndex(
                name: "IX_PlayableCharacter_PrefabId",
                table: "PlayableCharacter");

            migrationBuilder.DropIndex(
                name: "IX_Boss_PrefabId",
                table: "Boss");
        }
    }
}
