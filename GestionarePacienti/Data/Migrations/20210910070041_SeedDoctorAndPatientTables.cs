using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GestionarePacienti.Migrations
{
    public partial class SeedDoctorAndPatientTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Doctor",
                columns: new[] { "Id", "Name", "Specialization" },
                values: new object[,]
                {
                    { 1, "Dr. Test Unu", 1 },
                    { 2, "Dr. Test Doi", 4 },
                    { 3, "Dr. Test Trei", 3 }
                });

            migrationBuilder.InsertData(
                table: "Patient",
                columns: new[] { "Id", "Address", "City", "County", "DateOfBirth", "Gender", "MaritalStatus", "Name", "Phone" },
                values: new object[,]
                {
                    { 1, "no 51, Test Street", "Oradea", "Bihor", new DateTime(1980, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 0, "John Doe", "0777777777" },
                    { 2, "no 10, Test2 Street", "Alesd", "Bihor", new DateTime(1990, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, "Jane Doe", "0777111222" },
                    { 3, "no 20, Test3 Street", "Marghita", "Bihor", new DateTime(2000, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 0, "Jessica Doe", "0772333444" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Doctor",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Doctor",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Doctor",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Patient",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Patient",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Patient",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
