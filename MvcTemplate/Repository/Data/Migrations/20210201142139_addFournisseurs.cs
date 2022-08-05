using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Service.Data.Migrations
{
    public partial class addFournisseurs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Abonnement_Client_Abonnement_ID",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "Foursnisseur",
                columns: table => new
                {
                    Fournisseur_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Fournisseur_RaisonSocial = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    Fournisseur_ICE = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Fournisseur_Adresse = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    Fournisseur_NomContact = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Fournisseur_Telephone = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    DateSaisie = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    Fournisseur_UtilisateurId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Fournisseur_AbonnementId = table.Column<int>(type: "int", nullable: false),
                    Fournisseur_DateCreation = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    Fournisseur_IsActive = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foursnisseur", x => x.Fournisseur_Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Abonnement_Client_Abonnement_ID",
                table: "AspNetUsers",
                column: "Abonnement_ID",
                principalTable: "Abonnement_Client",
                principalColumn: "Abonnement_ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Abonnement_Client_Abonnement_ID",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Foursnisseur");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Abonnement_Client_Abonnement_ID",
                table: "AspNetUsers",
                column: "Abonnement_ID",
                principalTable: "Abonnement_Client",
                principalColumn: "Abonnement_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
