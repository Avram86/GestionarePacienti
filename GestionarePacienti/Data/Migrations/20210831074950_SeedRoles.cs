using GestionarePacienti.Data.Extensions;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GestionarePacienti.Migrations
{
    public partial class SeedRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddRole("admin");
            migrationBuilder.AddRole("operator");

            migrationBuilder.AddUserWithRoles(
                "admin@accountsapp.com",
                "Admin123!",
                new[] { "admin"});

            migrationBuilder.AddUserWithRoles(
                "operator@accountsapp.com",
                "Admin123!",
                new[] { "operator" });


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
