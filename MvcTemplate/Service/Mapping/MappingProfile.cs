using AutoMapper;
using Domain.Authentication;
using Domain.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Service.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Entities to Models mapping
            CreateMap<ApplicationUser, UserModel>();
            CreateMap<IdentityRole, RoleModel>();
            CreateMap<Abonnement_Client, Abonnement_ClientModel>()
                .ForMember(c => c.Abonnement_NomClient, opt => opt.Ignore())
                .ForMember(c => c.Password, opt => opt.Ignore());

            CreateMap<Fournisseur, FournisseurModel>();
            CreateMap<FamilleProduit, FamilleProduitModel>();
            CreateMap<Zone_Stockage, Zone_StockageModel>();
            CreateMap<Lieu_Stockage, Lieu_StockageModel>();
            CreateMap<Forme_Stockage, Forme_StockageModel>();
            CreateMap<Unite_Mesure, Unite_MesureModel>();
            CreateMap<Type_Contenu, Type_ContenuModel>();
            CreateMap<Section_Stockage, Section_StockageModel>();
            CreateMap<Point_Vente, Point_VenteModel>();
            CreateMap<ProduitVendable, ProduitVendableModel>();
            CreateMap<PrixProduit, PrixProduitModel>();
            CreateMap<PrixProduit, PrixProduitViewModel>();
            CreateMap<Forme_Produit, Forme_ProduitModel>();
            CreateMap<ProduitConsomableStokage, ProduitConsomableStokageModel>();
            CreateMap<Allergene, AllergeneModel>();
            CreateMap<MatierePremiere, MatierePremiereModel>();
            CreateMap<MatierePremiereStockage, MatierePremiereStockageModel>();
            CreateMap<ProduitFicheTechnique, ProduitFicheTechniqueModel>();
            CreateMap<ProduitPackage, ProduitPackageModel>();
            CreateMap<PlanificationJournee, PlanificationJourneeModel>();
            CreateMap<PlanificationdeProduction, PlanificationdeProductionModel>();
            CreateMap<Approvisionnement_ProduitConso, Approvisionnement_ProduitConsoModel>();
            CreateMap<ProduitPretConsomer, ProduitPretConsomerModel>();
            CreateMap<Table, TableModel>();
            CreateMap<ModePaiement, ModePaiementModel>();
            CreateMap<PointPorduction_Famille, PointPorduction_Famille >();
            CreateMap<FamilleProduitModel, FamilleProduit>();
            CreateMap<Echange_Produits, Echange_ProduitsModel>();
            CreateMap<EchangeProduit_Details, EchangeProduit_DetailsModel>();
            CreateMap<BonDeLivraison, BonDeLivraison_Model>().ForMember(b => b.BonDeLivraison_TTCWords, opt => opt.Ignore());
            CreateMap<BonDeCommande, BonDeCommande_Model>().ForMember(b => b.BonDeCommande_TTCWords, opt => opt.Ignore());
            CreateMap<Facture, FactureModel>();
            CreateMap<Matiere_Transfert, MatiereTransfert_Model>();





            // Models to Entities mapping
            CreateMap<Abonnement_ClientModel, Abonnement_Client>();
            CreateMap<FournisseurModel, Fournisseur>();
            CreateMap<Zone_StockageModel, Zone_Stockage>();
            CreateMap<Lieu_StockageModel, Lieu_Stockage>();
            CreateMap<Forme_StockageModel, Forme_Stockage>();
            CreateMap<Unite_MesureModel, Unite_Mesure>();
            //  CreateMap<Type_ContenuModel, Type_Contenu>();
            CreateMap<Section_StockageModel, Section_Stockage>();
            CreateMap<ProduitConsomableStokageModel, ProduitConsomableStokage>();
            CreateMap<ProduitVendableModel, ProduitVendable>();
            CreateMap<PrixProduitModel, PrixProduit>();
            CreateMap<PrixProduitViewModel, PrixProduit>();
            CreateMap<Forme_ProduitModel, Forme_Produit>();
            CreateMap<AllergeneModel, Allergene>();
            CreateMap<MatierePremiereModel, MatierePremiere>();
            CreateMap<MatierePremiereStockageModel, MatierePremiereStockage>();
            CreateMap<ProduitFicheTechniqueModel, ProduitFicheTechnique>();
            CreateMap<ProduitPackageModel, ProduitPackage>();
            CreateMap<PlanificationJourneeModel, PlanificationJournee>();
            CreateMap<PlanificationdeProductionModel, PlanificationdeProduction>();
            CreateMap<Approvisionnement_ProduitConsoModel, Approvisionnement_ProduitConso>();
            CreateMap<ProduitPretConsomerModel, ProduitPretConsomer>();
            CreateMap<TableModel, Table>();
            CreateMap<ModePaiementModel, ModePaiement>();
            CreateMap<Taux_TVAModel, Taux_TVA>();
            CreateMap<TvaModel, Tva>();
            CreateMap<FamilleProduitModel, FamilleProduit>();
            CreateMap<PaiementAbonnementModel, Paiement_Abonnement>();
            CreateMap<Echange_ProduitsModel, Echange_Produits>();
            CreateMap<EchangeProduit_DetailsModel, EchangeProduit_Details>();
            CreateMap<MatStockFlagModel, MatStockFlag>();
            CreateMap<matStockViewModel, MatStockView>();
            CreateMap<MatStockFlag, MatStockFlagModel>();
            CreateMap<MatStockView, matStockViewModel>();
            CreateMap<Reception_Stock, Reception_StockModel>();
            CreateMap<Approvisionnement_Matiere, Approvisionnement_MatiereModel>();
            CreateMap<FactureModel, Facture>();
            CreateMap<MatiereTransfert_Model, Matiere_Transfert>();

        }
    }
}
