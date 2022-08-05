using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Service.Data.Migrations
{
    public partial class Add_Stockage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produit_Vendable_Forme_Stockage_FORMEFormStockage_Id",
                table: "Produit_Vendable");

            migrationBuilder.DropForeignKey(
                name: "FK_Produit_Vendable_Famille_Produit_FamilleProduit_Id",
                table: "Produit_Vendable");

            migrationBuilder.DropForeignKey(
                name: "FK_Produit_Vendable_Unite_Mesure_UniteMesure_Id",
                table: "Produit_Vendable");

            migrationBuilder.DropForeignKey(
                name: "FK_Zone_Stockage_Forme_Stockage_FORMEFormStockage_Id",
                table: "Zone_Stockage");

            migrationBuilder.DropForeignKey(
                name: "FK_Zone_Stockage_Lieu_Stockage_LieuStockag_Id",
                table: "Zone_Stockage");

            migrationBuilder.DropForeignKey(
                name: "FK_Zone_Stockage_Unite_Mesure_UniteMesure_Id",
                table: "Zone_Stockage");

            migrationBuilder.DropIndex(
                name: "IX_Zone_Stockage_FORMEFormStockage_Id",
                table: "Zone_Stockage");

            migrationBuilder.DropIndex(
                name: "IX_Zone_Stockage_LieuStockag_Id",
                table: "Zone_Stockage");

            migrationBuilder.DropIndex(
                name: "IX_Zone_Stockage_UniteMesure_Id",
                table: "Zone_Stockage");

            migrationBuilder.DropIndex(
                name: "IX_Produit_Vendable_FORMEFormStockage_Id",
                table: "Produit_Vendable");

            migrationBuilder.DropIndex(
                name: "IX_Produit_Vendable_FamilleProduit_Id",
                table: "Produit_Vendable");

            migrationBuilder.DropIndex(
                name: "IX_Produit_Vendable_UniteMesure_Id",
                table: "Produit_Vendable");

            migrationBuilder.DropColumn(
                name: "FORMEFormStockage_Id",
                table: "Zone_Stockage");

            migrationBuilder.DropColumn(
                name: "LieuStockag_Id",
                table: "Zone_Stockage");

            migrationBuilder.DropColumn(
                name: "UniteMesure_Id",
                table: "Zone_Stockage");

            migrationBuilder.DropColumn(
                name: "FORMEFormStockage_Id",
                table: "Produit_Vendable");

            migrationBuilder.DropColumn(
                name: "FamilleProduit_Id",
                table: "Produit_Vendable");

            migrationBuilder.DropColumn(
                name: "UniteMesure_Id",
                table: "Produit_Vendable");

            migrationBuilder.AddColumn<bool>(
                name: "EnStock",
                table: "Produit_Vendable",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Allergene",
                columns: table => new
                {
                    Allergene_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Allergene_Libelle = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    Allergene_IsActive = table.Column<int>(nullable: false),
                    Allergene_AbonnementId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Allergene", x => x.Allergene_Id);
                });

            migrationBuilder.CreateTable(
                name: "Matiere_Premiere",
                columns: table => new
                {
                    MatierePremiere_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MatierePremiere_Libelle = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    MatierePremiere_AchatUniteMesureId = table.Column<int>(type: "int", nullable: false),
                    MatierePremiere_Quantite_FT = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    MatierePremiere_UniteMesureId_FT = table.Column<int>(type: "int", nullable: false),
                    MatierePremiere_PourcentrageTolerancePerte = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    MatierePremiere_PrixMoyenAchat = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    MatierePremiere_FormeStockageId = table.Column<int>(nullable: false),
                    MatierePremiere_EstAllergene = table.Column<int>(type: "int", nullable: false),
                    MatierePremiere_AllergeneId = table.Column<int>(nullable: false),
                    MatierePremiere_IsActive = table.Column<int>(nullable: false),
                    MatierePremiere_AbonnementId = table.Column<int>(type: "int", nullable: false),
                    MatierePremiere_DateCreation = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    EnStock = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matiere_Premiere", x => x.MatierePremiere_Id);
                });

            migrationBuilder.CreateTable(
                name: "MatierePremiere_Stokage",
                columns: table => new
                {
                    MatierePremiereStokage_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MatierePremiereStokage_SectionStockageId = table.Column<int>(nullable: false),
                    MatierePremiereStokage_MatierePremiereId = table.Column<int>(nullable: false),
                    MatierePremiereStokage_StockMinimum = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    MatierePremiereStokage_StockMaximum = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    MatierePremiereStokage_IsActive = table.Column<int>(nullable: false),
                    MatierePremiereStokage_AbonnementId = table.Column<int>(type: "int", nullable: false),
                    MatierePremiereStokage_DateCreation = table.Column<DateTime>(type: "smalldatetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatierePremiere_Stokage", x => x.MatierePremiereStokage_Id);
                });

            migrationBuilder.CreateTable(
                name: "Produit_FicheTechnique",
                columns: table => new
                {
                    FicheTechnique_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FicheTechnique_ProduitVendableId = table.Column<int>(nullable: false),
                    FicheTechnique_MatierePremiereId = table.Column<int>(nullable: false),
                    FicheTechnique_UniteMesureId = table.Column<int>(nullable: false),
                    FicheTechnique_QuantiteMatiere = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    FicheTechnique_Prix = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    FicheTechnique_IsActive = table.Column<int>(nullable: false),
                    FicheTechnique_AbonnementId = table.Column<int>(type: "int", nullable: false),
                    FicheTechnique_DateCreation = table.Column<DateTime>(type: "smalldatetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produit_FicheTechnique", x => x.FicheTechnique_Id);
                });

            migrationBuilder.CreateTable(
                name: "ProduitVendable_Stokage",
                columns: table => new
                {
                    ProduitVendableStokage_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProduitVendableStokage_SectionStockageId = table.Column<int>(nullable: false),
                    ProduitVendableStokage_ProduitVendableId = table.Column<int>(nullable: false),
                    ProduitVendableStokage_StockMinimum = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ProduitVendableStokage_StockMaximum = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ProduitVendableStokage_IsActive = table.Column<int>(nullable: false),
                    ProduitVendableStokage_AbonnementId = table.Column<int>(type: "int", nullable: false),
                    ProduitVendableStokage_DateCreation = table.Column<DateTime>(type: "smalldatetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProduitVendable_Stokage", x => x.ProduitVendableStokage_Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Allergene");

            migrationBuilder.DropTable(
                name: "Matiere_Premiere");

            migrationBuilder.DropTable(
                name: "MatierePremiere_Stokage");

            migrationBuilder.DropTable(
                name: "Produit_FicheTechnique");

            migrationBuilder.DropTable(
                name: "ProduitVendable_Stokage");

            migrationBuilder.DropColumn(
                name: "EnStock",
                table: "Produit_Vendable");

            migrationBuilder.AddColumn<int>(
                name: "FORMEFormStockage_Id",
                table: "Zone_Stockage",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LieuStockag_Id",
                table: "Zone_Stockage",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UniteMesure_Id",
                table: "Zone_Stockage",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FORMEFormStockage_Id",
                table: "Produit_Vendable",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FamilleProduit_Id",
                table: "Produit_Vendable",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UniteMesure_Id",
                table: "Produit_Vendable",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Zone_Stockage_FORMEFormStockage_Id",
                table: "Zone_Stockage",
                column: "FORMEFormStockage_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Zone_Stockage_LieuStockag_Id",
                table: "Zone_Stockage",
                column: "LieuStockag_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Zone_Stockage_UniteMesure_Id",
                table: "Zone_Stockage",
                column: "UniteMesure_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Produit_Vendable_FORMEFormStockage_Id",
                table: "Produit_Vendable",
                column: "FORMEFormStockage_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Produit_Vendable_FamilleProduit_Id",
                table: "Produit_Vendable",
                column: "FamilleProduit_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Produit_Vendable_UniteMesure_Id",
                table: "Produit_Vendable",
                column: "UniteMesure_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Produit_Vendable_Forme_Stockage_FORMEFormStockage_Id",
                table: "Produit_Vendable",
                column: "FORMEFormStockage_Id",
                principalTable: "Forme_Stockage",
                principalColumn: "FormStockage_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Produit_Vendable_Famille_Produit_FamilleProduit_Id",
                table: "Produit_Vendable",
                column: "FamilleProduit_Id",
                principalTable: "Famille_Produit",
                principalColumn: "FamilleProduit_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Produit_Vendable_Unite_Mesure_UniteMesure_Id",
                table: "Produit_Vendable",
                column: "UniteMesure_Id",
                principalTable: "Unite_Mesure",
                principalColumn: "UniteMesure_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Zone_Stockage_Forme_Stockage_FORMEFormStockage_Id",
                table: "Zone_Stockage",
                column: "FORMEFormStockage_Id",
                principalTable: "Forme_Stockage",
                principalColumn: "FormStockage_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Zone_Stockage_Lieu_Stockage_LieuStockag_Id",
                table: "Zone_Stockage",
                column: "LieuStockag_Id",
                principalTable: "Lieu_Stockage",
                principalColumn: "LieuStockag_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Zone_Stockage_Unite_Mesure_UniteMesure_Id",
                table: "Zone_Stockage",
                column: "UniteMesure_Id",
                principalTable: "Unite_Mesure",
                principalColumn: "UniteMesure_Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
