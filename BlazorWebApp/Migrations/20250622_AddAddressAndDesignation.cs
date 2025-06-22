using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorWebApp.Migrations
{
    public partial class AddAddressAndDesignation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Registrations",
                type: "text",
                nullable: true);
            migrationBuilder.AddColumn<string>(
                name: "Designation",
                table: "Registrations",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Registrations");
            migrationBuilder.DropColumn(
                name: "Designation",
                table: "Registrations");
        }
    }
}
