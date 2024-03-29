﻿using Domain.Entities;
using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.IServices
{
    public interface IProduitVendableService
    {
        Task<bool> CreateProduitVendable(ProduitVendableModel produitModel);
        Task<bool> StockerProduitVendable(int Id, ProduitConsomableStokageModel produitStockerModel);
        //Task<bool> PlanifierAProduire(PlanificationdeProductionModel planificationdeProductionModel);
        Task<int?> Planifier(PlanificationJourneeModel planModel);
        Task<bool> AnnulerPlanification(int planID);
        Task<bool> RetourStockPlan(int planID);
        Task<bool> CreerDemande(DemandeModel demandeModel);
        Task<bool> ProductionPackage(PackageProductionModel productionpackageModel, int pdvId);
        Task<bool> RetourStock(RetourStockModel demandeModel);
        Task<bool> Approvisionnement(ApprovisionnementModel approvisionnementModel);
        Task<bool> ApprovisionnementProduit(Approvisionnement_ProduitConsoModel approvisionnementModel);
        Task<bool> PackageProduit(ProduitPackageModel produitPackage);
        Task<bool> ProduitConsomable(ProduitPretConsomerModel produitPretModel);
        Task<bool> FournisseurProduit(FournisseurProduitsModel fournisseurP);
        IEnumerable<ApprovisionnementModel> ListeApprovisionnement(int Id, int? ateId, int? point, string date, int? etat);
        IEnumerable<ApprovisionnementModel> ListeApprovisionnementAtelier(int Id, int atelierId, int? pdv, string date, int? etat);
        IEnumerable<ApprovisionnementModel> ListeApprovisionnementPointVente(int Id, int pdvId, int? atelier, string date);
        IEnumerable<Approvisionnement_ProduitConsoModel> ListeApprovisionnementProduit(int Id);
        IEnumerable<Approvisionnement_ProduitConsoModel> ListeApprovisionnementProduitAtelier(int Id, int atelierId, int? pdv, string date);
        IEnumerable<Approvisionnement_ProduitConsoModel> ListeApprovisionnementProduitPdv(int Id, int pdvId, int? atelier, string date);
        IEnumerable<FonctionModel> getListFonction();
        IEnumerable<VilleModel> getListeVille();
        IEnumerable<AtelierModel> getListAteliers(int aboId);
        IEnumerable<Unite_MesureModel> getListUniteMesure();
        IEnumerable<Unite_MesureModel> findFormulaireUniteMesure(int unite);
        IEnumerable<Section_StockageModel> getListeSectionStockage();
        IEnumerable<FamilleProduitModel> getListFamilleProduit(int Id);
        IEnumerable<Forme_StockageModel> getListFormeSotckage(int Id);
        IEnumerable<ProduitVendableModel> getListProduitVendable(int Id, int? categ, int? SousCateg, string name);
        IEnumerable<ProduitVendableModel> getListProduitVendableAvecPlanif(int Id,int AboId);
        IEnumerable<ProduitPretConsomerModel> getListProduitConsomable(int Id,int? categ,int? SousCateg);
        IEnumerable<ProduitPackageModel> getListProduitpackage(int aboId);
        IEnumerable<ProduitPackageModel> getListProduitpackagePointVente(int aboId, int pdvid);
        IEnumerable<Sous_ProduitPackageModel> getListSousProduitpackage(int produitId);
        IEnumerable<PrixProduitViewModel> getListPrix(int Id);
        IEnumerable<PrixProduitModel> getListPrixFormes(int Id);
        IEnumerable<PlanificationJourneeModel> getListPansDemandes(int Id, int adresse);
        IEnumerable<PlanificationJourneeModel> getListPansRetours(int Id);
        IEnumerable<PlanificationJourneeModel> getListPansAtelier(int Id, int? atelierId, string etat, int? lieu, string date);
        Task<IEnumerable<PlanificationJourneeModel>> getListPansStock(int Id, int lieuId, string etat, int? atelier, string date);
        IEnumerable<DemandeModel> GetDemandes(int Id, string date, int? lieuID, int? atelierID);
        IEnumerable<DemandeModel> GetDemandesAtelier(int Id, int atelierId);
        IEnumerable<DemandeModel> GetDemandesStock(int Id, int lieuId);
        IEnumerable<RetourStockModel> GetRetourStocks(int Id);
        IEnumerable<RetourStockModel> GetRetourStocksByAtelier(int Id, int atelierId);
        IEnumerable<RetourStockModel> GetRetourStocksByStock(int Id, int stockId);
        Task<MatStockFlagModel> updateFormulaireQteProduite(int id, List<PlanificationdeProductionModel> plans);

        IEnumerable<ProduitConsomableStokageModel> getListProduitVendableStocker(int Id);
        ProduitConsomableStokageModel finformulaireProduitConsomableStockage(int produitConsoStockageId);
        IEnumerable<ProduitConsomableStokageModel> getListProduitVendableStockerByAtelier(int? Id);
        IEnumerable<ProduitConsomableStokageModel> getListProduitVendableStockerByProduit(int? Id,int produitId);
        IEnumerable<PdV_ProduitsPretModel> getListProduitsStockesPdv(int Id,int formeId);
        ProduitVendableModel findFormulaireProduitVendable(int formulaireProduitId);
        ProduitVendableModel findFormulaireProduitVendablePDF(int formulaireProduitId);
        Forme_ProduitModel findFormulaireFormeProduit(int formulaireProduitId);
        PlanificationdeProductionModel findFormulairePlans(int formulaireProduitId);
        ProduitPackageModel findFormulaireProduitPackage(int formulaireProduitId);
        ProduitPretConsomerModel findFormulaireProduitPret(int formulaireProduitId);
        FormeDetailsModel getFormeDetails(int formulaireProduitId, int? pdvId);
        IEnumerable<FormeDetailsModel> getFormeDetailsAll(int AboId);
        Task<bool> updateFormulaireProduitVendable(int id, ProduitVendableModel produitModel);
        Task<bool> deleteProduitVendable(int ID);
        Task<bool> deleteProduitVendableStockes(int ID);
        bool ProduitStocker(int ID);
        Task<bool> deletePlan(int ID);
        FicheTechniqueBridgeModel GetFicheTech(int Id);
        IEnumerable<PlanificationdeProductionModel> getDetailPlan(int aboId, int Id);
        IEnumerable<PlanificationdeProductionModel> getDetailPlanByPlan(int aboId, int Id,int planId);
        IEnumerable<PlanificationdeProductionModel> getDetailPlanProduitList(int aboId, int Id);
        IEnumerable<BonDetailsModel> getBonDetails(int Id);
        Task<bool> AccepterDemande(int demandeId, int adresse,string valideId);
        Task<bool> AccepterRetour(int demandeId, string adresse,string valideId);
        Task<bool> ValiderLivraison(int demandeId);
        Task<bool> ValiderApprovisionnement(int ApprovisionnementId,string valideId,int pdvId);
        Task<bool> ValiderApprovisionnementproduit(int ApprovisionnementId,string userid,int pdvId);
        IEnumerable<MouvementStockModel> GetMouvementStocks(int Id);
        IEnumerable<MouvementStockModel> GetMouvementStocksActiveOnly(int Id,int? matiere, string date);
        IEnumerable<MouvementStockModel> GetMouvementStocksBystock(int Id, int lieuId);
        IEnumerable<MouvementStockModel> GetMouvementStocksBystockActiveOnly(int Id, int lieuId, int? matiere, string date);
        Task<MatStockFlagModel> AccepterPlanification(int planificationId, int adresse, List<BonViewModel> ListBons, string validePar);
        Task<MatStockFlagModel> ValiderPlanification(int planificationI);
        IEnumerable<PlanificationdeProductionModel> getlPlansWithQte(int aboId);
        IEnumerable<ProduitApproModel> GetProduitAppros(int aboId,int atelierId, int produitId);
        decimal FindFormulaireProduitAppro(int aboId,int current, int produitApproId);
        //Task<bool> AjouterPrixProduit(int id, List<PrixProduitModel> prix);
        Task<bool> AjouterFormeProduitAsync(int id, Forme_ProduitModel forme);
        Task<bool> AjouterFormeProduitPackageAsync(int id, Forme_ProduitModel forme);
        Task<bool> AjouterFormeProduitPretAsync(int id, Forme_ProduitModel forme);
        decimal GetPrix(int Id, decimal qte);
        IEnumerable<Forme_ProduitModel> getListFormeProduits(int AboId, int produitId);
        IEnumerable<Forme_ProduitModel> getListFormes(int AboId, int produitId);
        IEnumerable<Forme_ProduitModel> getListFormeProduitsPackage(int AboId, int produitId);
        IEnumerable<Forme_ProduitModel> getListFormeProduitsPret(int AboId, int produitId);
        IEnumerable<FournisseurProduitsModel> ListeFournisseurProduits(int AboId,int? VilleId,int? statut);
        IEnumerable<MatierePremiereModel> getListMatiere(int Id);
        Task<bool?> AjouterMatiere(int idFournisseur, List<int> listMatiere);
        FournisseurProduitsModel findFormulaireFournisseur(int formulaireFourisseurId);
        Task<bool> deleteFournisseurPrdConso(int ID, int code);
        Task<bool> deleteProduitConso(int ID);
        Task<bool> deleteProduitsLink(int ID, int code);
        Task<bool> updateFormulaireFournisseur(int id, FournisseurProduitsModel newFournisseurModel);
        IEnumerable<Fournisseur_ProduitConsoModel> ListeProduitFournisseur(int fournisseurId);
        string GetUnitebyFrome(int formeProduitId);
        Task<bool> deletePositionVente(int ID, int code);
        Task<bool> deleteSalle(int ID, int code);
        IEnumerable<PdV_ProduitsPretModel> getListProduitConsomableEnStock(int Id, int pdv);
        Task<bool> updateFormePrix(int formeId, decimal prix);
        Task<int> GetPlanificationsForStock(int stockId);
        Task<int> GetPlanificationsForAtelier(int atelierId);
        IEnumerable<Demande_PretModel> getListDemandesPretStock(int stockID, int aboID, string date, int? atelierID, string etat);
        IEnumerable<Demande_PretModel> getListDemandesPretAtelier(int atelierID, int aboID, string date, int? stockID, string etat);
        IEnumerable<DemandePret_DetailsModel> getListDetailsDemandesPretStock(int demandePretID, int aboID, int stockID);
        IEnumerable<DemandePret_DetailsModel> getListDetailsDemandesPretAtelier(int demandePretID, int aboID, int atelierID);
        Task<bool> AccepterDemandePrets(int demandeID, int adresse, List<BonViewModel> listBon);
        Task<bool> ValiderDemande(int demandeID, int atelier);
        IEnumerable<ProduitPret_AtelierModel> getListProduitPretAtelier(int atelierID, int? produitID);
        IEnumerable<PlanificationdeProductionModel> getDetailPlanFormeList(int Id, int produitId);
        IEnumerable<ProduitApproModel> getListProduitMaisonStock(int atelierID, int aboID);
        ViewBonSortieModel getQteEnMagasin(int atelierId, int matiereId, int uniteId);
        IEnumerable<MatiereStock_AtelierModel> getListMatiereStock(int atelierId, int aboId);
        ViewBonSortieModel getQuantiteEnMagasin(int atelierId, int matiereId);
        ViewBonSortieModel getQuantiteAtelier(int matiereStockID);
        ViewBonSortieModel getQuantiteStock(int matiereStockID);
        IEnumerable<ProduitPackageModel> getProduitpackagePointVente(int formulaireProduitId);
        Task<bool> PackageForme(Package_FormeModel packFormeModel);
        IEnumerable<Package_FormeModel> getListeFichePackage(int? aboId, int? produitID);
        IEnumerable<PackageForme_DetailsModel> getDetailsFichePackage(int produitID);
        List<SousProduitsViewModel> getPackageFormeDetails(int packageId, int formeId, int current);
        Task<bool> DemanderComposants(List<SousProduitsViewModel> sousProds, int atelierID, int lieuId, int aboId);
        Task<bool> ApprovisionnementProduitPackage(Approvisionnement_ProduitPackageModel approvisionnementModel);
        Task<bool> ValiderApprovisionnementPackage(int ApprovisionnementId, int pdvId);
        IEnumerable<ProduitPack_AtelierModel> getProduitPackEnMagasin(int atelierId);
        IEnumerable<ProduitPack_AtelierModel> FindFormesProduitPackEnMagasin(int produitId, int atelierID);
        ProduitPack_AtelierModel FindformulairePackageMagasin(int ID);
        IEnumerable<Approvisionnement_ProduitPackageModel> getListApprovisionnement(int aboId, int? atelierID, int? pdvId, string date);
        Task<bool> DeclarationPerte(Perte_PretModel perte_PretModel);
        IEnumerable<Perte_PretModel> getListPertesPrets(int aboId, int? atelierID, string date);
        IEnumerable<ProduitPret_AtelierModel> findFormesPret_Atelier(int atelierID, int prduitpretID);
        ProduitPret_AtelierModel FindformulairePretMagasin(int ID);
        Task<bool> DemanderPret(Demande_PretModel demande_PretModel);
        IEnumerable<ProduitPretConsomerModel> getListProduitConsomableEnMagasin(int Id, int pdv, int? familleID);
        IEnumerable<Forme_ProduitModel> getFormesConsomableEnMagasin(int Id, int pdv);
        Task<bool> CreateProduitBase(ProduitBaseModel produitBaseModel);
        IEnumerable<ProduitBaseModel> getListProduitBase(int Id, int? formeID);
        Task<bool> updateFormulaireProduitBase(int id, ProduitBaseModel newProduitModel);
        Task<bool> deleteProduitBase(int ID);
        ProduitBaseModel findFormulaireProduitBase(int produitId, int aboId);
        IEnumerable<PlanificationJourneeBaseModel> getListPlansBaseAtelier(int Id, int? atelierId, string etat, int? lieu, string date);
        Task<bool> PlanifierBase(PlanificationJourneeBaseModel planModel);
        Task<MatStockFlagModel> AccepterPlanificationBase(int planificationId, int adresse, List<BonViewModel> ListBons, string validePar);
        Task<MatStockFlagModel> ValiderPlanificationBase(int planificationId);
        IEnumerable<PlanificationdeProductionBaseModel> getDetailPlanBase(int aboId, int Id);
        FicheTehcniqueProduitBaseModel GetFicheTechBase(int Id);
        IEnumerable<PlanificationdeProductionBaseModel> getDetailPlanProduitListBase(int aboId, int Id);
        IEnumerable<PlanificationdeProductionBaseModel> getDetailByPlanBase(int aboId, int Id, int planId);
        Task<bool?> updateFormulaireQteProduitebase(int id, List<PlanificationdeProductionBaseModel> plans);
        IEnumerable<ProdBase_AtelierModel> getListProduitBaseStock(int atelierID, int aboID);
        IEnumerable<FicheTech_ProduitBaseModel> GetFicheTech_ProduitBases(int Id, int aboID);
        Task<bool> updateFicheForme(int formeId, int produitID, decimal qteForme);
        IEnumerable<Taux_TVAModel> getListTVA(int aboID);
        Task<bool> CreateTVA(Taux_TVAModel tauxTVA);
        Task<List<matStockViewModel>> SetProdsDeBase(int planificationId);
        List<matStockViewModel> getProdBasePlan(int planificationId);
        Task<int?> PlanificationBaseAuto(int planID);
        IEnumerable<MatierePremiereModel> fillMatiereDemande(int planJourneeID);
        Task<MatStockFlagModel> CloturerDemande(int demandeID);
        Task<bool> CreateDemandeApprov(DemandeApprov_Model demandeApprov);
        Task<List<DemandeApprov_Model>> GetListDemandeApprovs(int aboId, int? pointVenteID, string etat, string dateLiv);
        Task<List<DemandeApprov_DetailsModel>> GetListDemandeApprovsDetails(int aboId, int? poinVenteID, int? demandeID, string etat, int? produitCateg);
    }
}
