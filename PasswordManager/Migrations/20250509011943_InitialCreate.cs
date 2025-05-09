using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PasswordManager.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientBase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MasterPassword = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    isNull = table.Column<bool>(type: "bit", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientBase", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SecretBase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IClientId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecretQuality = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: true),
                    isNull = table.Column<bool>(type: "bit", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    SiteUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecretBase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SecretBase_ClientBase_ClientId",
                        column: x => x.ClientId,
                        principalTable: "ClientBase",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SecretBase_ClientBase_UserId",
                        column: x => x.UserId,
                        principalTable: "ClientBase",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SecretBase_ClientId",
                table: "SecretBase",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_SecretBase_UserId",
                table: "SecretBase",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SecretBase");

            migrationBuilder.DropTable(
                name: "ClientBase");
        }
    }
}
