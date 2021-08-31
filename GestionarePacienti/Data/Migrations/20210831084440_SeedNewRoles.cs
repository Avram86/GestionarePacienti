using GestionarePacienti.Data.Extensions;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GestionarePacienti.Migrations
{
    public partial class SeedNewRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUserWithRoles(
               "test@accountsapp.com",
               "P@ssword1",
               new[] { "operator" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
