using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebProje.Data.Migrations
{
    public partial class addClasses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ilanlar",
                columns: table => new
                {
                    ilanID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ilanNo = table.Column<int>(nullable: false),
                    hayvanId = table.Column<int>(nullable: false),
                    ilanTarihi = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ilanlar", x => x.ilanID);
                });

            migrationBuilder.CreateTable(
                name: "Hayvanlar",
                columns: table => new
                {
                    hayvanId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    hayvanTuruId = table.Column<int>(nullable: false),
                    hayvanYasi = table.Column<int>(nullable: false),
                    hayvanCinsi = table.Column<string>(nullable: true),
                    hayvanCinsiyeti = table.Column<string>(nullable: true),
                    IlanlarilanID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hayvanlar", x => x.hayvanId);
                    table.ForeignKey(
                        name: "FK_Hayvanlar_Ilanlar_IlanlarilanID",
                        column: x => x.IlanlarilanID,
                        principalTable: "Ilanlar",
                        principalColumn: "ilanID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hayvanlar_IlanlarilanID",
                table: "Hayvanlar",
                column: "IlanlarilanID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Hayvanlar");

            migrationBuilder.DropTable(
                name: "Ilanlar");
        }
    }
}
