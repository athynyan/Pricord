using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pricord.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifyUnitType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boss_Units_PrefabId",
                table: "Boss");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayableCharacter_BattleRecords_BattleRecordId",
                table: "PlayableCharacter");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayableCharacter_Units_PrefabId",
                table: "PlayableCharacter");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Units",
                table: "Units");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlayableCharacter",
                table: "PlayableCharacter");

            migrationBuilder.RenameTable(
                name: "Units",
                newName: "Unit");

            migrationBuilder.RenameTable(
                name: "PlayableCharacter",
                newName: "PlayableCharacters");

            migrationBuilder.RenameIndex(
                name: "IX_PlayableCharacter_PrefabId",
                table: "PlayableCharacters",
                newName: "IX_PlayableCharacters_PrefabId");

            migrationBuilder.RenameIndex(
                name: "IX_PlayableCharacter_BattleRecordId",
                table: "PlayableCharacters",
                newName: "IX_PlayableCharacters_BattleRecordId");

            migrationBuilder.AlterColumn<string>(
                name: "AttackerId",
                table: "TimelineItem",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "PrefabId",
                table: "Boss",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<long>(
                name: "Health",
                table: "Boss",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Unit",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "PrefabId",
                table: "PlayableCharacters",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Unit",
                table: "Unit",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlayableCharacters",
                table: "PlayableCharacters",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Boss_Unit_PrefabId",
                table: "Boss",
                column: "PrefabId",
                principalTable: "Unit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayableCharacters_BattleRecords_BattleRecordId",
                table: "PlayableCharacters",
                column: "BattleRecordId",
                principalTable: "BattleRecords",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayableCharacters_Unit_PrefabId",
                table: "PlayableCharacters",
                column: "PrefabId",
                principalTable: "Unit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boss_Unit_PrefabId",
                table: "Boss");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayableCharacters_BattleRecords_BattleRecordId",
                table: "PlayableCharacters");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayableCharacters_Unit_PrefabId",
                table: "PlayableCharacters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Unit",
                table: "Unit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlayableCharacters",
                table: "PlayableCharacters");

            migrationBuilder.RenameTable(
                name: "Unit",
                newName: "Units");

            migrationBuilder.RenameTable(
                name: "PlayableCharacters",
                newName: "PlayableCharacter");

            migrationBuilder.RenameIndex(
                name: "IX_PlayableCharacters_PrefabId",
                table: "PlayableCharacter",
                newName: "IX_PlayableCharacter_PrefabId");

            migrationBuilder.RenameIndex(
                name: "IX_PlayableCharacters_BattleRecordId",
                table: "PlayableCharacter",
                newName: "IX_PlayableCharacter_BattleRecordId");

            migrationBuilder.AlterColumn<int>(
                name: "AttackerId",
                table: "TimelineItem",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "PrefabId",
                table: "Boss",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "Health",
                table: "Boss",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Units",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "PrefabId",
                table: "PlayableCharacter",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Units",
                table: "Units",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlayableCharacter",
                table: "PlayableCharacter",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Boss_Units_PrefabId",
                table: "Boss",
                column: "PrefabId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayableCharacter_BattleRecords_BattleRecordId",
                table: "PlayableCharacter",
                column: "BattleRecordId",
                principalTable: "BattleRecords",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayableCharacter_Units_PrefabId",
                table: "PlayableCharacter",
                column: "PrefabId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
