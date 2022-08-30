using Domain.Authentication;
using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Repository.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Abonnement_Client> abonnement_Clients { get; set; }
        public DbSet<Paiement_Abonnement> paiement_Abonnements { get; set; }
        public DbSet<Fournisseur> fournisseurs { get; set; }
        public DbSet<FamilleProduit> familleProduits { get; set; }
        public DbSet<SousFamille> sousFamilles { get; set; }
        public DbSet<Unite_Mesure> unite_Mesures { get; set; }
        public DbSet<Type_Contenu> type_Contenus { get; set; }
        public DbSet<Forme_Stockage> forme_Stockages { get; set; }
        public DbSet<Point_Vente> point_Ventes { get; set; }
        public DbSet<Lieu_Stockage> lieu_Stockages { get; set; }
        public DbSet<Zone_Stockage> zone_Stockages { get; set; }
        public DbSet<Section_Stockage> section_Stockages { get; set; }
        public DbSet<ProduitVendable> produitVendables { get; set; }
        public DbSet<PrixProduit> prixProduits { get; set; }
        public DbSet<Forme_Produit> forme_Produits { get; set; }
        public DbSet<ProduitConsomableStokage> produitConsomableStokages { get; set; }
        public DbSet<Allergene> allergenes { get; set; }
        public DbSet<MatierePremiere> matierePremieres { get; set; }
        public DbSet<MatiereFamille> matiereFamilles { get; set; }
        public DbSet<MatireFamille_Parent> matireFamille_Parents { get; set; }
        public DbSet<MatierePremiereStockage> matierePremiereStockages { get; set; }
        public DbSet<ProduitFicheTechnique> produitFicheTechniques { get; set; }
        public DbSet<Approvisionnement> approvisionnements { get; set; }
        public DbSet<BonDeSortie> bonDeSorties { get; set; }
        public DbSet<BonDetails> bonDetails { get; set; }
        public DbSet<PlanificationJournee> planificationJournees { get; set; }
        public DbSet<PlanificationdeProduction> planificationdeProductions { get; set; }
        public DbSet<FicheTechniqueBridge> ficheTechniqueBridges { get; set; }
        public DbSet<FicheForme> ficheFormes { get; set; }
        public DbSet<Demande> demandes { get; set; }
        public DbSet<MouvementStock> mouvements { get; set; }
        public DbSet<MouvementType> mouvementTypes { get; set; }
        public DbSet<Atelier> ateliers { get; set; }
        public DbSet<Fonction> fonctions { get; set; }
        public DbSet<FournisseurContact> fournisseurContacts { get; set; }
        public DbSet<ProduitAppro> produitAppros { get; set; }
        public DbSet<DetailsProduitAppro> detailsProduitAppros { get; set; }
        public DbSet<PointVente_Stock> pointVente_Stocks { get; set; }
        public DbSet<Ville> villes { get; set; }
        public DbSet<ProduitPackage> produitPackages { get; set; }
        public DbSet<Sous_ProduitPackage> sous_ProduitPackages { get; set; }
        public DbSet<RetourStock> retours { get; set; }
        public DbSet<FournisseurMatiere> fournisseurMatieres { get; set; }
        public DbSet<ProduitPretConsomer> produitPrets { get; set; }
        public DbSet<MouvementProduitsConso> mouvementProduits { get; set; }
        public DbSet<FournisseurProduits> fournisseurProduits { get; set; }
        public DbSet<Fournisseur_ProduitConso> FournisserusProduits_Link { get; set; }
        public DbSet<Approvisionnement_ProduitConso> approvisionnement_ProduitConsos { get; set; }
        public DbSet<PdV_ProduitsPret> pdV_ProduitsPrets { get; set; }
        public DbSet<Vente> vente { get; set; }
        public DbSet<VenteDetails> venteDetails { get; set; }
        public DbSet<PointVente_Famille> pointVente_Familles { get; set; }
        public DbSet<PackageProduction> packageProductions { get; set; }
        public DbSet<DetailsProduction> detailsProductions { get; set; }
        public DbSet<FormeDetails> formeDetails { get; set; }
        public DbSet<PositionVente> positionVentes { get; set; }
        public DbSet<Table> tables { get; set; }
        public DbSet<Salle> salles { get; set; }
        public DbSet<AgentServeur> agents { get; set; }
        public DbSet<ModePaiement> modePaiements { get; set; }
        public DbSet<Affectation_Agent_Table> affectation_Agent_Tables { get; set; }
        public DbSet<Unite_MesureMatiere> unite_MesureMatieres { get; set; }
        public DbSet<RetourProduits> retourProduits { get; set; }
        public DbSet<Retour_Details> retour_Details { get; set; }
        public DbSet<AllimentationCaisse> allimentations { get; set; }
        public DbSet<MouvementCaisse> mouvementsCaisse { get; set; }
        public DbSet<MouvementCaisse_Type> mouvementCaisse_Types { get; set; }
        public DbSet<RetraitType> retraitTypes { get; set; }
        public DbSet<RetraitCaisse> retraitCaisses { get; set; }
        public DbSet<ClotureJournee> cloture { get; set; }
        public DbSet<Commande> commandes { get; set; }
        public DbSet<CommandeDetail> commandeDetails { get; set; }
        public DbSet<Statut_Livraison> Statut_livraisons { get; set; }
        public DbSet<Statut_PaiementCommande> statut_Paiements { get; set; }
        public DbSet<Commande_Paiement> paiementCommandes { get; set; }
        public DbSet<Perte> pertes { get; set; }
        public DbSet<PerteDetails> perteDetails { get; set; }
        public DbSet<Gratuite> gratuites { get; set; }
        public DbSet<GratuiteDetails> gratuiteDetails { get; set; }
        public DbSet<LogoUser> logoUser { get; set; }
        public DbSet<PointVente_User> pointVente_Users { get; set; }
        public DbSet<Stock_User> stock_Users { get; set; }
        public DbSet<Atelier_User> atelier_Users { get; set; }
        public DbSet<Demande_Pret> demande_Prets { get; set; }
        public DbSet<DemandePret_Details> demandePret_Details { get; set; }
        public DbSet<ProduitPret_Atelier> produitPret_Ateliers { get; set; }
        public DbSet<Perte_Matiere> perte_Matieres { get; set; }
        public DbSet<MatiereStock_Atelier> MatiereStock_Ateliers { get; set; }
        public DbSet<Package_Forme> package_Formes { get; set; }
        public DbSet<PackageForme_Details> packageForme_Details { get; set; }
        public DbSet<ProduitPack_Atelier> produitPack_Ateliers { get; set; }
        public DbSet<Approvisionnement_ProduitPackage> approvisionnement_ProduitPackages { get; set; }
        public DbSet<Perte_Pret> perte_Prets { get; set; }
        public DbSet<Perte_MatiereStock> perte_MatiereStocks { get; set; }
        public DbSet<ProduitBase> produitBases { get; set; }
        public DbSet<FicheTechniqueProduitBase> ficheTechniqueProduitBases { get; set; }
        public DbSet<ProduitBaseFicheTechnique> produitBaseFicheTechniques { get; set; }
        public DbSet<FicheTech_ProduitBase> ficheTech_ProduitBases { get; set; }
        public DbSet<UniteMesure_ProdBase> uniteMesure_ProdBases { get; set; }
        public DbSet<PlanificationdeProductionBase> planificationdeProductionBases { get; set; }
        public DbSet<PlanificationJourneeBase> planificationJourneeBases { get; set; }
        public DbSet<ProdBase_Atelier> prodBase_Ateliers { get; set; }
        public DbSet<PointPorduction_Famille>  pointPorduction_Familles { get; set; }
        public DbSet<Taux_TVA>  taux_TVAs { get; set; }
        public DbSet<Tva>  tvas { get; set; }
        public DbSet<Echange_Produits>  echange_Produits { get; set; }
        public DbSet<EchangeProduit_Details>  echangeProduit_Details { get; set; }
        public DbSet<Planification_ProdBase>  planification_ProdBases { get; set; }
        public DbSet<Reception_Stock>  reception_Stocks { get; set; }
        public DbSet<Approvisionnement_Matiere>  approvisionnement_Matieres { get; set; }
        public DbSet<BonDeCommande> bonDeCommandes { get; set; }
        public DbSet<BonDeLivraison> bonDeLivraisons { get; set; }
        public DbSet<Article_BC> article_BCs { get; set; }
        public DbSet<Article_BL> article_BLs { get; set; }
        public DbSet<Facture> factures { get; set; }
        public DbSet<Stock_Achat> stock_Achats { get; set; }
        public DbSet<Transfert_Matiere> transfert_Matieres { get; set; }
        public DbSet<Matiere_Transfert> matiere_Transferts { get; set; }
        public DbSet<Payement_Facture> payement_Factures { get; set; }
        public DbSet<MatPrem_Allergene> matPrem_Allergenes { get; set; }
        public DbSet<Produit_Composants> produit_Composants { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
