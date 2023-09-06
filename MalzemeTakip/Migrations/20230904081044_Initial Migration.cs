using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MalzemeTakip.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MalzemeYemek_Malzemeler_MalzemelerMalzemeId",
                table: "MalzemeYemek");

            migrationBuilder.DropForeignKey(
                name: "FK_MalzemeYemek_Yemekler_YemeklerYemekId",
                table: "MalzemeYemek");

            migrationBuilder.RenameColumn(
                name: "YemekId",
                table: "Yemekler",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "YemeklerYemekId",
                table: "MalzemeYemek",
                newName: "YemeklerId");

            migrationBuilder.RenameColumn(
                name: "MalzemelerMalzemeId",
                table: "MalzemeYemek",
                newName: "MalzemelerId");

            migrationBuilder.RenameIndex(
                name: "IX_MalzemeYemek_YemeklerYemekId",
                table: "MalzemeYemek",
                newName: "IX_MalzemeYemek_YemeklerId");

            migrationBuilder.RenameColumn(
                name: "MalzemeId",
                table: "Malzemeler",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MalzemeYemek_Malzemeler_MalzemelerId",
                table: "MalzemeYemek",
                column: "MalzemelerId",
                principalTable: "Malzemeler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MalzemeYemek_Yemekler_YemeklerId",
                table: "MalzemeYemek",
                column: "YemeklerId",
                principalTable: "Yemekler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MalzemeYemek_Malzemeler_MalzemelerId",
                table: "MalzemeYemek");

            migrationBuilder.DropForeignKey(
                name: "FK_MalzemeYemek_Yemekler_YemeklerId",
                table: "MalzemeYemek");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Yemekler",
                newName: "YemekId");

            migrationBuilder.RenameColumn(
                name: "YemeklerId",
                table: "MalzemeYemek",
                newName: "YemeklerYemekId");

            migrationBuilder.RenameColumn(
                name: "MalzemelerId",
                table: "MalzemeYemek",
                newName: "MalzemelerMalzemeId");

            migrationBuilder.RenameIndex(
                name: "IX_MalzemeYemek_YemeklerId",
                table: "MalzemeYemek",
                newName: "IX_MalzemeYemek_YemeklerYemekId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Malzemeler",
                newName: "MalzemeId");

            migrationBuilder.AddForeignKey(
                name: "FK_MalzemeYemek_Malzemeler_MalzemelerMalzemeId",
                table: "MalzemeYemek",
                column: "MalzemelerMalzemeId",
                principalTable: "Malzemeler",
                principalColumn: "MalzemeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MalzemeYemek_Yemekler_YemeklerYemekId",
                table: "MalzemeYemek",
                column: "YemeklerYemekId",
                principalTable: "Yemekler",
                principalColumn: "YemekId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
