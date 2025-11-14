using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Migrations
{
    /// <inheritdoc />
    public partial class FixUserReferences : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GatePasses_Students_StudentId",
                table: "GatePasses");

            migrationBuilder.DropForeignKey(
                name: "FK_LockerRequests_Students_StudentId",
                table: "LockerRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Students_StudentId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_StudentId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_LockerRequests_StudentId",
                table: "LockerRequests");

            migrationBuilder.DropIndex(
                name: "IX_GatePasses_StudentId",
                table: "GatePasses");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "LockerRequests");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "GatePasses");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Reservations",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "LockerRequests",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "GatePasses",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_UserId",
                table: "Reservations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LockerRequests_UserId",
                table: "LockerRequests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GatePasses_UserId",
                table: "GatePasses",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_GatePasses_AspNetUsers_UserId",
                table: "GatePasses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LockerRequests_AspNetUsers_UserId",
                table: "LockerRequests",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_AspNetUsers_UserId",
                table: "Reservations",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GatePasses_AspNetUsers_UserId",
                table: "GatePasses");

            migrationBuilder.DropForeignKey(
                name: "FK_LockerRequests_AspNetUsers_UserId",
                table: "LockerRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_AspNetUsers_UserId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_UserId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_LockerRequests_UserId",
                table: "LockerRequests");

            migrationBuilder.DropIndex(
                name: "IX_GatePasses_UserId",
                table: "GatePasses");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "LockerRequests");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "GatePasses");

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "LockerRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "GatePasses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_StudentId",
                table: "Reservations",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_LockerRequests_StudentId",
                table: "LockerRequests",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_GatePasses_StudentId",
                table: "GatePasses",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_GatePasses_Students_StudentId",
                table: "GatePasses",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LockerRequests_Students_StudentId",
                table: "LockerRequests",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Students_StudentId",
                table: "Reservations",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
