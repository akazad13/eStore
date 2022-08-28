using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exino.Persistence.Migrations
{
    public partial class Update_AppUser_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSubscribeToNewsletter",
                table: "AppUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSubscribeToNewsletter",
                table: "AppUsers");
        }
    }
}
