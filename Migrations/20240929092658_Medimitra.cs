using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MediMitra.Migrations
{
    /// <inheritdoc />
    public partial class Medimitra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                });

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

            migrationBuilder.InsertData(
                table: "registerModels",
                columns: new[] { "Id", "Email", "Otp", "Password", "Role", "Username" },
                values: new object[,]
                {
                    { 1, "khanalvaidurga71@gmail.com", 0, "$2a$11$FSzUlziklXrrzCKsUqYdA.SCzuUlWagk1yjU1uBR98putt94k5FyG", "Admin", "Durga Khanal" },
                    { 2, "sumildumre555@gmail.com", 0, "$2a$11$SyGE50/xOdA7bAYEc0fVp.00LqedRH3orjDNFg.krdRXTcE46jBWu", "Moderator", "Sunil Dumre" },
                    { 3, "bhushaltilak9@gmail.com", 0, "$2a$11$/mYxoFxsf07oZGCYh7XKa.Hia7FZZovGXywz7QsDL12jXXubS7a3K", "Moderator", "Tilak Bhusal" }
                });
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
