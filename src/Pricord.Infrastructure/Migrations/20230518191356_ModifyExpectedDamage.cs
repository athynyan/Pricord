using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pricord.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifyExpectedDamage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boss_Unit_PrefabId",
                table: "Boss");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayableCharacters_Unit_PrefabId",
                table: "PlayableCharacters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Unit",
                table: "Unit");

            migrationBuilder.RenameTable(
                name: "Unit",
                newName: "Units");

            migrationBuilder.AlterColumn<long>(
                name: "ExpectedDamage",
                table: "BattleRecords",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Units",
                table: "Units",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Boss_Units_PrefabId",
                table: "Boss",
                column: "PrefabId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayableCharacters_Units_PrefabId",
                table: "PlayableCharacters",
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
                name: "FK_PlayableCharacters_Units_PrefabId",
                table: "PlayableCharacters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Units",
                table: "Units");

            migrationBuilder.RenameTable(
                name: "Units",
                newName: "Unit");

            migrationBuilder.AlterColumn<int>(
                name: "ExpectedDamage",
                table: "BattleRecords",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Unit",
                table: "Unit",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Boss_Unit_PrefabId",
                table: "Boss",
                column: "PrefabId",
                principalTable: "Unit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayableCharacters_Unit_PrefabId",
                table: "PlayableCharacters",
                column: "PrefabId",
                principalTable: "Unit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
