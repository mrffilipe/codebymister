using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Codebymister.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAlreadyApproachedToLead : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "already_approached",
                table: "leads",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "already_approached",
                table: "leads");
        }
    }
}
