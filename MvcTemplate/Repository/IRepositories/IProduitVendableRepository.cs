﻿using Domain.Entities;
using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.IRepositories
{
    public interface IProduitVendableRepository
    {
        Task<int?> CreateProduit(ProduitVendable produitVendable);
        Task<int?> StockerProduit(int Id, ProduitConsomableStokage produitVendableStockage);
        Task<int?> Planifier(PlanificationJournee plan);
        Task<bool> AnnulerPlanification(int planID);
        Task<bool> RetourStockPlan(int planID);
        Task<int?> CreerDemande(Demande demande);
        Task<int?> ProductionPackage(PackageProduction productionpackage, int pdvId);
        Task<int?> RetourStock(RetourStock retour);
        Task<int?> Approvisionnement(Approvisionnement approvisionnement);
        Task<int?> ApprovisionnementProduitConso(Approvisionnement_ProduitConso approvisionnement);
        Task<int?> PackageProduit(ProduitPackage produitPackage);
        Task<int?> ProduitConsomable(ProduitPretConsomer produitPret);
        Task<int?> FournisseurProduits(FournisseurProduits fournisseurP);
        IEnumerable<Approvisionnement> ListeApprovisionnement(int Id, int? ateId, int? point, string date, int? etat);
        IEnumerable<Approvisionnement> ListeApprovisionnementAtelier(int Id, int atelierId, int? pdv, string date,int? etat);
        IEnumerable<Approvisionnement> ListeApprovisionnementPointVente(int Id, int pdvId, int? atelier, string date);
        IEnumerable<Approvisionnement_ProduitConso> ListeApprovisionnementProduit(int Id);
        IEnumerable<Approvisionnement_ProduitConso> ListeApprovisionnementProduitAtelier(int Id, int atelierId, int? pdv, string date);
        IEnumerable<Approvisionnement_ProduitConso> ListeApprovisionnementProduitPdv(int Id, int pdvId, int? atelier, string date);
        IEnumerable<Unite_Mesure> getListUniteMesure();
        IEnumerable<Unite_Mesure> findFormulaireUnite(int unite);
        IEnumerable<Section_Stockage> getListSectionStockage();
        IEnumerable<Forme_Stockage> getListFormeSotckage(int Id);
        IEnumerable<FamilleProduit> getListFamilleProduit(int Id);
        IEnumerable<ProduitVendable> getListProduitVendable(int Id, int? categ,int? SousCateg, string name);
        IEnumerable<ProduitVendable> getListProduitVendableAvecPlanif(int Id,int AboId);
        IEnumerable<ProduitPretConsomer> getListProduitConsomable(int Id,int?categ,int? SousCateg);
        IEnumerable<ProduitConsomableStokage> getListProduitsStockes(int Id);
        ProduitConsomableStokage finformulaireProduitConsomableStockage(int produitConsoStockageId);
        IEnumerable<ProduitConsomableStokage> getListProduitsStockesByAtelier(int? Id);
        IEnumerable<ProduitConsomableStokage> getListProduitsStockesByProduit(int? Id,int produitId);
        IEnumerable<PdV_ProduitsPret> getListProduitsStockesPdv(int Id,int formeId);
       
        IEnumerable<PrixProduit> getListPrix(int Id);
        IEnumerable<PrixProduit> getListPrixFormes(int Id);
        IEnumerable<PlanificationJournee> getListPansDemandes(int Id, int adresse);
        IEnumerable<PlanificationJournee> getListPansRetours(int Id);
        IEnumerable<PlanificationJournee> getListPansAtelier(int Id, int? atelierId,string etat,int? lieu,string date);
        IEnumerable<PlanificationJournee> getListPansStock(int Id, int lieuId, string etat, int? atelier, string date);
        IEnumerable<Demande> GetDemandes(int Id, string date, int? lieuID, int? atelierID);
        IEnumerable<Demande> GetDemandesAtelier(int Id, int atelierId);
        IEnumerable<Demande> GetDemandesStock(int Id , int lieuId);
        IEnumerable<RetourStock> GetRetourStocks(int Id);
        IEnumerable<RetourStock> GetRetourStocksByAtelier(int Id, int atelierId);
        IEnumerable<RetourStock> GetRetourStocksByStock(int Id, int stockId);
        IEnumerable<MouvementStock> GetMouvementStocks(int Id);
        IEnumerable<MouvementStock> GetMouvementStocksActiveOnly(int Id, int? matiere, string date);
        IEnumerable<MouvementStock> GetMouvementStocksBystock(int Id, int lieuId);
        IEnumerable<MouvementStock> GetMouvementStocksBystockActiveOnly(int Id, int lieuId, int? matiere, string date);
        IEnumerable<Fonction> getListFonction();
        IEnumerable<ProduitPackage> getListProduitpackage(int aboId);
        IEnumerable<ProduitPackage> getListProduitpackagePointVente(int aboId, int pdvId);
        IEnumerable<Sous_ProduitPackage> getListSousProduitpackage(int produitId);
        IEnumerable<Ville> getListVilles();
        IEnumerable<Atelier> getListAteliers(int aboId);

        ProduitVendable findFormulaireProduit(int formulaireProduitId);
        ProduitVendable findFormulaireProduitPDF(int formulaireProduitId);
        Forme_Produit findFormulaireFormeProduit(int formulaireProduitId);
        PlanificationdeProduction findFormulairePlans(int formulaireProduitId);
        ProduitPackage findFormulaireProduitPackage(int formulaireProduitId);
        ProduitPretConsomer findFormulaireProduitPret(int formulaireProduitId);
        FormeDetails getFormeDetails(int formulaireProduitId, int? pdvId);
        IEnumerable<FormeDetails> getFormeDetailsAll(int AboId);
        Task<bool> updateFormulaireProduitVendable(int id, ProduitVendable newProduit);
        Task<bool> updateFormulaireFournisseur(int id, FournisseurProduits newFournisseur);
        //Task<bool> AjouterPrixProduit(int id, IEnumerable<PrixProduit> prix);
        Task<bool> AjouterFormeProduit(int id, Forme_Produit forme);
        Task<bool> AjouterFormeProduitPackage(int id, Forme_Produit forme);
        Task<bool> AjouterFormeProduitPretConsomer(int id, Forme_Produit forme);
        Task<MatStockFlagModel> updateFormulaireQteProduite(int id, List<PlanificationdeProductionModel> plans);
        Task<bool> deleteProduitVendable(int ID);
        Task<bool> deleteProduitVendablesStockes(int ID);
        Task<bool> deletePlan(int ID);
        Task<bool> updateCoutDeRevient(int ProduitId, decimal coutDeRevient, int fichId);

        bool ProduitStocker(int id);
        decimal getProduitCoutDeRevient(int Id);
        FicheTechniqueBridge GetFicheTech(int Id);
        IEnumerable<PlanificationdeProduction> getDetailPlan(int aboId, int Id);
        IEnumerable<PlanificationdeProduction> getDetailPlanByPlan(int aboId, int Id, int planId);
        IEnumerable<PlanificationdeProduction> getDetailPlanProduitList(int aboId, int Id);
        IEnumerable<BonDetails> getBonDetails(int Id);
        Task<bool> AccepterDemande(int Id, int adresse, string validId);
        Task<bool> AccepterRetour(int Id, string adresse, string valideId);
        Task<bool> ValiderLivraison(int demandeId);
        Task<MatStockFlagModel> AccepterPlanification(int planificationId, int adresse, List<BonViewModel> ListBons, string validePar);
        Task<MatStockFlagModel> ValiderPlanification(int planificationI);
        IEnumerable<PlanificationdeProduction> getlPlansWithQte(int aboId);
        IEnumerable<ProduitAppro> GetProduitAppros(int aboId,int atelierId, int produiId);
        decimal FindFormulaireProduitAppro(int aboId,int current, int produitApproId);
        Task<bool> ValiderApprovisionnement(int ApprovisionnementId, string valideId,int pdvId);
        Task<bool> ValiderApprovisionnementProduit(int ApprovisionnementId,string userid,int pdvId);
        decimal GetPrix(int Id, decimal qte);
        IEnumerable<Forme_Produit> getListFormeProduits(int AboId, int produitId);
        IEnumerable<Forme_Produit> getListFormes(int AboId, int produitId);
        IEnumerable<Forme_Produit> getListFormeProduitsPackage(int AboId, int produitId);
        IEnumerable<Forme_Produit> getListFormeProduitsPret(int AboId, int produitId);
        IEnumerable<MatierePremiere> getListMatiere(int Id);
        IEnumerable<FournisseurProduits> ListeFournisseurProduits(int Id,int? VilleId,int? statut);
        Task<bool?> AjouterMatiere(int idFournisseur, List<int> listMatiere);
        FournisseurProduits findFormulaireFournisseur(int formulaireFourisseurId);
        string GetUnitebyFrome(int formeProduitId);
        bool ProduitConsoStocker(int id);
        Task<bool> deleteFournisseurPrdConso(int ID, int code);
        Task<bool> deleteProduitsLink(int ID, int code);
        Task<bool> deleteProduitConso(int ID);
        IEnumerable<Fournisseur_ProduitConso> ListeProduitFournisseur(int fournisseurId);
        Task<bool> deletePositionVente(int ID, int code);
        Task<bool> deleteSalle(int ID, int code);
        IEnumerable<PdV_ProduitsPret> getListProduitConsomableEnStock(int Id, int pdv);
        Task<bool> updateFormePrix(int formeId, decimal prix);
        Task<int> GetPlanificationsForStock(int stockId);
        Task PlansSeenByStock(int lieuId);
        Task<int> GetPlanificationsForAtelier(int atelierId);
        Task PlansSeenByAtelier(int planId);
        IEnumerable<Demande_Pret> getListDemandesPretStock(int stockID, int aboID, string date, int? atelierID, string etat);
        IEnumerable<Demande_Pret> getListDemandesPretAtelier(int atelierID, int aboID, string date, int? stockID, string etat);
        IEnumerable<DemandePret_Details> getListDetailsDemandesPretStock(int demandePretID, int aboID, int stockID);
        IEnumerable<DemandePret_Details> getListDetailsDemandesPretAtelier(int demandePretID, int aboID, int atelierID);
        Task<bool> AccepterDemandePrets(int demandeID, int adresse, List<BonViewModel> listBon);
        Task<bool> ValiderDemande(int demandeID, int atelier);
        IEnumerable<ProduitPret_Atelier> getListProduitPretAtelier(int atelierID, int? produitID);
        IEnumerable<PlanificationdeProduction> getDetailPlanFormeList(int Id, int produitId);
        IEnumerable<ProduitAppro> getListProduitMaisonStock(int atelierID, int aboID);
        ViewBonSortieModel getQteEnMagasin(int atelierId, int matiereId,int uniteId);
        IEnumerable<MatiereStock_Atelier> getListMatiereStock(int atelierId, int aboId);
        ViewBonSortieModel getQuantiteEnMagasin(int atelierId, int matiereId);
        ViewBonSortieModel getQuantiteAtelier(int matiereStockID);
        ViewBonSortieModel getQuantiteStock(int matiereStockID);
        IEnumerable<ProduitPackage> getProduitpackagePointVente(int formulaireProduitId);
        Task<int?> PackageForme(Package_Forme packForme);
        IEnumerable<Package_Forme> getListeFichePackage(int? aboId, int? produitID);
        IEnumerable<PackageForme_Details> getDetailsFichePackage(int produitID);
        Package_Forme getPackageFormeDetails(int packageId, int formeId);
        Sous_ProduitPackage findFormulaireSousProduitPackage(int sousProdId);
        decimal getQteProduitPretAtelier(int atelierID, int formeID);
        Task<int?> DemanderComposants(List<SousProduitsViewModel> sousProds, int atelierID, int lieuId, int aboId);
        Task<int?> ApprovisionnementProduitPackage(Approvisionnement_ProduitPackage approvisionnement);
        Task<bool> ValiderApprovisionnementPackage(int ApprovisionnementId, int pdvId);
        IEnumerable<ProduitPack_Atelier> getProduitPackEnMagasin(int atelierId);
        IEnumerable<ProduitPack_Atelier> FindFormesProduitPackEnMagasin(int produitId, int atelierID);
        ProduitPack_Atelier FindformulairePackageMagasin(int ID);
        IEnumerable<Approvisionnement_ProduitPackage> getListApprovisionnement(int aboId, int? atelierID, int? pdvId, string date);
        Task<int?> DeclarationPerte(Perte_Pret perte_Pret);
        IEnumerable<Perte_Pret> getListPertesPrets(int aboId, int? atelierID, string date);
        IEnumerable<ProduitPret_Atelier> findFormesPret_Atelier(int atelierID, int prduitpretID);
        ProduitPret_Atelier FindformulairePretMagasin(int ID);
        Task<int?> DemanderPret(Demande_Pret demande_Pret);
        IEnumerable<ProduitPretConsomer> getListProduitConsomableEnMagasin(int Id, int pdv,int? familleID);
        IEnumerable<Forme_Produit> getFormesConsomableEnMagasin(int Id, int pdv);
        Task<int?> CreateProduitBase(ProduitBase produitBase);
        IEnumerable<ProduitBase> getListProduitBase(int Id, int? formeID);
        Task<bool> updateFormulaireProduitBase(int id, ProduitBase newProduit);
        Task<bool> deleteProduitBase(int ID);
        Task<bool> updateCoutDeRevientBase(int ProduitId, decimal coutDeRevient, int fichId);
        ProduitBase findFormulaireProduitBase(int produitId, int aboId);
        IEnumerable<PlanificationJourneeBase> getListPlansBaseAtelier(int Id, int? atelierId, string etat, int? lieu, string date);
        Task<int?> PlanifierBase(PlanificationJourneeBase plan);
        Task<MatStockFlagModel> AccepterPlanificationBase(int planificationId, int adresse, List<BonViewModel> ListBons, string validePar);
        Task<MatStockFlagModel> ValiderPlanificationBase(int planificationId);
        IEnumerable<PlanificationdeProductionBase> getDetailPlanBase(int aboId, int Id);
        FicheTechniqueProduitBase GetFicheTechBase(int Id);
        IEnumerable<PlanificationdeProductionBase> getDetailPlanProduitListBase(int aboId, int Id);
        IEnumerable<PlanificationdeProductionBase> getDetailByPlanBase(int aboId, int Id, int planId);
        Task<bool?> updateFormulaireQteProduitebase(int id, List<PlanificationdeProductionBaseModel> plans);
        IEnumerable<ProdBase_Atelier> getListProduitBaseStock(int atelierID, int aboID);
        IEnumerable<FicheTech_ProduitBase> GetFicheTech_ProduitBases(int Id, int aboID);
        Task<bool> updateFicheForme(int formeId, int produitID, decimal qteForme);
        IEnumerable<Taux_TVA> getListTVA(int aboID);
        Task<int?> CreateTVA(Taux_TVA tauxTVA);
        Task<List<matStockViewModel>> SetProdsDeBase(int planificationId);
        List<matStockViewModel> getProdBasePlan(int planificationId);
        Task<int?> PlanificationBaseAuto(int planID);
        IEnumerable<MatierePremiere> fillMatiereDemande(int planJourneeID);
        Task<MatStockFlagModel> CloturerDemande(int demandeID);
        Task<int> CreateDemandeApprov(DemandeApprov demandeApprov);
        Task<List<DemandeApprov>> GetListDemandeApprovs(int aboId, int? pointVenteID, string etat, string dateLiv);
        Task<List<DemandeApprov_Details>> GetListDemandeApprovsDetails(int aboId, int? poinVenteID, int? demandeID, string etat, int? produitCateg);
    }
}
