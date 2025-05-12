using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PasswordManager.Migrations
{
    /// <inheritdoc />
    public partial class MyMigr5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isNull",
                table: "Secrets");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isNull",
                table: "Secrets",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
