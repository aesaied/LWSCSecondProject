using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LWSCSecondProject.Migrations
{
    /// <inheritdoc />
    public partial class AddFullNameDefaultValue2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Update [AspNetUsers] set  FullName='Unkown User' where FullName is null ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
