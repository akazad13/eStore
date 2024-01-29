using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eStore.Persistence.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaterialImage",
                table: "Materials",
                newName: "MaterialImagePath"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaterialImagePath",
                table: "Materials",
                newName: "MaterialImage"
            );
        }
    }
}
