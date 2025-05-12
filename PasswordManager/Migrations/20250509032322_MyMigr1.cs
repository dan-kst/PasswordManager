using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PasswordManager.Migrations
{
    /// <inheritdoc />
    public partial class MyMigr1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SecretBase_ClientBase_ClientId",
                table: "SecretBase");

            migrationBuilder.DropForeignKey(
                name: "FK_SecretBase_ClientBase_UserId",
                table: "SecretBase");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SecretBase",
                table: "SecretBase");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClientBase",
                table: "ClientBase");

            migrationBuilder.RenameTable(
                name: "SecretBase",
                newName: "Secrets");

            migrationBuilder.RenameTable(
                name: "ClientBase",
                newName: "Clients");

            migrationBuilder.RenameIndex(
                name: "IX_SecretBase_UserId",
                table: "Secrets",
                newName: "IX_Secrets_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_SecretBase_ClientId",
                table: "Secrets",
                newName: "IX_Secrets_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Secrets",
                table: "Secrets",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clients",
                table: "Clients",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Secrets_Clients_ClientId",
                table: "Secrets",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Secrets_Clients_UserId",
                table: "Secrets",
                column: "UserId",
                principalTable: "Clients",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Secrets_Clients_ClientId",
                table: "Secrets");

            migrationBuilder.DropForeignKey(
                name: "FK_Secrets_Clients_UserId",
                table: "Secrets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Secrets",
                table: "Secrets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clients",
                table: "Clients");

            migrationBuilder.RenameTable(
                name: "Secrets",
                newName: "SecretBase");

            migrationBuilder.RenameTable(
                name: "Clients",
                newName: "ClientBase");

            migrationBuilder.RenameIndex(
                name: "IX_Secrets_UserId",
                table: "SecretBase",
                newName: "IX_SecretBase_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Secrets_ClientId",
                table: "SecretBase",
                newName: "IX_SecretBase_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SecretBase",
                table: "SecretBase",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClientBase",
                table: "ClientBase",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SecretBase_ClientBase_ClientId",
                table: "SecretBase",
                column: "ClientId",
                principalTable: "ClientBase",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SecretBase_ClientBase_UserId",
                table: "SecretBase",
                column: "UserId",
                principalTable: "ClientBase",
                principalColumn: "Id");
        }
    }
}
