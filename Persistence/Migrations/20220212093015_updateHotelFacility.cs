using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class updateHotelFacility : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Facilities_Hotels_HotelId",
                table: "Facilities");

            migrationBuilder.DropIndex(
                name: "IX_Facilities_HotelId",
                table: "Facilities");

            migrationBuilder.DropColumn(
                name: "HotelId",
                table: "Facilities");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HotelId",
                table: "Facilities",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Facilities_HotelId",
                table: "Facilities",
                column: "HotelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Facilities_Hotels_HotelId",
                table: "Facilities",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id");
        }
    }
}
