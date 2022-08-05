using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Service.Data.Migrations
{
    public partial class AddFamille : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Foursnisseur",
                table: "Foursnisseur");

            migrationBuilder.RenameTable(
                name: "Foursnisseur",
                newName: "Founisseur");

            migrationBuilder.RenameColumn(
                name: "Fournisseur_UtilisateurId",
                table: "Founisseur",
                newName: "Founisseur_UtilisateurId");

            migrationBuilder.RenameColumn(
                name: "Fournisseur_Telephone",
                table: "Founisseur",
                newName: "Founisseur_Telephone");

            migrationBuilder.RenameColumn(
                name: "Fournisseur_RaisonSocial",
                table: "Founisseur",
                newName: "Founisseur_RaisonSocial");

            migrationBuilder.RenameColumn(
                name: "Fournisseur_NomContact",
                table: "Founisseur",
                newName: "Founisseur_NomContact");

            migrationBuilder.RenameColumn(
                name: "Fournisseur_IsActive",
                table: "Founisseur",
                newName: "Founisseur_IsActive");

            migrationBuilder.RenameColumn(
                name: "Fournisseur_ICE",
                table: "Founisseur",
                newName: "Founisseur_ICE");

            migrationBuilder.RenameColumn(
                name: "Fournisseur_DateCreation",
                table: "Founisseur",
                newName: "Founisseur_DateSaisie");

            migrationBuilder.RenameColumn(
                name: "Fournisseur_Adresse",
                table: "Founisseur",
                newName: "Founisseur_Adresse");

            migrationBuilder.RenameColumn(
                name: "Fournisseur_AbonnementId",
                table: "Founisseur",
                newName: "Founisseur_AbonnementId");

            migrationBuilder.RenameColumn(
                name: "DateSaisie",
                table: "Founisseur",
                newName: "Founisseur_DateCreation");

            migrationBuilder.RenameColumn(
                name: "Fournisseur_Id",
                table: "Founisseur",
                newName: "Founisseur_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Founisseur",
                table: "Founisseur",
                column: "Founisseur_Id");

            migrationBuilder.CreateTable(
                name: "Famille_Produit",
                columns: table => new
                {
                    FamilleProduit_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FamilleProduit_Libelle = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    FamilleProduit_ParentId = table.Column<int>(type: "int", nullable: false),
                    FamilleProduit_AbonnemnetId = table.Column<int>(nullable: false),
                    FamilleProduit_DateCreation = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    FamilleProduit_IsActive = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Famille_Produit", x => x.FamilleProduit_Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Famille_Produit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Founisseur",
                table: "Founisseur");

            migrationBuilder.RenameTable(
                name: "Founisseur",
                newName: "Foursnisseur");

            migrationBuilder.RenameColumn(
                name: "Founisseur_UtilisateurId",
                table: "Foursnisseur",
                newName: "Fournisseur_UtilisateurId");

            migrationBuilder.RenameColumn(
                name: "Founisseur_Telephone",
                table: "Foursnisseur",
                newName: "Fournisseur_Telephone");

            migrationBuilder.RenameColumn(
                name: "Founisseur_RaisonSocial",
                table: "Foursnisseur",
                newName: "Fournisseur_RaisonSocial");

            migrationBuilder.RenameColumn(
                name: "Founisseur_NomContact",
                table: "Foursnisseur",
                newName: "Fournisseur_NomContact");

            migrationBuilder.RenameColumn(
                name: "Founisseur_IsActive",
                table: "Foursnisseur",
                newName: "Fournisseur_IsActive");

            migrationBuilder.RenameColumn(
                name: "Founisseur_ICE",
                table: "Foursnisseur",
                newName: "Fournisseur_ICE");

            migrationBuilder.RenameColumn(
                name: "Founisseur_DateSaisie",
                table: "Foursnisseur",
                newName: "Fournisseur_DateCreation");

            migrationBuilder.RenameColumn(
                name: "Founisseur_DateCreation",
                table: "Foursnisseur",
                newName: "DateSaisie");

            migrationBuilder.RenameColumn(
                name: "Founisseur_Adresse",
                table: "Foursnisseur",
                newName: "Fournisseur_Adresse");

            migrationBuilder.RenameColumn(
                name: "Founisseur_AbonnementId",
                table: "Foursnisseur",
                newName: "Fournisseur_AbonnementId");

            migrationBuilder.RenameColumn(
                name: "Founisseur_Id",
                table: "Foursnisseur",
                newName: "Fournisseur_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Foursnisseur",
                table: "Foursnisseur",
                column: "Fournisseur_Id");
        }
    }
}
