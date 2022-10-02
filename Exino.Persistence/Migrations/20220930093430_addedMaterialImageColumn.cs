using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exino.Persistence.Migrations
{
    public partial class addedMaterialImageColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MaterialImage",
                table: "Materials",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaterialImage",
                table: "Materials");
        }
    }
}
