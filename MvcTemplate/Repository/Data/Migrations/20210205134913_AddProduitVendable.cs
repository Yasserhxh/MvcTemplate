using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Service.Data.Migrations
{
    public partial class AddProduitVendable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Forme_Stockage",
                columns: table => new
                {
                    FormStockage_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FormStockage_Libelle = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    FormStockage_DateCreation = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    FormStockage_AbonnementId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forme_Stockage", x => x.FormStockage_Id);
                });

            migrationBuilder.CreateTable(
                name: "Lieu_Stockage",
                columns: table => new
                {
                    LieuStockag_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LieuStockag_Nom = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    LieuStockag_Adresse = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    LieuStockag_NomResponsable = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    LieuStockag_Telephone = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    LieuStockag_IsActive = table.Column<int>(nullable: false),
                    LieuStockag_AbonnementId = table.Column<int>(type: "int", nullable: false),
                    LieuStockag_DateCreation = table.Column<DateTime>(type: "smalldatetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lieu_Stockage", x => x.LieuStockag_Id);
                });

            migrationBuilder.CreateTable(
                name: "Paiemenet_Abonnement",
                columns: table => new
                {
                    PaiementAbonnement_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PaiementAbonnement_Date = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    PaiementAbonnemet_Montant = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    PaiementAbonnement_DateDebutPeriode = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    PaiementAbonnement_DateFinPeriode = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    PaiementAbonnement_DateSaisie = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    PaiementAbonnement_AdminUserId = table.Column<Guid>(type: "uniqueidentifier(16)", nullable: false),
                    PaiementAbonnement_AbonnementId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paiemenet_Abonnement", x => x.PaiementAbonnement_ID);
                });

            migrationBuilder.CreateTable(
                name: "Point_Vente",
                columns: table => new
                {
                    PointVente_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PointVente_Nom = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    PointVente_Adresse = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    PointVente_NomResponsable = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    PointVente_Telephone = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    PointVente_IsActive = table.Column<int>(nullable: false),
                    PointVente_DateSaisie = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    PointVente_UtilisateurId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PointVente_AbonnementId = table.Column<int>(type: "int", nullable: false),
                    PointVente_DateCreation = table.Column<DateTime>(type: "smalldatetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Point_Vente", x => x.PointVente_Id);
                });

            migrationBuilder.CreateTable(
                name: "Section_Stockage",
                columns: table => new
                {
                    Section_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Section_Designation = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    Section_ZoneStockageId = table.Column<int>(nullable: false),
                    Section_IsActive = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Section_Stockage", x => x.Section_Id);
                });

            migrationBuilder.CreateTable(
                name: "Type_Contenu",
                columns: table => new
                {
                    TypeContenu_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TypeContenu_Libelle = table.Column<string>(type: "nvarchar(150)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Type_Contenu", x => x.TypeContenu_Id);
                });

            migrationBuilder.CreateTable(
                name: "Unite_Mesure",
                columns: table => new
                {
                    UniteMesure_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UniteMesure_Libelle = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unite_Mesure", x => x.UniteMesure_Id);
                });

            migrationBuilder.CreateTable(
                name: "Produit_Vendable",
                columns: table => new
                {
                    ProduitVendable_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProduitVendable_FamilleProduitId = table.Column<int>(nullable: true),
                    ProduitVendable_Designation = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    ProduitVendable_Description = table.Column<string>(type: "nvarchar(1000)", nullable: true),
                    ProduitVendable_FormStockageId = table.Column<int>(nullable: true),
                    ProduitVendable_UniteMesureId = table.Column<int>(nullable: true),
                    ProduitVendable_UniteMesureId_FT = table.Column<int>(type: "int", nullable: true),
                    ProduitVendable_StockMaximum = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    ProduitVendable_StockMinimum = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    ProduitVendable_DelaiConsommation = table.Column<int>(type: "int", nullable: true),
                    ProduitVendable_CodeBarre = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    ProduitVendable_GrandePhoto = table.Column<string>(type: "nvarchar(300)", nullable: true),
                    ProduitVendable_PetitePhoto = table.Column<string>(type: "nvarchar(300)", nullable: true),
                    ProduitVendable_IsActive = table.Column<int>(nullable: false),
                    ProduitVendable_AbonnementId = table.Column<int>(type: "int", nullable: true),
                    ProduitVendable_DateCreation = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    UniteMesure_Id = table.Column<int>(nullable: true),
                    FamilleProduit_Id = table.Column<int>(nullable: true),
                    FORMEFormStockage_Id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produit_Vendable", x => x.ProduitVendable_Id);
                    table.ForeignKey(
                        name: "FK_Produit_Vendable_Forme_Stockage_FORMEFormStockage_Id",
                        column: x => x.FORMEFormStockage_Id,
                        principalTable: "Forme_Stockage",
                        principalColumn: "FormStockage_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Produit_Vendable_Famille_Produit_FamilleProduit_Id",
                        column: x => x.FamilleProduit_Id,
                        principalTable: "Famille_Produit",
                        principalColumn: "FamilleProduit_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Produit_Vendable_Unite_Mesure_UniteMesure_Id",
                        column: x => x.UniteMesure_Id,
                        principalTable: "Unite_Mesure",
                        principalColumn: "UniteMesure_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Zone_Stockage",
                columns: table => new
                {
                    ZoneStockage_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ZoneStockage_LieuStockageId = table.Column<int>(nullable: true),
                    ZoneStockage_FormeStockageId = table.Column<int>(nullable: true),
                    ZoneStockage_CapaciteStockage = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ZoneStockage_UniteMesureId = table.Column<int>(nullable: true),
                    ZoneStockage_TypeContenuId = table.Column<int>(nullable: true),
                    ZoneStockage_IsActive = table.Column<int>(nullable: true),
                    ZoneStockage_AbonnementId = table.Column<int>(type: "int", nullable: true),
                    ZoneStockage_DateCreation = table.Column<DateTime>(type: "smalldatatime", nullable: true),
                    UniteMesure_Id = table.Column<int>(nullable: true),
                    TypeContenu_Id = table.Column<int>(nullable: true),
                    LieuStockag_Id = table.Column<int>(nullable: true),
                    FORMEFormStockage_Id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zone_Stockage", x => x.ZoneStockage_Id);
                    table.ForeignKey(
                        name: "FK_Zone_Stockage_Forme_Stockage_FORMEFormStockage_Id",
                        column: x => x.FORMEFormStockage_Id,
                        principalTable: "Forme_Stockage",
                        principalColumn: "FormStockage_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Zone_Stockage_Lieu_Stockage_LieuStockag_Id",
                        column: x => x.LieuStockag_Id,
                        principalTable: "Lieu_Stockage",
                        principalColumn: "LieuStockag_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Zone_Stockage_Type_Contenu_TypeContenu_Id",
                        column: x => x.TypeContenu_Id,
                        principalTable: "Type_Contenu",
                        principalColumn: "TypeContenu_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Zone_Stockage_Unite_Mesure_UniteMesure_Id",
                        column: x => x.UniteMesure_Id,
                        principalTable: "Unite_Mesure",
                        principalColumn: "UniteMesure_Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Zone_Stockage_FORMEFormStockage_Id",
                table: "Zone_Stockage",
                column: "FORMEFormStockage_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Zone_Stockage_LieuStockag_Id",
                table: "Zone_Stockage",
                column: "LieuStockag_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Zone_Stockage_TypeContenu_Id",
                table: "Zone_Stockage",
                column: "TypeContenu_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Zone_Stockage_UniteMesure_Id",
                table: "Zone_Stockage",
                column: "UniteMesure_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Paiemenet_Abonnement");

            migrationBuilder.DropTable(
                name: "Point_Vente");

            migrationBuilder.DropTable(
                name: "Produit_Vendable");

            migrationBuilder.DropTable(
                name: "Section_Stockage");

            migrationBuilder.DropTable(
                name: "Zone_Stockage");

            migrationBuilder.DropTable(
                name: "Forme_Stockage");

            migrationBuilder.DropTable(
                name: "Lieu_Stockage");

            migrationBuilder.DropTable(
                name: "Type_Contenu");

            migrationBuilder.DropTable(
                name: "Unite_Mesure");
        }
    }
}
