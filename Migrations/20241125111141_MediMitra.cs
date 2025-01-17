using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediMitra.Migrations
{
    /// <inheritdoc />
    public partial class MediMitra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "registerModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Otp = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_registerModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "vaccinations",
                columns: table => new
                {
                    VaccinationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VaccinationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VaccinationType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VaccinationDose = table.Column<int>(type: "int", nullable: false),
                    AgeGroup = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServeDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vaccinations", x => x.VaccinationId);
                });

            migrationBuilder.CreateTable(
                name: "bookingVaccinations",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DOB = table.Column<DateOnly>(type: "date", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookingDate = table.Column<DateOnly>(type: "date", nullable: false),
                    VaccinationId = table.Column<int>(type: "int", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bookingVaccinations", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_bookingVaccinations_vaccinations_VaccinationId",
                        column: x => x.VaccinationId,
                        principalTable: "vaccinations",
                        principalColumn: "VaccinationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "registerModels",
                columns: new[] { "Id", "Email", "Otp", "Password", "Role", "Username" },
                values: new object[] { 1, "khanalvaidurga71@gmail.com", 0, "$2a$11$wj4gEdTno3K8lhgw3HRXteI74/fqYadHd/hNUY3B64pv1gzxdTgbO", "Admin", "Durga Khanal" });

            migrationBuilder.CreateIndex(
                name: "IX_bookingVaccinations_VaccinationId",
                table: "bookingVaccinations",
                column: "VaccinationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bookingVaccinations");

            migrationBuilder.DropTable(
                name: "registerModels");

            migrationBuilder.DropTable(
                name: "vaccinations");
        }
    }
}
