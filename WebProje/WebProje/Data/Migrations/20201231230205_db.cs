using Microsoft.EntityFrameworkCore.Migrations;

namespace WebProje.Data.Migrations
{
    public partial class db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hayvanlar_Ilanlar_IlanlarilanID",
                table: "Hayvanlar");

            migrationBuilder.DropIndex(
                name: "IX_Hayvanlar_IlanlarilanID",
                table: "Hayvanlar");

            migrationBuilder.DropColumn(
                name: "IlanlarilanID",
                table: "Hayvanlar");

            migrationBuilder.CreateIndex(
                name: "IX_Ilanlar_hayvanId",
                table: "Ilanlar",
                column: "hayvanId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Ilanlar_Hayvanlar_hayvanId",
                table: "Ilanlar",
                column: "hayvanId",
                principalTable: "Hayvanlar",
                principalColumn: "hayvanId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ilanlar_Hayvanlar_hayvanId",
                table: "Ilanlar");

            migrationBuilder.DropIndex(
                name: "IX_Ilanlar_hayvanId",
                table: "Ilanlar");

            migrationBuilder.AddColumn<int>(
                name: "IlanlarilanID",
                table: "Hayvanlar",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hayvanlar_IlanlarilanID",
                table: "Hayvanlar",
                column: "IlanlarilanID");

            migrationBuilder.AddForeignKey(
                name: "FK_Hayvanlar_Ilanlar_IlanlarilanID",
                table: "Hayvanlar",
                column: "IlanlarilanID",
                principalTable: "Ilanlar",
                principalColumn: "ilanID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
