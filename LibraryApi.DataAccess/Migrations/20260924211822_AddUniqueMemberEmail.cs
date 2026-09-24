using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryApi.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueMemberEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF EXISTS (SELECT 1 FROM [Members] WHERE DATALENGTH([Email]) > 512)
                    THROW 51000, 'Member email longer than 256 characters; migration stopped.', 1;

                IF EXISTS (
                    SELECT 1 FROM [Members]
                    GROUP BY CONVERT(nvarchar(256), [Email])
                    HAVING COUNT(*) > 1
                )
                    THROW 51001, 'Duplicate member emails; migration stopped.', 1;
            ");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Members",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Members_Email",
                table: "Members",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Members_Email",
                table: "Members");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Members",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);
        }
    }
}
