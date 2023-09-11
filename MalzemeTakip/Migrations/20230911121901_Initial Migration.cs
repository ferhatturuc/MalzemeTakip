using System;
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
            migrationBuilder.CreateTable(
                name: "Malzemeler",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MalzemeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MalzemeMiktar = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Malzemeler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Yemekler",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    YemekName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MalzemeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MalzemeMiktar = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Yemekler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MalzemeYemek",
                columns: table => new
                {
                    MalzemelerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    YemeklerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MalzemeYemek", x => new { x.MalzemelerId, x.YemeklerId });
                    table.ForeignKey(
                        name: "FK_MalzemeYemek_Malzemeler_MalzemelerId",
                        column: x => x.MalzemelerId,
                        principalTable: "Malzemeler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MalzemeYemek_Yemekler_YemeklerId",
                        column: x => x.YemeklerId,
                        principalTable: "Yemekler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MalzemeYemek_YemeklerId",
                table: "MalzemeYemek",
                column: "YemeklerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MalzemeYemek");

            migrationBuilder.DropTable(
                name: "Malzemeler");

            migrationBuilder.DropTable(
                name: "Yemekler");
        }
    }
}
