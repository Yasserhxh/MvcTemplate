using AutoMapper;
using Domain.Authentication;
using Domain.Entities;
using Domain.Models;

namespace Repository.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Entities to Models mapping
            CreateMap<ApplicationUser, UserModel>();
            CreateMap<Abonnement_Client, Abonnement_ClientModel>()
                           .ForMember(b => b.UserName, opt => opt.Ignore())
                           .ForMember(b => b.Password, opt => opt.Ignore());
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
            CreateMap<ProduitConsomableStokage, ProduitConsomableStokageModel>();
            CreateMap<PrixProduit, PrixProduitModel>();
            CreateMap<PrixProduit, PrixProduitViewModel>();
            CreateMap<Forme_Produit, Forme_ProduitModel>();
            CreateMap<Allergene, AllergeneModel>();
            CreateMap<MatierePremiere, MatierePremiereModel>();
            CreateMap<MatierePremiereStockage, MatierePremiereStockageModel>();
            CreateMap<ProduitFicheTechnique, ProduitFicheTechniqueModel>();
            CreateMap<ProduitPackage, ProduitPackageModel>();
            CreateMap<Sous_ProduitPackage, Sous_ProduitPackageModel>();
            CreateMap<PlanificationJournee, PlanificationJourneeModel>();
            CreateMap<PlanificationdeProduction, PlanificationdeProductionModel>();
            CreateMap<Approvisionnement_ProduitConso, Approvisionnement_ProduitConsoModel>();
            CreateMap<Vente, VenteModel>();
            CreateMap<RetourProduits, RetourProduitsModel>();
            CreateMap<Retour_Details, RetourDetailsModel>();
            CreateMap<VenteDetails, VenteDetailsModel>();
            CreateMap<ProduitPretConsomerModel, ProduitPretConsomer>();
            CreateMap<ModePaiementModel, ModePaiement>();
            CreateMap<Echange_ProduitsModel, Echange_Produits>();
            CreateMap<EchangeProduit_DetailsModel, EchangeProduit_Details>();
            CreateMap<Facture, FactureModel>();

            // Models to Entities mapping
            CreateMap<Abonnement_ClientModel, Abonnement_Client>();
            CreateMap<FournisseurModel, Fournisseur>();
            CreateMap<Zone_StockageModel, Zone_Stockage>();
            CreateMap<Lieu_StockageModel, Lieu_Stockage>();
            CreateMap<Forme_StockageModel, Forme_Stockage>();
            CreateMap<Unite_MesureModel, Unite_Mesure>();
            CreateMap<Section_StockageModel, Section_Stockage>();
            CreateMap<Point_VenteModel, Point_Vente>();
            CreateMap<ProduitVendableModel, ProduitVendable>();
            CreateMap<PrixProduitModel, PrixProduit>();
            CreateMap<PrixProduitViewModel, PrixProduit>();
            CreateMap<Forme_ProduitModel, Forme_Produit>();
            CreateMap<ProduitConsomableStokageModel, ProduitConsomableStokage>();
            CreateMap<AllergeneModel, Allergene>();
            CreateMap<MatierePremiereModel, MatierePremiere>();
            CreateMap<MatierePremiereStockageModel, MatierePremiereStockage>();
            CreateMap<ProduitFicheTechniqueModel, ProduitFicheTechnique>();
            CreateMap<ProduitPackageModel, ProduitPackage>();
            CreateMap<Sous_ProduitPackageModel, Sous_ProduitPackage>();
            CreateMap<PlanificationJourneeModel, PlanificationJournee>();
            CreateMap<PlanificationdeProductionModel, PlanificationdeProduction>();
            CreateMap<Approvisionnement_ProduitConsoModel, Approvisionnement_ProduitConso>();
            CreateMap<VenteModel, Vente>();
            CreateMap<RetourProduitsModel, RetourProduits>();
            CreateMap<RetourDetailsModel, Retour_Details>();
            CreateMap<VenteDetailsModel, VenteDetails>();
            CreateMap<ProduitPretConsomer, ProduitPretConsomerModel>();
            CreateMap<ModePaiement ,ModePaiementModel> ();
            CreateMap<PointPorduction_Famille ,PointPorduction_Famille> ();
            CreateMap<Taux_TVA ,Taux_TVAModel> ();
            CreateMap<Tva ,TvaModel> ();
            CreateMap<Paiement_Abonnement ,PaiementAbonnementModel> ();
            CreateMap<Echange_Produits ,Echange_ProduitsModel> ();
            CreateMap<EchangeProduit_Details ,EchangeProduit_DetailsModel> ();
            CreateMap<MatStockFlag, MatStockFlagModel>();
            CreateMap<MatStockView, matStockViewModel>();
            CreateMap<MatStockFlagModel, MatStockFlag>();
            CreateMap<matStockViewModel, MatStockView>();
            CreateMap<Reception_StockModel, Reception_Stock>();
            CreateMap<Approvisionnement_MatiereModel, Approvisionnement_Matiere>();
            CreateMap<Approvisionnement_MatiereModel, Approvisionnement_Matiere>();
            CreateMap<Approvisionnement_MatiereModel, Approvisionnement_Matiere>();
            CreateMap<FactureModel, Facture>();

        }
    }
}
