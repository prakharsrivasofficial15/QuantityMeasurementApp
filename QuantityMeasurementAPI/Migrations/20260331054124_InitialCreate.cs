using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuantityMeasurementAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    LastLoginAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MeasurementRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Operation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Operand1_Value = table.Column<double>(type: "float", nullable: true),
                    Operand1_Unit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Operand1_Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Operand2_Value = table.Column<double>(type: "float", nullable: true),
                    Operand2_Unit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Operand2_Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Result_Value = table.Column<double>(type: "float", nullable: true),
                    Result_Unit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Result_Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    HasError = table.Column<bool>(type: "bit", nullable: false),
                    ErrorMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasurementRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeasurementRecords_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MeasurementRecords_Operation",
                table: "MeasurementRecords",
                column: "Operation");

            migrationBuilder.CreateIndex(
                name: "IX_MeasurementRecords_Timestamp",
                table: "MeasurementRecords",
                column: "Timestamp");

            migrationBuilder.CreateIndex(
                name: "IX_MeasurementRecords_UserId",
                table: "MeasurementRecords",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MeasurementRecords");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
