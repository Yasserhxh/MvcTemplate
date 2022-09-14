using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Data.Migrations
{
    public partial class addTabUniteCat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Zone_Stockage_Type_Contenu_TypeContenu_Id",
                table: "Zone_Stockage");

            migrationBuilder.DropTable(
                name: "ProduitVendable_Stokage");

            migrationBuilder.DropIndex(
                name: "IX_Zone_Stockage_TypeContenu_Id",
                table: "Zone_Stockage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Paiemenet_Abonnement",
                table: "Paiemenet_Abonnement");

            migrationBuilder.DropColumn(
                name: "TypeContenu_Id",
                table: "Zone_Stockage");

            migrationBuilder.DropColumn(
                name: "EnStock",
                table: "Produit_Vendable");

            migrationBuilder.DropColumn(
                name: "FamilleProduit_ParentId",
                table: "Famille_Produit");

            migrationBuilder.RenameTable(
                name: "Paiemenet_Abonnement",
                newName: "Paiement_Abonnement");

            migrationBuilder.RenameColumn(
                name: "MatierePremiere_AllergeneId",
                table: "Matiere_Premiere",
                newName: "MatierePremiere_FamilleID");

            migrationBuilder.RenameColumn(
                name: "Abonnement_ID",
                table: "Abonnement_Client",
                newName: "Abonnement_Id");

            migrationBuilder.RenameColumn(
                name: "PaiementAbonnemet_Montant",
                table: "Paiement_Abonnement",
                newName: "PaiementAbonnement_PeriodeDePaiement");

            migrationBuilder.AddColumn<string>(
                name: "ZoneStockage_Designation",
                table: "Zone_Stockage",
                type: "nvarchar(150)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Unite_CategorieID",
                table: "Unite_Mesure",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "ProduitVendable_FamilleProduitId",
                table: "Produit_Vendable",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProduitVendable_Description",
                table: "Produit_Vendable",
                type: "nvarchar(150)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProduitVendable_AvecFT",
                table: "Produit_Vendable",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProduitVendable_CodeProduit",
                table: "Produit_Vendable",
                type: "nvarchar(150)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProduitVendable_Conditionnement",
                table: "Produit_Vendable",
                type: "nvarchar(150)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProduitVendable_DesignationAR",
                table: "Produit_Vendable",
                type: "nvarchar(150)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProduitVendable_DesignationEN",
                table: "Produit_Vendable",
                type: "nvarchar(150)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProduitVendable_PlanificationFlag",
                table: "Produit_Vendable",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProduitVendable_Specification",
                table: "Produit_Vendable",
                type: "nvarchar(150)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProduitVendable_TempConservation",
                table: "Produit_Vendable",
                type: "nvarchar(150)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProduitVendable_TvaId",
                table: "Produit_Vendable",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Produit_CoutDeRevient",
                table: "Produit_Vendable",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FicheTechnique_FicheTechniqueBridgeID",
                table: "Produit_FicheTechnique",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FicheTechnique_Libelle",
                table: "Produit_FicheTechnique",
                type: "nvarchar(250)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PointVente_UtilisateurId",
                table: "Point_Vente",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PointVente_AtelierID",
                table: "Point_Vente",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PointVente_CodePostal",
                table: "Point_Vente",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PointVente_StockID",
                table: "Point_Vente",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PointVente_TypeService",
                table: "Point_Vente",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PointVente_VilleID",
                table: "Point_Vente",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "MatierePremiereStokage_QuantiteActuelle",
                table: "MatierePremiere_Stokage",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<int>(
                name: "MatierePremiere_FormeStockageId",
                table: "Matiere_Premiere",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "MatierePremiere_CodeArticle",
                table: "Matiere_Premiere",
                type: "nvarchar(150)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MatierePremiere_Composants",
                table: "Matiere_Premiere",
                type: "nvarchar(150)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MatierePremiere_CoutTVAID",
                table: "Matiere_Premiere",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MatierePremiere_LibelleAR",
                table: "Matiere_Premiere",
                type: "nvarchar(150)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MatierePremiere_LibelleEN",
                table: "Matiere_Premiere",
                type: "nvarchar(150)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MatierePremiere_QuantiteActuelle",
                table: "Matiere_Premiere",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "LieuStockag_CodePostal",
                table: "Lieu_Stockage",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LieuStockag_ParDefaut",
                table: "Lieu_Stockage",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LieuStockag_UTILISATEUR",
                table: "Lieu_Stockage",
                type: "nvarchar(250)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LieuStockag_VilleID",
                table: "Lieu_Stockage",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Founisseur_UtilisateurId",
                table: "Founisseur",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Founisseur_VilleID",
                table: "Founisseur",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FamilleProduit_Image",
                table: "Famille_Produit",
                type: "nvarchar(350)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Adresse",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AtelierID",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LieuStockage_ID",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "AspNetUsers",
                type: "nvarchar(150)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nom",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nom_Complet",
                table: "AspNetUsers",
                type: "nvarchar(150)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PointVente_ID",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PositionVente_ID",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Prenom",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Allergene_LibelleAR",
                table: "Allergene",
                type: "nvarchar(150)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Abonnement_VilleId",
                table: "Abonnement_Client",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(4)");

            migrationBuilder.AddColumn<string>(
                name: "Abonnement_Logo",
                table: "Abonnement_Client",
                type: "nvarchar(150)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Abonnement_NomClientAR",
                table: "Abonnement_Client",
                type: "nvarchar(150)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Abonnement_ONSSAAuthorisation",
                table: "Abonnement_Client",
                type: "nvarchar(150)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PaiementAbonnement_AdminUserId",
                table: "Paiement_Abonnement",
                type: "nvarchar(16)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier(16)");

            migrationBuilder.AddColumn<decimal>(
                name: "PaiementAbonnement_Montant",
                table: "Paiement_Abonnement",
                type: "decimal(8,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "PaiementAbonnement_PointVenteID",
                table: "Paiement_Abonnement",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Paiement_Abonnement",
                table: "Paiement_Abonnement",
                column: "PaiementAbonnement_ID");

            migrationBuilder.CreateTable(
                name: "Agent_Serveur",
                columns: table => new
                {
                    Agent_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Agent_NomPrenom = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Agent_PointVenteId = table.Column<int>(type: "int", nullable: false),
                    Agent_IsActive = table.Column<int>(type: "int", nullable: false),
                    Agent_AbonnementId = table.Column<int>(type: "int", nullable: false),
                    Agent_UtilisateurId = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    Agent_DateCreation = table.Column<DateTime>(type: "smalldatetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agent_Serveur", x => x.Agent_Id);
                });

            migrationBuilder.CreateTable(
                name: "Bon_De_Commande",
                columns: table => new
                {
                    BonDeCommande_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BonDeCommande_Numero = table.Column<string>(nullable: true),
                    BonDeCommande_FournisseurID = table.Column<int>(nullable: false),
                    BonDeCommande_CreePar = table.Column<string>(nullable: true),
                    BonDeCommande_ValidéPar = table.Column<string>(nullable: true),
                    BonDeCommande_DateCreation = table.Column<DateTime>(nullable: false),
                    BonDeCommande_DateValidation = table.Column<DateTime>(nullable: true),
                    BonDeCommande_AbonnementID = table.Column<int>(nullable: false),
                    BonDeCommande_TotalHT = table.Column<decimal>(nullable: false),
                    BonDeCommande_TotalTVA = table.Column<decimal>(nullable: false),
                    BonDeCommande_TotalTTC = table.Column<decimal>(nullable: false),
                    BonDeCommande_Statut = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bon_De_Commande", x => x.BonDeCommande_ID);
                    table.ForeignKey(
                        name: "FK_Bon_De_Commande_Abonnement_Client_BonDeCommande_AbonnementID",
                        column: x => x.BonDeCommande_AbonnementID,
                        principalTable: "Abonnement_Client",
                        principalColumn: "Abonnement_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bon_De_Commande_Founisseur_BonDeCommande_FournisseurID",
                        column: x => x.BonDeCommande_FournisseurID,
                        principalTable: "Founisseur",
                        principalColumn: "Founisseur_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BonDe_Sortie",
                columns: table => new
                {
                    BonDeSortie_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BonDeSortie_Libelle = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    BonDeSortie_DateProduction = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    BonDeSortie_DateCreation = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    BonDeSortie_AbonnementID = table.Column<int>(type: "int", nullable: false),
                    BonDeSortie_StockID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BonDe_Sortie", x => x.BonDeSortie_ID);
                    table.ForeignKey(
                        name: "FK_BonDe_Sortie_Lieu_Stockage_BonDeSortie_StockID",
                        column: x => x.BonDeSortie_StockID,
                        principalTable: "Lieu_Stockage",
                        principalColumn: "LieuStockag_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Distributeur",
                columns: table => new
                {
                    Distributeur_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Distributeur_RaisonSocial = table.Column<string>(nullable: true),
                    Distributeur_RaisonSocialAR = table.Column<string>(nullable: true),
                    Distributeur_ICE = table.Column<int>(nullable: false),
                    Distributeur_IF = table.Column<int>(nullable: false),
                    Distributeur_Adresse = table.Column<string>(nullable: true),
                    Distributeur_NomContact = table.Column<string>(nullable: true),
                    Distributeur_Telephone = table.Column<string>(nullable: true),
                    Distributeur_IsActive = table.Column<int>(nullable: false),
                    Distributeur_DateSaisie = table.Column<DateTime>(nullable: false),
                    Distributeur_AbonnementID = table.Column<int>(nullable: false),
                    Distributeur_DateCreation = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Distributeur", x => x.Distributeur_ID);
                });

            migrationBuilder.CreateTable(
                name: "Echange_Produits",
                columns: table => new
                {
                    EchangeProduits_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EchangeProduits_AdminID = table.Column<string>(nullable: true),
                    EchangeProduits_PdvFournisseurID = table.Column<int>(nullable: false),
                    EchangeProduits_PdvRecepID = table.Column<int>(nullable: false),
                    EchangeProduits_Statut = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    EchangeProduits_PdvFournisseurUserID = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    EchangeProduits_PdvRecepUserID = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    EchangeProduits_DateEchange = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    EchangeProduits_AbonnementID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Echange_Produits", x => x.EchangeProduits_ID);
                    table.ForeignKey(
                        name: "FK_Echange_Produits_AspNetUsers_EchangeProduits_AdminID",
                        column: x => x.EchangeProduits_AdminID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Echange_Produits_Point_Vente_EchangeProduits_PdvFournisseurID",
                        column: x => x.EchangeProduits_PdvFournisseurID,
                        principalTable: "Point_Vente",
                        principalColumn: "PointVente_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Echange_Produits_Point_Vente_EchangeProduits_PdvRecepID",
                        column: x => x.EchangeProduits_PdvRecepID,
                        principalTable: "Point_Vente",
                        principalColumn: "PointVente_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FicheTechnique_Bridge",
                columns: table => new
                {
                    FicheTechniqueBridge_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FicheTechniqueBridge_ProduitVendableID = table.Column<int>(nullable: false),
                    FicheTechniqueBridge_Libelle = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    FicheTechniqueBridge_CoutDeRevient = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FicheTechniqueBridge_InUse = table.Column<bool>(type: "bit", nullable: false),
                    FicheTechniqueBridge_AbonnementID = table.Column<int>(type: "int", nullable: false),
                    FicheTechniqueBridge_CoutEmbalage = table.Column<decimal>(nullable: true),
                    FicheTechniqueBridge_CoutMainOeuvre = table.Column<decimal>(nullable: true),
                    FicheTechniqueBridge_IsActive = table.Column<int>(type: "int", nullable: false),
                    FicheTechniqueBridge_DateCreation = table.Column<DateTime>(type: "smalldatetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FicheTechnique_Bridge", x => x.FicheTechniqueBridge_ID);
                    table.ForeignKey(
                        name: "FK_FicheTechnique_Bridge_Produit_Vendable_FicheTechniqueBridge_ProduitVendableID",
                        column: x => x.FicheTechniqueBridge_ProduitVendableID,
                        principalTable: "Produit_Vendable",
                        principalColumn: "ProduitVendable_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fonction",
                columns: table => new
                {
                    Fonction_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Fonction_Libelle = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fonction", x => x.Fonction_ID);
                });

            migrationBuilder.CreateTable(
                name: "FournisseurMatiere",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Founisseur_Id = table.Column<int>(nullable: false),
                    MatierePremiere_Id = table.Column<int>(nullable: false),
                    Abonnement_ID = table.Column<int>(nullable: false),
                    IsActive = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FournisseurMatiere", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FournisseurMatiere_Founisseur_Founisseur_Id",
                        column: x => x.Founisseur_Id,
                        principalTable: "Founisseur",
                        principalColumn: "Founisseur_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FournisseurMatiere_Matiere_Premiere_MatierePremiere_Id",
                        column: x => x.MatierePremiere_Id,
                        principalTable: "Matiere_Premiere",
                        principalColumn: "MatierePremiere_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LogoUser",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Logo = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    Abonnement_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogoUser", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Matiere_Composants",
                columns: table => new
                {
                    MatiereComposants_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MatiereComposants_MatiereID = table.Column<int>(nullable: false),
                    MatiereComposants_Name = table.Column<string>(nullable: true),
                    MatiereComposants_NameAR = table.Column<string>(nullable: true),
                    MatiereComposants_Valeur = table.Column<string>(nullable: true),
                    MatiereComposants_IsActive = table.Column<int>(nullable: false),
                    MatiereComposants_AbonnementID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matiere_Composants", x => x.MatiereComposants_ID);
                    table.ForeignKey(
                        name: "FK_Matiere_Composants_Matiere_Premiere_MatiereComposants_MatiereID",
                        column: x => x.MatiereComposants_MatiereID,
                        principalTable: "Matiere_Premiere",
                        principalColumn: "MatierePremiere_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatiereFamille_Parent",
                columns: table => new
                {
                    MatiereFamilleParent_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MatiereFamilleParent_Libelle = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    MatiereFamilleParent_AbonnementID = table.Column<int>(type: "int", nullable: false),
                    MatiereFamilleParent_DateCreation = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    MatiereFamilleParent_IsActive = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatiereFamille_Parent", x => x.MatiereFamilleParent_ID);
                });

            migrationBuilder.CreateTable(
                name: "MatPrem_Allergene",
                columns: table => new
                {
                    MatPrem_AleergID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MatiereID = table.Column<int>(nullable: false),
                    AllergenID = table.Column<int>(nullable: false),
                    IsActive = table.Column<int>(nullable: false),
                    AbonnementID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatPrem_Allergene", x => x.MatPrem_AleergID);
                    table.ForeignKey(
                        name: "FK_MatPrem_Allergene_Allergene_AllergenID",
                        column: x => x.AllergenID,
                        principalTable: "Allergene",
                        principalColumn: "Allergene_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatPrem_Allergene_Matiere_Premiere_MatiereID",
                        column: x => x.MatiereID,
                        principalTable: "Matiere_Premiere",
                        principalColumn: "MatierePremiere_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mode_Paiement",
                columns: table => new
                {
                    ModePaiement_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ModePaiement_Libelle = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mode_Paiement", x => x.ModePaiement_Id);
                });

            migrationBuilder.CreateTable(
                name: "Mouvement_Type",
                columns: table => new
                {
                    MouvementType_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MouvementType_Libelle = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    MouvementType_AbonnementId = table.Column<int>(type: "int", nullable: false),
                    MouvementType_DateCreation = table.Column<DateTime>(type: "smalldatetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mouvement_Type", x => x.MouvementType_Id);
                });

            migrationBuilder.CreateTable(
                name: "MouvementCaisse_Type",
                columns: table => new
                {
                    TypeMouvementCaisse_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TypeMouvementCaisse_Libelle = table.Column<string>(type: "nvarchar(150)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MouvementCaisse_Type", x => x.TypeMouvementCaisse_ID);
                });

            migrationBuilder.CreateTable(
                name: "Perte_MatiereStock",
                columns: table => new
                {
                    PerteMatiereStock_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PerteMatiere_MatierePremiereStockageID = table.Column<int>(nullable: false),
                    PerteMatiere_Quantite = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    PerteMatiere_UniteMesureID = table.Column<int>(nullable: false),
                    PerteMatiere_StockID = table.Column<int>(nullable: false),
                    PerteMatiere_DateCreation = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    PerteMatiere_Utilisateur = table.Column<string>(nullable: true),
                    PerteMatiere_AbonnementID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perte_MatiereStock", x => x.PerteMatiereStock_ID);
                    table.ForeignKey(
                        name: "FK_Perte_MatiereStock_MatierePremiere_Stokage_PerteMatiere_MatierePremiereStockageID",
                        column: x => x.PerteMatiere_MatierePremiereStockageID,
                        principalTable: "MatierePremiere_Stokage",
                        principalColumn: "MatierePremiereStokage_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Perte_MatiereStock_Lieu_Stockage_PerteMatiere_StockID",
                        column: x => x.PerteMatiere_StockID,
                        principalTable: "Lieu_Stockage",
                        principalColumn: "LieuStockag_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Perte_MatiereStock_Unite_Mesure_PerteMatiere_UniteMesureID",
                        column: x => x.PerteMatiere_UniteMesureID,
                        principalTable: "Unite_Mesure",
                        principalColumn: "UniteMesure_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Perte_MatiereStock_AspNetUsers_PerteMatiere_Utilisateur",
                        column: x => x.PerteMatiere_Utilisateur,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PointVente_Famille",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PointVente_Id = table.Column<int>(nullable: false),
                    FamilleProduit_Id = table.Column<int>(nullable: false),
                    Abonnement_Id = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<int>(type: "int", nullable: false),
                    DateCreation = table.Column<DateTime>(type: "smalldatetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PointVente_Famille", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PointVente_Famille_Famille_Produit_FamilleProduit_Id",
                        column: x => x.FamilleProduit_Id,
                        principalTable: "Famille_Produit",
                        principalColumn: "FamilleProduit_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PointVente_Famille_Point_Vente_PointVente_Id",
                        column: x => x.PointVente_Id,
                        principalTable: "Point_Vente",
                        principalColumn: "PointVente_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PointVente_User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    User_Id = table.Column<string>(nullable: true),
                    PointVente_Id = table.Column<int>(nullable: false),
                    Abonnement_ID = table.Column<int>(nullable: false),
                    IsActive = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PointVente_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PointVente_User_Point_Vente_PointVente_Id",
                        column: x => x.PointVente_Id,
                        principalTable: "Point_Vente",
                        principalColumn: "PointVente_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PointVente_User_AspNetUsers_User_Id",
                        column: x => x.User_Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Produit_Composants",
                columns: table => new
                {
                    ProduitComposant_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProduitComposant_ProduitID = table.Column<int>(nullable: false),
                    ProduitComposant_ComposantName = table.Column<string>(nullable: true),
                    ProduitComposant_ComposantNameAR = table.Column<string>(nullable: true),
                    ProduitComposant_Valeur = table.Column<string>(nullable: true),
                    ProduitComposant_isActive = table.Column<int>(nullable: false),
                    ProduitComposant_AbonnementID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produit_Composants", x => x.ProduitComposant_ID);
                    table.ForeignKey(
                        name: "FK_Produit_Composants_Produit_Vendable_ProduitComposant_ProduitID",
                        column: x => x.ProduitComposant_ProduitID,
                        principalTable: "Produit_Vendable",
                        principalColumn: "ProduitVendable_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProduitBase",
                columns: table => new
                {
                    ProduitBase_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProduitBase_Designation = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    ProduitBase_FormeStockageID = table.Column<int>(nullable: false),
                    ProduitBase_UniteMesureID = table.Column<int>(nullable: false),
                    ProduitBase_CoutDeRevient = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ProduitBase_IsActive = table.Column<int>(type: "int", nullable: false),
                    ProduitBase_AbonnementID = table.Column<int>(type: "int", nullable: false),
                    ProduitBase_DateCreation = table.Column<DateTime>(type: "smalldatetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProduitBase", x => x.ProduitBase_ID);
                    table.ForeignKey(
                        name: "FK_ProduitBase_Forme_Stockage_ProduitBase_FormeStockageID",
                        column: x => x.ProduitBase_FormeStockageID,
                        principalTable: "Forme_Stockage",
                        principalColumn: "FormStockage_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProduitBase_Unite_Mesure_ProduitBase_UniteMesureID",
                        column: x => x.ProduitBase_UniteMesureID,
                        principalTable: "Unite_Mesure",
                        principalColumn: "UniteMesure_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Retrait_Type",
                columns: table => new
                {
                    RetraitType_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RetraitType_Libelle = table.Column<string>(type: "nvarchar(250)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Retrait_Type", x => x.RetraitType_ID);
                });

            migrationBuilder.CreateTable(
                name: "Sous_Famille",
                columns: table => new
                {
                    SousFamille_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SousFamille_Libelle = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    SousFamille_Image = table.Column<string>(type: "nvarchar(350)", nullable: true),
                    SousFamille_ParentID = table.Column<int>(nullable: false),
                    SousFamille_AbonnementID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sous_Famille", x => x.SousFamille_ID);
                    table.ForeignKey(
                        name: "FK_Sous_Famille_Famille_Produit_SousFamille_ParentID",
                        column: x => x.SousFamille_ParentID,
                        principalTable: "Famille_Produit",
                        principalColumn: "FamilleProduit_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Statut_BL",
                columns: table => new
                {
                    StatutBL_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StatutBL_Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statut_BL", x => x.StatutBL_ID);
                });

            migrationBuilder.CreateTable(
                name: "Statut_Livraison",
                columns: table => new
                {
                    StatutCommande_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StatutCommande_Libelle = table.Column<string>(type: "varchar(50)", nullable: true),
                    StatutCommande_AbonnementId = table.Column<int>(type: "int", nullable: false),
                    StatutCommande_DateCreation = table.Column<DateTime>(type: "smalldatetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statut_Livraison", x => x.StatutCommande_Id);
                });

            migrationBuilder.CreateTable(
                name: "Statut_PaiementCommande",
                columns: table => new
                {
                    StatutPaiementCommande_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StatutPaiementCommande_Libelle = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statut_PaiementCommande", x => x.StatutPaiementCommande_ID);
                });

            migrationBuilder.CreateTable(
                name: "Stock_Achat",
                columns: table => new
                {
                    StockAchat_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StockAchat_MatiereID = table.Column<int>(nullable: false),
                    StockAchat_UniteMesureID = table.Column<int>(nullable: false),
                    StockAchat_LotIntern = table.Column<string>(nullable: true),
                    StockAchat_LotFournisseur = table.Column<string>(nullable: true),
                    StockAchat_DateLimiteConso = table.Column<DateTime>(nullable: true),
                    StockAchat_DateReception = table.Column<DateTime>(nullable: true),
                    StockAchat_Temperature = table.Column<string>(nullable: true),
                    StockAchat_QuantiteMatiere = table.Column<decimal>(nullable: false),
                    StockAchat_QuantiteRestante = table.Column<decimal>(nullable: false),
                    StockAchat_AbonnementID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stock_Achat", x => x.StockAchat_ID);
                    table.ForeignKey(
                        name: "FK_Stock_Achat_Matiere_Premiere_StockAchat_MatiereID",
                        column: x => x.StockAchat_MatiereID,
                        principalTable: "Matiere_Premiere",
                        principalColumn: "MatierePremiere_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stock_Achat_Unite_Mesure_StockAchat_UniteMesureID",
                        column: x => x.StockAchat_UniteMesureID,
                        principalTable: "Unite_Mesure",
                        principalColumn: "UniteMesure_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stock_User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    User_Id = table.Column<string>(nullable: true),
                    Stock_Id = table.Column<int>(nullable: false),
                    Abonnement_ID = table.Column<int>(nullable: false),
                    IsActive = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stock_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stock_User_Lieu_Stockage_Stock_Id",
                        column: x => x.Stock_Id,
                        principalTable: "Lieu_Stockage",
                        principalColumn: "LieuStockag_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stock_User_AspNetUsers_User_Id",
                        column: x => x.User_Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Taux_TVA",
                columns: table => new
                {
                    TauxTVA_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TauxTVA_Pourcentage = table.Column<decimal>(type: "decimal(4, 2)", nullable: false),
                    TauxTVA_AbonnementId = table.Column<int>(nullable: false),
                    TauxTVA_DateCreation = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taux_TVA", x => x.TauxTVA_Id);
                });

            migrationBuilder.CreateTable(
                name: "Transfert_Matiere",
                columns: table => new
                {
                    TransfertMat_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TransfertMat_CreePar = table.Column<string>(nullable: true),
                    TransfertMat_Numero = table.Column<string>(nullable: true),
                    TransfertMat_Statut = table.Column<string>(nullable: true),
                    TransfertMat_ValidePar = table.Column<string>(nullable: true),
                    TransfertMat_DateCreation = table.Column<DateTime>(nullable: false),
                    TransfertMat_DateValidation = table.Column<DateTime>(nullable: true),
                    TransfertMat_AbonnementID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transfert_Matiere", x => x.TransfertMat_ID);
                });

            migrationBuilder.CreateTable(
                name: "Unite_Categorie",
                columns: table => new
                {
                    UniteCategorie_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UniteCategorie_Libelle = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unite_Categorie", x => x.UniteCategorie_Id);
                });

            migrationBuilder.CreateTable(
                name: "Unite_MesureMatiere",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Unite_Id = table.Column<int>(nullable: false),
                    MatierePremiere_Id = table.Column<int>(nullable: false),
                    Abonnement_ID = table.Column<int>(nullable: false),
                    IsActive = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unite_MesureMatiere", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Unite_MesureMatiere_Matiere_Premiere_MatierePremiere_Id",
                        column: x => x.MatierePremiere_Id,
                        principalTable: "Matiere_Premiere",
                        principalColumn: "MatierePremiere_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Unite_MesureMatiere_Unite_Mesure_Unite_Id",
                        column: x => x.Unite_Id,
                        principalTable: "Unite_Mesure",
                        principalColumn: "UniteMesure_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ville",
                columns: table => new
                {
                    Ville_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ville_Libelle = table.Column<string>(type: "nvarchar(250)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ville", x => x.Ville_ID);
                });

            migrationBuilder.CreateTable(
                name: "Article_BC",
                columns: table => new
                {
                    ArticleBC_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ArticleBC_Designation = table.Column<string>(nullable: true),
                    ArticleBC_MatiereID = table.Column<int>(nullable: false),
                    ArticleBC_PU = table.Column<decimal>(nullable: false),
                    ArticleBC_Total = table.Column<decimal>(nullable: false),
                    ArticleBC_QteRest = table.Column<decimal>(nullable: false),
                    ArticleBC_UniteMesure = table.Column<int>(nullable: false),
                    ArticleBC_Quantite = table.Column<decimal>(nullable: false),
                    ArticleBC_BCID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Article_BC", x => x.ArticleBC_ID);
                    table.ForeignKey(
                        name: "FK_Article_BC_Bon_De_Commande_ArticleBC_BCID",
                        column: x => x.ArticleBC_BCID,
                        principalTable: "Bon_De_Commande",
                        principalColumn: "BonDeCommande_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Article_BC_Matiere_Premiere_ArticleBC_MatiereID",
                        column: x => x.ArticleBC_MatiereID,
                        principalTable: "Matiere_Premiere",
                        principalColumn: "MatierePremiere_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Article_BC_Unite_Mesure_ArticleBC_UniteMesure",
                        column: x => x.ArticleBC_UniteMesure,
                        principalTable: "Unite_Mesure",
                        principalColumn: "UniteMesure_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Facture",
                columns: table => new
                {
                    Facture_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Facture_Numero = table.Column<string>(nullable: true),
                    Facture_Etat = table.Column<string>(nullable: true),
                    Facture_FournisseurID = table.Column<int>(nullable: false),
                    Facture_BonDeCommandeID = table.Column<int>(nullable: false),
                    Facture_AbonnementID = table.Column<int>(nullable: false),
                    Facture_MontantTVA = table.Column<decimal>(nullable: false),
                    Facture_TotalHT = table.Column<decimal>(nullable: false),
                    Facture_TotalTTC = table.Column<decimal>(nullable: false),
                    Facture_DateFacture = table.Column<DateTime>(nullable: false),
                    Facture_DateSaisie = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facture", x => x.Facture_ID);
                    table.ForeignKey(
                        name: "FK_Facture_Bon_De_Commande_Facture_BonDeCommandeID",
                        column: x => x.Facture_BonDeCommandeID,
                        principalTable: "Bon_De_Commande",
                        principalColumn: "BonDeCommande_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Facture_Founisseur_Facture_FournisseurID",
                        column: x => x.Facture_FournisseurID,
                        principalTable: "Founisseur",
                        principalColumn: "Founisseur_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bon_Details",
                columns: table => new
                {
                    BonDetails_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BonDeSortie_Quantite = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    BonDeSortie_QuantiteEnStock = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    BonDeSortie_QuantiteDemandee = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    BonDeSortie_QuantiteLivree = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    BonDeSortie_UniteMesureId = table.Column<int>(nullable: false),
                    BonDeSortie_MatiereId = table.Column<int>(nullable: false),
                    BonDeSortie_BonDeSortieID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bon_Details", x => x.BonDetails_ID);
                    table.ForeignKey(
                        name: "FK_Bon_Details_BonDe_Sortie_BonDeSortie_BonDeSortieID",
                        column: x => x.BonDeSortie_BonDeSortieID,
                        principalTable: "BonDe_Sortie",
                        principalColumn: "BonDeSortie_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bon_Details_Matiere_Premiere_BonDeSortie_MatiereId",
                        column: x => x.BonDeSortie_MatiereId,
                        principalTable: "Matiere_Premiere",
                        principalColumn: "MatierePremiere_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bon_Details_Unite_Mesure_BonDeSortie_UniteMesureId",
                        column: x => x.BonDeSortie_UniteMesureId,
                        principalTable: "Unite_Mesure",
                        principalColumn: "UniteMesure_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Matiere_Famille",
                columns: table => new
                {
                    MatiereFamille_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MatiereFamille_Libelle = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    MatiereFamille_ParentID = table.Column<int>(nullable: false),
                    MatiereFamille_AbonnementID = table.Column<int>(type: "int", nullable: false),
                    MatiereFamille_DateCreation = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    MatiereFamille_IsActive = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matiere_Famille", x => x.MatiereFamille_ID);
                    table.ForeignKey(
                        name: "FK_Matiere_Famille_MatiereFamille_Parent_MatiereFamille_ParentID",
                        column: x => x.MatiereFamille_ParentID,
                        principalTable: "MatiereFamille_Parent",
                        principalColumn: "MatiereFamilleParent_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Position_Vente",
                columns: table => new
                {
                    PositionVente_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PositionVente_PointVenteId = table.Column<int>(nullable: false),
                    PositionVente_Libelle = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    PositionVente_IsActive = table.Column<int>(type: "int", nullable: false),
                    PositionVente_ModePaiementId = table.Column<int>(nullable: false),
                    PositionVente_AbonnementId = table.Column<int>(type: "int", nullable: false),
                    PositionVente_UtilisateurId = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    PositionVente_DateCreation = table.Column<DateTime>(type: "smalldatetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Position_Vente", x => x.PositionVente_Id);
                    table.ForeignKey(
                        name: "FK_Position_Vente_Mode_Paiement_PositionVente_ModePaiementId",
                        column: x => x.PositionVente_ModePaiementId,
                        principalTable: "Mode_Paiement",
                        principalColumn: "ModePaiement_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Position_Vente_Point_Vente_PositionVente_PointVenteId",
                        column: x => x.PositionVente_PointVenteId,
                        principalTable: "Point_Vente",
                        principalColumn: "PointVente_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mouvement_Stock",
                columns: table => new
                {
                    MouvementStock_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MouvementStock_MatierePremiereStokageId = table.Column<int>(nullable: false),
                    MouvementStock_Date = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    MouvementStock_MouvementTypeId = table.Column<int>(nullable: false),
                    MouvementStock_Quantite = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MouvementStock_PrixAchatUnite = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MouvementStock_UniteMesureId = table.Column<int>(nullable: false),
                    MouvementStock_FournisseurId = table.Column<int>(nullable: true),
                    MouvementStock_DateSaisie = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    MouvementStock_DestinationStockId = table.Column<int>(nullable: true),
                    MouvementStock_ReceptionStatutId = table.Column<bool>(type: "bit", nullable: false),
                    MouvementStock_DateReception = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    MouvementStock_AbonnementId = table.Column<int>(type: "int", nullable: false),
                    MouvementStock_DateCreation = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    MouvementStock_MatiereQuantiteActuelle = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MouvementStock_IsActive = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mouvement_Stock", x => x.MouvementStock_Id);
                    table.ForeignKey(
                        name: "FK_Mouvement_Stock_Lieu_Stockage_MouvementStock_DestinationStockId",
                        column: x => x.MouvementStock_DestinationStockId,
                        principalTable: "Lieu_Stockage",
                        principalColumn: "LieuStockag_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mouvement_Stock_Founisseur_MouvementStock_FournisseurId",
                        column: x => x.MouvementStock_FournisseurId,
                        principalTable: "Founisseur",
                        principalColumn: "Founisseur_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mouvement_Stock_MatierePremiere_Stokage_MouvementStock_MatierePremiereStokageId",
                        column: x => x.MouvementStock_MatierePremiereStokageId,
                        principalTable: "MatierePremiere_Stokage",
                        principalColumn: "MatierePremiereStokage_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Mouvement_Stock_Mouvement_Type_MouvementStock_MouvementTypeId",
                        column: x => x.MouvementStock_MouvementTypeId,
                        principalTable: "Mouvement_Type",
                        principalColumn: "MouvementType_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Mouvement_Stock_Unite_Mesure_MouvementStock_UniteMesureId",
                        column: x => x.MouvementStock_UniteMesureId,
                        principalTable: "Unite_Mesure",
                        principalColumn: "UniteMesure_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FicheTech_ProduitBase",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FicheTech_ID = table.Column<int>(nullable: false),
                    ProduitBase_ID = table.Column<int>(nullable: false),
                    Abonnement_ID = table.Column<int>(nullable: false),
                    IsActive = table.Column<int>(nullable: false),
                    ProduitQte = table.Column<decimal>(nullable: false),
                    UniteMesure_ID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FicheTech_ProduitBase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FicheTech_ProduitBase_FicheTechnique_Bridge_FicheTech_ID",
                        column: x => x.FicheTech_ID,
                        principalTable: "FicheTechnique_Bridge",
                        principalColumn: "FicheTechniqueBridge_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FicheTech_ProduitBase_ProduitBase_ProduitBase_ID",
                        column: x => x.ProduitBase_ID,
                        principalTable: "ProduitBase",
                        principalColumn: "ProduitBase_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FicheTech_ProduitBase_Unite_Mesure_UniteMesure_ID",
                        column: x => x.UniteMesure_ID,
                        principalTable: "Unite_Mesure",
                        principalColumn: "UniteMesure_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FicheTechniqueProduitBase",
                columns: table => new
                {
                    FicheTechniqueProduitBase_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FicheTechniqueProduitBase_ProduitBaseID = table.Column<int>(nullable: false),
                    FicheTechniqueProduitBase_UniteMesureID = table.Column<int>(nullable: false),
                    FicheTechniqueProduitBase_Libelle = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    FicheTechniqueProduitBase_CoutDeRevient = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FicheTechniqueProduitBase_QuantiteProd = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FicheTechniqueProduitBase_InUse = table.Column<bool>(type: "bit", nullable: false),
                    FicheTechniqueProduitBase_AbonnementID = table.Column<int>(type: "int", nullable: false),
                    FicheTechniqueProduitBase_IsActive = table.Column<int>(type: "int", nullable: false),
                    FicheTechniqueProduitBase_DateCreation = table.Column<DateTime>(type: "smalldatetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FicheTechniqueProduitBase", x => x.FicheTechniqueProduitBase_ID);
                    table.ForeignKey(
                        name: "FK_FicheTechniqueProduitBase_ProduitBase_FicheTechniqueProduitBase_ProduitBaseID",
                        column: x => x.FicheTechniqueProduitBase_ProduitBaseID,
                        principalTable: "ProduitBase",
                        principalColumn: "ProduitBase_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FicheTechniqueProduitBase_Unite_Mesure_FicheTechniqueProduitBase_UniteMesureID",
                        column: x => x.FicheTechniqueProduitBase_UniteMesureID,
                        principalTable: "Unite_Mesure",
                        principalColumn: "UniteMesure_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UniteMesure_ProdBase",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UniteMesure_ID = table.Column<int>(nullable: false),
                    ProduitBase_ID = table.Column<int>(nullable: false),
                    isActive = table.Column<int>(nullable: false),
                    Abonnement_ID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UniteMesure_ProdBase", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UniteMesure_ProdBase_ProduitBase_ProduitBase_ID",
                        column: x => x.ProduitBase_ID,
                        principalTable: "ProduitBase",
                        principalColumn: "ProduitBase_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UniteMesure_ProdBase_Unite_Mesure_UniteMesure_ID",
                        column: x => x.UniteMesure_ID,
                        principalTable: "Unite_Mesure",
                        principalColumn: "UniteMesure_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Produit_PretConsomer",
                columns: table => new
                {
                    ProduitPretConsomer_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProduitPretConsomer_Designation = table.Column<string>(nullable: true),
                    ProduitPretConsomer_Description = table.Column<string>(nullable: true),
                    ProduitPretConsomer_FormeStockageId = table.Column<int>(nullable: false),
                    ProduitPretConsomer_FamilleProduit = table.Column<int>(nullable: false),
                    ProduitPretConsomer_UniteMesureAchatId = table.Column<int>(nullable: false),
                    ProduitPretConsomer_StockMinimun = table.Column<decimal>(nullable: false),
                    ProduitPretConsomer_StockMaximum = table.Column<decimal>(nullable: false),
                    ProduitPretConsomer_DelaiConsomation = table.Column<int>(nullable: false),
                    ProduitPretConsomer_Photo = table.Column<string>(nullable: true),
                    ProduitPretConsomer_IsActive = table.Column<int>(nullable: false),
                    ProduitPretConsomer_AbonnementID = table.Column<int>(nullable: false),
                    ProduitPretConsomer_EnStock = table.Column<bool>(nullable: false),
                    ProduitPretConsomer_DateCreation = table.Column<DateTime>(nullable: false),
                    ProduitPretConsomer_PrixMoyenAchat = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produit_PretConsomer", x => x.ProduitPretConsomer_ID);
                    table.ForeignKey(
                        name: "FK_Produit_PretConsomer_Sous_Famille_ProduitPretConsomer_FamilleProduit",
                        column: x => x.ProduitPretConsomer_FamilleProduit,
                        principalTable: "Sous_Famille",
                        principalColumn: "SousFamille_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Produit_PretConsomer_Forme_Stockage_ProduitPretConsomer_FormeStockageId",
                        column: x => x.ProduitPretConsomer_FormeStockageId,
                        principalTable: "Forme_Stockage",
                        principalColumn: "FormStockage_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Produit_PretConsomer_Unite_Mesure_ProduitPretConsomer_UniteMesureAchatId",
                        column: x => x.ProduitPretConsomer_UniteMesureAchatId,
                        principalTable: "Unite_Mesure",
                        principalColumn: "UniteMesure_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProduitPackage",
                columns: table => new
                {
                    ProduitPackage_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProduitPackage_Photo = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    ProduitPackage_UniteVente = table.Column<int>(nullable: false),
                    ProduitPackage_FamilleID = table.Column<int>(nullable: false),
                    ProduitPackage_Designation = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    ProduitPackage_Quantite = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProduitPackage_CoutdeRevient = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProduitPackage_DateCreation = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    ProduitPackage_AbonnementID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProduitPackage", x => x.ProduitPackage_ID);
                    table.ForeignKey(
                        name: "FK_ProduitPackage_Sous_Famille_ProduitPackage_FamilleID",
                        column: x => x.ProduitPackage_FamilleID,
                        principalTable: "Sous_Famille",
                        principalColumn: "SousFamille_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProduitPackage_Unite_Mesure_ProduitPackage_UniteVente",
                        column: x => x.ProduitPackage_UniteVente,
                        principalTable: "Unite_Mesure",
                        principalColumn: "UniteMesure_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Matiere_Transfert",
                columns: table => new
                {
                    MatiereTrans_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MatiereTrans_MatiereID = table.Column<int>(nullable: false),
                    MatiereTrans_Quantite = table.Column<decimal>(nullable: false),
                    MatiereTrans_UniteID = table.Column<int>(nullable: false),
                    MatiereTrans_LotNumber = table.Column<string>(nullable: true),
                    MatiereTrans_Statut = table.Column<string>(nullable: true),
                    MatiereTrans_ValidePar = table.Column<string>(nullable: true),
                    MatiereTrans_DateValidation = table.Column<DateTime>(nullable: true),
                    MatiereTrans_TransferID = table.Column<int>(nullable: false),
                    MatiereTrans_StockID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matiere_Transfert", x => x.MatiereTrans_ID);
                    table.ForeignKey(
                        name: "FK_Matiere_Transfert_Matiere_Premiere_MatiereTrans_MatiereID",
                        column: x => x.MatiereTrans_MatiereID,
                        principalTable: "Matiere_Premiere",
                        principalColumn: "MatierePremiere_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Matiere_Transfert_Lieu_Stockage_MatiereTrans_StockID",
                        column: x => x.MatiereTrans_StockID,
                        principalTable: "Lieu_Stockage",
                        principalColumn: "LieuStockag_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Matiere_Transfert_Transfert_Matiere_MatiereTrans_TransferID",
                        column: x => x.MatiereTrans_TransferID,
                        principalTable: "Transfert_Matiere",
                        principalColumn: "TransfertMat_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Matiere_Transfert_Unite_Mesure_MatiereTrans_UniteID",
                        column: x => x.MatiereTrans_UniteID,
                        principalTable: "Unite_Mesure",
                        principalColumn: "UniteMesure_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Atelier",
                columns: table => new
                {
                    Atelier_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Atelier_Nom = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    Atelier_Adresse = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    Atelier_NomResponsable = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    Atelier_Telephone = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Atelier_IsActive = table.Column<int>(type: "int", nullable: false),
                    Atelier_UTILISATEUR = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    Atelier_AbonnementID = table.Column<int>(type: "int", nullable: false),
                    Atelier_DateCreation = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    Atelier_VilleID = table.Column<int>(nullable: false),
                    Atelier_CodePostal = table.Column<int>(type: "int", nullable: false),
                    Atelier_StockID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atelier", x => x.Atelier_ID);
                    table.ForeignKey(
                        name: "FK_Atelier_Lieu_Stockage_Atelier_StockID",
                        column: x => x.Atelier_StockID,
                        principalTable: "Lieu_Stockage",
                        principalColumn: "LieuStockag_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Atelier_Ville_Atelier_VilleID",
                        column: x => x.Atelier_VilleID,
                        principalTable: "Ville",
                        principalColumn: "Ville_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fournisseur_Produits",
                columns: table => new
                {
                    FournisseurProduitConso_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FournisseurProduitConso_RaisonSocial = table.Column<string>(nullable: true),
                    FournisseurProduitConso_ICE = table.Column<string>(nullable: true),
                    FournisseurProduitConso_Adresse = table.Column<string>(nullable: true),
                    FournisseurProduitConso_NomContact = table.Column<string>(nullable: true),
                    FournisseurProduitConso_TelephoneContact = table.Column<string>(nullable: true),
                    FournisseurProduitConso_DateCreation = table.Column<DateTime>(nullable: false),
                    FournisseurProduitConso_IsActive = table.Column<int>(nullable: false),
                    FournisseurProduitConso_AbonnementID = table.Column<int>(nullable: false),
                    FournisseurProduitConso_UtilisateurId = table.Column<string>(nullable: true),
                    FournisseurProduitConso_VilleID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fournisseur_Produits", x => x.FournisseurProduitConso_Id);
                    table.ForeignKey(
                        name: "FK_Fournisseur_Produits_Ville_FournisseurProduitConso_VilleID",
                        column: x => x.FournisseurProduitConso_VilleID,
                        principalTable: "Ville",
                        principalColumn: "Ville_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bon_De_Livraison",
                columns: table => new
                {
                    BonDeLivraison_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BonDeLivraison_BCID = table.Column<int>(nullable: false),
                    BonDeLivraison_Designation = table.Column<string>(nullable: true),
                    BonDeLivraison_AbonnementID = table.Column<int>(nullable: false),
                    BonDeLivraison_FactureID = table.Column<int>(nullable: true),
                    BonDeLivraison_StatutID = table.Column<int>(nullable: true),
                    BonDeLivraison_TotalHT = table.Column<decimal>(nullable: false),
                    BonDeLivraison_TotalTVA = table.Column<decimal>(nullable: false),
                    BonDeLivraison_TotalTTC = table.Column<decimal>(nullable: false),
                    BonDeLivraison_DateLivraison = table.Column<DateTime>(nullable: false),
                    BonDeLivraison_DateSaisie = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bon_De_Livraison", x => x.BonDeLivraison_ID);
                    table.ForeignKey(
                        name: "FK_Bon_De_Livraison_Abonnement_Client_BonDeLivraison_AbonnementID",
                        column: x => x.BonDeLivraison_AbonnementID,
                        principalTable: "Abonnement_Client",
                        principalColumn: "Abonnement_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bon_De_Livraison_Bon_De_Commande_BonDeLivraison_BCID",
                        column: x => x.BonDeLivraison_BCID,
                        principalTable: "Bon_De_Commande",
                        principalColumn: "BonDeCommande_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bon_De_Livraison_Facture_BonDeLivraison_FactureID",
                        column: x => x.BonDeLivraison_FactureID,
                        principalTable: "Facture",
                        principalColumn: "Facture_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bon_De_Livraison_Statut_BL_BonDeLivraison_StatutID",
                        column: x => x.BonDeLivraison_StatutID,
                        principalTable: "Statut_BL",
                        principalColumn: "StatutBL_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payement_Facture",
                columns: table => new
                {
                    PayementFacture_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PayementFacture_FactureID = table.Column<int>(nullable: false),
                    PayementFacture_Montant = table.Column<decimal>(nullable: false),
                    PayementFacture_Methode = table.Column<string>(nullable: true),
                    PayementFacture_Informations = table.Column<string>(nullable: true),
                    PayementFacture_DateEcheance = table.Column<DateTime>(nullable: true),
                    PayementFacture_DatePayement = table.Column<DateTime>(nullable: true),
                    PayementFacture_DateSaisie = table.Column<DateTime>(nullable: true),
                    PayementFacture_AbonnementID = table.Column<int>(nullable: false),
                    PayementFacture_CreePar = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payement_Facture", x => x.PayementFacture_ID);
                    table.ForeignKey(
                        name: "FK_Payement_Facture_Facture_PayementFacture_FactureID",
                        column: x => x.PayementFacture_FactureID,
                        principalTable: "Facture",
                        principalColumn: "Facture_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Allimentation_Caisse",
                columns: table => new
                {
                    AllimentationCaisse_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AllimentationCaisse_UtilsateurID = table.Column<string>(nullable: true),
                    AllimentationCaisse_Montant = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AllimentationCaisse_PointVenteID = table.Column<int>(type: "int", nullable: false),
                    AllimentationCaisse_PositionVenteID = table.Column<int>(nullable: false),
                    AllimentationCaisse_AbonnementID = table.Column<int>(type: "int", nullable: false),
                    AllimentationCaisse_FlagCloture = table.Column<int>(type: "int", nullable: false),
                    AllimentationCaisse_DateCreation = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Allimentation_Caisse", x => x.AllimentationCaisse_ID);
                    table.ForeignKey(
                        name: "FK_Allimentation_Caisse_Position_Vente_AllimentationCaisse_PositionVenteID",
                        column: x => x.AllimentationCaisse_PositionVenteID,
                        principalTable: "Position_Vente",
                        principalColumn: "PositionVente_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Allimentation_Caisse_AspNetUsers_AllimentationCaisse_UtilsateurID",
                        column: x => x.AllimentationCaisse_UtilsateurID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cloture_Journee",
                columns: table => new
                {
                    ClotueJournee_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClotueJournee_PositionVenteID = table.Column<int>(nullable: false),
                    ClotueJournee_SoldeDebit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClotueJournee_SoldeCredit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClotueJournee_Alimentation = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClotueJournee_Montant = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClotueJournee_Description = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    ClotueJournee_UtilisateurID = table.Column<string>(nullable: true),
                    ClotueJournee_DateCreation = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    ClotueJournee_AbonnementID = table.Column<int>(type: "int", nullable: false),
                    ClotueJournee_Valide = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cloture_Journee", x => x.ClotueJournee_ID);
                    table.ForeignKey(
                        name: "FK_Cloture_Journee_Position_Vente_ClotueJournee_PositionVenteID",
                        column: x => x.ClotueJournee_PositionVenteID,
                        principalTable: "Position_Vente",
                        principalColumn: "PositionVente_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cloture_Journee_AspNetUsers_ClotueJournee_UtilisateurID",
                        column: x => x.ClotueJournee_UtilisateurID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Commande",
                columns: table => new
                {
                    Commande_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Commande_Date = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    Commande_NomDemandeurs = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    Commande_NumeroTel = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    Commande_PointVenteId = table.Column<int>(nullable: true),
                    Commande_DateLivraisonPrevue = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    Commande_EtatDePaiement = table.Column<int>(nullable: false),
                    Commande_MontantTotal = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    Commande_TauxdeRemise = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    Commande_MontantSansRemise = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    Commande_Avance = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    Commande_DateCreation = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    Commande_EtatDeLivraison = table.Column<int>(nullable: false),
                    Commande_UtilisateurCommandeId = table.Column<string>(nullable: true),
                    Commande_UtilisateurLivraisonId = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    Commande_NumeroTicket = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    Commande_CaissePayementId = table.Column<int>(nullable: true),
                    Commande_CaisseCommandeId = table.Column<int>(type: "int", nullable: true),
                    Commande_AbonnementId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commande", x => x.Commande_Id);
                    table.ForeignKey(
                        name: "FK_Commande_Position_Vente_Commande_CaissePayementId",
                        column: x => x.Commande_CaissePayementId,
                        principalTable: "Position_Vente",
                        principalColumn: "PositionVente_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Commande_Statut_Livraison_Commande_EtatDeLivraison",
                        column: x => x.Commande_EtatDeLivraison,
                        principalTable: "Statut_Livraison",
                        principalColumn: "StatutCommande_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Commande_Statut_PaiementCommande_Commande_EtatDePaiement",
                        column: x => x.Commande_EtatDePaiement,
                        principalTable: "Statut_PaiementCommande",
                        principalColumn: "StatutPaiementCommande_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Commande_AspNetUsers_Commande_UtilisateurCommandeId",
                        column: x => x.Commande_UtilisateurCommandeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Gratuite",
                columns: table => new
                {
                    Gratuite_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Gratuite_CoutDeRevientTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Gratuite_DateCreation = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    Gratuite_PositionVente = table.Column<int>(nullable: false),
                    Gratuite_AbonnementID = table.Column<int>(type: "int", nullable: false),
                    Gratuite_UtilisateurID = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gratuite", x => x.Gratuite_ID);
                    table.ForeignKey(
                        name: "FK_Gratuite_Position_Vente_Gratuite_PositionVente",
                        column: x => x.Gratuite_PositionVente,
                        principalTable: "Position_Vente",
                        principalColumn: "PositionVente_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mouvements_Caisse",
                columns: table => new
                {
                    MouvementsCaisse_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MouvementsCaisse_TypeID = table.Column<int>(nullable: false),
                    MouvementsCaisse_Montant = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    MouvementsCaisse_DateMouvement = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    MouvementsCaisse_PositionVenteID = table.Column<int>(nullable: false),
                    MouvementsCaisse_UtilisateurID = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    MouvementsCaisse_AbonnementID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mouvements_Caisse", x => x.MouvementsCaisse_ID);
                    table.ForeignKey(
                        name: "FK_Mouvements_Caisse_Position_Vente_MouvementsCaisse_PositionVenteID",
                        column: x => x.MouvementsCaisse_PositionVenteID,
                        principalTable: "Position_Vente",
                        principalColumn: "PositionVente_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Mouvements_Caisse_MouvementCaisse_Type_MouvementsCaisse_TypeID",
                        column: x => x.MouvementsCaisse_TypeID,
                        principalTable: "MouvementCaisse_Type",
                        principalColumn: "TypeMouvementCaisse_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Perte",
                columns: table => new
                {
                    Perte_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Perte_DatePerte = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    Perte_ValeurTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Perte_AbonnementID = table.Column<int>(type: "int", nullable: false),
                    Perte_PositionVenteID = table.Column<int>(nullable: false),
                    Perte_UtilisateurID = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Perte_DateCreation = table.Column<DateTime>(type: "smalldatetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perte", x => x.Perte_ID);
                    table.ForeignKey(
                        name: "FK_Perte_Position_Vente_Perte_PositionVenteID",
                        column: x => x.Perte_PositionVenteID,
                        principalTable: "Position_Vente",
                        principalColumn: "PositionVente_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Retour_Produits",
                columns: table => new
                {
                    Retour_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Retour_PositionVenteID = table.Column<int>(nullable: false),
                    Retour_PrixTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Retour_DateCreation = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    Retour_UtilisateurID = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    Retour_AbonnementID = table.Column<int>(type: "int", nullable: false),
                    Retour_FlagCloture = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Retour_Produits", x => x.Retour_Id);
                    table.ForeignKey(
                        name: "FK_Retour_Produits_Position_Vente_Retour_PositionVenteID",
                        column: x => x.Retour_PositionVenteID,
                        principalTable: "Position_Vente",
                        principalColumn: "PositionVente_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Retrait_Caisse",
                columns: table => new
                {
                    RetraitCaisse_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RetraitCaisse_TypeRetraitID = table.Column<int>(nullable: false),
                    RetraitCaisse_Motif = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RetraitCaisse_Montant = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RetraitCaisse_AbonnementID = table.Column<int>(type: "int", nullable: false),
                    RetraitCaisse_PositionVenteID = table.Column<int>(nullable: false),
                    RetraitCaisse_UtilisateurID = table.Column<string>(nullable: true),
                    RetraitCaisse_DateCreation = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    RetraitCaisse_FlagCloture = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Retrait_Caisse", x => x.RetraitCaisse_ID);
                    table.ForeignKey(
                        name: "FK_Retrait_Caisse_Position_Vente_RetraitCaisse_PositionVenteID",
                        column: x => x.RetraitCaisse_PositionVenteID,
                        principalTable: "Position_Vente",
                        principalColumn: "PositionVente_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Retrait_Caisse_Retrait_Type_RetraitCaisse_TypeRetraitID",
                        column: x => x.RetraitCaisse_TypeRetraitID,
                        principalTable: "Retrait_Type",
                        principalColumn: "RetraitType_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Retrait_Caisse_AspNetUsers_RetraitCaisse_UtilisateurID",
                        column: x => x.RetraitCaisse_UtilisateurID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Salle",
                columns: table => new
                {
                    Salle_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Salle_Libelle = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Salle_PositionVenteId = table.Column<int>(nullable: false),
                    Salle_UtilisateurId = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    Salle_AbonnementId = table.Column<int>(nullable: false),
                    Salle_DateCreation = table.Column<DateTime>(type: "smalldatetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salle", x => x.Salle_Id);
                    table.ForeignKey(
                        name: "FK_Salle_Position_Vente_Salle_PositionVenteId",
                        column: x => x.Salle_PositionVenteId,
                        principalTable: "Position_Vente",
                        principalColumn: "PositionVente_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vente",
                columns: table => new
                {
                    Vente_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Vente_PointVenteId = table.Column<int>(nullable: false),
                    Vente_PositionVenteId = table.Column<int>(nullable: false),
                    Vente_Date = table.Column<DateTime>(nullable: false),
                    Vente_CommandeId = table.Column<int>(nullable: true),
                    Vente_ProduitVendableId = table.Column<int>(nullable: true),
                    Vente_Quantite = table.Column<decimal>(nullable: false),
                    Vente_Prix = table.Column<decimal>(nullable: false),
                    Vente_TauxDeRemise = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Vente_PrixTotalRemise = table.Column<decimal>(nullable: false),
                    Vente_PrixId = table.Column<int>(nullable: true),
                    Vente_Marge = table.Column<decimal>(nullable: false),
                    Vente_AbonnementId = table.Column<int>(nullable: false),
                    Vente_ModePaiement = table.Column<int>(nullable: false),
                    Vente_Commentaire = table.Column<string>(nullable: true),
                    Vente_DateCreation = table.Column<DateTime>(nullable: false),
                    Vente_UtilisateurId = table.Column<string>(nullable: true),
                    Vente_NumeroTicket = table.Column<string>(type: "nvarchar(150)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vente", x => x.Vente_Id);
                    table.ForeignKey(
                        name: "FK_Vente_Mode_Paiement_Vente_ModePaiement",
                        column: x => x.Vente_ModePaiement,
                        principalTable: "Mode_Paiement",
                        principalColumn: "ModePaiement_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vente_Point_Vente_Vente_PointVenteId",
                        column: x => x.Vente_PointVenteId,
                        principalTable: "Point_Vente",
                        principalColumn: "PointVente_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vente_Position_Vente_Vente_PositionVenteId",
                        column: x => x.Vente_PositionVenteId,
                        principalTable: "Position_Vente",
                        principalColumn: "PositionVente_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProduitBaseFicheTechnique",
                columns: table => new
                {
                    ProduitBaseFicheTechnique_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProduitBaseFicheTechnique_MatierePremiereID = table.Column<int>(nullable: false),
                    ProduitBaseFicheTechnique_UniteMesureID = table.Column<int>(nullable: false),
                    ProduitBaseFicheTechnique_QuantiteMatiere = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    ProduitBaseFicheTechnique_Prix = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    ProduitBaseFicheTechnique_AbonnementId = table.Column<int>(type: "int", nullable: false),
                    ProduitBaseFicheTechnique_DateCreation = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    ProduitBaseFicheTechnique_FicheTehcniqueProduitBaseID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProduitBaseFicheTechnique", x => x.ProduitBaseFicheTechnique_ID);
                    table.ForeignKey(
                        name: "FK_ProduitBaseFicheTechnique_FicheTechniqueProduitBase_ProduitBaseFicheTechnique_FicheTehcniqueProduitBaseID",
                        column: x => x.ProduitBaseFicheTechnique_FicheTehcniqueProduitBaseID,
                        principalTable: "FicheTechniqueProduitBase",
                        principalColumn: "FicheTechniqueProduitBase_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProduitBaseFicheTechnique_Matiere_Premiere_ProduitBaseFicheTechnique_MatierePremiereID",
                        column: x => x.ProduitBaseFicheTechnique_MatierePremiereID,
                        principalTable: "Matiere_Premiere",
                        principalColumn: "MatierePremiere_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProduitBaseFicheTechnique_Unite_Mesure_ProduitBaseFicheTechnique_UniteMesureID",
                        column: x => x.ProduitBaseFicheTechnique_UniteMesureID,
                        principalTable: "Unite_Mesure",
                        principalColumn: "UniteMesure_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Forme_Produit",
                columns: table => new
                {
                    FormeProduit_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FormeProduit_ProduitID = table.Column<int>(nullable: true),
                    FormeProduit_ProduitPackageId = table.Column<int>(nullable: true),
                    FormeProduit_ProduitPretId = table.Column<int>(nullable: true),
                    FormeProduit_Libelle = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    FormeProduit_CoutDeRevient = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FormeProduit_PrixVente = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FormeProduit_DateCreation = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    FormeProduit_AbonnementID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forme_Produit", x => x.FormeProduit_ID);
                    table.ForeignKey(
                        name: "FK_Forme_Produit_Produit_Vendable_FormeProduit_ProduitID",
                        column: x => x.FormeProduit_ProduitID,
                        principalTable: "Produit_Vendable",
                        principalColumn: "ProduitVendable_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Forme_Produit_ProduitPackage_FormeProduit_ProduitPackageId",
                        column: x => x.FormeProduit_ProduitPackageId,
                        principalTable: "ProduitPackage",
                        principalColumn: "ProduitPackage_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Forme_Produit_Produit_PretConsomer_FormeProduit_ProduitPretId",
                        column: x => x.FormeProduit_ProduitPretId,
                        principalTable: "Produit_PretConsomer",
                        principalColumn: "ProduitPretConsomer_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SousMatierePackage",
                columns: table => new
                {
                    SousMatierePackage_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SousMatierePackage_MatiereID = table.Column<int>(nullable: false),
                    SousMatierePackage_ProduitPackageID = table.Column<int>(nullable: false),
                    SousMatierePackage_AbonnementID = table.Column<int>(type: "int", nullable: false),
                    SousMatierePackage_DateCreation = table.Column<DateTime>(type: "smalldatetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SousMatierePackage", x => x.SousMatierePackage_ID);
                    table.ForeignKey(
                        name: "FK_SousMatierePackage_Matiere_Premiere_SousMatierePackage_MatiereID",
                        column: x => x.SousMatierePackage_MatiereID,
                        principalTable: "Matiere_Premiere",
                        principalColumn: "MatierePremiere_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SousMatierePackage_ProduitPackage_SousMatierePackage_ProduitPackageID",
                        column: x => x.SousMatierePackage_ProduitPackageID,
                        principalTable: "ProduitPackage",
                        principalColumn: "ProduitPackage_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Approvisionnement_Matiere",
                columns: table => new
                {
                    ApprovisionnementMatiere_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApprovisionnementMatiere_MatiereStockID = table.Column<int>(nullable: false),
                    ApprovisionnementMatiere_AtelierID = table.Column<int>(nullable: false),
                    ApprovisionnementMatiere_StockID = table.Column<int>(nullable: false),
                    ApprovisionnementMatiere_UniteID = table.Column<int>(nullable: false),
                    ApprovisionnementMatiere_Quantite = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    ApprovisionnementMatiere_DateCreation = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    ApprovisionnementMatiere_DateApprovisionnement = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    ApprovisionnementMatiere_AbonnementID = table.Column<int>(nullable: false),
                    ApprovisionnementMatiere_Etat = table.Column<string>(nullable: true),
                    ApprovisionnementMatiere_Utilisateur = table.Column<string>(nullable: true),
                    ApprovisionnementMatiere_ValidéPar = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Approvisionnement_Matiere", x => x.ApprovisionnementMatiere_ID);
                    table.ForeignKey(
                        name: "FK_Approvisionnement_Matiere_Atelier_ApprovisionnementMatiere_AtelierID",
                        column: x => x.ApprovisionnementMatiere_AtelierID,
                        principalTable: "Atelier",
                        principalColumn: "Atelier_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Approvisionnement_Matiere_MatierePremiere_Stokage_ApprovisionnementMatiere_MatiereStockID",
                        column: x => x.ApprovisionnementMatiere_MatiereStockID,
                        principalTable: "MatierePremiere_Stokage",
                        principalColumn: "MatierePremiereStokage_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Approvisionnement_Matiere_Lieu_Stockage_ApprovisionnementMatiere_StockID",
                        column: x => x.ApprovisionnementMatiere_StockID,
                        principalTable: "Lieu_Stockage",
                        principalColumn: "LieuStockag_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Approvisionnement_Matiere_Unite_Mesure_ApprovisionnementMatiere_UniteID",
                        column: x => x.ApprovisionnementMatiere_UniteID,
                        principalTable: "Unite_Mesure",
                        principalColumn: "UniteMesure_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Atelier_User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    User_Id = table.Column<string>(nullable: true),
                    Atelier_Id = table.Column<int>(nullable: false),
                    Abonnement_ID = table.Column<int>(nullable: false),
                    IsActive = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atelier_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Atelier_User_Atelier_Atelier_Id",
                        column: x => x.Atelier_Id,
                        principalTable: "Atelier",
                        principalColumn: "Atelier_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Atelier_User_AspNetUsers_User_Id",
                        column: x => x.User_Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Demande_Pret",
                columns: table => new
                {
                    DemandePret_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DemandePret_AtelierID = table.Column<int>(nullable: false),
                    DemandePret_StockID = table.Column<int>(nullable: false),
                    DemandePret_AbonnementID = table.Column<int>(type: "int", nullable: false),
                    DemandePret_Etat = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    DemandePret_DateCreation = table.Column<DateTime>(type: "smalldatetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Demande_Pret", x => x.DemandePret_ID);
                    table.ForeignKey(
                        name: "FK_Demande_Pret_Atelier_DemandePret_AtelierID",
                        column: x => x.DemandePret_AtelierID,
                        principalTable: "Atelier",
                        principalColumn: "Atelier_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Demande_Pret_Lieu_Stockage_DemandePret_StockID",
                        column: x => x.DemandePret_StockID,
                        principalTable: "Lieu_Stockage",
                        principalColumn: "LieuStockag_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "matiereStock_Atelier",
                columns: table => new
                {
                    MatiereStockAtelier_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MatiereStockAtelier_AtelierID = table.Column<int>(nullable: false),
                    MatiereStockAtelier_MatierePremiereID = table.Column<int>(nullable: false),
                    MatiereStockAtelier_QauntiteActuelle = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    MatiereStockAtelier_QuatiteAvecPlanification = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    MatiereStockAtelier_DateCreation = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    MatiereStockAtelier_AbonnementID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_matiereStock_Atelier", x => x.MatiereStockAtelier_ID);
                    table.ForeignKey(
                        name: "FK_matiereStock_Atelier_Atelier_MatiereStockAtelier_AtelierID",
                        column: x => x.MatiereStockAtelier_AtelierID,
                        principalTable: "Atelier",
                        principalColumn: "Atelier_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_matiereStock_Atelier_Matiere_Premiere_MatiereStockAtelier_MatierePremiereID",
                        column: x => x.MatiereStockAtelier_MatierePremiereID,
                        principalTable: "Matiere_Premiere",
                        principalColumn: "MatierePremiere_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Planification_Journee",
                columns: table => new
                {
                    PlanificationJournee_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Planification_GeneratedID = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    PlanificationJournee_Date = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    PlanificationJournee_Etat = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    PlanificationJournee_CoutDeRevient = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PlanificationJournee_DateCreation = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    PlanificationJournee_AbonnementID = table.Column<int>(type: "int", nullable: false),
                    PlanificationJournee_BonDeSortieID = table.Column<int>(nullable: false),
                    PlanificationJournee_AtelierID = table.Column<int>(nullable: true),
                    PlanificationJournee_LieuStockageID = table.Column<int>(nullable: true),
                    PlanificationJournee_UtilisateurId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PlanificationJournee_ValidePar = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PlanificationJournee_SeenByStock = table.Column<bool>(type: "bit", nullable: true),
                    PlanificationJournee_SeenByAtelier = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planification_Journee", x => x.PlanificationJournee_ID);
                    table.ForeignKey(
                        name: "FK_Planification_Journee_Atelier_PlanificationJournee_AtelierID",
                        column: x => x.PlanificationJournee_AtelierID,
                        principalTable: "Atelier",
                        principalColumn: "Atelier_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Planification_Journee_BonDe_Sortie_PlanificationJournee_BonDeSortieID",
                        column: x => x.PlanificationJournee_BonDeSortieID,
                        principalTable: "BonDe_Sortie",
                        principalColumn: "BonDeSortie_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Planification_Journee_Lieu_Stockage_PlanificationJournee_LieuStockageID",
                        column: x => x.PlanificationJournee_LieuStockageID,
                        principalTable: "Lieu_Stockage",
                        principalColumn: "LieuStockag_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlanificationJourneeBase",
                columns: table => new
                {
                    PlanificationJourneeBase_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PlanificationJourneeBase_Date = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    PlanificationJourneeBase_Etat = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    PlanificationJourneeBase_CoutDeRevient = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PlanificationJourneeBase_DateCreation = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    PlanificationJourneeBase_AbonnementID = table.Column<int>(type: "int", nullable: false),
                    PlanificationJourneeBase_BonDeSortieID = table.Column<int>(nullable: false),
                    PlanificationJourneeBase_AtelierID = table.Column<int>(nullable: true),
                    PlanificationJourneeBase_LieuStockageID = table.Column<int>(nullable: true),
                    PlanificationJourneeBase_UtilisateurId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PlanificationJourneeBase_ValidePar = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PlanificationJourneeBase_SeenByStock = table.Column<bool>(type: "bit", nullable: true),
                    PlanificationJourneeBase_SeenByAtelier = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanificationJourneeBase", x => x.PlanificationJourneeBase_ID);
                    table.ForeignKey(
                        name: "FK_PlanificationJourneeBase_Atelier_PlanificationJourneeBase_AtelierID",
                        column: x => x.PlanificationJourneeBase_AtelierID,
                        principalTable: "Atelier",
                        principalColumn: "Atelier_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlanificationJourneeBase_BonDe_Sortie_PlanificationJourneeBase_BonDeSortieID",
                        column: x => x.PlanificationJourneeBase_BonDeSortieID,
                        principalTable: "BonDe_Sortie",
                        principalColumn: "BonDeSortie_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlanificationJourneeBase_Lieu_Stockage_PlanificationJourneeBase_LieuStockageID",
                        column: x => x.PlanificationJourneeBase_LieuStockageID,
                        principalTable: "Lieu_Stockage",
                        principalColumn: "LieuStockag_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PointProduction_Famille",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PointProduction_ID = table.Column<int>(nullable: false),
                    FamilleProduit_ID = table.Column<int>(nullable: false),
                    Abonnement_Id = table.Column<int>(type: "int", nullable: false),
                    Is_Active = table.Column<int>(type: "int", nullable: false),
                    DateCreation = table.Column<DateTime>(type: "smalldatetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PointProduction_Famille", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PointProduction_Famille_Famille_Produit_FamilleProduit_ID",
                        column: x => x.FamilleProduit_ID,
                        principalTable: "Famille_Produit",
                        principalColumn: "FamilleProduit_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PointProduction_Famille_Atelier_PointProduction_ID",
                        column: x => x.PointProduction_ID,
                        principalTable: "Atelier",
                        principalColumn: "Atelier_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProdBase_Atelier",
                columns: table => new
                {
                    ProdBase_Atelier_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProdBase_Atelier_ProduitID = table.Column<int>(nullable: false),
                    ProdBase_Atelier_QuantiteProduite = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    ProdBase_Atelier_AbonnementID = table.Column<int>(type: "int", nullable: false),
                    ProdBase_Atelier_DateCreation = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    ProdBase_Atelier_DateModification = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    ProdBase_Atelier_AtelierID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdBase_Atelier", x => x.ProdBase_Atelier_Id);
                    table.ForeignKey(
                        name: "FK_ProdBase_Atelier_Atelier_ProdBase_Atelier_AtelierID",
                        column: x => x.ProdBase_Atelier_AtelierID,
                        principalTable: "Atelier",
                        principalColumn: "Atelier_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProdBase_Atelier_ProduitBase_ProdBase_Atelier_ProduitID",
                        column: x => x.ProdBase_Atelier_ProduitID,
                        principalTable: "ProduitBase",
                        principalColumn: "ProduitBase_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reception_Stock",
                columns: table => new
                {
                    ReceptionStock_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ReceptionStock_AtelierID = table.Column<int>(nullable: false),
                    ReceptionStock_StockID = table.Column<int>(nullable: false),
                    ReceptionStock_MatiereID = table.Column<int>(nullable: false),
                    ReceptionStock_UniteID = table.Column<int>(nullable: false),
                    ReceptionStock_Quantite = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    ReceptionStock_DateReception = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    ReceptionStock_DateCreation = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    ReceptionStock_AbonnementID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reception_Stock", x => x.ReceptionStock_ID);
                    table.ForeignKey(
                        name: "FK_Reception_Stock_Atelier_ReceptionStock_AtelierID",
                        column: x => x.ReceptionStock_AtelierID,
                        principalTable: "Atelier",
                        principalColumn: "Atelier_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reception_Stock_MatierePremiere_Stokage_ReceptionStock_MatiereID",
                        column: x => x.ReceptionStock_MatiereID,
                        principalTable: "MatierePremiere_Stokage",
                        principalColumn: "MatierePremiereStokage_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reception_Stock_Lieu_Stockage_ReceptionStock_StockID",
                        column: x => x.ReceptionStock_StockID,
                        principalTable: "Lieu_Stockage",
                        principalColumn: "LieuStockag_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reception_Stock_Unite_Mesure_ReceptionStock_UniteID",
                        column: x => x.ReceptionStock_UniteID,
                        principalTable: "Unite_Mesure",
                        principalColumn: "UniteMesure_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fournisseur_Contact",
                columns: table => new
                {
                    FournisseurContact_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FournisseurContact_Nom = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    FournisseurContact_FonctionID = table.Column<int>(nullable: false),
                    FournisseurContact_Email = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    FournisseurContact_GSM = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    FournisseurContact_FournisseurID = table.Column<int>(nullable: true),
                    FournisseurContact_FournisseurProduitID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fournisseur_Contact", x => x.FournisseurContact_ID);
                    table.ForeignKey(
                        name: "FK_Fournisseur_Contact_Fonction_FournisseurContact_FonctionID",
                        column: x => x.FournisseurContact_FonctionID,
                        principalTable: "Fonction",
                        principalColumn: "Fonction_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Fournisseur_Contact_Founisseur_FournisseurContact_FournisseurID",
                        column: x => x.FournisseurContact_FournisseurID,
                        principalTable: "Founisseur",
                        principalColumn: "Founisseur_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Fournisseur_Contact_Fournisseur_Produits_FournisseurContact_FournisseurProduitID",
                        column: x => x.FournisseurContact_FournisseurProduitID,
                        principalTable: "Fournisseur_Produits",
                        principalColumn: "FournisseurProduitConso_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Fournisseur_ProduitConso",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FournisseurProduits_Id = table.Column<int>(nullable: false),
                    ProduitConsomable_Id = table.Column<int>(nullable: false),
                    Abonnement_Id = table.Column<int>(nullable: false),
                    IsActive = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fournisseur_ProduitConso", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fournisseur_ProduitConso_Fournisseur_Produits_FournisseurProduits_Id",
                        column: x => x.FournisseurProduits_Id,
                        principalTable: "Fournisseur_Produits",
                        principalColumn: "FournisseurProduitConso_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Fournisseur_ProduitConso_Produit_PretConsomer_ProduitConsomable_Id",
                        column: x => x.ProduitConsomable_Id,
                        principalTable: "Produit_PretConsomer",
                        principalColumn: "ProduitPretConsomer_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Article_BL",
                columns: table => new
                {
                    ArticleBL_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ArticleBL_Designation = table.Column<string>(nullable: true),
                    ArticleBL_LotFournisseur = table.Column<string>(nullable: true),
                    ArticleBL_Teemperature = table.Column<string>(nullable: true),
                    ArticleBL_LotTemp = table.Column<string>(nullable: true),
                    ArticleBL_MatiereID = table.Column<int>(nullable: false),
                    ArticleBL_UniteMesureID = table.Column<int>(nullable: false),
                    ArticleBL_Quantie = table.Column<decimal>(nullable: false),
                    ArticleBL_QuantieRes = table.Column<decimal>(nullable: false),
                    ArticleBL_PU = table.Column<decimal>(nullable: false),
                    ArticleBL_PrixTotal = table.Column<decimal>(nullable: false),
                    ArticleBL_BonLivraisonID = table.Column<int>(nullable: false),
                    ArticleBL_DateReception = table.Column<DateTime>(nullable: true),
                    ArticleBL_DateProduction = table.Column<DateTime>(nullable: true),
                    ArticleBL_DateLimiteConso = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Article_BL", x => x.ArticleBL_ID);
                    table.ForeignKey(
                        name: "FK_Article_BL_Bon_De_Livraison_ArticleBL_BonLivraisonID",
                        column: x => x.ArticleBL_BonLivraisonID,
                        principalTable: "Bon_De_Livraison",
                        principalColumn: "BonDeLivraison_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Article_BL_Matiere_Premiere_ArticleBL_MatiereID",
                        column: x => x.ArticleBL_MatiereID,
                        principalTable: "Matiere_Premiere",
                        principalColumn: "MatierePremiere_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Article_BL_Unite_Mesure_ArticleBL_UniteMesureID",
                        column: x => x.ArticleBL_UniteMesureID,
                        principalTable: "Unite_Mesure",
                        principalColumn: "UniteMesure_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Commande_Paiement",
                columns: table => new
                {
                    CommandePaiement_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CommandePaiement_CommandeID = table.Column<int>(nullable: false),
                    CommandePaiement_ModePaiementID = table.Column<int>(nullable: false),
                    CommandePaiement_Montant = table.Column<decimal>(type: "decimal(8, 2)", nullable: false),
                    CommandePaiement_PositionVenteID = table.Column<int>(nullable: false),
                    CommandePaiement_UtilisateurID = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    CommandePaiement_DatePaiement = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    CommandePaiement_FlagCloture = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commande_Paiement", x => x.CommandePaiement_ID);
                    table.ForeignKey(
                        name: "FK_Commande_Paiement_Commande_CommandePaiement_CommandeID",
                        column: x => x.CommandePaiement_CommandeID,
                        principalTable: "Commande",
                        principalColumn: "Commande_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Commande_Paiement_Mode_Paiement_CommandePaiement_ModePaiementID",
                        column: x => x.CommandePaiement_ModePaiementID,
                        principalTable: "Mode_Paiement",
                        principalColumn: "ModePaiement_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Commande_Paiement_Position_Vente_CommandePaiement_PositionVenteID",
                        column: x => x.CommandePaiement_PositionVenteID,
                        principalTable: "Position_Vente",
                        principalColumn: "PositionVente_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Table",
                columns: table => new
                {
                    Table_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Table_SalleId = table.Column<int>(nullable: false),
                    Table_Numero = table.Column<int>(type: "int", nullable: false),
                    Table_IsActive = table.Column<int>(type: "int", nullable: false),
                    Table_AbonnementId = table.Column<int>(type: "int", nullable: false),
                    Table_UtilisateurId = table.Column<string>(type: "nvarchar(350)", nullable: true),
                    Table_DateCreation = table.Column<DateTime>(type: "smalldatetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Table", x => x.Table_Id);
                    table.ForeignKey(
                        name: "FK_Table_Salle_Table_SalleId",
                        column: x => x.Table_SalleId,
                        principalTable: "Salle",
                        principalColumn: "Salle_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tva",
                columns: table => new
                {
                    tva_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    tauxTva = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    totalHt = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    totalTtc = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    totalTva = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    commande_ID = table.Column<int>(nullable: true),
                    vente_ID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tva", x => x.tva_ID);
                    table.ForeignKey(
                        name: "FK_Tva_Commande_commande_ID",
                        column: x => x.commande_ID,
                        principalTable: "Commande",
                        principalColumn: "Commande_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tva_Vente_vente_ID",
                        column: x => x.vente_ID,
                        principalTable: "Vente",
                        principalColumn: "Vente_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Commande_Detail",
                columns: table => new
                {
                    CommandeDetail_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CommandeDetail_CommandeId = table.Column<int>(nullable: false),
                    CommandeDetail_FormeProduitId = table.Column<int>(nullable: false),
                    CommandeDetail_Quantite = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    CommandeDetail_UniteId = table.Column<int>(nullable: false),
                    CommandeDetail_Prix = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    CommandeDetail_AbonnementId = table.Column<int>(type: "int", nullable: false),
                    CommandeDetail_CoutdeRevient = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    CommandeDetail_Marge = table.Column<decimal>(type: "decimal(8,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commande_Detail", x => x.CommandeDetail_Id);
                    table.ForeignKey(
                        name: "FK_Commande_Detail_Commande_CommandeDetail_CommandeId",
                        column: x => x.CommandeDetail_CommandeId,
                        principalTable: "Commande",
                        principalColumn: "Commande_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Commande_Detail_Forme_Produit_CommandeDetail_FormeProduitId",
                        column: x => x.CommandeDetail_FormeProduitId,
                        principalTable: "Forme_Produit",
                        principalColumn: "FormeProduit_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Commande_Detail_Unite_Mesure_CommandeDetail_UniteId",
                        column: x => x.CommandeDetail_UniteId,
                        principalTable: "Unite_Mesure",
                        principalColumn: "UniteMesure_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EchangeProduit_Details",
                columns: table => new
                {
                    EchangeProduitDetails_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EchangeProduitDetails_EchangeID = table.Column<int>(nullable: false),
                    EchangeProduitDetails_FromeID = table.Column<int>(nullable: false),
                    EchangeProduitDetails_ProduitID = table.Column<int>(type: "int", nullable: false),
                    EchangeProduitDetails_Quantite = table.Column<decimal>(type: "decimal(8, 2)", nullable: false),
                    EchangeProduitDetails_UniteID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EchangeProduit_Details", x => x.EchangeProduitDetails_ID);
                    table.ForeignKey(
                        name: "FK_EchangeProduit_Details_Echange_Produits_EchangeProduitDetails_EchangeID",
                        column: x => x.EchangeProduitDetails_EchangeID,
                        principalTable: "Echange_Produits",
                        principalColumn: "EchangeProduits_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EchangeProduit_Details_Forme_Produit_EchangeProduitDetails_FromeID",
                        column: x => x.EchangeProduitDetails_FromeID,
                        principalTable: "Forme_Produit",
                        principalColumn: "FormeProduit_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EchangeProduit_Details_Unite_Mesure_EchangeProduitDetails_UniteID",
                        column: x => x.EchangeProduitDetails_UniteID,
                        principalTable: "Unite_Mesure",
                        principalColumn: "UniteMesure_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fiche_Forme",
                columns: table => new
                {
                    FicheForme_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FicheForme_FormeProduit = table.Column<int>(nullable: false),
                    FicheForme_uniteMesure = table.Column<int>(nullable: false),
                    FicheForme_CoutDeRevient = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FicheForme_Quantite = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FicheForme_FicheBridge = table.Column<int>(nullable: false),
                    FicheForme_IsActive = table.Column<int>(type: "int", nullable: false),
                    FicheForme_DateCreation = table.Column<DateTime>(type: "smalldatetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fiche_Forme", x => x.FicheForme_ID);
                    table.ForeignKey(
                        name: "FK_Fiche_Forme_FicheTechnique_Bridge_FicheForme_FicheBridge",
                        column: x => x.FicheForme_FicheBridge,
                        principalTable: "FicheTechnique_Bridge",
                        principalColumn: "FicheTechniqueBridge_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Fiche_Forme_Forme_Produit_FicheForme_FormeProduit",
                        column: x => x.FicheForme_FormeProduit,
                        principalTable: "Forme_Produit",
                        principalColumn: "FormeProduit_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Fiche_Forme_Unite_Mesure_FicheForme_uniteMesure",
                        column: x => x.FicheForme_uniteMesure,
                        principalTable: "Unite_Mesure",
                        principalColumn: "UniteMesure_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormeDetails",
                columns: table => new
                {
                    FormeDetails_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FormeDetails_FormeProduitID = table.Column<int>(nullable: false),
                    FormeDetails_PointVenteID = table.Column<int>(nullable: false),
                    FormeDetails_Quantite = table.Column<decimal>(nullable: false),
                    FormeDetails_ProduitPackageID = table.Column<int>(nullable: false),
                    FormeDetails_DateCreation = table.Column<DateTime>(nullable: false),
                    FormeDetails_AbonnementID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormeDetails", x => x.FormeDetails_ID);
                    table.ForeignKey(
                        name: "FK_FormeDetails_Forme_Produit_FormeDetails_FormeProduitID",
                        column: x => x.FormeDetails_FormeProduitID,
                        principalTable: "Forme_Produit",
                        principalColumn: "FormeProduit_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FormeDetails_Point_Vente_FormeDetails_PointVenteID",
                        column: x => x.FormeDetails_PointVenteID,
                        principalTable: "Point_Vente",
                        principalColumn: "PointVente_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FormeDetails_ProduitPackage_FormeDetails_ProduitPackageID",
                        column: x => x.FormeDetails_ProduitPackageID,
                        principalTable: "ProduitPackage",
                        principalColumn: "ProduitPackage_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GratuiteDetails",
                columns: table => new
                {
                    GratuiteDetails_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GratuiteDetails_GratuiteID = table.Column<int>(nullable: false),
                    GratuiteDetails_FormeID = table.Column<int>(nullable: false),
                    GratuiteDetails_UniteVenteID = table.Column<int>(nullable: false),
                    GratuiteDetails_Quantite = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GratuiteDetails_CoutDeRevient = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GratuiteDetails_DateCreation = table.Column<DateTime>(type: "smalldatetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GratuiteDetails", x => x.GratuiteDetails_ID);
                    table.ForeignKey(
                        name: "FK_GratuiteDetails_Forme_Produit_GratuiteDetails_FormeID",
                        column: x => x.GratuiteDetails_FormeID,
                        principalTable: "Forme_Produit",
                        principalColumn: "FormeProduit_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GratuiteDetails_Gratuite_GratuiteDetails_GratuiteID",
                        column: x => x.GratuiteDetails_GratuiteID,
                        principalTable: "Gratuite",
                        principalColumn: "Gratuite_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GratuiteDetails_Unite_Mesure_GratuiteDetails_UniteVenteID",
                        column: x => x.GratuiteDetails_UniteVenteID,
                        principalTable: "Unite_Mesure",
                        principalColumn: "UniteMesure_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Package_Forme",
                columns: table => new
                {
                    PackageForme_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PackageForme_FormeProduitID = table.Column<int>(nullable: false),
                    PackageForme_ProduitPackageID = table.Column<int>(nullable: false),
                    PackageForme_DateCreation = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    PackageForme_AbonnementID = table.Column<int>(type: "int", nullable: false),
                    PackageForme_IsInUse = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Package_Forme", x => x.PackageForme_ID);
                    table.ForeignKey(
                        name: "FK_Package_Forme_Forme_Produit_PackageForme_FormeProduitID",
                        column: x => x.PackageForme_FormeProduitID,
                        principalTable: "Forme_Produit",
                        principalColumn: "FormeProduit_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Package_Forme_ProduitPackage_PackageForme_ProduitPackageID",
                        column: x => x.PackageForme_ProduitPackageID,
                        principalTable: "ProduitPackage",
                        principalColumn: "ProduitPackage_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Package_Production",
                columns: table => new
                {
                    PackageProduction_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PackageProduction_ProduitPackageID = table.Column<int>(nullable: false),
                    PackageProduction_DateCreation = table.Column<DateTime>(nullable: false),
                    PackageProduction_ProduitID = table.Column<int>(nullable: false),
                    PackageProduction_AbonnementID = table.Column<int>(nullable: false),
                    PackageProduction_UtilisateurID = table.Column<string>(nullable: true),
                    PackageProduction_Quantite = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Package_Production", x => x.PackageProduction_ID);
                    table.ForeignKey(
                        name: "FK_Package_Production_ProduitPackage_PackageProduction_ProduitID",
                        column: x => x.PackageProduction_ProduitID,
                        principalTable: "ProduitPackage",
                        principalColumn: "ProduitPackage_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Package_Production_Forme_Produit_PackageProduction_ProduitPackageID",
                        column: x => x.PackageProduction_ProduitPackageID,
                        principalTable: "Forme_Produit",
                        principalColumn: "FormeProduit_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PdV_ProduitsPret",
                columns: table => new
                {
                    PdV_ProduitsPret_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PdV_ProduitsPret_PointVenteId = table.Column<int>(nullable: false),
                    PdV_ProduitsPret_ProduitConsomableId = table.Column<int>(nullable: false),
                    PdV_ProduitsPret_FormeProduitId = table.Column<int>(nullable: false),
                    PdV_ProduitsPret_Quantite = table.Column<decimal>(nullable: false),
                    PdV_ProduitsPret_DateModification = table.Column<DateTime>(nullable: false),
                    PdV_ProduitsPret_AbonnementId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PdV_ProduitsPret", x => x.PdV_ProduitsPret_Id);
                    table.ForeignKey(
                        name: "FK_PdV_ProduitsPret_Forme_Produit_PdV_ProduitsPret_FormeProduitId",
                        column: x => x.PdV_ProduitsPret_FormeProduitId,
                        principalTable: "Forme_Produit",
                        principalColumn: "FormeProduit_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PdV_ProduitsPret_Point_Vente_PdV_ProduitsPret_PointVenteId",
                        column: x => x.PdV_ProduitsPret_PointVenteId,
                        principalTable: "Point_Vente",
                        principalColumn: "PointVente_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PdV_ProduitsPret_Produit_PretConsomer_PdV_ProduitsPret_ProduitConsomableId",
                        column: x => x.PdV_ProduitsPret_ProduitConsomableId,
                        principalTable: "Produit_PretConsomer",
                        principalColumn: "ProduitPretConsomer_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Perte_Details",
                columns: table => new
                {
                    PerteDetails_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PerteDetails_PerteID = table.Column<int>(nullable: false),
                    PerteDetails_FormeID = table.Column<int>(nullable: false),
                    PerteDetails_UniteVenteID = table.Column<int>(nullable: false),
                    PerteDetails_Quantite = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PerteDetails_CoutDeRevient = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PerteDetails_DatePerte = table.Column<DateTime>(type: "smalldatetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perte_Details", x => x.PerteDetails_ID);
                    table.ForeignKey(
                        name: "FK_Perte_Details_Forme_Produit_PerteDetails_FormeID",
                        column: x => x.PerteDetails_FormeID,
                        principalTable: "Forme_Produit",
                        principalColumn: "FormeProduit_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Perte_Details_Perte_PerteDetails_PerteID",
                        column: x => x.PerteDetails_PerteID,
                        principalTable: "Perte",
                        principalColumn: "Perte_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Perte_Details_Unite_Mesure_PerteDetails_UniteVenteID",
                        column: x => x.PerteDetails_UniteVenteID,
                        principalTable: "Unite_Mesure",
                        principalColumn: "UniteMesure_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PointVente_Stock",
                columns: table => new
                {
                    PointVenteStock_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PointVenteStock_ProduitID = table.Column<int>(nullable: false),
                    PointVenteStock_FormeProduitID = table.Column<int>(nullable: false),
                    PointVenteStock_PointVenteID = table.Column<int>(nullable: false),
                    PointVenteStock_QuantiteProduit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PointVenteStock_DateModification = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    PointVenteStock_AbonnementID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PointVente_Stock", x => x.PointVenteStock_Id);
                    table.ForeignKey(
                        name: "FK_PointVente_Stock_Forme_Produit_PointVenteStock_FormeProduitID",
                        column: x => x.PointVenteStock_FormeProduitID,
                        principalTable: "Forme_Produit",
                        principalColumn: "FormeProduit_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PointVente_Stock_Point_Vente_PointVenteStock_PointVenteID",
                        column: x => x.PointVenteStock_PointVenteID,
                        principalTable: "Point_Vente",
                        principalColumn: "PointVente_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PointVente_Stock_Produit_Vendable_PointVenteStock_ProduitID",
                        column: x => x.PointVenteStock_ProduitID,
                        principalTable: "Produit_Vendable",
                        principalColumn: "ProduitVendable_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prix_Produit",
                columns: table => new
                {
                    PrixProduit_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PrixProduit_Description = table.Column<string>(type: "varchar(150)", nullable: true),
                    PrixProduit_FormeProduitId = table.Column<int>(nullable: false),
                    PrixProduit_Prix = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    PrixProduit_TauxTVAId = table.Column<int>(type: "int", nullable: true),
                    PrixProduit_QuantiteMinimale = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PrixProduit_IsActive = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prix_Produit", x => x.PrixProduit_Id);
                    table.ForeignKey(
                        name: "FK_Prix_Produit_Forme_Produit_PrixProduit_FormeProduitId",
                        column: x => x.PrixProduit_FormeProduitId,
                        principalTable: "Forme_Produit",
                        principalColumn: "FormeProduit_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProduitAppro",
                columns: table => new
                {
                    ProduitAppro_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProduitAppro_ProduitID = table.Column<int>(nullable: false),
                    ProduitAppro_QuantiteProduite = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProduitAppro_FormeProduitID = table.Column<int>(nullable: true),
                    ProduitAppro_AbonnementID = table.Column<int>(type: "int", nullable: false),
                    ProduitAppro_DateCreation = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    ProduitAppro_DateModification = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    ProduitAppro_AtelierID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProduitAppro", x => x.ProduitAppro_Id);
                    table.ForeignKey(
                        name: "FK_ProduitAppro_Atelier_ProduitAppro_AtelierID",
                        column: x => x.ProduitAppro_AtelierID,
                        principalTable: "Atelier",
                        principalColumn: "Atelier_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProduitAppro_Forme_Produit_ProduitAppro_FormeProduitID",
                        column: x => x.ProduitAppro_FormeProduitID,
                        principalTable: "Forme_Produit",
                        principalColumn: "FormeProduit_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProduitAppro_Produit_Vendable_ProduitAppro_ProduitID",
                        column: x => x.ProduitAppro_ProduitID,
                        principalTable: "Produit_Vendable",
                        principalColumn: "ProduitVendable_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProduitConsomable_Stokage",
                columns: table => new
                {
                    ProduitConsomableStokage_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProduitConsomableStokage_LieuStockID = table.Column<int>(nullable: false),
                    ProduitConsomableStokage_ProduitVendableId = table.Column<int>(nullable: false),
                    ProduitConsomableStokage_FormeProduitId = table.Column<int>(nullable: false),
                    ProduitConsomableStokage_StockMinimum = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ProduitConsomableStokage_StockMaximum = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ProduitConsomableStokage_IsActive = table.Column<int>(nullable: false),
                    ProduitConsomableStokage_AbonnementId = table.Column<int>(type: "int", nullable: false),
                    ProduitConsomableStokage_DateCreation = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    ProduitConsomableStokage_QuantiteActuelle = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProduitConsomable_Stokage", x => x.ProduitConsomableStokage_Id);
                    table.ForeignKey(
                        name: "FK_ProduitConsomable_Stokage_Forme_Produit_ProduitConsomableStokage_FormeProduitId",
                        column: x => x.ProduitConsomableStokage_FormeProduitId,
                        principalTable: "Forme_Produit",
                        principalColumn: "FormeProduit_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProduitConsomable_Stokage_Lieu_Stockage_ProduitConsomableStokage_LieuStockID",
                        column: x => x.ProduitConsomableStokage_LieuStockID,
                        principalTable: "Lieu_Stockage",
                        principalColumn: "LieuStockag_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProduitConsomable_Stokage_Produit_PretConsomer_ProduitConsomableStokage_ProduitVendableId",
                        column: x => x.ProduitConsomableStokage_ProduitVendableId,
                        principalTable: "Produit_PretConsomer",
                        principalColumn: "ProduitPretConsomer_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProduitPack_Atelier",
                columns: table => new
                {
                    ProduitPackAtelier_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProduitPackAtelier_AtelierID = table.Column<int>(nullable: false),
                    ProduitPackAtelier_ProduitPackID = table.Column<int>(nullable: false),
                    ProduitPackAtelier_FormeID = table.Column<int>(nullable: false),
                    ProduitPackAtelier_Quantite = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProduitPackAtelier_DateCreation = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    ProduitPackAtelier_AbonnementID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProduitPack_Atelier", x => x.ProduitPackAtelier_ID);
                    table.ForeignKey(
                        name: "FK_ProduitPack_Atelier_Atelier_ProduitPackAtelier_AtelierID",
                        column: x => x.ProduitPackAtelier_AtelierID,
                        principalTable: "Atelier",
                        principalColumn: "Atelier_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProduitPack_Atelier_Forme_Produit_ProduitPackAtelier_FormeID",
                        column: x => x.ProduitPackAtelier_FormeID,
                        principalTable: "Forme_Produit",
                        principalColumn: "FormeProduit_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProduitPack_Atelier_ProduitPackage_ProduitPackAtelier_ProduitPackID",
                        column: x => x.ProduitPackAtelier_ProduitPackID,
                        principalTable: "ProduitPackage",
                        principalColumn: "ProduitPackage_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProduitPret_Atelier",
                columns: table => new
                {
                    ProduitPretAtelier_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProduitPretAtelier_AtelierID = table.Column<int>(nullable: false),
                    ProduitPretAtelier_FormeID = table.Column<int>(nullable: false),
                    ProduitPretAtelier__ProduitID = table.Column<int>(nullable: false),
                    ProduitPretAtelier_Quantite = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProduitPretAtelier_DateCreation = table.Column<DateTime>(type: "smalldatetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProduitPret_Atelier", x => x.ProduitPretAtelier_ID);
                    table.ForeignKey(
                        name: "FK_ProduitPret_Atelier_Atelier_ProduitPretAtelier_AtelierID",
                        column: x => x.ProduitPretAtelier_AtelierID,
                        principalTable: "Atelier",
                        principalColumn: "Atelier_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProduitPret_Atelier_Forme_Produit_ProduitPretAtelier_FormeID",
                        column: x => x.ProduitPretAtelier_FormeID,
                        principalTable: "Forme_Produit",
                        principalColumn: "FormeProduit_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProduitPret_Atelier_Produit_PretConsomer_ProduitPretAtelier__ProduitID",
                        column: x => x.ProduitPretAtelier__ProduitID,
                        principalTable: "Produit_PretConsomer",
                        principalColumn: "ProduitPretConsomer_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Retour_Details",
                columns: table => new
                {
                    RetourDetails_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RetourDetails_RetourID = table.Column<int>(nullable: false),
                    RetourDetails_FormeID = table.Column<int>(nullable: false),
                    RetourDetails_UniteVente = table.Column<int>(nullable: false),
                    RetourDetails_PrixProduit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RetourDetails_Quantite = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RetourDetails_PrixTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RetourDetails_DateRetour = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    RetourDetails_isPerte = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Retour_Details", x => x.RetourDetails_ID);
                    table.ForeignKey(
                        name: "FK_Retour_Details_Forme_Produit_RetourDetails_FormeID",
                        column: x => x.RetourDetails_FormeID,
                        principalTable: "Forme_Produit",
                        principalColumn: "FormeProduit_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Retour_Details_Retour_Produits_RetourDetails_RetourID",
                        column: x => x.RetourDetails_RetourID,
                        principalTable: "Retour_Produits",
                        principalColumn: "Retour_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Retour_Details_Unite_Mesure_RetourDetails_UniteVente",
                        column: x => x.RetourDetails_UniteVente,
                        principalTable: "Unite_Mesure",
                        principalColumn: "UniteMesure_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sous_ProduitPackage",
                columns: table => new
                {
                    SousProduitPackage_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SousProduitPackage_FormeProduittID = table.Column<int>(nullable: false),
                    SousProduitPackage_ProduitPackageID = table.Column<int>(nullable: false),
                    SousProduitPackage_QuantiteProduit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SousProduitPackage_CoutDeRevient = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SousProduitPackage_DateCreation = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    SousProduitPackage_AbonnementID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sous_ProduitPackage", x => x.SousProduitPackage_ID);
                    table.ForeignKey(
                        name: "FK_Sous_ProduitPackage_Forme_Produit_SousProduitPackage_FormeProduittID",
                        column: x => x.SousProduitPackage_FormeProduittID,
                        principalTable: "Forme_Produit",
                        principalColumn: "FormeProduit_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sous_ProduitPackage_ProduitPackage_SousProduitPackage_ProduitPackageID",
                        column: x => x.SousProduitPackage_ProduitPackageID,
                        principalTable: "ProduitPackage",
                        principalColumn: "ProduitPackage_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VenteDetails",
                columns: table => new
                {
                    VenteDetails_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VenteDetails_VentId = table.Column<int>(nullable: false),
                    VenteDetails_FormeProduitId = table.Column<int>(nullable: false),
                    VenteDetails_Quantite = table.Column<decimal>(type: "decimal(12,8)", nullable: false),
                    VenteDetails_UniteId = table.Column<int>(nullable: false),
                    VenteDetails_CoutDeRevient = table.Column<decimal>(type: "decimal(12,8)", nullable: false),
                    VenteDetails_Prix = table.Column<decimal>(type: "decimal(12,8)", nullable: false),
                    VenteDetails_Marge = table.Column<decimal>(type: "decimal(12,8)", nullable: false),
                    VenteDetails_DateCreation = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    VenteDetails_AbonnementID = table.Column<int>(type: "int", nullable: false),
                    VenteDetails_FlagCloture = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VenteDetails", x => x.VenteDetails_Id);
                    table.ForeignKey(
                        name: "FK_VenteDetails_Forme_Produit_VenteDetails_FormeProduitId",
                        column: x => x.VenteDetails_FormeProduitId,
                        principalTable: "Forme_Produit",
                        principalColumn: "FormeProduit_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VenteDetails_Unite_Mesure_VenteDetails_UniteId",
                        column: x => x.VenteDetails_UniteId,
                        principalTable: "Unite_Mesure",
                        principalColumn: "UniteMesure_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VenteDetails_Vente_VenteDetails_VentId",
                        column: x => x.VenteDetails_VentId,
                        principalTable: "Vente",
                        principalColumn: "Vente_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DemandePret_Details",
                columns: table => new
                {
                    DemandePretDetails_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DemandePretDetails_DemandeID = table.Column<int>(nullable: false),
                    DemandePretDetails_ProduitID = table.Column<int>(nullable: false),
                    DemandePretDetails_FormeID = table.Column<int>(nullable: false),
                    DemandePretDetails_UniteMesureID = table.Column<int>(nullable: false),
                    DemandePretDetails_Quantite = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DemandePretDetails_QuantiteLivre = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DemandePret_Details", x => x.DemandePretDetails_ID);
                    table.ForeignKey(
                        name: "FK_DemandePret_Details_Demande_Pret_DemandePretDetails_DemandeID",
                        column: x => x.DemandePretDetails_DemandeID,
                        principalTable: "Demande_Pret",
                        principalColumn: "DemandePret_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DemandePret_Details_Forme_Produit_DemandePretDetails_FormeID",
                        column: x => x.DemandePretDetails_FormeID,
                        principalTable: "Forme_Produit",
                        principalColumn: "FormeProduit_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DemandePret_Details_Produit_PretConsomer_DemandePretDetails_ProduitID",
                        column: x => x.DemandePretDetails_ProduitID,
                        principalTable: "Produit_PretConsomer",
                        principalColumn: "ProduitPretConsomer_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DemandePret_Details_Unite_Mesure_DemandePretDetails_UniteMesureID",
                        column: x => x.DemandePretDetails_UniteMesureID,
                        principalTable: "Unite_Mesure",
                        principalColumn: "UniteMesure_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Perte_Matiere",
                columns: table => new
                {
                    PerteMatiere_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PerteMatiere_MatierePremiereStockageID = table.Column<int>(nullable: false),
                    PerteMatiere_Quantite = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    PerteMatiere_UniteMesureID = table.Column<int>(nullable: false),
                    PerteMatiere_AtelierID = table.Column<int>(nullable: false),
                    PerteMatiere_DateCreation = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    PerteMatiere_Utilisateur = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    PerteMatiere_AbonnementID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perte_Matiere", x => x.PerteMatiere_ID);
                    table.ForeignKey(
                        name: "FK_Perte_Matiere_Atelier_PerteMatiere_AtelierID",
                        column: x => x.PerteMatiere_AtelierID,
                        principalTable: "Atelier",
                        principalColumn: "Atelier_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Perte_Matiere_matiereStock_Atelier_PerteMatiere_MatierePremiereStockageID",
                        column: x => x.PerteMatiere_MatierePremiereStockageID,
                        principalTable: "matiereStock_Atelier",
                        principalColumn: "MatiereStockAtelier_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Perte_Matiere_Unite_Mesure_PerteMatiere_UniteMesureID",
                        column: x => x.PerteMatiere_UniteMesureID,
                        principalTable: "Unite_Mesure",
                        principalColumn: "UniteMesure_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Demande",
                columns: table => new
                {
                    Demande_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Demande_Type = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    Demande_BonDeSortieID = table.Column<int>(nullable: true),
                    Demande_LieuStockageID = table.Column<int>(nullable: false),
                    Demande_AtelierID = table.Column<int>(nullable: false),
                    Demande_PlanificationID = table.Column<int>(nullable: true),
                    Demande_DateCreation = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    Demande_AbonnementID = table.Column<int>(type: "int", nullable: false),
                    Demande_Etat = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    Demande_Motif = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    Demande_UtilisateurID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Demande_ValideParID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Demande", x => x.Demande_ID);
                    table.ForeignKey(
                        name: "FK_Demande_Atelier_Demande_AtelierID",
                        column: x => x.Demande_AtelierID,
                        principalTable: "Atelier",
                        principalColumn: "Atelier_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Demande_BonDe_Sortie_Demande_BonDeSortieID",
                        column: x => x.Demande_BonDeSortieID,
                        principalTable: "BonDe_Sortie",
                        principalColumn: "BonDeSortie_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Demande_Lieu_Stockage_Demande_LieuStockageID",
                        column: x => x.Demande_LieuStockageID,
                        principalTable: "Lieu_Stockage",
                        principalColumn: "LieuStockag_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Demande_Planification_Journee_Demande_PlanificationID",
                        column: x => x.Demande_PlanificationID,
                        principalTable: "Planification_Journee",
                        principalColumn: "PlanificationJournee_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Planification_ProdBase",
                columns: table => new
                {
                    PlanificationProdBase_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PlanificationProdBase_ProdBaseID = table.Column<int>(nullable: false),
                    PlanificationProdBase_PlanificationID = table.Column<int>(nullable: false),
                    PlanificationProdBase_ProdBaseDesignation = table.Column<string>(nullable: true),
                    PlanificationProdBase_Quantite = table.Column<decimal>(nullable: false),
                    PlanificationProdBase_UniteDesi = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planification_ProdBase", x => x.PlanificationProdBase_ID);
                    table.ForeignKey(
                        name: "FK_Planification_ProdBase_Planification_Journee_PlanificationProdBase_PlanificationID",
                        column: x => x.PlanificationProdBase_PlanificationID,
                        principalTable: "Planification_Journee",
                        principalColumn: "PlanificationJournee_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Planification_ProdBase_ProduitBase_PlanificationProdBase_ProdBaseID",
                        column: x => x.PlanificationProdBase_ProdBaseID,
                        principalTable: "ProduitBase",
                        principalColumn: "ProduitBase_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Planification_Production",
                columns: table => new
                {
                    PlanificationProduction_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PlanificationProduction_Date = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    PlanificationProduction_ProduitVendableId = table.Column<int>(nullable: false),
                    PlanificationProduction_FormeProduitId = table.Column<int>(nullable: false),
                    PlanificationProduction_QuantitePrevue = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    PlanificationProduction_QuantiteProduite = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    PlanificationProduction_Motif = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    PlanificationProduction_CoutRevient = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    PlanificationProduction_QuantiteRestante = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PlanificationProduction_AbonnementId = table.Column<int>(type: "int", nullable: false),
                    PlanificationProduction_DateCreation = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    PlanificationProduction_DateModification = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    PlanificationProduction_PlanificationJourneeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planification_Production", x => x.PlanificationProduction_Id);
                    table.ForeignKey(
                        name: "FK_Planification_Production_Forme_Produit_PlanificationProduction_FormeProduitId",
                        column: x => x.PlanificationProduction_FormeProduitId,
                        principalTable: "Forme_Produit",
                        principalColumn: "FormeProduit_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Planification_Production_Planification_Journee_PlanificationProduction_PlanificationJourneeID",
                        column: x => x.PlanificationProduction_PlanificationJourneeID,
                        principalTable: "Planification_Journee",
                        principalColumn: "PlanificationJournee_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Planification_Production_Produit_Vendable_PlanificationProduction_ProduitVendableId",
                        column: x => x.PlanificationProduction_ProduitVendableId,
                        principalTable: "Produit_Vendable",
                        principalColumn: "ProduitVendable_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Retour_Stock",
                columns: table => new
                {
                    RetourStok_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RetourStok_Motif = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    RetourStok_Etat = table.Column<int>(type: "int", nullable: false),
                    RetourStok_PlanificationID = table.Column<int>(nullable: false),
                    RetourStok_BonDeSortieID = table.Column<int>(nullable: false),
                    RetourStok_DateCreation = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    RetourStok_AbonnementID = table.Column<int>(type: "int", nullable: false),
                    RetourStok_UtilisateurID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RetourStok_ValideParID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Retour_Stock", x => x.RetourStok_ID);
                    table.ForeignKey(
                        name: "FK_Retour_Stock_BonDe_Sortie_RetourStok_BonDeSortieID",
                        column: x => x.RetourStok_BonDeSortieID,
                        principalTable: "BonDe_Sortie",
                        principalColumn: "BonDeSortie_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Retour_Stock_Planification_Journee_RetourStok_PlanificationID",
                        column: x => x.RetourStok_PlanificationID,
                        principalTable: "Planification_Journee",
                        principalColumn: "PlanificationJournee_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlanificationdeProductionBase",
                columns: table => new
                {
                    PlanificationProductionBase_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PlanificationProductionBase_Date = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    PlanificationProductionBase_ProduitBase = table.Column<int>(nullable: false),
                    PlanificationProductionBase_QuantitePrevue = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    PlanificationProductionBase_QuantiteProduite = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    PlanificationProductionBase_CoutRevient = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    PlanificationProductionBase_QuantiteRestante = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PlanificationProductionBase_AbonnementId = table.Column<int>(type: "int", nullable: false),
                    PlanificationProductionBase_DateCreation = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    PlanificationProductionBase_DateModification = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    PlanificationProductionBase_PlanificationJourneeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanificationdeProductionBase", x => x.PlanificationProductionBase_Id);
                    table.ForeignKey(
                        name: "FK_PlanificationdeProductionBase_PlanificationJourneeBase_PlanificationProductionBase_PlanificationJourneeID",
                        column: x => x.PlanificationProductionBase_PlanificationJourneeID,
                        principalTable: "PlanificationJourneeBase",
                        principalColumn: "PlanificationJourneeBase_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlanificationdeProductionBase_ProduitBase_PlanificationProductionBase_ProduitBase",
                        column: x => x.PlanificationProductionBase_ProduitBase,
                        principalTable: "ProduitBase",
                        principalColumn: "ProduitBase_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Affectation_Agent_Table",
                columns: table => new
                {
                    Affectation_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Affectation_AgentId = table.Column<int>(nullable: false),
                    Affectation_TableId = table.Column<int>(nullable: false),
                    Affectation_IsActive = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Affectation_Agent_Table", x => x.Affectation_Id);
                    table.ForeignKey(
                        name: "FK_Affectation_Agent_Table_Agent_Serveur_Affectation_AgentId",
                        column: x => x.Affectation_AgentId,
                        principalTable: "Agent_Serveur",
                        principalColumn: "Agent_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Affectation_Agent_Table_Table_Affectation_TableId",
                        column: x => x.Affectation_TableId,
                        principalTable: "Table",
                        principalColumn: "Table_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PackageFormeDetailsMatiere",
                columns: table => new
                {
                    PackageFormeDetailsMatiere_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PackageFormeDetailsMatiere_PackageFormeID = table.Column<int>(nullable: false),
                    PackageFormeDetailsMatiere_SousMatiereID = table.Column<int>(nullable: false),
                    PackageFormeDetailsMatiere_UniteMesureID = table.Column<int>(nullable: false),
                    PackageFormeDetailsMatiere_Quantite = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    PackageFormeDetailsMatiere_CoutDeRevient = table.Column<decimal>(type: "decimal(18, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageFormeDetailsMatiere", x => x.PackageFormeDetailsMatiere_ID);
                    table.ForeignKey(
                        name: "FK_PackageFormeDetailsMatiere_Package_Forme_PackageFormeDetailsMatiere_PackageFormeID",
                        column: x => x.PackageFormeDetailsMatiere_PackageFormeID,
                        principalTable: "Package_Forme",
                        principalColumn: "PackageForme_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PackageFormeDetailsMatiere_SousMatierePackage_PackageFormeDetailsMatiere_SousMatiereID",
                        column: x => x.PackageFormeDetailsMatiere_SousMatiereID,
                        principalTable: "SousMatierePackage",
                        principalColumn: "SousMatierePackage_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PackageFormeDetailsMatiere_Unite_Mesure_PackageFormeDetailsMatiere_UniteMesureID",
                        column: x => x.PackageFormeDetailsMatiere_UniteMesureID,
                        principalTable: "Unite_Mesure",
                        principalColumn: "UniteMesure_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Production_Details",
                columns: table => new
                {
                    ProductionDetails_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductionDetails_ProductionID = table.Column<int>(nullable: false),
                    ProductionDetails_FormeID = table.Column<int>(nullable: false),
                    ProductionDetails_Quantite = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductionDetails_CoutDeRevient = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Production_Details", x => x.ProductionDetails_Id);
                    table.ForeignKey(
                        name: "FK_Production_Details_Forme_Produit_ProductionDetails_FormeID",
                        column: x => x.ProductionDetails_FormeID,
                        principalTable: "Forme_Produit",
                        principalColumn: "FormeProduit_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Production_Details_Package_Production_ProductionDetails_ProductionID",
                        column: x => x.ProductionDetails_ProductionID,
                        principalTable: "Package_Production",
                        principalColumn: "PackageProduction_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Approvisionnement",
                columns: table => new
                {
                    Approvisionnement_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Approvisionnement_Date = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    Approvisionnement_PointVenteId = table.Column<int>(nullable: false),
                    Approvisionnement_ProduitId = table.Column<int>(nullable: false),
                    Approvisionnement_FormeProduitId = table.Column<int>(nullable: false),
                    Approvisionnement_ProduitApproID = table.Column<int>(nullable: false),
                    Approvisionnement_DateSaisie = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    Approvisionnement_DateModification = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    Approvisionnement_UtilisateurId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Approvisionnement_ValideParId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Approvisionnement_AbonnementId = table.Column<int>(type: "int", nullable: false),
                    Approvisionnement_AtelierID = table.Column<int>(nullable: false),
                    Approvisionnement_Etat = table.Column<int>(type: "int", nullable: false),
                    Approvisionnement_Quantite = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Approvisionnement_CoutDeRevient = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Approvisionnement_QuantiteRestante = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Approvisionnement", x => x.Approvisionnement_Id);
                    table.ForeignKey(
                        name: "FK_Approvisionnement_Atelier_Approvisionnement_AtelierID",
                        column: x => x.Approvisionnement_AtelierID,
                        principalTable: "Atelier",
                        principalColumn: "Atelier_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Approvisionnement_Forme_Produit_Approvisionnement_FormeProduitId",
                        column: x => x.Approvisionnement_FormeProduitId,
                        principalTable: "Forme_Produit",
                        principalColumn: "FormeProduit_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Approvisionnement_Point_Vente_Approvisionnement_PointVenteId",
                        column: x => x.Approvisionnement_PointVenteId,
                        principalTable: "Point_Vente",
                        principalColumn: "PointVente_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Approvisionnement_ProduitAppro_Approvisionnement_ProduitApproID",
                        column: x => x.Approvisionnement_ProduitApproID,
                        principalTable: "ProduitAppro",
                        principalColumn: "ProduitAppro_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Approvisionnement_Produit_Vendable_Approvisionnement_ProduitId",
                        column: x => x.Approvisionnement_ProduitId,
                        principalTable: "Produit_Vendable",
                        principalColumn: "ProduitVendable_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetailsProduitAppro",
                columns: table => new
                {
                    DetailsProduitAppro_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DetailsProduitAppro_ProduitApproID = table.Column<int>(nullable: false),
                    DetailsProduitAppro_QuantiteProduite = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DetailsProduitAppro_PrixProduit = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailsProduitAppro", x => x.DetailsProduitAppro_ID);
                    table.ForeignKey(
                        name: "FK_DetailsProduitAppro_ProduitAppro_DetailsProduitAppro_ProduitApproID",
                        column: x => x.DetailsProduitAppro_ProduitApproID,
                        principalTable: "ProduitAppro",
                        principalColumn: "ProduitAppro_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Approvisionnement_ProduitConsomable",
                columns: table => new
                {
                    ApprovisionnementProduit_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApprovisionnementProduit_Date = table.Column<DateTime>(nullable: false),
                    ApprovisionnementProduit_PointVenteID = table.Column<int>(nullable: false),
                    ApprovisionnementProduit_StockID = table.Column<int>(nullable: false),
                    ApprovisionnementProduit_ProduitStockageId = table.Column<int>(nullable: false),
                    ApprovisionnementProduit_Quantite = table.Column<decimal>(nullable: false),
                    ApprovisionnementProduit_UniteMesureId = table.Column<int>(nullable: true),
                    ApprovisionnementProduit_DateReception = table.Column<DateTime>(nullable: true),
                    ApprovisionnementProduit_DateCreation = table.Column<DateTime>(nullable: false),
                    ApprovisionnementProduit_Etat = table.Column<string>(nullable: true),
                    ApprovisionnementProduit_LieuUserId = table.Column<string>(nullable: true),
                    ApprovisionnementProduit_PointVenteUserId = table.Column<string>(nullable: true),
                    ApprovisionnementProduit_AbonnementId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Approvisionnement_ProduitConsomable", x => x.ApprovisionnementProduit_Id);
                    table.ForeignKey(
                        name: "FK_Approvisionnement_ProduitConsomable_Point_Vente_ApprovisionnementProduit_PointVenteID",
                        column: x => x.ApprovisionnementProduit_PointVenteID,
                        principalTable: "Point_Vente",
                        principalColumn: "PointVente_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Approvisionnement_ProduitConsomable_ProduitConsomable_Stokage_ApprovisionnementProduit_ProduitStockageId",
                        column: x => x.ApprovisionnementProduit_ProduitStockageId,
                        principalTable: "ProduitConsomable_Stokage",
                        principalColumn: "ProduitConsomableStokage_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Approvisionnement_ProduitConsomable_Lieu_Stockage_ApprovisionnementProduit_StockID",
                        column: x => x.ApprovisionnementProduit_StockID,
                        principalTable: "Lieu_Stockage",
                        principalColumn: "LieuStockag_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Approvisionnement_ProduitConsomable_Unite_Mesure_ApprovisionnementProduit_UniteMesureId",
                        column: x => x.ApprovisionnementProduit_UniteMesureId,
                        principalTable: "Unite_Mesure",
                        principalColumn: "UniteMesure_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Mouvement_ProduitsConso",
                columns: table => new
                {
                    MouvementProduitsConso_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MouvementProduitsConso_ProduitConsoId = table.Column<int>(nullable: false),
                    MouvementProduitsConso_DateMouvement = table.Column<DateTime>(nullable: false),
                    MouvementProduitsConso_MouvementType = table.Column<int>(nullable: false),
                    MouvementProduitsConso_Quantite = table.Column<decimal>(nullable: false),
                    MouvementProduitsConso_QuantiteActuelle = table.Column<decimal>(nullable: false),
                    MouvementProduitsConso_UniteMesureId = table.Column<int>(nullable: false),
                    MouvementProduitsConso_FournisseurProduitId = table.Column<int>(nullable: false),
                    MouvementProduitsConso_StockFournisseurId = table.Column<int>(nullable: true),
                    MouvementProduitsConso_ReceptionStockId = table.Column<int>(nullable: true),
                    MouvementProduitsConso_Utilisateur = table.Column<string>(nullable: true),
                    MouvementProduitsConso_ReceptionStatut = table.Column<bool>(nullable: false),
                    MouvementProduitsConso_ReceptionUtilisateur = table.Column<string>(nullable: true),
                    MouvementProduitsConso_AbonnementID = table.Column<int>(nullable: false),
                    MouvementProduitsConso_IsActive = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mouvement_ProduitsConso", x => x.MouvementProduitsConso_Id);
                    table.ForeignKey(
                        name: "FK_Mouvement_ProduitsConso_Lieu_Stockage_MouvementProduitsConso_FournisseurProduitId",
                        column: x => x.MouvementProduitsConso_FournisseurProduitId,
                        principalTable: "Lieu_Stockage",
                        principalColumn: "LieuStockag_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Mouvement_ProduitsConso_Mouvement_Type_MouvementProduitsConso_MouvementType",
                        column: x => x.MouvementProduitsConso_MouvementType,
                        principalTable: "Mouvement_Type",
                        principalColumn: "MouvementType_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Mouvement_ProduitsConso_ProduitConsomable_Stokage_MouvementProduitsConso_ProduitConsoId",
                        column: x => x.MouvementProduitsConso_ProduitConsoId,
                        principalTable: "ProduitConsomable_Stokage",
                        principalColumn: "ProduitConsomableStokage_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Mouvement_ProduitsConso_Point_Vente_MouvementProduitsConso_StockFournisseurId",
                        column: x => x.MouvementProduitsConso_StockFournisseurId,
                        principalTable: "Point_Vente",
                        principalColumn: "PointVente_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mouvement_ProduitsConso_Unite_Mesure_MouvementProduitsConso_UniteMesureId",
                        column: x => x.MouvementProduitsConso_UniteMesureId,
                        principalTable: "Unite_Mesure",
                        principalColumn: "UniteMesure_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Approvisionnement_ProduitPackage",
                columns: table => new
                {
                    ApprovisionnementProduitPackage_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApprovisionnementProduitPackage_Date = table.Column<DateTime>(nullable: false),
                    ApprovisionnementProduitPackage_PointVenteID = table.Column<int>(nullable: false),
                    ApprovisionnementProduitPackage_AtelierID = table.Column<int>(nullable: false),
                    ApprovisionnementProduitPackage_ProduitpackAtelierId = table.Column<int>(nullable: false),
                    ApprovisionnementProduitPackage__Quantite = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ApprovisionnementProduitPackage__UniteMesureId = table.Column<int>(nullable: true),
                    ApprovisionnementProduitPackage__DateReception = table.Column<DateTime>(nullable: true),
                    ApprovisionnementProduitPackage__DateCreation = table.Column<DateTime>(nullable: false),
                    ApprovisionnementProduitPackage__Etat = table.Column<string>(nullable: true),
                    ApprovisionnementProduitPackage_AbonnementId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Approvisionnement_ProduitPackage", x => x.ApprovisionnementProduitPackage_Id);
                    table.ForeignKey(
                        name: "FK_Approvisionnement_ProduitPackage_Atelier_ApprovisionnementProduitPackage_AtelierID",
                        column: x => x.ApprovisionnementProduitPackage_AtelierID,
                        principalTable: "Atelier",
                        principalColumn: "Atelier_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Approvisionnement_ProduitPackage_Point_Vente_ApprovisionnementProduitPackage_PointVenteID",
                        column: x => x.ApprovisionnementProduitPackage_PointVenteID,
                        principalTable: "Point_Vente",
                        principalColumn: "PointVente_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Approvisionnement_ProduitPackage_ProduitPack_Atelier_ApprovisionnementProduitPackage_ProduitpackAtelierId",
                        column: x => x.ApprovisionnementProduitPackage_ProduitpackAtelierId,
                        principalTable: "ProduitPack_Atelier",
                        principalColumn: "ProduitPackAtelier_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Approvisionnement_ProduitPackage_Unite_Mesure_ApprovisionnementProduitPackage__UniteMesureId",
                        column: x => x.ApprovisionnementProduitPackage__UniteMesureId,
                        principalTable: "Unite_Mesure",
                        principalColumn: "UniteMesure_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Perte_Pret",
                columns: table => new
                {
                    PertePret_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PertePret_ProduitPretAtelierID = table.Column<int>(nullable: false),
                    PertePret_Quantite = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PertePret_UniteMesureID = table.Column<int>(nullable: false),
                    PertePret_AtelierID = table.Column<int>(nullable: false),
                    PertePret_AbonnmentID = table.Column<int>(type: "int", nullable: false),
                    PertePret_DateCreation = table.Column<DateTime>(type: "smalldatetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perte_Pret", x => x.PertePret_ID);
                    table.ForeignKey(
                        name: "FK_Perte_Pret_Atelier_PertePret_AtelierID",
                        column: x => x.PertePret_AtelierID,
                        principalTable: "Atelier",
                        principalColumn: "Atelier_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Perte_Pret_ProduitPret_Atelier_PertePret_ProduitPretAtelierID",
                        column: x => x.PertePret_ProduitPretAtelierID,
                        principalTable: "ProduitPret_Atelier",
                        principalColumn: "ProduitPretAtelier_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Perte_Pret_Unite_Mesure_PertePret_UniteMesureID",
                        column: x => x.PertePret_UniteMesureID,
                        principalTable: "Unite_Mesure",
                        principalColumn: "UniteMesure_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PackageForme_Details",
                columns: table => new
                {
                    PackageFormeDetails_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PackageFormeDetails_PackageFormeID = table.Column<int>(nullable: false),
                    PackageFormeDetails_SousProduitID = table.Column<int>(nullable: false),
                    PackageFormeDetails_Quantite = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PackageFormeDetails_CoutdeRevient = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageForme_Details", x => x.PackageFormeDetails_ID);
                    table.ForeignKey(
                        name: "FK_PackageForme_Details_Package_Forme_PackageFormeDetails_PackageFormeID",
                        column: x => x.PackageFormeDetails_PackageFormeID,
                        principalTable: "Package_Forme",
                        principalColumn: "PackageForme_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PackageForme_Details_Sous_ProduitPackage_PackageFormeDetails_SousProduitID",
                        column: x => x.PackageFormeDetails_SousProduitID,
                        principalTable: "Sous_ProduitPackage",
                        principalColumn: "SousProduitPackage_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Zone_Stockage_ZoneStockage_FormeStockageId",
                table: "Zone_Stockage",
                column: "ZoneStockage_FormeStockageId");

            migrationBuilder.CreateIndex(
                name: "IX_Zone_Stockage_ZoneStockage_LieuStockageId",
                table: "Zone_Stockage",
                column: "ZoneStockage_LieuStockageId");

            migrationBuilder.CreateIndex(
                name: "IX_Zone_Stockage_ZoneStockage_TypeContenuId",
                table: "Zone_Stockage",
                column: "ZoneStockage_TypeContenuId");

            migrationBuilder.CreateIndex(
                name: "IX_Zone_Stockage_ZoneStockage_UniteMesureId",
                table: "Zone_Stockage",
                column: "ZoneStockage_UniteMesureId");

            migrationBuilder.CreateIndex(
                name: "IX_Section_Stockage_Section_ZoneStockageId",
                table: "Section_Stockage",
                column: "Section_ZoneStockageId");

            migrationBuilder.CreateIndex(
                name: "IX_Produit_Vendable_ProduitVendable_FamilleProduitId",
                table: "Produit_Vendable",
                column: "ProduitVendable_FamilleProduitId");

            migrationBuilder.CreateIndex(
                name: "IX_Produit_Vendable_ProduitVendable_FormStockageId",
                table: "Produit_Vendable",
                column: "ProduitVendable_FormStockageId");

            migrationBuilder.CreateIndex(
                name: "IX_Produit_Vendable_ProduitVendable_TvaId",
                table: "Produit_Vendable",
                column: "ProduitVendable_TvaId");

            migrationBuilder.CreateIndex(
                name: "IX_Produit_Vendable_ProduitVendable_UniteMesureId",
                table: "Produit_Vendable",
                column: "ProduitVendable_UniteMesureId");

            migrationBuilder.CreateIndex(
                name: "IX_Produit_FicheTechnique_FicheTechnique_FicheTechniqueBridgeID",
                table: "Produit_FicheTechnique",
                column: "FicheTechnique_FicheTechniqueBridgeID");

            migrationBuilder.CreateIndex(
                name: "IX_Produit_FicheTechnique_FicheTechnique_MatierePremiereId",
                table: "Produit_FicheTechnique",
                column: "FicheTechnique_MatierePremiereId");

            migrationBuilder.CreateIndex(
                name: "IX_Produit_FicheTechnique_FicheTechnique_ProduitVendableId",
                table: "Produit_FicheTechnique",
                column: "FicheTechnique_ProduitVendableId");

            migrationBuilder.CreateIndex(
                name: "IX_Produit_FicheTechnique_FicheTechnique_UniteMesureId",
                table: "Produit_FicheTechnique",
                column: "FicheTechnique_UniteMesureId");

            migrationBuilder.CreateIndex(
                name: "IX_Point_Vente_PointVente_AtelierID",
                table: "Point_Vente",
                column: "PointVente_AtelierID");

            migrationBuilder.CreateIndex(
                name: "IX_Point_Vente_PointVente_StockID",
                table: "Point_Vente",
                column: "PointVente_StockID");

            migrationBuilder.CreateIndex(
                name: "IX_Point_Vente_PointVente_VilleID",
                table: "Point_Vente",
                column: "PointVente_VilleID");

            migrationBuilder.CreateIndex(
                name: "IX_MatierePremiere_Stokage_MatierePremiereStokage_MatierePremiereId",
                table: "MatierePremiere_Stokage",
                column: "MatierePremiereStokage_MatierePremiereId");

            migrationBuilder.CreateIndex(
                name: "IX_MatierePremiere_Stokage_MatierePremiereStokage_SectionStockageId",
                table: "MatierePremiere_Stokage",
                column: "MatierePremiereStokage_SectionStockageId");

            migrationBuilder.CreateIndex(
                name: "IX_Matiere_Premiere_MatierePremiere_AchatUniteMesureId",
                table: "Matiere_Premiere",
                column: "MatierePremiere_AchatUniteMesureId");

            migrationBuilder.CreateIndex(
                name: "IX_Matiere_Premiere_MatierePremiere_CoutTVAID",
                table: "Matiere_Premiere",
                column: "MatierePremiere_CoutTVAID");

            migrationBuilder.CreateIndex(
                name: "IX_Matiere_Premiere_MatierePremiere_FamilleID",
                table: "Matiere_Premiere",
                column: "MatierePremiere_FamilleID");

            migrationBuilder.CreateIndex(
                name: "IX_Matiere_Premiere_MatierePremiere_FormeStockageId",
                table: "Matiere_Premiere",
                column: "MatierePremiere_FormeStockageId");

            migrationBuilder.CreateIndex(
                name: "IX_Lieu_Stockage_LieuStockag_VilleID",
                table: "Lieu_Stockage",
                column: "LieuStockag_VilleID");

            migrationBuilder.CreateIndex(
                name: "IX_Founisseur_Founisseur_VilleID",
                table: "Founisseur",
                column: "Founisseur_VilleID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AtelierID",
                table: "AspNetUsers",
                column: "AtelierID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_LieuStockage_ID",
                table: "AspNetUsers",
                column: "LieuStockage_ID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PointVente_ID",
                table: "AspNetUsers",
                column: "PointVente_ID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PositionVente_ID",
                table: "AspNetUsers",
                column: "PositionVente_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Paiement_Abonnement_PaiementAbonnement_AbonnementId",
                table: "Paiement_Abonnement",
                column: "PaiementAbonnement_AbonnementId");

            migrationBuilder.CreateIndex(
                name: "IX_Paiement_Abonnement_PaiementAbonnement_PointVenteID",
                table: "Paiement_Abonnement",
                column: "PaiementAbonnement_PointVenteID");

            migrationBuilder.CreateIndex(
                name: "IX_Affectation_Agent_Table_Affectation_AgentId",
                table: "Affectation_Agent_Table",
                column: "Affectation_AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_Affectation_Agent_Table_Affectation_TableId",
                table: "Affectation_Agent_Table",
                column: "Affectation_TableId");

            migrationBuilder.CreateIndex(
                name: "IX_Allimentation_Caisse_AllimentationCaisse_PositionVenteID",
                table: "Allimentation_Caisse",
                column: "AllimentationCaisse_PositionVenteID");

            migrationBuilder.CreateIndex(
                name: "IX_Allimentation_Caisse_AllimentationCaisse_UtilsateurID",
                table: "Allimentation_Caisse",
                column: "AllimentationCaisse_UtilsateurID");

            migrationBuilder.CreateIndex(
                name: "IX_Approvisionnement_Approvisionnement_AtelierID",
                table: "Approvisionnement",
                column: "Approvisionnement_AtelierID");

            migrationBuilder.CreateIndex(
                name: "IX_Approvisionnement_Approvisionnement_FormeProduitId",
                table: "Approvisionnement",
                column: "Approvisionnement_FormeProduitId");

            migrationBuilder.CreateIndex(
                name: "IX_Approvisionnement_Approvisionnement_PointVenteId",
                table: "Approvisionnement",
                column: "Approvisionnement_PointVenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Approvisionnement_Approvisionnement_ProduitApproID",
                table: "Approvisionnement",
                column: "Approvisionnement_ProduitApproID");

            migrationBuilder.CreateIndex(
                name: "IX_Approvisionnement_Approvisionnement_ProduitId",
                table: "Approvisionnement",
                column: "Approvisionnement_ProduitId");

            migrationBuilder.CreateIndex(
                name: "IX_Approvisionnement_Matiere_ApprovisionnementMatiere_AtelierID",
                table: "Approvisionnement_Matiere",
                column: "ApprovisionnementMatiere_AtelierID");

            migrationBuilder.CreateIndex(
                name: "IX_Approvisionnement_Matiere_ApprovisionnementMatiere_MatiereStockID",
                table: "Approvisionnement_Matiere",
                column: "ApprovisionnementMatiere_MatiereStockID");

            migrationBuilder.CreateIndex(
                name: "IX_Approvisionnement_Matiere_ApprovisionnementMatiere_StockID",
                table: "Approvisionnement_Matiere",
                column: "ApprovisionnementMatiere_StockID");

            migrationBuilder.CreateIndex(
                name: "IX_Approvisionnement_Matiere_ApprovisionnementMatiere_UniteID",
                table: "Approvisionnement_Matiere",
                column: "ApprovisionnementMatiere_UniteID");

            migrationBuilder.CreateIndex(
                name: "IX_Approvisionnement_ProduitConsomable_ApprovisionnementProduit_PointVenteID",
                table: "Approvisionnement_ProduitConsomable",
                column: "ApprovisionnementProduit_PointVenteID");

            migrationBuilder.CreateIndex(
                name: "IX_Approvisionnement_ProduitConsomable_ApprovisionnementProduit_ProduitStockageId",
                table: "Approvisionnement_ProduitConsomable",
                column: "ApprovisionnementProduit_ProduitStockageId");

            migrationBuilder.CreateIndex(
                name: "IX_Approvisionnement_ProduitConsomable_ApprovisionnementProduit_StockID",
                table: "Approvisionnement_ProduitConsomable",
                column: "ApprovisionnementProduit_StockID");

            migrationBuilder.CreateIndex(
                name: "IX_Approvisionnement_ProduitConsomable_ApprovisionnementProduit_UniteMesureId",
                table: "Approvisionnement_ProduitConsomable",
                column: "ApprovisionnementProduit_UniteMesureId");

            migrationBuilder.CreateIndex(
                name: "IX_Approvisionnement_ProduitPackage_ApprovisionnementProduitPackage_AtelierID",
                table: "Approvisionnement_ProduitPackage",
                column: "ApprovisionnementProduitPackage_AtelierID");

            migrationBuilder.CreateIndex(
                name: "IX_Approvisionnement_ProduitPackage_ApprovisionnementProduitPackage_PointVenteID",
                table: "Approvisionnement_ProduitPackage",
                column: "ApprovisionnementProduitPackage_PointVenteID");

            migrationBuilder.CreateIndex(
                name: "IX_Approvisionnement_ProduitPackage_ApprovisionnementProduitPackage_ProduitpackAtelierId",
                table: "Approvisionnement_ProduitPackage",
                column: "ApprovisionnementProduitPackage_ProduitpackAtelierId");

            migrationBuilder.CreateIndex(
                name: "IX_Approvisionnement_ProduitPackage_ApprovisionnementProduitPackage__UniteMesureId",
                table: "Approvisionnement_ProduitPackage",
                column: "ApprovisionnementProduitPackage__UniteMesureId");

            migrationBuilder.CreateIndex(
                name: "IX_Article_BC_ArticleBC_BCID",
                table: "Article_BC",
                column: "ArticleBC_BCID");

            migrationBuilder.CreateIndex(
                name: "IX_Article_BC_ArticleBC_MatiereID",
                table: "Article_BC",
                column: "ArticleBC_MatiereID");

            migrationBuilder.CreateIndex(
                name: "IX_Article_BC_ArticleBC_UniteMesure",
                table: "Article_BC",
                column: "ArticleBC_UniteMesure");

            migrationBuilder.CreateIndex(
                name: "IX_Article_BL_ArticleBL_BonLivraisonID",
                table: "Article_BL",
                column: "ArticleBL_BonLivraisonID");

            migrationBuilder.CreateIndex(
                name: "IX_Article_BL_ArticleBL_MatiereID",
                table: "Article_BL",
                column: "ArticleBL_MatiereID");

            migrationBuilder.CreateIndex(
                name: "IX_Article_BL_ArticleBL_UniteMesureID",
                table: "Article_BL",
                column: "ArticleBL_UniteMesureID");

            migrationBuilder.CreateIndex(
                name: "IX_Atelier_Atelier_StockID",
                table: "Atelier",
                column: "Atelier_StockID");

            migrationBuilder.CreateIndex(
                name: "IX_Atelier_Atelier_VilleID",
                table: "Atelier",
                column: "Atelier_VilleID");

            migrationBuilder.CreateIndex(
                name: "IX_Atelier_User_Atelier_Id",
                table: "Atelier_User",
                column: "Atelier_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Atelier_User_User_Id",
                table: "Atelier_User",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Bon_De_Commande_BonDeCommande_AbonnementID",
                table: "Bon_De_Commande",
                column: "BonDeCommande_AbonnementID");

            migrationBuilder.CreateIndex(
                name: "IX_Bon_De_Commande_BonDeCommande_FournisseurID",
                table: "Bon_De_Commande",
                column: "BonDeCommande_FournisseurID");

            migrationBuilder.CreateIndex(
                name: "IX_Bon_De_Livraison_BonDeLivraison_AbonnementID",
                table: "Bon_De_Livraison",
                column: "BonDeLivraison_AbonnementID");

            migrationBuilder.CreateIndex(
                name: "IX_Bon_De_Livraison_BonDeLivraison_BCID",
                table: "Bon_De_Livraison",
                column: "BonDeLivraison_BCID");

            migrationBuilder.CreateIndex(
                name: "IX_Bon_De_Livraison_BonDeLivraison_FactureID",
                table: "Bon_De_Livraison",
                column: "BonDeLivraison_FactureID");

            migrationBuilder.CreateIndex(
                name: "IX_Bon_De_Livraison_BonDeLivraison_StatutID",
                table: "Bon_De_Livraison",
                column: "BonDeLivraison_StatutID");

            migrationBuilder.CreateIndex(
                name: "IX_Bon_Details_BonDeSortie_BonDeSortieID",
                table: "Bon_Details",
                column: "BonDeSortie_BonDeSortieID");

            migrationBuilder.CreateIndex(
                name: "IX_Bon_Details_BonDeSortie_MatiereId",
                table: "Bon_Details",
                column: "BonDeSortie_MatiereId");

            migrationBuilder.CreateIndex(
                name: "IX_Bon_Details_BonDeSortie_UniteMesureId",
                table: "Bon_Details",
                column: "BonDeSortie_UniteMesureId");

            migrationBuilder.CreateIndex(
                name: "IX_BonDe_Sortie_BonDeSortie_StockID",
                table: "BonDe_Sortie",
                column: "BonDeSortie_StockID");

            migrationBuilder.CreateIndex(
                name: "IX_Cloture_Journee_ClotueJournee_PositionVenteID",
                table: "Cloture_Journee",
                column: "ClotueJournee_PositionVenteID");

            migrationBuilder.CreateIndex(
                name: "IX_Cloture_Journee_ClotueJournee_UtilisateurID",
                table: "Cloture_Journee",
                column: "ClotueJournee_UtilisateurID");

            migrationBuilder.CreateIndex(
                name: "IX_Commande_Commande_CaissePayementId",
                table: "Commande",
                column: "Commande_CaissePayementId");

            migrationBuilder.CreateIndex(
                name: "IX_Commande_Commande_EtatDeLivraison",
                table: "Commande",
                column: "Commande_EtatDeLivraison");

            migrationBuilder.CreateIndex(
                name: "IX_Commande_Commande_EtatDePaiement",
                table: "Commande",
                column: "Commande_EtatDePaiement");

            migrationBuilder.CreateIndex(
                name: "IX_Commande_Commande_UtilisateurCommandeId",
                table: "Commande",
                column: "Commande_UtilisateurCommandeId");

            migrationBuilder.CreateIndex(
                name: "IX_Commande_Detail_CommandeDetail_CommandeId",
                table: "Commande_Detail",
                column: "CommandeDetail_CommandeId");

            migrationBuilder.CreateIndex(
                name: "IX_Commande_Detail_CommandeDetail_FormeProduitId",
                table: "Commande_Detail",
                column: "CommandeDetail_FormeProduitId");

            migrationBuilder.CreateIndex(
                name: "IX_Commande_Detail_CommandeDetail_UniteId",
                table: "Commande_Detail",
                column: "CommandeDetail_UniteId");

            migrationBuilder.CreateIndex(
                name: "IX_Commande_Paiement_CommandePaiement_CommandeID",
                table: "Commande_Paiement",
                column: "CommandePaiement_CommandeID");

            migrationBuilder.CreateIndex(
                name: "IX_Commande_Paiement_CommandePaiement_ModePaiementID",
                table: "Commande_Paiement",
                column: "CommandePaiement_ModePaiementID");

            migrationBuilder.CreateIndex(
                name: "IX_Commande_Paiement_CommandePaiement_PositionVenteID",
                table: "Commande_Paiement",
                column: "CommandePaiement_PositionVenteID");

            migrationBuilder.CreateIndex(
                name: "IX_Demande_Demande_AtelierID",
                table: "Demande",
                column: "Demande_AtelierID");

            migrationBuilder.CreateIndex(
                name: "IX_Demande_Demande_BonDeSortieID",
                table: "Demande",
                column: "Demande_BonDeSortieID");

            migrationBuilder.CreateIndex(
                name: "IX_Demande_Demande_LieuStockageID",
                table: "Demande",
                column: "Demande_LieuStockageID");

            migrationBuilder.CreateIndex(
                name: "IX_Demande_Demande_PlanificationID",
                table: "Demande",
                column: "Demande_PlanificationID");

            migrationBuilder.CreateIndex(
                name: "IX_Demande_Pret_DemandePret_AtelierID",
                table: "Demande_Pret",
                column: "DemandePret_AtelierID");

            migrationBuilder.CreateIndex(
                name: "IX_Demande_Pret_DemandePret_StockID",
                table: "Demande_Pret",
                column: "DemandePret_StockID");

            migrationBuilder.CreateIndex(
                name: "IX_DemandePret_Details_DemandePretDetails_DemandeID",
                table: "DemandePret_Details",
                column: "DemandePretDetails_DemandeID");

            migrationBuilder.CreateIndex(
                name: "IX_DemandePret_Details_DemandePretDetails_FormeID",
                table: "DemandePret_Details",
                column: "DemandePretDetails_FormeID");

            migrationBuilder.CreateIndex(
                name: "IX_DemandePret_Details_DemandePretDetails_ProduitID",
                table: "DemandePret_Details",
                column: "DemandePretDetails_ProduitID");

            migrationBuilder.CreateIndex(
                name: "IX_DemandePret_Details_DemandePretDetails_UniteMesureID",
                table: "DemandePret_Details",
                column: "DemandePretDetails_UniteMesureID");

            migrationBuilder.CreateIndex(
                name: "IX_DetailsProduitAppro_DetailsProduitAppro_ProduitApproID",
                table: "DetailsProduitAppro",
                column: "DetailsProduitAppro_ProduitApproID");

            migrationBuilder.CreateIndex(
                name: "IX_Echange_Produits_EchangeProduits_AdminID",
                table: "Echange_Produits",
                column: "EchangeProduits_AdminID");

            migrationBuilder.CreateIndex(
                name: "IX_Echange_Produits_EchangeProduits_PdvFournisseurID",
                table: "Echange_Produits",
                column: "EchangeProduits_PdvFournisseurID");

            migrationBuilder.CreateIndex(
                name: "IX_Echange_Produits_EchangeProduits_PdvRecepID",
                table: "Echange_Produits",
                column: "EchangeProduits_PdvRecepID");

            migrationBuilder.CreateIndex(
                name: "IX_EchangeProduit_Details_EchangeProduitDetails_EchangeID",
                table: "EchangeProduit_Details",
                column: "EchangeProduitDetails_EchangeID");

            migrationBuilder.CreateIndex(
                name: "IX_EchangeProduit_Details_EchangeProduitDetails_FromeID",
                table: "EchangeProduit_Details",
                column: "EchangeProduitDetails_FromeID");

            migrationBuilder.CreateIndex(
                name: "IX_EchangeProduit_Details_EchangeProduitDetails_UniteID",
                table: "EchangeProduit_Details",
                column: "EchangeProduitDetails_UniteID");

            migrationBuilder.CreateIndex(
                name: "IX_Facture_Facture_BonDeCommandeID",
                table: "Facture",
                column: "Facture_BonDeCommandeID");

            migrationBuilder.CreateIndex(
                name: "IX_Facture_Facture_FournisseurID",
                table: "Facture",
                column: "Facture_FournisseurID");

            migrationBuilder.CreateIndex(
                name: "IX_Fiche_Forme_FicheForme_FicheBridge",
                table: "Fiche_Forme",
                column: "FicheForme_FicheBridge");

            migrationBuilder.CreateIndex(
                name: "IX_Fiche_Forme_FicheForme_FormeProduit",
                table: "Fiche_Forme",
                column: "FicheForme_FormeProduit");

            migrationBuilder.CreateIndex(
                name: "IX_Fiche_Forme_FicheForme_uniteMesure",
                table: "Fiche_Forme",
                column: "FicheForme_uniteMesure");

            migrationBuilder.CreateIndex(
                name: "IX_FicheTech_ProduitBase_FicheTech_ID",
                table: "FicheTech_ProduitBase",
                column: "FicheTech_ID");

            migrationBuilder.CreateIndex(
                name: "IX_FicheTech_ProduitBase_ProduitBase_ID",
                table: "FicheTech_ProduitBase",
                column: "ProduitBase_ID");

            migrationBuilder.CreateIndex(
                name: "IX_FicheTech_ProduitBase_UniteMesure_ID",
                table: "FicheTech_ProduitBase",
                column: "UniteMesure_ID");

            migrationBuilder.CreateIndex(
                name: "IX_FicheTechnique_Bridge_FicheTechniqueBridge_ProduitVendableID",
                table: "FicheTechnique_Bridge",
                column: "FicheTechniqueBridge_ProduitVendableID");

            migrationBuilder.CreateIndex(
                name: "IX_FicheTechniqueProduitBase_FicheTechniqueProduitBase_ProduitBaseID",
                table: "FicheTechniqueProduitBase",
                column: "FicheTechniqueProduitBase_ProduitBaseID");

            migrationBuilder.CreateIndex(
                name: "IX_FicheTechniqueProduitBase_FicheTechniqueProduitBase_UniteMesureID",
                table: "FicheTechniqueProduitBase",
                column: "FicheTechniqueProduitBase_UniteMesureID");

            migrationBuilder.CreateIndex(
                name: "IX_Forme_Produit_FormeProduit_ProduitID",
                table: "Forme_Produit",
                column: "FormeProduit_ProduitID");

            migrationBuilder.CreateIndex(
                name: "IX_Forme_Produit_FormeProduit_ProduitPackageId",
                table: "Forme_Produit",
                column: "FormeProduit_ProduitPackageId");

            migrationBuilder.CreateIndex(
                name: "IX_Forme_Produit_FormeProduit_ProduitPretId",
                table: "Forme_Produit",
                column: "FormeProduit_ProduitPretId");

            migrationBuilder.CreateIndex(
                name: "IX_FormeDetails_FormeDetails_FormeProduitID",
                table: "FormeDetails",
                column: "FormeDetails_FormeProduitID");

            migrationBuilder.CreateIndex(
                name: "IX_FormeDetails_FormeDetails_PointVenteID",
                table: "FormeDetails",
                column: "FormeDetails_PointVenteID");

            migrationBuilder.CreateIndex(
                name: "IX_FormeDetails_FormeDetails_ProduitPackageID",
                table: "FormeDetails",
                column: "FormeDetails_ProduitPackageID");

            migrationBuilder.CreateIndex(
                name: "IX_Fournisseur_Contact_FournisseurContact_FonctionID",
                table: "Fournisseur_Contact",
                column: "FournisseurContact_FonctionID");

            migrationBuilder.CreateIndex(
                name: "IX_Fournisseur_Contact_FournisseurContact_FournisseurID",
                table: "Fournisseur_Contact",
                column: "FournisseurContact_FournisseurID");

            migrationBuilder.CreateIndex(
                name: "IX_Fournisseur_Contact_FournisseurContact_FournisseurProduitID",
                table: "Fournisseur_Contact",
                column: "FournisseurContact_FournisseurProduitID");

            migrationBuilder.CreateIndex(
                name: "IX_Fournisseur_ProduitConso_FournisseurProduits_Id",
                table: "Fournisseur_ProduitConso",
                column: "FournisseurProduits_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Fournisseur_ProduitConso_ProduitConsomable_Id",
                table: "Fournisseur_ProduitConso",
                column: "ProduitConsomable_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Fournisseur_Produits_FournisseurProduitConso_VilleID",
                table: "Fournisseur_Produits",
                column: "FournisseurProduitConso_VilleID");

            migrationBuilder.CreateIndex(
                name: "IX_FournisseurMatiere_Founisseur_Id",
                table: "FournisseurMatiere",
                column: "Founisseur_Id");

            migrationBuilder.CreateIndex(
                name: "IX_FournisseurMatiere_MatierePremiere_Id",
                table: "FournisseurMatiere",
                column: "MatierePremiere_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Gratuite_Gratuite_PositionVente",
                table: "Gratuite",
                column: "Gratuite_PositionVente");

            migrationBuilder.CreateIndex(
                name: "IX_GratuiteDetails_GratuiteDetails_FormeID",
                table: "GratuiteDetails",
                column: "GratuiteDetails_FormeID");

            migrationBuilder.CreateIndex(
                name: "IX_GratuiteDetails_GratuiteDetails_GratuiteID",
                table: "GratuiteDetails",
                column: "GratuiteDetails_GratuiteID");

            migrationBuilder.CreateIndex(
                name: "IX_GratuiteDetails_GratuiteDetails_UniteVenteID",
                table: "GratuiteDetails",
                column: "GratuiteDetails_UniteVenteID");

            migrationBuilder.CreateIndex(
                name: "IX_Matiere_Composants_MatiereComposants_MatiereID",
                table: "Matiere_Composants",
                column: "MatiereComposants_MatiereID");

            migrationBuilder.CreateIndex(
                name: "IX_Matiere_Famille_MatiereFamille_ParentID",
                table: "Matiere_Famille",
                column: "MatiereFamille_ParentID");

            migrationBuilder.CreateIndex(
                name: "IX_Matiere_Transfert_MatiereTrans_MatiereID",
                table: "Matiere_Transfert",
                column: "MatiereTrans_MatiereID");

            migrationBuilder.CreateIndex(
                name: "IX_Matiere_Transfert_MatiereTrans_StockID",
                table: "Matiere_Transfert",
                column: "MatiereTrans_StockID");

            migrationBuilder.CreateIndex(
                name: "IX_Matiere_Transfert_MatiereTrans_TransferID",
                table: "Matiere_Transfert",
                column: "MatiereTrans_TransferID");

            migrationBuilder.CreateIndex(
                name: "IX_Matiere_Transfert_MatiereTrans_UniteID",
                table: "Matiere_Transfert",
                column: "MatiereTrans_UniteID");

            migrationBuilder.CreateIndex(
                name: "IX_matiereStock_Atelier_MatiereStockAtelier_AtelierID",
                table: "matiereStock_Atelier",
                column: "MatiereStockAtelier_AtelierID");

            migrationBuilder.CreateIndex(
                name: "IX_matiereStock_Atelier_MatiereStockAtelier_MatierePremiereID",
                table: "matiereStock_Atelier",
                column: "MatiereStockAtelier_MatierePremiereID");

            migrationBuilder.CreateIndex(
                name: "IX_MatPrem_Allergene_AllergenID",
                table: "MatPrem_Allergene",
                column: "AllergenID");

            migrationBuilder.CreateIndex(
                name: "IX_MatPrem_Allergene_MatiereID",
                table: "MatPrem_Allergene",
                column: "MatiereID");

            migrationBuilder.CreateIndex(
                name: "IX_Mouvement_ProduitsConso_MouvementProduitsConso_FournisseurProduitId",
                table: "Mouvement_ProduitsConso",
                column: "MouvementProduitsConso_FournisseurProduitId");

            migrationBuilder.CreateIndex(
                name: "IX_Mouvement_ProduitsConso_MouvementProduitsConso_MouvementType",
                table: "Mouvement_ProduitsConso",
                column: "MouvementProduitsConso_MouvementType");

            migrationBuilder.CreateIndex(
                name: "IX_Mouvement_ProduitsConso_MouvementProduitsConso_ProduitConsoId",
                table: "Mouvement_ProduitsConso",
                column: "MouvementProduitsConso_ProduitConsoId");

            migrationBuilder.CreateIndex(
                name: "IX_Mouvement_ProduitsConso_MouvementProduitsConso_StockFournisseurId",
                table: "Mouvement_ProduitsConso",
                column: "MouvementProduitsConso_StockFournisseurId");

            migrationBuilder.CreateIndex(
                name: "IX_Mouvement_ProduitsConso_MouvementProduitsConso_UniteMesureId",
                table: "Mouvement_ProduitsConso",
                column: "MouvementProduitsConso_UniteMesureId");

            migrationBuilder.CreateIndex(
                name: "IX_Mouvement_Stock_MouvementStock_DestinationStockId",
                table: "Mouvement_Stock",
                column: "MouvementStock_DestinationStockId");

            migrationBuilder.CreateIndex(
                name: "IX_Mouvement_Stock_MouvementStock_FournisseurId",
                table: "Mouvement_Stock",
                column: "MouvementStock_FournisseurId");

            migrationBuilder.CreateIndex(
                name: "IX_Mouvement_Stock_MouvementStock_MatierePremiereStokageId",
                table: "Mouvement_Stock",
                column: "MouvementStock_MatierePremiereStokageId");

            migrationBuilder.CreateIndex(
                name: "IX_Mouvement_Stock_MouvementStock_MouvementTypeId",
                table: "Mouvement_Stock",
                column: "MouvementStock_MouvementTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Mouvement_Stock_MouvementStock_UniteMesureId",
                table: "Mouvement_Stock",
                column: "MouvementStock_UniteMesureId");

            migrationBuilder.CreateIndex(
                name: "IX_Mouvements_Caisse_MouvementsCaisse_PositionVenteID",
                table: "Mouvements_Caisse",
                column: "MouvementsCaisse_PositionVenteID");

            migrationBuilder.CreateIndex(
                name: "IX_Mouvements_Caisse_MouvementsCaisse_TypeID",
                table: "Mouvements_Caisse",
                column: "MouvementsCaisse_TypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Package_Forme_PackageForme_FormeProduitID",
                table: "Package_Forme",
                column: "PackageForme_FormeProduitID");

            migrationBuilder.CreateIndex(
                name: "IX_Package_Forme_PackageForme_ProduitPackageID",
                table: "Package_Forme",
                column: "PackageForme_ProduitPackageID");

            migrationBuilder.CreateIndex(
                name: "IX_Package_Production_PackageProduction_ProduitID",
                table: "Package_Production",
                column: "PackageProduction_ProduitID");

            migrationBuilder.CreateIndex(
                name: "IX_Package_Production_PackageProduction_ProduitPackageID",
                table: "Package_Production",
                column: "PackageProduction_ProduitPackageID");

            migrationBuilder.CreateIndex(
                name: "IX_PackageForme_Details_PackageFormeDetails_PackageFormeID",
                table: "PackageForme_Details",
                column: "PackageFormeDetails_PackageFormeID");

            migrationBuilder.CreateIndex(
                name: "IX_PackageForme_Details_PackageFormeDetails_SousProduitID",
                table: "PackageForme_Details",
                column: "PackageFormeDetails_SousProduitID");

            migrationBuilder.CreateIndex(
                name: "IX_PackageFormeDetailsMatiere_PackageFormeDetailsMatiere_PackageFormeID",
                table: "PackageFormeDetailsMatiere",
                column: "PackageFormeDetailsMatiere_PackageFormeID");

            migrationBuilder.CreateIndex(
                name: "IX_PackageFormeDetailsMatiere_PackageFormeDetailsMatiere_SousMatiereID",
                table: "PackageFormeDetailsMatiere",
                column: "PackageFormeDetailsMatiere_SousMatiereID");

            migrationBuilder.CreateIndex(
                name: "IX_PackageFormeDetailsMatiere_PackageFormeDetailsMatiere_UniteMesureID",
                table: "PackageFormeDetailsMatiere",
                column: "PackageFormeDetailsMatiere_UniteMesureID");

            migrationBuilder.CreateIndex(
                name: "IX_Payement_Facture_PayementFacture_FactureID",
                table: "Payement_Facture",
                column: "PayementFacture_FactureID");

            migrationBuilder.CreateIndex(
                name: "IX_PdV_ProduitsPret_PdV_ProduitsPret_FormeProduitId",
                table: "PdV_ProduitsPret",
                column: "PdV_ProduitsPret_FormeProduitId");

            migrationBuilder.CreateIndex(
                name: "IX_PdV_ProduitsPret_PdV_ProduitsPret_PointVenteId",
                table: "PdV_ProduitsPret",
                column: "PdV_ProduitsPret_PointVenteId");

            migrationBuilder.CreateIndex(
                name: "IX_PdV_ProduitsPret_PdV_ProduitsPret_ProduitConsomableId",
                table: "PdV_ProduitsPret",
                column: "PdV_ProduitsPret_ProduitConsomableId");

            migrationBuilder.CreateIndex(
                name: "IX_Perte_Perte_PositionVenteID",
                table: "Perte",
                column: "Perte_PositionVenteID");

            migrationBuilder.CreateIndex(
                name: "IX_Perte_Details_PerteDetails_FormeID",
                table: "Perte_Details",
                column: "PerteDetails_FormeID");

            migrationBuilder.CreateIndex(
                name: "IX_Perte_Details_PerteDetails_PerteID",
                table: "Perte_Details",
                column: "PerteDetails_PerteID");

            migrationBuilder.CreateIndex(
                name: "IX_Perte_Details_PerteDetails_UniteVenteID",
                table: "Perte_Details",
                column: "PerteDetails_UniteVenteID");

            migrationBuilder.CreateIndex(
                name: "IX_Perte_Matiere_PerteMatiere_AtelierID",
                table: "Perte_Matiere",
                column: "PerteMatiere_AtelierID");

            migrationBuilder.CreateIndex(
                name: "IX_Perte_Matiere_PerteMatiere_MatierePremiereStockageID",
                table: "Perte_Matiere",
                column: "PerteMatiere_MatierePremiereStockageID");

            migrationBuilder.CreateIndex(
                name: "IX_Perte_Matiere_PerteMatiere_UniteMesureID",
                table: "Perte_Matiere",
                column: "PerteMatiere_UniteMesureID");

            migrationBuilder.CreateIndex(
                name: "IX_Perte_MatiereStock_PerteMatiere_MatierePremiereStockageID",
                table: "Perte_MatiereStock",
                column: "PerteMatiere_MatierePremiereStockageID");

            migrationBuilder.CreateIndex(
                name: "IX_Perte_MatiereStock_PerteMatiere_StockID",
                table: "Perte_MatiereStock",
                column: "PerteMatiere_StockID");

            migrationBuilder.CreateIndex(
                name: "IX_Perte_MatiereStock_PerteMatiere_UniteMesureID",
                table: "Perte_MatiereStock",
                column: "PerteMatiere_UniteMesureID");

            migrationBuilder.CreateIndex(
                name: "IX_Perte_MatiereStock_PerteMatiere_Utilisateur",
                table: "Perte_MatiereStock",
                column: "PerteMatiere_Utilisateur");

            migrationBuilder.CreateIndex(
                name: "IX_Perte_Pret_PertePret_AtelierID",
                table: "Perte_Pret",
                column: "PertePret_AtelierID");

            migrationBuilder.CreateIndex(
                name: "IX_Perte_Pret_PertePret_ProduitPretAtelierID",
                table: "Perte_Pret",
                column: "PertePret_ProduitPretAtelierID");

            migrationBuilder.CreateIndex(
                name: "IX_Perte_Pret_PertePret_UniteMesureID",
                table: "Perte_Pret",
                column: "PertePret_UniteMesureID");

            migrationBuilder.CreateIndex(
                name: "IX_Planification_Journee_PlanificationJournee_AtelierID",
                table: "Planification_Journee",
                column: "PlanificationJournee_AtelierID");

            migrationBuilder.CreateIndex(
                name: "IX_Planification_Journee_PlanificationJournee_BonDeSortieID",
                table: "Planification_Journee",
                column: "PlanificationJournee_BonDeSortieID");

            migrationBuilder.CreateIndex(
                name: "IX_Planification_Journee_PlanificationJournee_LieuStockageID",
                table: "Planification_Journee",
                column: "PlanificationJournee_LieuStockageID");

            migrationBuilder.CreateIndex(
                name: "IX_Planification_ProdBase_PlanificationProdBase_PlanificationID",
                table: "Planification_ProdBase",
                column: "PlanificationProdBase_PlanificationID");

            migrationBuilder.CreateIndex(
                name: "IX_Planification_ProdBase_PlanificationProdBase_ProdBaseID",
                table: "Planification_ProdBase",
                column: "PlanificationProdBase_ProdBaseID");

            migrationBuilder.CreateIndex(
                name: "IX_Planification_Production_PlanificationProduction_FormeProduitId",
                table: "Planification_Production",
                column: "PlanificationProduction_FormeProduitId");

            migrationBuilder.CreateIndex(
                name: "IX_Planification_Production_PlanificationProduction_PlanificationJourneeID",
                table: "Planification_Production",
                column: "PlanificationProduction_PlanificationJourneeID");

            migrationBuilder.CreateIndex(
                name: "IX_Planification_Production_PlanificationProduction_ProduitVendableId",
                table: "Planification_Production",
                column: "PlanificationProduction_ProduitVendableId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanificationdeProductionBase_PlanificationProductionBase_PlanificationJourneeID",
                table: "PlanificationdeProductionBase",
                column: "PlanificationProductionBase_PlanificationJourneeID");

            migrationBuilder.CreateIndex(
                name: "IX_PlanificationdeProductionBase_PlanificationProductionBase_ProduitBase",
                table: "PlanificationdeProductionBase",
                column: "PlanificationProductionBase_ProduitBase");

            migrationBuilder.CreateIndex(
                name: "IX_PlanificationJourneeBase_PlanificationJourneeBase_AtelierID",
                table: "PlanificationJourneeBase",
                column: "PlanificationJourneeBase_AtelierID");

            migrationBuilder.CreateIndex(
                name: "IX_PlanificationJourneeBase_PlanificationJourneeBase_BonDeSortieID",
                table: "PlanificationJourneeBase",
                column: "PlanificationJourneeBase_BonDeSortieID");

            migrationBuilder.CreateIndex(
                name: "IX_PlanificationJourneeBase_PlanificationJourneeBase_LieuStockageID",
                table: "PlanificationJourneeBase",
                column: "PlanificationJourneeBase_LieuStockageID");

            migrationBuilder.CreateIndex(
                name: "IX_PointProduction_Famille_FamilleProduit_ID",
                table: "PointProduction_Famille",
                column: "FamilleProduit_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PointProduction_Famille_PointProduction_ID",
                table: "PointProduction_Famille",
                column: "PointProduction_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PointVente_Famille_FamilleProduit_Id",
                table: "PointVente_Famille",
                column: "FamilleProduit_Id");

            migrationBuilder.CreateIndex(
                name: "IX_PointVente_Famille_PointVente_Id",
                table: "PointVente_Famille",
                column: "PointVente_Id");

            migrationBuilder.CreateIndex(
                name: "IX_PointVente_Stock_PointVenteStock_FormeProduitID",
                table: "PointVente_Stock",
                column: "PointVenteStock_FormeProduitID");

            migrationBuilder.CreateIndex(
                name: "IX_PointVente_Stock_PointVenteStock_PointVenteID",
                table: "PointVente_Stock",
                column: "PointVenteStock_PointVenteID");

            migrationBuilder.CreateIndex(
                name: "IX_PointVente_Stock_PointVenteStock_ProduitID",
                table: "PointVente_Stock",
                column: "PointVenteStock_ProduitID");

            migrationBuilder.CreateIndex(
                name: "IX_PointVente_User_PointVente_Id",
                table: "PointVente_User",
                column: "PointVente_Id");

            migrationBuilder.CreateIndex(
                name: "IX_PointVente_User_User_Id",
                table: "PointVente_User",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Position_Vente_PositionVente_ModePaiementId",
                table: "Position_Vente",
                column: "PositionVente_ModePaiementId");

            migrationBuilder.CreateIndex(
                name: "IX_Position_Vente_PositionVente_PointVenteId",
                table: "Position_Vente",
                column: "PositionVente_PointVenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Prix_Produit_PrixProduit_FormeProduitId",
                table: "Prix_Produit",
                column: "PrixProduit_FormeProduitId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdBase_Atelier_ProdBase_Atelier_AtelierID",
                table: "ProdBase_Atelier",
                column: "ProdBase_Atelier_AtelierID");

            migrationBuilder.CreateIndex(
                name: "IX_ProdBase_Atelier_ProdBase_Atelier_ProduitID",
                table: "ProdBase_Atelier",
                column: "ProdBase_Atelier_ProduitID");

            migrationBuilder.CreateIndex(
                name: "IX_Production_Details_ProductionDetails_FormeID",
                table: "Production_Details",
                column: "ProductionDetails_FormeID");

            migrationBuilder.CreateIndex(
                name: "IX_Production_Details_ProductionDetails_ProductionID",
                table: "Production_Details",
                column: "ProductionDetails_ProductionID");

            migrationBuilder.CreateIndex(
                name: "IX_Produit_Composants_ProduitComposant_ProduitID",
                table: "Produit_Composants",
                column: "ProduitComposant_ProduitID");

            migrationBuilder.CreateIndex(
                name: "IX_Produit_PretConsomer_ProduitPretConsomer_FamilleProduit",
                table: "Produit_PretConsomer",
                column: "ProduitPretConsomer_FamilleProduit");

            migrationBuilder.CreateIndex(
                name: "IX_Produit_PretConsomer_ProduitPretConsomer_FormeStockageId",
                table: "Produit_PretConsomer",
                column: "ProduitPretConsomer_FormeStockageId");

            migrationBuilder.CreateIndex(
                name: "IX_Produit_PretConsomer_ProduitPretConsomer_UniteMesureAchatId",
                table: "Produit_PretConsomer",
                column: "ProduitPretConsomer_UniteMesureAchatId");

            migrationBuilder.CreateIndex(
                name: "IX_ProduitAppro_ProduitAppro_AtelierID",
                table: "ProduitAppro",
                column: "ProduitAppro_AtelierID");

            migrationBuilder.CreateIndex(
                name: "IX_ProduitAppro_ProduitAppro_FormeProduitID",
                table: "ProduitAppro",
                column: "ProduitAppro_FormeProduitID");

            migrationBuilder.CreateIndex(
                name: "IX_ProduitAppro_ProduitAppro_ProduitID",
                table: "ProduitAppro",
                column: "ProduitAppro_ProduitID");

            migrationBuilder.CreateIndex(
                name: "IX_ProduitBase_ProduitBase_FormeStockageID",
                table: "ProduitBase",
                column: "ProduitBase_FormeStockageID");

            migrationBuilder.CreateIndex(
                name: "IX_ProduitBase_ProduitBase_UniteMesureID",
                table: "ProduitBase",
                column: "ProduitBase_UniteMesureID");

            migrationBuilder.CreateIndex(
                name: "IX_ProduitBaseFicheTechnique_ProduitBaseFicheTechnique_FicheTehcniqueProduitBaseID",
                table: "ProduitBaseFicheTechnique",
                column: "ProduitBaseFicheTechnique_FicheTehcniqueProduitBaseID");

            migrationBuilder.CreateIndex(
                name: "IX_ProduitBaseFicheTechnique_ProduitBaseFicheTechnique_MatierePremiereID",
                table: "ProduitBaseFicheTechnique",
                column: "ProduitBaseFicheTechnique_MatierePremiereID");

            migrationBuilder.CreateIndex(
                name: "IX_ProduitBaseFicheTechnique_ProduitBaseFicheTechnique_UniteMesureID",
                table: "ProduitBaseFicheTechnique",
                column: "ProduitBaseFicheTechnique_UniteMesureID");

            migrationBuilder.CreateIndex(
                name: "IX_ProduitConsomable_Stokage_ProduitConsomableStokage_FormeProduitId",
                table: "ProduitConsomable_Stokage",
                column: "ProduitConsomableStokage_FormeProduitId");

            migrationBuilder.CreateIndex(
                name: "IX_ProduitConsomable_Stokage_ProduitConsomableStokage_LieuStockID",
                table: "ProduitConsomable_Stokage",
                column: "ProduitConsomableStokage_LieuStockID");

            migrationBuilder.CreateIndex(
                name: "IX_ProduitConsomable_Stokage_ProduitConsomableStokage_ProduitVendableId",
                table: "ProduitConsomable_Stokage",
                column: "ProduitConsomableStokage_ProduitVendableId");

            migrationBuilder.CreateIndex(
                name: "IX_ProduitPack_Atelier_ProduitPackAtelier_AtelierID",
                table: "ProduitPack_Atelier",
                column: "ProduitPackAtelier_AtelierID");

            migrationBuilder.CreateIndex(
                name: "IX_ProduitPack_Atelier_ProduitPackAtelier_FormeID",
                table: "ProduitPack_Atelier",
                column: "ProduitPackAtelier_FormeID");

            migrationBuilder.CreateIndex(
                name: "IX_ProduitPack_Atelier_ProduitPackAtelier_ProduitPackID",
                table: "ProduitPack_Atelier",
                column: "ProduitPackAtelier_ProduitPackID");

            migrationBuilder.CreateIndex(
                name: "IX_ProduitPackage_ProduitPackage_FamilleID",
                table: "ProduitPackage",
                column: "ProduitPackage_FamilleID");

            migrationBuilder.CreateIndex(
                name: "IX_ProduitPackage_ProduitPackage_UniteVente",
                table: "ProduitPackage",
                column: "ProduitPackage_UniteVente");

            migrationBuilder.CreateIndex(
                name: "IX_ProduitPret_Atelier_ProduitPretAtelier_AtelierID",
                table: "ProduitPret_Atelier",
                column: "ProduitPretAtelier_AtelierID");

            migrationBuilder.CreateIndex(
                name: "IX_ProduitPret_Atelier_ProduitPretAtelier_FormeID",
                table: "ProduitPret_Atelier",
                column: "ProduitPretAtelier_FormeID");

            migrationBuilder.CreateIndex(
                name: "IX_ProduitPret_Atelier_ProduitPretAtelier__ProduitID",
                table: "ProduitPret_Atelier",
                column: "ProduitPretAtelier__ProduitID");

            migrationBuilder.CreateIndex(
                name: "IX_Reception_Stock_ReceptionStock_AtelierID",
                table: "Reception_Stock",
                column: "ReceptionStock_AtelierID");

            migrationBuilder.CreateIndex(
                name: "IX_Reception_Stock_ReceptionStock_MatiereID",
                table: "Reception_Stock",
                column: "ReceptionStock_MatiereID");

            migrationBuilder.CreateIndex(
                name: "IX_Reception_Stock_ReceptionStock_StockID",
                table: "Reception_Stock",
                column: "ReceptionStock_StockID");

            migrationBuilder.CreateIndex(
                name: "IX_Reception_Stock_ReceptionStock_UniteID",
                table: "Reception_Stock",
                column: "ReceptionStock_UniteID");

            migrationBuilder.CreateIndex(
                name: "IX_Retour_Details_RetourDetails_FormeID",
                table: "Retour_Details",
                column: "RetourDetails_FormeID");

            migrationBuilder.CreateIndex(
                name: "IX_Retour_Details_RetourDetails_RetourID",
                table: "Retour_Details",
                column: "RetourDetails_RetourID");

            migrationBuilder.CreateIndex(
                name: "IX_Retour_Details_RetourDetails_UniteVente",
                table: "Retour_Details",
                column: "RetourDetails_UniteVente");

            migrationBuilder.CreateIndex(
                name: "IX_Retour_Produits_Retour_PositionVenteID",
                table: "Retour_Produits",
                column: "Retour_PositionVenteID");

            migrationBuilder.CreateIndex(
                name: "IX_Retour_Stock_RetourStok_BonDeSortieID",
                table: "Retour_Stock",
                column: "RetourStok_BonDeSortieID");

            migrationBuilder.CreateIndex(
                name: "IX_Retour_Stock_RetourStok_PlanificationID",
                table: "Retour_Stock",
                column: "RetourStok_PlanificationID");

            migrationBuilder.CreateIndex(
                name: "IX_Retrait_Caisse_RetraitCaisse_PositionVenteID",
                table: "Retrait_Caisse",
                column: "RetraitCaisse_PositionVenteID");

            migrationBuilder.CreateIndex(
                name: "IX_Retrait_Caisse_RetraitCaisse_TypeRetraitID",
                table: "Retrait_Caisse",
                column: "RetraitCaisse_TypeRetraitID");

            migrationBuilder.CreateIndex(
                name: "IX_Retrait_Caisse_RetraitCaisse_UtilisateurID",
                table: "Retrait_Caisse",
                column: "RetraitCaisse_UtilisateurID");

            migrationBuilder.CreateIndex(
                name: "IX_Salle_Salle_PositionVenteId",
                table: "Salle",
                column: "Salle_PositionVenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Sous_Famille_SousFamille_ParentID",
                table: "Sous_Famille",
                column: "SousFamille_ParentID");

            migrationBuilder.CreateIndex(
                name: "IX_Sous_ProduitPackage_SousProduitPackage_FormeProduittID",
                table: "Sous_ProduitPackage",
                column: "SousProduitPackage_FormeProduittID");

            migrationBuilder.CreateIndex(
                name: "IX_Sous_ProduitPackage_SousProduitPackage_ProduitPackageID",
                table: "Sous_ProduitPackage",
                column: "SousProduitPackage_ProduitPackageID");

            migrationBuilder.CreateIndex(
                name: "IX_SousMatierePackage_SousMatierePackage_MatiereID",
                table: "SousMatierePackage",
                column: "SousMatierePackage_MatiereID");

            migrationBuilder.CreateIndex(
                name: "IX_SousMatierePackage_SousMatierePackage_ProduitPackageID",
                table: "SousMatierePackage",
                column: "SousMatierePackage_ProduitPackageID");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_Achat_StockAchat_MatiereID",
                table: "Stock_Achat",
                column: "StockAchat_MatiereID");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_Achat_StockAchat_UniteMesureID",
                table: "Stock_Achat",
                column: "StockAchat_UniteMesureID");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_User_Stock_Id",
                table: "Stock_User",
                column: "Stock_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_User_User_Id",
                table: "Stock_User",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Table_Table_SalleId",
                table: "Table",
                column: "Table_SalleId");

            migrationBuilder.CreateIndex(
                name: "IX_Tva_commande_ID",
                table: "Tva",
                column: "commande_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Tva_vente_ID",
                table: "Tva",
                column: "vente_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Unite_MesureMatiere_MatierePremiere_Id",
                table: "Unite_MesureMatiere",
                column: "MatierePremiere_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Unite_MesureMatiere_Unite_Id",
                table: "Unite_MesureMatiere",
                column: "Unite_Id");

            migrationBuilder.CreateIndex(
                name: "IX_UniteMesure_ProdBase_ProduitBase_ID",
                table: "UniteMesure_ProdBase",
                column: "ProduitBase_ID");

            migrationBuilder.CreateIndex(
                name: "IX_UniteMesure_ProdBase_UniteMesure_ID",
                table: "UniteMesure_ProdBase",
                column: "UniteMesure_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Vente_Vente_ModePaiement",
                table: "Vente",
                column: "Vente_ModePaiement");

            migrationBuilder.CreateIndex(
                name: "IX_Vente_Vente_PointVenteId",
                table: "Vente",
                column: "Vente_PointVenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Vente_Vente_PositionVenteId",
                table: "Vente",
                column: "Vente_PositionVenteId");

            migrationBuilder.CreateIndex(
                name: "IX_VenteDetails_VenteDetails_FormeProduitId",
                table: "VenteDetails",
                column: "VenteDetails_FormeProduitId");

            migrationBuilder.CreateIndex(
                name: "IX_VenteDetails_VenteDetails_UniteId",
                table: "VenteDetails",
                column: "VenteDetails_UniteId");

            migrationBuilder.CreateIndex(
                name: "IX_VenteDetails_VenteDetails_VentId",
                table: "VenteDetails",
                column: "VenteDetails_VentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Atelier_AtelierID",
                table: "AspNetUsers",
                column: "AtelierID",
                principalTable: "Atelier",
                principalColumn: "Atelier_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Lieu_Stockage_LieuStockage_ID",
                table: "AspNetUsers",
                column: "LieuStockage_ID",
                principalTable: "Lieu_Stockage",
                principalColumn: "LieuStockag_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Point_Vente_PointVente_ID",
                table: "AspNetUsers",
                column: "PointVente_ID",
                principalTable: "Point_Vente",
                principalColumn: "PointVente_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Position_Vente_PositionVente_ID",
                table: "AspNetUsers",
                column: "PositionVente_ID",
                principalTable: "Position_Vente",
                principalColumn: "PositionVente_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Founisseur_Ville_Founisseur_VilleID",
                table: "Founisseur",
                column: "Founisseur_VilleID",
                principalTable: "Ville",
                principalColumn: "Ville_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lieu_Stockage_Ville_LieuStockag_VilleID",
                table: "Lieu_Stockage",
                column: "LieuStockag_VilleID",
                principalTable: "Ville",
                principalColumn: "Ville_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Matiere_Premiere_Unite_Mesure_MatierePremiere_AchatUniteMesureId",
                table: "Matiere_Premiere",
                column: "MatierePremiere_AchatUniteMesureId",
                principalTable: "Unite_Mesure",
                principalColumn: "UniteMesure_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Matiere_Premiere_Taux_TVA_MatierePremiere_CoutTVAID",
                table: "Matiere_Premiere",
                column: "MatierePremiere_CoutTVAID",
                principalTable: "Taux_TVA",
                principalColumn: "TauxTVA_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Matiere_Premiere_Matiere_Famille_MatierePremiere_FamilleID",
                table: "Matiere_Premiere",
                column: "MatierePremiere_FamilleID",
                principalTable: "Matiere_Famille",
                principalColumn: "MatiereFamille_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Matiere_Premiere_Forme_Stockage_MatierePremiere_FormeStockageId",
                table: "Matiere_Premiere",
                column: "MatierePremiere_FormeStockageId",
                principalTable: "Forme_Stockage",
                principalColumn: "FormStockage_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MatierePremiere_Stokage_Matiere_Premiere_MatierePremiereStokage_MatierePremiereId",
                table: "MatierePremiere_Stokage",
                column: "MatierePremiereStokage_MatierePremiereId",
                principalTable: "Matiere_Premiere",
                principalColumn: "MatierePremiere_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MatierePremiere_Stokage_Section_Stockage_MatierePremiereStokage_SectionStockageId",
                table: "MatierePremiere_Stokage",
                column: "MatierePremiereStokage_SectionStockageId",
                principalTable: "Section_Stockage",
                principalColumn: "Section_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Paiement_Abonnement_Abonnement_Client_PaiementAbonnement_AbonnementId",
                table: "Paiement_Abonnement",
                column: "PaiementAbonnement_AbonnementId",
                principalTable: "Abonnement_Client",
                principalColumn: "Abonnement_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Paiement_Abonnement_Point_Vente_PaiementAbonnement_PointVenteID",
                table: "Paiement_Abonnement",
                column: "PaiementAbonnement_PointVenteID",
                principalTable: "Point_Vente",
                principalColumn: "PointVente_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Point_Vente_Atelier_PointVente_AtelierID",
                table: "Point_Vente",
                column: "PointVente_AtelierID",
                principalTable: "Atelier",
                principalColumn: "Atelier_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Point_Vente_Lieu_Stockage_PointVente_StockID",
                table: "Point_Vente",
                column: "PointVente_StockID",
                principalTable: "Lieu_Stockage",
                principalColumn: "LieuStockag_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Point_Vente_Ville_PointVente_VilleID",
                table: "Point_Vente",
                column: "PointVente_VilleID",
                principalTable: "Ville",
                principalColumn: "Ville_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Produit_FicheTechnique_FicheTechnique_Bridge_FicheTechnique_FicheTechniqueBridgeID",
                table: "Produit_FicheTechnique",
                column: "FicheTechnique_FicheTechniqueBridgeID",
                principalTable: "FicheTechnique_Bridge",
                principalColumn: "FicheTechniqueBridge_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Produit_FicheTechnique_Matiere_Premiere_FicheTechnique_MatierePremiereId",
                table: "Produit_FicheTechnique",
                column: "FicheTechnique_MatierePremiereId",
                principalTable: "Matiere_Premiere",
                principalColumn: "MatierePremiere_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Produit_FicheTechnique_Produit_Vendable_FicheTechnique_ProduitVendableId",
                table: "Produit_FicheTechnique",
                column: "FicheTechnique_ProduitVendableId",
                principalTable: "Produit_Vendable",
                principalColumn: "ProduitVendable_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Produit_FicheTechnique_Unite_Mesure_FicheTechnique_UniteMesureId",
                table: "Produit_FicheTechnique",
                column: "FicheTechnique_UniteMesureId",
                principalTable: "Unite_Mesure",
                principalColumn: "UniteMesure_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Produit_Vendable_Sous_Famille_ProduitVendable_FamilleProduitId",
                table: "Produit_Vendable",
                column: "ProduitVendable_FamilleProduitId",
                principalTable: "Sous_Famille",
                principalColumn: "SousFamille_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Produit_Vendable_Forme_Stockage_ProduitVendable_FormStockageId",
                table: "Produit_Vendable",
                column: "ProduitVendable_FormStockageId",
                principalTable: "Forme_Stockage",
                principalColumn: "FormStockage_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Produit_Vendable_Taux_TVA_ProduitVendable_TvaId",
                table: "Produit_Vendable",
                column: "ProduitVendable_TvaId",
                principalTable: "Taux_TVA",
                principalColumn: "TauxTVA_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Produit_Vendable_Unite_Mesure_ProduitVendable_UniteMesureId",
                table: "Produit_Vendable",
                column: "ProduitVendable_UniteMesureId",
                principalTable: "Unite_Mesure",
                principalColumn: "UniteMesure_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Section_Stockage_Zone_Stockage_Section_ZoneStockageId",
                table: "Section_Stockage",
                column: "Section_ZoneStockageId",
                principalTable: "Zone_Stockage",
                principalColumn: "ZoneStockage_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Zone_Stockage_Forme_Stockage_ZoneStockage_FormeStockageId",
                table: "Zone_Stockage",
                column: "ZoneStockage_FormeStockageId",
                principalTable: "Forme_Stockage",
                principalColumn: "FormStockage_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Zone_Stockage_Lieu_Stockage_ZoneStockage_LieuStockageId",
                table: "Zone_Stockage",
                column: "ZoneStockage_LieuStockageId",
                principalTable: "Lieu_Stockage",
                principalColumn: "LieuStockag_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Zone_Stockage_Type_Contenu_ZoneStockage_TypeContenuId",
                table: "Zone_Stockage",
                column: "ZoneStockage_TypeContenuId",
                principalTable: "Type_Contenu",
                principalColumn: "TypeContenu_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Zone_Stockage_Unite_Mesure_ZoneStockage_UniteMesureId",
                table: "Zone_Stockage",
                column: "ZoneStockage_UniteMesureId",
                principalTable: "Unite_Mesure",
                principalColumn: "UniteMesure_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Atelier_AtelierID",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Lieu_Stockage_LieuStockage_ID",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Point_Vente_PointVente_ID",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Position_Vente_PositionVente_ID",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Founisseur_Ville_Founisseur_VilleID",
                table: "Founisseur");

            migrationBuilder.DropForeignKey(
                name: "FK_Lieu_Stockage_Ville_LieuStockag_VilleID",
                table: "Lieu_Stockage");

            migrationBuilder.DropForeignKey(
                name: "FK_Matiere_Premiere_Unite_Mesure_MatierePremiere_AchatUniteMesureId",
                table: "Matiere_Premiere");

            migrationBuilder.DropForeignKey(
                name: "FK_Matiere_Premiere_Taux_TVA_MatierePremiere_CoutTVAID",
                table: "Matiere_Premiere");

            migrationBuilder.DropForeignKey(
                name: "FK_Matiere_Premiere_Matiere_Famille_MatierePremiere_FamilleID",
                table: "Matiere_Premiere");

            migrationBuilder.DropForeignKey(
                name: "FK_Matiere_Premiere_Forme_Stockage_MatierePremiere_FormeStockageId",
                table: "Matiere_Premiere");

            migrationBuilder.DropForeignKey(
                name: "FK_MatierePremiere_Stokage_Matiere_Premiere_MatierePremiereStokage_MatierePremiereId",
                table: "MatierePremiere_Stokage");

            migrationBuilder.DropForeignKey(
                name: "FK_MatierePremiere_Stokage_Section_Stockage_MatierePremiereStokage_SectionStockageId",
                table: "MatierePremiere_Stokage");

            migrationBuilder.DropForeignKey(
                name: "FK_Paiement_Abonnement_Abonnement_Client_PaiementAbonnement_AbonnementId",
                table: "Paiement_Abonnement");

            migrationBuilder.DropForeignKey(
                name: "FK_Paiement_Abonnement_Point_Vente_PaiementAbonnement_PointVenteID",
                table: "Paiement_Abonnement");

            migrationBuilder.DropForeignKey(
                name: "FK_Point_Vente_Atelier_PointVente_AtelierID",
                table: "Point_Vente");

            migrationBuilder.DropForeignKey(
                name: "FK_Point_Vente_Lieu_Stockage_PointVente_StockID",
                table: "Point_Vente");

            migrationBuilder.DropForeignKey(
                name: "FK_Point_Vente_Ville_PointVente_VilleID",
                table: "Point_Vente");

            migrationBuilder.DropForeignKey(
                name: "FK_Produit_FicheTechnique_FicheTechnique_Bridge_FicheTechnique_FicheTechniqueBridgeID",
                table: "Produit_FicheTechnique");

            migrationBuilder.DropForeignKey(
                name: "FK_Produit_FicheTechnique_Matiere_Premiere_FicheTechnique_MatierePremiereId",
                table: "Produit_FicheTechnique");

            migrationBuilder.DropForeignKey(
                name: "FK_Produit_FicheTechnique_Produit_Vendable_FicheTechnique_ProduitVendableId",
                table: "Produit_FicheTechnique");

            migrationBuilder.DropForeignKey(
                name: "FK_Produit_FicheTechnique_Unite_Mesure_FicheTechnique_UniteMesureId",
                table: "Produit_FicheTechnique");

            migrationBuilder.DropForeignKey(
                name: "FK_Produit_Vendable_Sous_Famille_ProduitVendable_FamilleProduitId",
                table: "Produit_Vendable");

            migrationBuilder.DropForeignKey(
                name: "FK_Produit_Vendable_Forme_Stockage_ProduitVendable_FormStockageId",
                table: "Produit_Vendable");

            migrationBuilder.DropForeignKey(
                name: "FK_Produit_Vendable_Taux_TVA_ProduitVendable_TvaId",
                table: "Produit_Vendable");

            migrationBuilder.DropForeignKey(
                name: "FK_Produit_Vendable_Unite_Mesure_ProduitVendable_UniteMesureId",
                table: "Produit_Vendable");

            migrationBuilder.DropForeignKey(
                name: "FK_Section_Stockage_Zone_Stockage_Section_ZoneStockageId",
                table: "Section_Stockage");

            migrationBuilder.DropForeignKey(
                name: "FK_Zone_Stockage_Forme_Stockage_ZoneStockage_FormeStockageId",
                table: "Zone_Stockage");

            migrationBuilder.DropForeignKey(
                name: "FK_Zone_Stockage_Lieu_Stockage_ZoneStockage_LieuStockageId",
                table: "Zone_Stockage");

            migrationBuilder.DropForeignKey(
                name: "FK_Zone_Stockage_Type_Contenu_ZoneStockage_TypeContenuId",
                table: "Zone_Stockage");

            migrationBuilder.DropForeignKey(
                name: "FK_Zone_Stockage_Unite_Mesure_ZoneStockage_UniteMesureId",
                table: "Zone_Stockage");

            migrationBuilder.DropTable(
                name: "Affectation_Agent_Table");

            migrationBuilder.DropTable(
                name: "Allimentation_Caisse");

            migrationBuilder.DropTable(
                name: "Approvisionnement");

            migrationBuilder.DropTable(
                name: "Approvisionnement_Matiere");

            migrationBuilder.DropTable(
                name: "Approvisionnement_ProduitConsomable");

            migrationBuilder.DropTable(
                name: "Approvisionnement_ProduitPackage");

            migrationBuilder.DropTable(
                name: "Article_BC");

            migrationBuilder.DropTable(
                name: "Article_BL");

            migrationBuilder.DropTable(
                name: "Atelier_User");

            migrationBuilder.DropTable(
                name: "Bon_Details");

            migrationBuilder.DropTable(
                name: "Cloture_Journee");

            migrationBuilder.DropTable(
                name: "Commande_Detail");

            migrationBuilder.DropTable(
                name: "Commande_Paiement");

            migrationBuilder.DropTable(
                name: "Demande");

            migrationBuilder.DropTable(
                name: "DemandePret_Details");

            migrationBuilder.DropTable(
                name: "DetailsProduitAppro");

            migrationBuilder.DropTable(
                name: "Distributeur");

            migrationBuilder.DropTable(
                name: "EchangeProduit_Details");

            migrationBuilder.DropTable(
                name: "Fiche_Forme");

            migrationBuilder.DropTable(
                name: "FicheTech_ProduitBase");

            migrationBuilder.DropTable(
                name: "FormeDetails");

            migrationBuilder.DropTable(
                name: "Fournisseur_Contact");

            migrationBuilder.DropTable(
                name: "Fournisseur_ProduitConso");

            migrationBuilder.DropTable(
                name: "FournisseurMatiere");

            migrationBuilder.DropTable(
                name: "GratuiteDetails");

            migrationBuilder.DropTable(
                name: "LogoUser");

            migrationBuilder.DropTable(
                name: "Matiere_Composants");

            migrationBuilder.DropTable(
                name: "Matiere_Famille");

            migrationBuilder.DropTable(
                name: "Matiere_Transfert");

            migrationBuilder.DropTable(
                name: "MatPrem_Allergene");

            migrationBuilder.DropTable(
                name: "Mouvement_ProduitsConso");

            migrationBuilder.DropTable(
                name: "Mouvement_Stock");

            migrationBuilder.DropTable(
                name: "Mouvements_Caisse");

            migrationBuilder.DropTable(
                name: "PackageForme_Details");

            migrationBuilder.DropTable(
                name: "PackageFormeDetailsMatiere");

            migrationBuilder.DropTable(
                name: "Payement_Facture");

            migrationBuilder.DropTable(
                name: "PdV_ProduitsPret");

            migrationBuilder.DropTable(
                name: "Perte_Details");

            migrationBuilder.DropTable(
                name: "Perte_Matiere");

            migrationBuilder.DropTable(
                name: "Perte_MatiereStock");

            migrationBuilder.DropTable(
                name: "Perte_Pret");

            migrationBuilder.DropTable(
                name: "Planification_ProdBase");

            migrationBuilder.DropTable(
                name: "Planification_Production");

            migrationBuilder.DropTable(
                name: "PlanificationdeProductionBase");

            migrationBuilder.DropTable(
                name: "PointProduction_Famille");

            migrationBuilder.DropTable(
                name: "PointVente_Famille");

            migrationBuilder.DropTable(
                name: "PointVente_Stock");

            migrationBuilder.DropTable(
                name: "PointVente_User");

            migrationBuilder.DropTable(
                name: "Prix_Produit");

            migrationBuilder.DropTable(
                name: "ProdBase_Atelier");

            migrationBuilder.DropTable(
                name: "Production_Details");

            migrationBuilder.DropTable(
                name: "Produit_Composants");

            migrationBuilder.DropTable(
                name: "ProduitBaseFicheTechnique");

            migrationBuilder.DropTable(
                name: "Reception_Stock");

            migrationBuilder.DropTable(
                name: "Retour_Details");

            migrationBuilder.DropTable(
                name: "Retour_Stock");

            migrationBuilder.DropTable(
                name: "Retrait_Caisse");

            migrationBuilder.DropTable(
                name: "Stock_Achat");

            migrationBuilder.DropTable(
                name: "Stock_User");

            migrationBuilder.DropTable(
                name: "Taux_TVA");

            migrationBuilder.DropTable(
                name: "Tva");

            migrationBuilder.DropTable(
                name: "Unite_Categorie");

            migrationBuilder.DropTable(
                name: "Unite_MesureMatiere");

            migrationBuilder.DropTable(
                name: "UniteMesure_ProdBase");

            migrationBuilder.DropTable(
                name: "VenteDetails");

            migrationBuilder.DropTable(
                name: "Agent_Serveur");

            migrationBuilder.DropTable(
                name: "Table");

            migrationBuilder.DropTable(
                name: "ProduitPack_Atelier");

            migrationBuilder.DropTable(
                name: "Bon_De_Livraison");

            migrationBuilder.DropTable(
                name: "Demande_Pret");

            migrationBuilder.DropTable(
                name: "ProduitAppro");

            migrationBuilder.DropTable(
                name: "Echange_Produits");

            migrationBuilder.DropTable(
                name: "FicheTechnique_Bridge");

            migrationBuilder.DropTable(
                name: "Fonction");

            migrationBuilder.DropTable(
                name: "Fournisseur_Produits");

            migrationBuilder.DropTable(
                name: "Gratuite");

            migrationBuilder.DropTable(
                name: "MatiereFamille_Parent");

            migrationBuilder.DropTable(
                name: "Transfert_Matiere");

            migrationBuilder.DropTable(
                name: "ProduitConsomable_Stokage");

            migrationBuilder.DropTable(
                name: "Mouvement_Type");

            migrationBuilder.DropTable(
                name: "MouvementCaisse_Type");

            migrationBuilder.DropTable(
                name: "Sous_ProduitPackage");

            migrationBuilder.DropTable(
                name: "Package_Forme");

            migrationBuilder.DropTable(
                name: "SousMatierePackage");

            migrationBuilder.DropTable(
                name: "Perte");

            migrationBuilder.DropTable(
                name: "matiereStock_Atelier");

            migrationBuilder.DropTable(
                name: "ProduitPret_Atelier");

            migrationBuilder.DropTable(
                name: "PlanificationJourneeBase");

            migrationBuilder.DropTable(
                name: "Package_Production");

            migrationBuilder.DropTable(
                name: "FicheTechniqueProduitBase");

            migrationBuilder.DropTable(
                name: "Retour_Produits");

            migrationBuilder.DropTable(
                name: "Planification_Journee");

            migrationBuilder.DropTable(
                name: "Retrait_Type");

            migrationBuilder.DropTable(
                name: "Commande");

            migrationBuilder.DropTable(
                name: "Vente");

            migrationBuilder.DropTable(
                name: "Salle");

            migrationBuilder.DropTable(
                name: "Facture");

            migrationBuilder.DropTable(
                name: "Statut_BL");

            migrationBuilder.DropTable(
                name: "Forme_Produit");

            migrationBuilder.DropTable(
                name: "ProduitBase");

            migrationBuilder.DropTable(
                name: "Atelier");

            migrationBuilder.DropTable(
                name: "BonDe_Sortie");

            migrationBuilder.DropTable(
                name: "Statut_Livraison");

            migrationBuilder.DropTable(
                name: "Statut_PaiementCommande");

            migrationBuilder.DropTable(
                name: "Position_Vente");

            migrationBuilder.DropTable(
                name: "Bon_De_Commande");

            migrationBuilder.DropTable(
                name: "ProduitPackage");

            migrationBuilder.DropTable(
                name: "Produit_PretConsomer");

            migrationBuilder.DropTable(
                name: "Ville");

            migrationBuilder.DropTable(
                name: "Mode_Paiement");

            migrationBuilder.DropTable(
                name: "Sous_Famille");

            migrationBuilder.DropIndex(
                name: "IX_Zone_Stockage_ZoneStockage_FormeStockageId",
                table: "Zone_Stockage");

            migrationBuilder.DropIndex(
                name: "IX_Zone_Stockage_ZoneStockage_LieuStockageId",
                table: "Zone_Stockage");

            migrationBuilder.DropIndex(
                name: "IX_Zone_Stockage_ZoneStockage_TypeContenuId",
                table: "Zone_Stockage");

            migrationBuilder.DropIndex(
                name: "IX_Zone_Stockage_ZoneStockage_UniteMesureId",
                table: "Zone_Stockage");

            migrationBuilder.DropIndex(
                name: "IX_Section_Stockage_Section_ZoneStockageId",
                table: "Section_Stockage");

            migrationBuilder.DropIndex(
                name: "IX_Produit_Vendable_ProduitVendable_FamilleProduitId",
                table: "Produit_Vendable");

            migrationBuilder.DropIndex(
                name: "IX_Produit_Vendable_ProduitVendable_FormStockageId",
                table: "Produit_Vendable");

            migrationBuilder.DropIndex(
                name: "IX_Produit_Vendable_ProduitVendable_TvaId",
                table: "Produit_Vendable");

            migrationBuilder.DropIndex(
                name: "IX_Produit_Vendable_ProduitVendable_UniteMesureId",
                table: "Produit_Vendable");

            migrationBuilder.DropIndex(
                name: "IX_Produit_FicheTechnique_FicheTechnique_FicheTechniqueBridgeID",
                table: "Produit_FicheTechnique");

            migrationBuilder.DropIndex(
                name: "IX_Produit_FicheTechnique_FicheTechnique_MatierePremiereId",
                table: "Produit_FicheTechnique");

            migrationBuilder.DropIndex(
                name: "IX_Produit_FicheTechnique_FicheTechnique_ProduitVendableId",
                table: "Produit_FicheTechnique");

            migrationBuilder.DropIndex(
                name: "IX_Produit_FicheTechnique_FicheTechnique_UniteMesureId",
                table: "Produit_FicheTechnique");

            migrationBuilder.DropIndex(
                name: "IX_Point_Vente_PointVente_AtelierID",
                table: "Point_Vente");

            migrationBuilder.DropIndex(
                name: "IX_Point_Vente_PointVente_StockID",
                table: "Point_Vente");

            migrationBuilder.DropIndex(
                name: "IX_Point_Vente_PointVente_VilleID",
                table: "Point_Vente");

            migrationBuilder.DropIndex(
                name: "IX_MatierePremiere_Stokage_MatierePremiereStokage_MatierePremiereId",
                table: "MatierePremiere_Stokage");

            migrationBuilder.DropIndex(
                name: "IX_MatierePremiere_Stokage_MatierePremiereStokage_SectionStockageId",
                table: "MatierePremiere_Stokage");

            migrationBuilder.DropIndex(
                name: "IX_Matiere_Premiere_MatierePremiere_AchatUniteMesureId",
                table: "Matiere_Premiere");

            migrationBuilder.DropIndex(
                name: "IX_Matiere_Premiere_MatierePremiere_CoutTVAID",
                table: "Matiere_Premiere");

            migrationBuilder.DropIndex(
                name: "IX_Matiere_Premiere_MatierePremiere_FamilleID",
                table: "Matiere_Premiere");

            migrationBuilder.DropIndex(
                name: "IX_Matiere_Premiere_MatierePremiere_FormeStockageId",
                table: "Matiere_Premiere");

            migrationBuilder.DropIndex(
                name: "IX_Lieu_Stockage_LieuStockag_VilleID",
                table: "Lieu_Stockage");

            migrationBuilder.DropIndex(
                name: "IX_Founisseur_Founisseur_VilleID",
                table: "Founisseur");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AtelierID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_LieuStockage_ID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PointVente_ID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PositionVente_ID",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Paiement_Abonnement",
                table: "Paiement_Abonnement");

            migrationBuilder.DropIndex(
                name: "IX_Paiement_Abonnement_PaiementAbonnement_AbonnementId",
                table: "Paiement_Abonnement");

            migrationBuilder.DropIndex(
                name: "IX_Paiement_Abonnement_PaiementAbonnement_PointVenteID",
                table: "Paiement_Abonnement");

            migrationBuilder.DropColumn(
                name: "ZoneStockage_Designation",
                table: "Zone_Stockage");

            migrationBuilder.DropColumn(
                name: "Unite_CategorieID",
                table: "Unite_Mesure");

            migrationBuilder.DropColumn(
                name: "ProduitVendable_AvecFT",
                table: "Produit_Vendable");

            migrationBuilder.DropColumn(
                name: "ProduitVendable_CodeProduit",
                table: "Produit_Vendable");

            migrationBuilder.DropColumn(
                name: "ProduitVendable_Conditionnement",
                table: "Produit_Vendable");

            migrationBuilder.DropColumn(
                name: "ProduitVendable_DesignationAR",
                table: "Produit_Vendable");

            migrationBuilder.DropColumn(
                name: "ProduitVendable_DesignationEN",
                table: "Produit_Vendable");

            migrationBuilder.DropColumn(
                name: "ProduitVendable_PlanificationFlag",
                table: "Produit_Vendable");

            migrationBuilder.DropColumn(
                name: "ProduitVendable_Specification",
                table: "Produit_Vendable");

            migrationBuilder.DropColumn(
                name: "ProduitVendable_TempConservation",
                table: "Produit_Vendable");

            migrationBuilder.DropColumn(
                name: "ProduitVendable_TvaId",
                table: "Produit_Vendable");

            migrationBuilder.DropColumn(
                name: "Produit_CoutDeRevient",
                table: "Produit_Vendable");

            migrationBuilder.DropColumn(
                name: "FicheTechnique_FicheTechniqueBridgeID",
                table: "Produit_FicheTechnique");

            migrationBuilder.DropColumn(
                name: "FicheTechnique_Libelle",
                table: "Produit_FicheTechnique");

            migrationBuilder.DropColumn(
                name: "PointVente_AtelierID",
                table: "Point_Vente");

            migrationBuilder.DropColumn(
                name: "PointVente_CodePostal",
                table: "Point_Vente");

            migrationBuilder.DropColumn(
                name: "PointVente_StockID",
                table: "Point_Vente");

            migrationBuilder.DropColumn(
                name: "PointVente_TypeService",
                table: "Point_Vente");

            migrationBuilder.DropColumn(
                name: "PointVente_VilleID",
                table: "Point_Vente");

            migrationBuilder.DropColumn(
                name: "MatierePremiereStokage_QuantiteActuelle",
                table: "MatierePremiere_Stokage");

            migrationBuilder.DropColumn(
                name: "MatierePremiere_CodeArticle",
                table: "Matiere_Premiere");

            migrationBuilder.DropColumn(
                name: "MatierePremiere_Composants",
                table: "Matiere_Premiere");

            migrationBuilder.DropColumn(
                name: "MatierePremiere_CoutTVAID",
                table: "Matiere_Premiere");

            migrationBuilder.DropColumn(
                name: "MatierePremiere_LibelleAR",
                table: "Matiere_Premiere");

            migrationBuilder.DropColumn(
                name: "MatierePremiere_LibelleEN",
                table: "Matiere_Premiere");

            migrationBuilder.DropColumn(
                name: "MatierePremiere_QuantiteActuelle",
                table: "Matiere_Premiere");

            migrationBuilder.DropColumn(
                name: "LieuStockag_CodePostal",
                table: "Lieu_Stockage");

            migrationBuilder.DropColumn(
                name: "LieuStockag_ParDefaut",
                table: "Lieu_Stockage");

            migrationBuilder.DropColumn(
                name: "LieuStockag_UTILISATEUR",
                table: "Lieu_Stockage");

            migrationBuilder.DropColumn(
                name: "LieuStockag_VilleID",
                table: "Lieu_Stockage");

            migrationBuilder.DropColumn(
                name: "Founisseur_VilleID",
                table: "Founisseur");

            migrationBuilder.DropColumn(
                name: "FamilleProduit_Image",
                table: "Famille_Produit");

            migrationBuilder.DropColumn(
                name: "Adresse",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AtelierID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LieuStockage_ID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Logo",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Nom",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Nom_Complet",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PointVente_ID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PositionVente_ID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Prenom",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Allergene_LibelleAR",
                table: "Allergene");

            migrationBuilder.DropColumn(
                name: "Abonnement_Logo",
                table: "Abonnement_Client");

            migrationBuilder.DropColumn(
                name: "Abonnement_NomClientAR",
                table: "Abonnement_Client");

            migrationBuilder.DropColumn(
                name: "Abonnement_ONSSAAuthorisation",
                table: "Abonnement_Client");

            migrationBuilder.DropColumn(
                name: "PaiementAbonnement_Montant",
                table: "Paiement_Abonnement");

            migrationBuilder.DropColumn(
                name: "PaiementAbonnement_PointVenteID",
                table: "Paiement_Abonnement");

            migrationBuilder.RenameTable(
                name: "Paiement_Abonnement",
                newName: "Paiemenet_Abonnement");

            migrationBuilder.RenameColumn(
                name: "MatierePremiere_FamilleID",
                table: "Matiere_Premiere",
                newName: "MatierePremiere_AllergeneId");

            migrationBuilder.RenameColumn(
                name: "Abonnement_Id",
                table: "Abonnement_Client",
                newName: "Abonnement_ID");

            migrationBuilder.RenameColumn(
                name: "PaiementAbonnement_PeriodeDePaiement",
                table: "Paiemenet_Abonnement",
                newName: "PaiementAbonnemet_Montant");

            migrationBuilder.AddColumn<int>(
                name: "TypeContenu_Id",
                table: "Zone_Stockage",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProduitVendable_FamilleProduitId",
                table: "Produit_Vendable",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "ProduitVendable_Description",
                table: "Produit_Vendable",
                type: "nvarchar(1000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EnStock",
                table: "Produit_Vendable",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<Guid>(
                name: "PointVente_UtilisateurId",
                table: "Point_Vente",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MatierePremiere_FormeStockageId",
                table: "Matiere_Premiere",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Founisseur_UtilisateurId",
                table: "Founisseur",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FamilleProduit_ParentId",
                table: "Famille_Produit",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Abonnement_VilleId",
                table: "Abonnement_Client",
                type: "nvarchar(4)",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<Guid>(
                name: "PaiementAbonnement_AdminUserId",
                table: "Paiemenet_Abonnement",
                type: "uniqueidentifier(16)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(16)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Paiemenet_Abonnement",
                table: "Paiemenet_Abonnement",
                column: "PaiementAbonnement_ID");

            migrationBuilder.CreateTable(
                name: "ProduitVendable_Stokage",
                columns: table => new
                {
                    ProduitVendableStokage_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProduitVendableStokage_AbonnementId = table.Column<int>(type: "int", nullable: false),
                    ProduitVendableStokage_DateCreation = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    ProduitVendableStokage_IsActive = table.Column<int>(nullable: false),
                    ProduitVendableStokage_ProduitVendableId = table.Column<int>(nullable: false),
                    ProduitVendableStokage_SectionStockageId = table.Column<int>(nullable: false),
                    ProduitVendableStokage_StockMaximum = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ProduitVendableStokage_StockMinimum = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProduitVendable_Stokage", x => x.ProduitVendableStokage_Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Zone_Stockage_TypeContenu_Id",
                table: "Zone_Stockage",
                column: "TypeContenu_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Zone_Stockage_Type_Contenu_TypeContenu_Id",
                table: "Zone_Stockage",
                column: "TypeContenu_Id",
                principalTable: "Type_Contenu",
                principalColumn: "TypeContenu_Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
