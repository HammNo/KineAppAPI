using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KineApp.DAL.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    EncodedPassword = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Salt = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Weeks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstDay = table.Column<DateTime>(type: "Date", nullable: false),
                    LastDay = table.Column<DateTime>(type: "Date", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weeks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Days",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "Date", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Visible = table.Column<bool>(type: "bit", nullable: false),
                    WeekId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Days", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Days_Weeks_WeekId",
                        column: x => x.WeekId,
                        principalTable: "Weeks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TimeSlots",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Booked = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DayId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSlots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeSlots_Days_DayId",
                        column: x => x.DayId,
                        principalTable: "Days",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TimeSlots_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "Email", "EncodedPassword", "Name", "Role", "Salt" },
                values: new object[] { new Guid("e41efc2b-2e4b-4942-a19a-f0ad8b317875"), "admin@mail.be", new byte[] { 66, 30, 63, 185, 39, 135, 232, 218, 158, 41, 163, 216, 247, 241, 4, 249, 156, 189, 239, 28, 235, 17, 251, 65, 94, 40, 154, 126, 56, 234, 223, 56, 97, 97, 51, 81, 35, 248, 48, 42, 221, 217, 82, 30, 106, 61, 150, 9, 86, 120, 150, 0, 136, 224, 43, 164, 46, 157, 34, 55, 18, 31, 81, 60 }, "Admin", 0, new Guid("d65dd8df-3e8a-4dd2-a4f5-6eb63c4e8819") });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "Gender", "LastName", "PhoneNumber" },
                values: new object[] { new Guid("7393c03a-6e44-4c1e-9a13-56ebb71c1b8d"), "test@mail.com", "Firsty", 2, "Zero", "0111111111" });

            migrationBuilder.InsertData(
                table: "Weeks",
                columns: new[] { "Id", "FirstDay", "LastDay", "Note" },
                values: new object[] { new Guid("c017bdec-757b-4c5e-ae96-d6f7da1c2b10"), new DateTime(2021, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_Email",
                table: "Admins",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Admins_Name",
                table: "Admins",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Admins_Salt",
                table: "Admins",
                column: "Salt",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Days_WeekId",
                table: "Days",
                column: "WeekId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSlots_DayId",
                table: "TimeSlots",
                column: "DayId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSlots_UserId",
                table: "TimeSlots",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "TimeSlots");

            migrationBuilder.DropTable(
                name: "Days");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Weeks");
        }
    }
}
