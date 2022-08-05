using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Data.Migrations
{
    public partial class Add_AboClient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Abonnement_ISACTIVE",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Abonnement_IsActive",
                table: "Abonnement_Client",
                nullable: false,
                oldClrType: typeof(bool));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Abonnement_ISACTIVE",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Abonnement_IsActive",
                table: "Abonnement_Client",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
