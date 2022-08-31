using AutoMapper;
using Domain.Entities;
using Domain.Models;
using Microsoft.EntityFrameworkCore.Storage;
using Repository.IRepositories;
using Repository.UnitOfWork;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ProduitVendableService : IProduitVendableService
    {
        private readonly IProduitVendableRepository produitVendableRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        public ProduitVendableService(IProduitVendableRepository produitVendableRepository, IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.produitVendableRepository = produitVendableRepository;
        }
        public async Task<bool> CreateProduitVendable(ProduitVendableModel produitModel)
        {
            using (IDbContextTransaction transaction = this.unitOfWork.BeginTransaction())
            {
                try
                {
                    // Créer le produit
                    ProduitVendable produit = mapper.Map<ProduitVendableModel, ProduitVendable>(produitModel);
                    var idProduitVendable = await this.produitVendableRepository.CreateProduit(produit);
                    if (idProduitVendable == null)
                    {
                        return false;
                    }
                    transaction.Commit();
                    return true;

                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public Task<bool> deletePlan(int ID)
        {
            return this.produitVendableRepository.deletePlan(ID);
        }

        public Task<bool> deleteProduitVendable(int ID)
        {
            return this.produitVendableRepository.deleteProduitVendable(ID);
        }

        public Task<bool> deleteProduitConso(int ID)
        {
            return this.produitVendableRepository.deleteProduitConso(ID);
        }

        public Task<bool> deleteProduitVendableStockes(int ID)
        {
            return this.produitVendableRepository.deleteProduitVendablesStockes(ID);
        }

        public PlanificationdeProductionModel findFormulairePlans(int formulaireProduitId)
        {
            return mapper.Map<PlanificationdeProduction, PlanificationdeProductionModel>(this.produitVendableRepository.findFormulairePlans(formulaireProduitId));
        }

        public ProduitVendableModel findFormulaireProduitVendable(int formulaireProduitId)
        {
            return mapper.Map<ProduitVendable, ProduitVendableModel>(this.produitVendableRepository.findFormulaireProduit(formulaireProduitId));
        }
        public ProduitVendableModel findFormulaireProduitVendablePDF(int formulaireProduitId)
        {
            return mapper.Map<ProduitVendable, ProduitVendableModel>(this.produitVendableRepository.findFormulaireProduitPDF(formulaireProduitId));
        }

        public IEnumerable<Section_StockageModel> getListeSectionStockage()
        {
            return mapper.Map<IEnumerable<Section_Stockage>, IEnumerable<Section_StockageModel>>(this.produitVendableRepository.getListSectionStockage());
        }

        public IEnumerable<FamilleProduitModel> getListFamilleProduit(int Id)
        {
            return mapper.Map<IEnumerable<FamilleProduit>, IEnumerable<FamilleProduitModel>>(this.produitVendableRepository.getListFamilleProduit(Id));
        }

        public IEnumerable<Forme_StockageModel> getListFormeSotckage(int Id)
        {
            return mapper.Map<IEnumerable<Forme_Stockage>, IEnumerable<Forme_StockageModel>>(produitVendableRepository.getListFormeSotckage(Id));
        }

        public IEnumerable<PlanificationJourneeModel> getListPansDemandes(int Id, int adresse)
        {
            return mapper.Map<IEnumerable<PlanificationJournee>, IEnumerable<PlanificationJourneeModel>>(produitVendableRepository.getListPansDemandes(Id, adresse));
        }
        public IEnumerable<PlanificationJourneeModel> getListPansRetours(int Id)
        {
            return mapper.Map<IEnumerable<PlanificationJournee>, IEnumerable<PlanificationJourneeModel>>(produitVendableRepository.getListPansRetours(Id));
        } 
        public IEnumerable<PlanificationJourneeModel> getListPansAtelier(int Id, int? atelierId, string etat, int? lieu, string date)
        {
            return mapper.Map<IEnumerable<PlanificationJournee>, IEnumerable<PlanificationJourneeModel>>(produitVendableRepository.getListPansAtelier(Id, atelierId,etat,lieu,date));
        }   
        public async Task<IEnumerable<PlanificationJourneeModel>> getListPansStock(int Id, int lieuId,string etat,int? atelier,string date)
        {
            await produitVendableRepository.PlansSeenByStock(lieuId);
            return mapper.Map<IEnumerable<PlanificationJournee>, IEnumerable<PlanificationJourneeModel>>(produitVendableRepository.getListPansStock(Id, lieuId, etat, atelier, date));
        }

        public IEnumerable<ProduitVendableModel> getListProduitVendable(int Id, int? categ, int? SousCateg, string name)
        {
            return mapper.Map<IEnumerable<ProduitVendable>, IEnumerable<ProduitVendableModel>>(produitVendableRepository.getListProduitVendable(Id, categ, SousCateg, name));
        }
        public IEnumerable<ProduitVendableModel> getListProduitVendableAvecPlanif(int Id,int AboId)
        {
            return mapper.Map<IEnumerable<ProduitVendable>, IEnumerable<ProduitVendableModel>>(produitVendableRepository.getListProduitVendableAvecPlanif(Id, AboId));
        }

        public IEnumerable<ProduitConsomableStokageModel> getListProduitVendableStocker(int Id)
        {
            return mapper.Map<IEnumerable<ProduitConsomableStokage>, IEnumerable<ProduitConsomableStokageModel>>(produitVendableRepository.getListProduitsStockes(Id));
        }
        public ProduitConsomableStokageModel finformulaireProduitConsomableStockage(int produitConsoStockageId)
        {
            return mapper.Map<ProduitConsomableStokage, ProduitConsomableStokageModel>(produitVendableRepository.finformulaireProduitConsomableStockage(produitConsoStockageId));
        } 
        public IEnumerable<ProduitConsomableStokageModel> getListProduitVendableStockerByAtelier(int? Id)
        {
            return mapper.Map<IEnumerable<ProduitConsomableStokage>, IEnumerable<ProduitConsomableStokageModel>>(produitVendableRepository.getListProduitsStockesByAtelier(Id));
        }
        public IEnumerable<ProduitConsomableStokageModel> getListProduitVendableStockerByProduit(int? Id,int produitId)
        {
            return mapper.Map<IEnumerable<ProduitConsomableStokage>, IEnumerable<ProduitConsomableStokageModel>>(produitVendableRepository.getListProduitsStockesByProduit(Id,produitId));
        }
        public IEnumerable<Unite_MesureModel> getListUniteMesure()
        {
            return mapper.Map<IEnumerable<Unite_Mesure>, IEnumerable<Unite_MesureModel>>(produitVendableRepository.getListUniteMesure());
        }

        /*public async Task<bool> PlanifierAProduire(PlanificationdeProductionModel planificationdeProductionModel)
        {

            using (IDbContextTransaction transaction = this.unitOfWork.BeginTransaction())
            {
                try
                {

                    PlanificationdeProduction produit = mapper.Map<PlanificationdeProductionModel, PlanificationdeProduction>(planificationdeProductionModel);
                    var idProduitVendable = await this.produitVendableRepository.PlanifierAProduire(produit);
                    if (idProduitVendable == null)
                    {
                        return false;
                    }
                    transaction.Commit();
                    return true;

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }*/



        public bool ProduitStocker(int ID)
        {
            var res = this.produitVendableRepository.ProduitStocker(ID);
            return res;
        }

        public async Task<bool> StockerProduitVendable(int Id, ProduitConsomableStokageModel produitStockerModel)
        {

            using (IDbContextTransaction transaction = this.unitOfWork.BeginTransaction())
            {
                try
                {

                    ProduitConsomableStokage produit = mapper.Map<ProduitConsomableStokageModel, ProduitConsomableStokage>(produitStockerModel);
                    var idProduitVendable = await this.produitVendableRepository.StockerProduit(Id, produit);
                    if (idProduitVendable == null)
                    {
                        return false;
                    }
                    transaction.Commit();
                    return true;

                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public Task<MatStockFlagModel> updateFormulaireQteProduite(int id, List<PlanificationdeProductionModel> plans)
        {
            return produitVendableRepository.updateFormulaireQteProduite(id, plans);
        }

        public Task<bool> updateFormulaireProduitVendable(int id, ProduitVendableModel produitModel)
        {
            ProduitVendable produit = mapper.Map<ProduitVendableModel, ProduitVendable>(produitModel);
            return this.produitVendableRepository.updateFormulaireProduitVendable(id, produit);
        }


        public async Task<int?> Planifier(PlanificationJourneeModel planModel)
        {
            using (IDbContextTransaction transaction = this.unitOfWork.BeginTransaction())
            {
                try
                {
                    // Créer le transporteur
                    foreach (PlanificationdeProductionModel PlanProd in planModel.Planification_Production)
                    {
                        PlanProd.PlanificationProduction_DateCreation = DateTime.Now;
                        PlanProd.PlanificationProduction_AbonnementId = planModel.PlanificationJournee_AbonnementID;
                        //value +=getProduitCoutDeRevient(PlanProd.PlanificationProduction_ProduitVendableId);

                    }

                    planModel.BonDe_Sortie.BonDeSortie_DateCreation = DateTime.Now;
                    planModel.BonDe_Sortie.BonDeSortie_AbonnementID = planModel.PlanificationJournee_AbonnementID;
                    planModel.BonDe_Sortie.BonDeSortie_DateProduction = planModel.PlanificationJournee_Date;
                    planModel.BonDe_Sortie.BonDeSortie_Libelle = "Bon De planification du :" + planModel.BonDe_Sortie.BonDeSortie_DateCreation.ToShortDateString();
                    foreach (BonDetailsModel details in planModel.BonDe_Sortie.Bon_Details)
                    {
                        details.BonDeSortie_BonDeSortieID = planModel.BonDe_Sortie.BonDeSortie_ID;

                    }
                    PlanificationJournee plan = mapper.Map<PlanificationJourneeModel, PlanificationJournee>(planModel);
                    var idTransporteur = await this.produitVendableRepository.Planifier(plan);
                    transaction.Commit();
                    return plan.PlanificationJournee_ID;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return null;
                }
            }
        }

        public FicheTechniqueBridgeModel GetFicheTech(int Id)
        {
            return mapper.Map<FicheTechniqueBridge, FicheTechniqueBridgeModel>(this.produitVendableRepository.GetFicheTech(Id));
        }
        public IEnumerable<PlanificationdeProductionModel> getDetailPlan(int aboId, int Id)
        {
            return mapper.Map<IEnumerable<PlanificationdeProduction>, IEnumerable<PlanificationdeProductionModel>>(produitVendableRepository.getDetailPlan(aboId, Id));
        }
        public IEnumerable<PlanificationdeProductionModel> getDetailPlanByPlan(int aboId, int Id, int planId)
        {
            return mapper.Map<IEnumerable<PlanificationdeProduction>, IEnumerable<PlanificationdeProductionModel>>(produitVendableRepository.getDetailPlanByPlan(aboId, Id,planId));
        }
        public IEnumerable<PlanificationdeProductionModel> getDetailPlanProduitList(int aboId, int Id)
        {
            return mapper.Map<IEnumerable<PlanificationdeProduction>, IEnumerable<PlanificationdeProductionModel>>(produitVendableRepository.getDetailPlanProduitList(aboId, Id));
        }

        public IEnumerable<BonDetailsModel> getBonDetails(int Id)
        {
            return mapper.Map<IEnumerable<BonDetails>, IEnumerable<BonDetailsModel>>(this.produitVendableRepository.getBonDetails(Id));

        }

        public async Task<bool> CreerDemande(DemandeModel demandeModel)
        {
            using (IDbContextTransaction transaction = this.unitOfWork.BeginTransaction())
            {
                try
                {
                    // Créer le transporteur


                    demandeModel.BonDe_Sortie.BonDeSortie_DateCreation = DateTime.Now;
                    demandeModel.BonDe_Sortie.BonDeSortie_AbonnementID = demandeModel.Demande_AbonnementID;
                    demandeModel.BonDe_Sortie.BonDeSortie_Libelle = "Bon Demande " + demandeModel.Demande_Type;
                    foreach (BonDetailsModel details in demandeModel.BonDe_Sortie.Bon_Details)
                    {
                        details.BonDeSortie_BonDeSortieID = demandeModel.BonDe_Sortie.BonDeSortie_ID;

                    }
                    // demandeModel.BonDe_Sortie.BonDeSortie_DateProduction = demandeModel.Planification_Journee.PlanificationJournee_Date;
                    Demande demande = mapper.Map<DemandeModel, Demande>(demandeModel);
                    var idDemande = await this.produitVendableRepository.CreerDemande(demande);
                    transaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public IEnumerable<DemandeModel> GetDemandes(int Id, string date, int? lieuID, int? atelierID)
        {
            return mapper.Map<IEnumerable<Demande>, IEnumerable<DemandeModel>>(produitVendableRepository.GetDemandes(Id, date, lieuID, atelierID));

        } 
        public IEnumerable<DemandeModel> GetDemandesAtelier(int Id, int atelierId)
        {
            return mapper.Map<IEnumerable<Demande>, IEnumerable<DemandeModel>>(produitVendableRepository.GetDemandesAtelier(Id, atelierId));

        } 
        public IEnumerable<DemandeModel> GetDemandesStock(int Id, int lieuId)
        {
            return mapper.Map<IEnumerable<Demande>, IEnumerable<DemandeModel>>(produitVendableRepository.GetDemandesStock(Id, lieuId));

        }

        public async Task<bool> AccepterDemande(int demandeId, int adresse, string valideId)
        {
            return await this.produitVendableRepository.AccepterDemande(demandeId, adresse,valideId);
        }

        public async Task<bool> ValiderLivraison(int demandeId)
        {
            return await produitVendableRepository.ValiderLivraison(demandeId);
        }

        public IEnumerable<MouvementStockModel> GetMouvementStocks(int Id)
        {
            return mapper.Map<IEnumerable<MouvementStock>, IEnumerable<MouvementStockModel>>(produitVendableRepository.GetMouvementStocks(Id));
        } 
        public IEnumerable<MouvementStockModel> GetMouvementStocksActiveOnly(int Id,int? matiere, string date)
        {
            return mapper.Map<IEnumerable<MouvementStock>, IEnumerable<MouvementStockModel>>(produitVendableRepository.GetMouvementStocksActiveOnly(Id,matiere,date));
        }
        public IEnumerable<MouvementStockModel> GetMouvementStocksBystock(int Id, int lieuId)
        {
            return mapper.Map<IEnumerable<MouvementStock>, IEnumerable<MouvementStockModel>>(produitVendableRepository.GetMouvementStocksBystock(Id, lieuId));
        }
        public IEnumerable<MouvementStockModel> GetMouvementStocksBystockActiveOnly(int Id, int lieuId, int? matiere, string date)
        {
            return mapper.Map<IEnumerable<MouvementStock>, IEnumerable<MouvementStockModel>>(produitVendableRepository.GetMouvementStocksBystockActiveOnly(Id, lieuId,matiere,date));
        }

        public async Task<MatStockFlagModel> AccepterPlanification(int planificationId, int adresse, List<BonViewModel> ListBons, string validePar)
        {
            return  await produitVendableRepository.AccepterPlanification(planificationId, adresse, ListBons, validePar);

        }

        public async Task<MatStockFlagModel> ValiderPlanification(int planificationI)
        {
            await produitVendableRepository.PlansSeenByAtelier(planificationI);
            return await produitVendableRepository.ValiderPlanification(planificationI);
        }

        public IEnumerable<PlanificationdeProductionModel> getlPlansWithQte(int aboId)
        {
            return mapper.Map<IEnumerable<PlanificationdeProduction>, IEnumerable<PlanificationdeProductionModel>>(produitVendableRepository.getlPlansWithQte(aboId));
        }

        public IEnumerable<ProduitApproModel> GetProduitAppros(int aboId,int atelierId, int produitId)
        {
            return mapper.Map<IEnumerable<ProduitAppro>, IEnumerable<ProduitApproModel>>(produitVendableRepository.GetProduitAppros(aboId,atelierId,produitId));
        }

        public decimal FindFormulaireProduitAppro(int aboId,int current, int produitApproId)
        {
            return produitVendableRepository.FindFormulaireProduitAppro(aboId, current, produitApproId);
        }

        public async Task<bool> Approvisionnement(ApprovisionnementModel approvisionnementModel)
        {
            using (IDbContextTransaction transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    Approvisionnement appro = mapper.Map<ApprovisionnementModel, Approvisionnement>(approvisionnementModel);
                    await produitVendableRepository.Approvisionnement(appro);
                    transaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public IEnumerable<ApprovisionnementModel> ListeApprovisionnement(int Id, int? ateId, int? point, string date, int? etat)
        {
            return mapper.Map<IEnumerable<Approvisionnement>, IEnumerable<ApprovisionnementModel>>(produitVendableRepository.ListeApprovisionnement(Id,ateId,point,date,etat));
        }
        public IEnumerable<ApprovisionnementModel> ListeApprovisionnementAtelier(int Id, int atelierId, int? pdv, string date, int? etat)
        {
            return mapper.Map<IEnumerable<Approvisionnement>, IEnumerable<ApprovisionnementModel>>(produitVendableRepository.ListeApprovisionnementAtelier(Id, atelierId, pdv, date, etat));
        }  
        public IEnumerable<ApprovisionnementModel> ListeApprovisionnementPointVente(int Id, int pdvId, int? atelier, string date)
        {
            return mapper.Map<IEnumerable<Approvisionnement>, IEnumerable<ApprovisionnementModel>>(produitVendableRepository.ListeApprovisionnementPointVente(Id, pdvId, atelier, date));
        }

        public async Task<bool> ValiderApprovisionnement(int ApprovisionnementId,string valideId, int pdvId)
        {
            return await produitVendableRepository.ValiderApprovisionnement(ApprovisionnementId, valideId,pdvId);

        }

        public async Task<bool> PackageProduit(ProduitPackageModel produitPackageModel)
        {
            using (IDbContextTransaction transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    ProduitPackage appro = mapper.Map<ProduitPackageModel, ProduitPackage>(produitPackageModel);
                    await produitVendableRepository.PackageProduit(appro);
                    transaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        /*public async Task<bool> AjouterPrixProduit(int id, List<PrixProduitModel> prix)
        {
            using (IDbContextTransaction transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    // Créer le produit
                    var produit = mapper.Map<IEnumerable<PrixProduitModel>, IEnumerable<PrixProduit>>(prix);
                    var idprix = await produitVendableRepository.AjouterPrixProduit(id, produit);

                    transaction.Commit();
                    return true;

                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }

        }*/

        public decimal GetPrix(int Id, decimal qte)
        {
            return produitVendableRepository.GetPrix(Id, qte);
        }

        public IEnumerable<Forme_ProduitModel> getListFormeProduits(int AboId, int produitId)
        {
            return mapper.Map<IEnumerable<Forme_Produit>, IEnumerable<Forme_ProduitModel>>(produitVendableRepository.getListFormeProduits(AboId, produitId));
        }

        public async Task<bool> AjouterFormeProduitAsync(int id, Forme_ProduitModel forme)
        {
            using (IDbContextTransaction transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    // Créer le produit
                    var produit = mapper.Map<Forme_ProduitModel, Forme_Produit>(forme);
                    var idprix = await produitVendableRepository.AjouterFormeProduit(id, produit);
                    if (idprix == false)
                    {
                        return false;
                    }
                    transaction.Commit();
                    return true;

                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public async Task<bool> RetourStock(RetourStockModel demandeModel)
        {
            using (IDbContextTransaction transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    // Créer le transporteur


                    demandeModel.BonDe_Sortie.BonDeSortie_DateCreation = DateTime.Now;
                    demandeModel.BonDe_Sortie.BonDeSortie_AbonnementID = demandeModel.RetourStok_AbonnementID;
                    demandeModel.BonDe_Sortie.BonDeSortie_Libelle = "Bon Demande " + "Retour du stock";
                    foreach (BonDetailsModel details in demandeModel.BonDe_Sortie.Bon_Details)
                    {
                        details.BonDeSortie_BonDeSortieID = demandeModel.BonDe_Sortie.BonDeSortie_ID;

                    }
                    // demandeModel.BonDe_Sortie.BonDeSortie_DateProduction = demandeModel.Planification_Journee.PlanificationJournee_Date;
                    RetourStock demande = mapper.Map<RetourStockModel, RetourStock>(demandeModel);
                    var idDemande = await produitVendableRepository.RetourStock(demande);
                    transaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public async Task<bool> AccepterRetour(int demandeId, string adresse,string valideId)
        {
            return await produitVendableRepository.AccepterRetour(demandeId, adresse, valideId);
        }

        public IEnumerable<RetourStockModel> GetRetourStocks(int Id)
        {
            return mapper.Map<IEnumerable<RetourStock>, IEnumerable<RetourStockModel>>(produitVendableRepository.GetRetourStocks(Id));        
        }     
        public IEnumerable<RetourStockModel> GetRetourStocksByAtelier(int Id, int atelierId)
        {
            return mapper.Map<IEnumerable<RetourStock>, IEnumerable<RetourStockModel>>(produitVendableRepository.GetRetourStocksByAtelier(Id,atelierId));
        }     
        public IEnumerable<RetourStockModel> GetRetourStocksByStock(int Id, int stockId)
        {
            return mapper.Map<IEnumerable<RetourStock>, IEnumerable<RetourStockModel>>(produitVendableRepository.GetRetourStocksByStock(Id,stockId));

        }

        public async Task<bool> ProduitConsomable(ProduitPretConsomerModel produitPretModel)
        {
            using (IDbContextTransaction transaction = this.unitOfWork.BeginTransaction())
            {
                try
                {
                    // Créer le produit
                    ProduitPretConsomer produitPret = mapper.Map<ProduitPretConsomerModel, ProduitPretConsomer>(produitPretModel);
                    var idProduitVendable = await produitVendableRepository.ProduitConsomable(produitPret);
                    if (idProduitVendable == null)
                    {
                        return false;
                    }
                    transaction.Commit();
                    return true;

                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public IEnumerable<ProduitPretConsomerModel> getListProduitConsomable(int Id,int? categ,int? SousCateg)
        {
            return mapper.Map<IEnumerable<ProduitPretConsomer>, IEnumerable<ProduitPretConsomerModel>>(produitVendableRepository.getListProduitConsomable(Id,categ,SousCateg));
        }

        public async Task<bool> FournisseurProduit(FournisseurProduitsModel fournisseurP)
        {
            using (IDbContextTransaction transaction = this.unitOfWork.BeginTransaction())
            {
                try
                {
                    // Créer le produit
                    FournisseurProduits fournisseur = mapper.Map<FournisseurProduitsModel, FournisseurProduits>(fournisseurP);
                    var idProduitVendable = await produitVendableRepository.FournisseurProduits(fournisseur);
                    if (idProduitVendable == null)
                    {
                        return false;
                    }
                    transaction.Commit();
                    return true;

                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public IEnumerable<FonctionModel> getListFonction()
        {
            return mapper.Map<IEnumerable<Fonction>, IEnumerable<FonctionModel>>(produitVendableRepository.getListFonction());
        }

        public IEnumerable<VilleModel> getListeVille()
        {
            return mapper.Map<IEnumerable<Ville>, IEnumerable<VilleModel>>(produitVendableRepository.getListVilles());
        }

        public IEnumerable<MatierePremiereModel> getListMatiere(int Id)
        {
            return mapper.Map<IEnumerable<MatierePremiere>, IEnumerable<MatierePremiereModel>>(produitVendableRepository.getListMatiere(Id));
        }

        public async Task<bool?> AjouterMatiere(int idFournisseur, List<int> listMatiere)
        {
            using (IDbContextTransaction transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    var id = await produitVendableRepository.AjouterMatiere(idFournisseur, listMatiere);
                    if (id == null)
                        return null;
                    transaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public FournisseurProduitsModel findFormulaireFournisseur(int formulaireFourisseurId)
        {
            return mapper.Map<FournisseurProduits, FournisseurProduitsModel>(produitVendableRepository.findFormulaireFournisseur(formulaireFourisseurId));
        }

        public async Task<bool> ApprovisionnementProduit(Approvisionnement_ProduitConsoModel approvisionnementModel)
        {
            using (IDbContextTransaction transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    Approvisionnement_ProduitConso produit = mapper.Map<Approvisionnement_ProduitConsoModel, Approvisionnement_ProduitConso>(approvisionnementModel);
                    var idProduitVendable = await produitVendableRepository.ApprovisionnementProduitConso(produit);
                    if (idProduitVendable == null)
                    {
                        return false;
                    }
                    transaction.Commit();
                    return true;

                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public IEnumerable<Approvisionnement_ProduitConsoModel> ListeApprovisionnementProduit(int Id)
        {
            return mapper.Map<IEnumerable<Approvisionnement_ProduitConso>, IEnumerable<Approvisionnement_ProduitConsoModel>>(produitVendableRepository.ListeApprovisionnementProduit(Id));
        }
        public IEnumerable<Approvisionnement_ProduitConsoModel> ListeApprovisionnementProduitAtelier(int Id, int atelierId, int? pdv, string date)
        {
            return mapper.Map<IEnumerable<Approvisionnement_ProduitConso>, IEnumerable<Approvisionnement_ProduitConsoModel>>(produitVendableRepository.ListeApprovisionnementProduitAtelier(Id,atelierId, pdv, date));
        }   
        public IEnumerable<Approvisionnement_ProduitConsoModel> ListeApprovisionnementProduitPdv(int Id, int pdvId, int? atelier, string date)
        {
            return mapper.Map<IEnumerable<Approvisionnement_ProduitConso>, IEnumerable<Approvisionnement_ProduitConsoModel>>(produitVendableRepository.ListeApprovisionnementProduitPdv(Id,pdvId, atelier, date));
        }

        public async Task<bool> ValiderApprovisionnementproduit(int ApprovisionnementId,string userid,int pdvId)
        {
            return await produitVendableRepository.ValiderApprovisionnementProduit(ApprovisionnementId,userid, pdvId);
        }

        public Forme_ProduitModel findFormulaireFormeProduit(int formulaireProduitId)
        {
            return mapper.Map<Forme_Produit, Forme_ProduitModel>(produitVendableRepository.findFormulaireFormeProduit(formulaireProduitId));
        }

        public IEnumerable<ProduitPackageModel> getListProduitpackage(int aboId)
        {
            return mapper.Map<IEnumerable<ProduitPackage>, IEnumerable<ProduitPackageModel>>(produitVendableRepository.getListProduitpackage(aboId));
        }
        public IEnumerable<ProduitPackageModel> getListProduitpackagePointVente(int aboId, int pdvid)
        {
            return mapper.Map<IEnumerable<ProduitPackage>, IEnumerable<ProduitPackageModel>>(produitVendableRepository.getListProduitpackagePointVente(aboId, pdvid));
        }

        public ProduitPackageModel findFormulaireProduitPackage(int formulaireProduitId)
        {
            return mapper.Map<ProduitPackage, ProduitPackageModel>(produitVendableRepository.findFormulaireProduitPackage(formulaireProduitId));
        }

        public async Task<bool> AjouterFormeProduitPackageAsync(int id, Forme_ProduitModel forme)
        {
            using (IDbContextTransaction transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    // Créer le produit
                    var produit = mapper.Map<Forme_ProduitModel, Forme_Produit>(forme);
                    var idprix = await produitVendableRepository.AjouterFormeProduitPackage(id, produit);
                    if (idprix == false)
                    {
                        return false;
                    }
                    transaction.Commit();
                    return true;

                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public async Task<bool> AjouterFormeProduitPretAsync(int id, Forme_ProduitModel forme)
        {
            using (IDbContextTransaction transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    // Créer le produit
                    var produit = mapper.Map<Forme_ProduitModel, Forme_Produit>(forme);
                    var idprix = await produitVendableRepository.AjouterFormeProduitPretConsomer(id, produit);
                    if (idprix == false)
                    {
                        return false;
                    }
                    transaction.Commit();
                    return true;

                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public IEnumerable<Forme_ProduitModel> getListFormeProduitsPackage(int AboId, int produitId)
        {
            return mapper.Map<IEnumerable<Forme_Produit>, IEnumerable<Forme_ProduitModel>>(produitVendableRepository.getListFormeProduitsPackage(AboId, produitId));
        }

        public IEnumerable<Forme_ProduitModel> getListFormeProduitsPret(int AboId, int produitId)
        {
            return mapper.Map<IEnumerable<Forme_Produit>, IEnumerable<Forme_ProduitModel>>(produitVendableRepository.getListFormeProduitsPret(AboId, produitId));
        }

        public async Task<bool> ProductionPackage(PackageProductionModel productionpackageModel, int pdvId)
        {
            using (IDbContextTransaction transaction = this.unitOfWork.BeginTransaction())
            {
                try
                {
                    // Créer le produit
                    PackageProduction produit = mapper.Map<PackageProductionModel, PackageProduction>(productionpackageModel);
                    var idProduitVendable = await produitVendableRepository.ProductionPackage(produit, pdvId);
                    if (idProduitVendable == null)
                    {
                        return false;
                    }
                    transaction.Commit();
                    return true;

                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public FormeDetailsModel getFormeDetails(int formulaireProduitId, int? pdvId)
        {
            return mapper.Map<FormeDetails, FormeDetailsModel>(produitVendableRepository.getFormeDetails(formulaireProduitId , pdvId));
        }
        IEnumerable<FormeDetailsModel> IProduitVendableService.getFormeDetailsAll(int AboId)
        {
            return mapper.Map<IEnumerable<FormeDetails>, IEnumerable<FormeDetailsModel>>(produitVendableRepository.getFormeDetailsAll(AboId));
        }

        public ProduitPretConsomerModel findFormulaireProduitPret(int formulaireProduitId)
        {
            return mapper.Map<ProduitPretConsomer, ProduitPretConsomerModel>(produitVendableRepository.findFormulaireProduitPret(formulaireProduitId));
        }

        public IEnumerable<PdV_ProduitsPretModel> getListProduitsStockesPdv(int Id,int formeId)
        {
            return mapper.Map<IEnumerable<PdV_ProduitsPret>, IEnumerable<PdV_ProduitsPretModel>>(produitVendableRepository.getListProduitsStockesPdv(Id,formeId));
        }

        public IEnumerable<FournisseurProduitsModel> ListeFournisseurProduits(int AboId,int? VilleId,int? statut)
        {
            return mapper.Map<IEnumerable<FournisseurProduits>, IEnumerable<FournisseurProduitsModel>>(produitVendableRepository.ListeFournisseurProduits(AboId,VilleId,statut));
        }

        public IEnumerable<AtelierModel> getListAteliers(int aboId)
        {
            return mapper.Map<IEnumerable<Atelier>, IEnumerable<AtelierModel>>(produitVendableRepository.getListAteliers(aboId));
        }

        public IEnumerable<PrixProduitViewModel> getListPrix(int Id)
        {
            return mapper.Map<IEnumerable<PrixProduit>, IEnumerable<PrixProduitViewModel>>(produitVendableRepository.getListPrix(Id));
        }

        public IEnumerable<PrixProduitModel> getListPrixFormes(int Id)
        {
            return mapper.Map<IEnumerable<PrixProduit>, IEnumerable<PrixProduitModel>>(produitVendableRepository.getListPrixFormes(Id));
        }

        public IEnumerable<Forme_ProduitModel> getListFormes(int AboId, int produitId)
        {
            return mapper.Map<IEnumerable<Forme_Produit>, IEnumerable<Forme_ProduitModel>>(produitVendableRepository.getListFormes(AboId, produitId));
        }

        public IEnumerable<Sous_ProduitPackageModel> getListSousProduitpackage(int produitId)
        {
            return mapper.Map<IEnumerable<Sous_ProduitPackage>, IEnumerable<Sous_ProduitPackageModel>>(produitVendableRepository.getListSousProduitpackage(produitId));
        }

        public Task<bool> deleteFournisseurPrdConso(int ID, int code)
        {
            return this.produitVendableRepository.deleteFournisseurPrdConso(ID, code);
        }

        public IEnumerable<Unite_MesureModel> findFormulaireUniteMesure(int unite)
        {
            return mapper.Map<IEnumerable<Unite_Mesure>, IEnumerable<Unite_MesureModel>>(produitVendableRepository.findFormulaireUnite(unite));
        }

        public Task<bool> updateFormulaireFournisseur(int id, FournisseurProduitsModel newFournisseurModel)
        {
            FournisseurProduits newfournisseur = mapper.Map<FournisseurProduitsModel, FournisseurProduits>(newFournisseurModel);
            return produitVendableRepository.updateFormulaireFournisseur(id, newfournisseur);
        }

        public IEnumerable<Fournisseur_ProduitConsoModel> ListeProduitFournisseur(int fournisseurId)
        {
            return mapper.Map<IEnumerable<Fournisseur_ProduitConso>, IEnumerable<Fournisseur_ProduitConsoModel>>(produitVendableRepository.ListeProduitFournisseur(fournisseurId));
        }

        public Task<bool> deleteProduitsLink(int ID, int code)
        {
            return produitVendableRepository.deleteProduitsLink(ID, code);

        }

        public string GetUnitebyFrome(int formeProduitId)
        {
            return produitVendableRepository.GetUnitebyFrome(formeProduitId);
        }

        public Task<bool> deletePositionVente(int ID, int code)
        {
            return produitVendableRepository.deletePositionVente(ID, code);
        }

        public Task<bool> deleteSalle(int ID, int code)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PdV_ProduitsPretModel> getListProduitConsomableEnStock(int Id, int pdv)
        {
            return mapper.Map<IEnumerable<PdV_ProduitsPret>, IEnumerable<PdV_ProduitsPretModel>>(produitVendableRepository.getListProduitConsomableEnStock(Id, pdv));
        }

        public Task<bool> updateFormePrix(int formeId, decimal prix)
        {
            return produitVendableRepository.updateFormePrix(formeId, prix);
        }

        public async Task<int> GetPlanificationsForStock(int stockId)
        {
            return await produitVendableRepository.GetPlanificationsForStock(stockId);
        }

        public async Task<int> GetPlanificationsForAtelier(int atelierId)
        {
            return await produitVendableRepository.GetPlanificationsForAtelier(atelierId);
        }

        public IEnumerable<Demande_PretModel> getListDemandesPretStock(int stockID, int aboID, string date, int? atelierID, string etat)
        {
            return mapper.Map<IEnumerable<Demande_Pret>, IEnumerable<Demande_PretModel>>(produitVendableRepository.getListDemandesPretStock(stockID, aboID, date, atelierID, etat));
        }

        public IEnumerable<Demande_PretModel> getListDemandesPretAtelier(int atelierID, int aboID, string date, int? stockID, string etat)
        {
            return mapper.Map<IEnumerable<Demande_Pret>, IEnumerable<Demande_PretModel>>(produitVendableRepository.getListDemandesPretAtelier(atelierID, aboID, date, stockID, etat));
        }

        public IEnumerable<DemandePret_DetailsModel> getListDetailsDemandesPretStock(int demandePretID, int aboID, int stockID)
        {
            return mapper.Map<IEnumerable<DemandePret_Details>, IEnumerable<DemandePret_DetailsModel>>(produitVendableRepository.getListDetailsDemandesPretStock(demandePretID, aboID, stockID));
        }

        public IEnumerable<DemandePret_DetailsModel> getListDetailsDemandesPretAtelier(int demandePretID, int aboID, int atelierID)
        {
            return mapper.Map<IEnumerable<DemandePret_Details>, IEnumerable<DemandePret_DetailsModel>>(produitVendableRepository.getListDetailsDemandesPretAtelier(demandePretID, aboID, atelierID));
        }

        public async Task<bool> AccepterDemandePrets(int demandeID, int adresse, List<BonViewModel> listBon)
        {
            return await produitVendableRepository.AccepterDemandePrets(demandeID, adresse, listBon);
        }

        public async Task<bool> ValiderDemande(int demandeID, int atelier)
        {
            return await produitVendableRepository.ValiderDemande(demandeID, atelier);
        }

        public IEnumerable<ProduitPret_AtelierModel> getListProduitPretAtelier(int atelierID, int? produitID)
        {
            return mapper.Map<IEnumerable<ProduitPret_Atelier>, IEnumerable<ProduitPret_AtelierModel>>(produitVendableRepository.getListProduitPretAtelier(atelierID, produitID));
        }

        public IEnumerable<PlanificationdeProductionModel> getDetailPlanFormeList(int Id, int produitId)
        {
            return mapper.Map<IEnumerable<PlanificationdeProduction>, IEnumerable<PlanificationdeProductionModel>>(produitVendableRepository.getDetailPlanFormeList(Id, produitId));
        }

        public IEnumerable<ProduitApproModel> getListProduitMaisonStock(int atelierID, int aboID)
        {
            return mapper.Map<IEnumerable<ProduitAppro>, IEnumerable<ProduitApproModel>>(produitVendableRepository.getListProduitMaisonStock(atelierID, aboID));
        }

        public ViewBonSortieModel getQteEnMagasin(int atelierId, int matiereId, int uniteId)
        {
            return produitVendableRepository.getQteEnMagasin(atelierId, matiereId, uniteId);
        }

        public IEnumerable<MatiereStock_AtelierModel> getListMatiereStock(int atelierId, int aboId)
        {
            return mapper.Map<IEnumerable<MatiereStock_Atelier>, IEnumerable<MatiereStock_AtelierModel>>(produitVendableRepository.getListMatiereStock(atelierId, aboId));
        }

        public ViewBonSortieModel getQuantiteEnMagasin(int atelierId, int matiereId)
        {
            return produitVendableRepository.getQuantiteEnMagasin(atelierId, matiereId);
        }

        public ViewBonSortieModel getQuantiteAtelier(int matiereStockID)
        {
            return produitVendableRepository.getQuantiteAtelier(matiereStockID);
        } 
        public ViewBonSortieModel getQuantiteStock(int matiereStockID)
        {
            return produitVendableRepository.getQuantiteStock(matiereStockID);
        }

        public IEnumerable<ProduitPackageModel> getProduitpackagePointVente(int formulaireProduitId)
        {
            return mapper.Map<IEnumerable<ProduitPackage>, IEnumerable<ProduitPackageModel>>(produitVendableRepository.getProduitpackagePointVente(formulaireProduitId));
        }

        public async Task<bool> PackageForme(Package_FormeModel packFormeModel)
        {
            using (IDbContextTransaction transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    Package_Forme packForme = mapper.Map<Package_FormeModel, Package_Forme>(packFormeModel);
                    var idProduitVendable = await produitVendableRepository.PackageForme(packForme);
                    if (idProduitVendable == null)
                    {
                        return false;
                    }
                    transaction.Commit();
                    return true;

                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public IEnumerable<Package_FormeModel> getListeFichePackage(int? aboId, int? produitID)
        {
            return mapper.Map<IEnumerable<Package_Forme>, IEnumerable<Package_FormeModel>>(produitVendableRepository.getListeFichePackage(aboId, produitID));
        }

        public IEnumerable<PackageForme_DetailsModel> getDetailsFichePackage(int produitID)
        {
            return mapper.Map<IEnumerable<PackageForme_Details>, IEnumerable<PackageForme_DetailsModel>>(produitVendableRepository.getDetailsFichePackage(produitID));
        }

        public List<SousProduitsViewModel> getPackageFormeDetails(int packageId, int formeId, int current)
        {
            List<SousProduitsViewModel> sousProds = new List<SousProduitsViewModel>();
            var list =  mapper.Map<Package_Forme, Package_FormeModel>(produitVendableRepository.getPackageFormeDetails(packageId, formeId));
            foreach(var d in list.details)
            {
                string desi = "";
                decimal qte = 0;
                var id = 0;
                var type = "";
                var formeid = 0;
                var uniteid = 0;
                var unitedesi = "";
                var FormesousProdtuit = mapper.Map<Sous_ProduitPackage, Sous_ProduitPackageModel>(produitVendableRepository.findFormulaireSousProduitPackage(d.PackageFormeDetails_SousProduitID)).Forme_Produit;
                if (FormesousProdtuit.Produit_Vendable != null)
                {
                    desi = FormesousProdtuit.Produit_Vendable.ProduitVendable_Designation;
                    qte = produitVendableRepository.FindFormulaireProduitAppro(FormesousProdtuit.FormeProduit_AbonnementID, current, FormesousProdtuit.FormeProduit_ID);
                    id = (int)FormesousProdtuit.FormeProduit_ProduitID;
                    type = "vendable";
                    formeid = FormesousProdtuit.FormeProduit_ID;
                    uniteid = FormesousProdtuit.Produit_Vendable.Unite_Mesure.UniteMesure_Id;
                    unitedesi = FormesousProdtuit.Produit_Vendable.Unite_Mesure.UniteMesure_Libelle;
                }
                else
                {
                    desi = FormesousProdtuit.Produit_PretConsomer.ProduitPretConsomer_Designation;
                    qte = produitVendableRepository.getQteProduitPretAtelier(current, FormesousProdtuit.FormeProduit_ID);
                    id = (int)FormesousProdtuit.FormeProduit_ProduitPretId;
                    type = "consommable";
                    formeid = FormesousProdtuit.FormeProduit_ID;
                    uniteid = FormesousProdtuit.Produit_PretConsomer.Unite_Mesure.UniteMesure_Id;
                    unitedesi = FormesousProdtuit.Produit_PretConsomer.Unite_Mesure.UniteMesure_Libelle;
                }
                var sousProd = new SousProduitsViewModel()
                {
                    SousProduit_ProduitDesi = desi,
                    SousProduit_FormeProduit = FormesousProdtuit.FormeProduit_Libelle,
                    SousProduit_Quantite = d.PackageFormeDetails_Quantite,
                    SousProduit_QuantiteEnStock = qte,
                    SousProduit_FormeProduitID = formeid,
                    SousProduit_ProduitID = id,
                    SousProduit_ProduitPackageID = packageId,
                    SousProduit_ProduitType = type,
                    SousProduit_UniteMesureDesi = unitedesi,
                    SousProduit_UniteMesureId = uniteid

                };
                sousProds.Add(sousProd);
            }
            return sousProds;
        }

        public async Task<bool> DemanderComposants(List<SousProduitsViewModel> sousProds, int atelierID, int lieuId, int aboId)
        {
            using (IDbContextTransaction transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    var idDemande = await produitVendableRepository.DemanderComposants(sousProds, atelierID, lieuId, aboId);
                    if (idDemande == null)
                    {
                        return false;
                    }
                    transaction.Commit();
                    return true;

                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public async Task<bool> ApprovisionnementProduitPackage(Approvisionnement_ProduitPackageModel approvisionnementModel)
        {
            using (IDbContextTransaction transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    Approvisionnement_ProduitPackage approv = mapper.Map<Approvisionnement_ProduitPackageModel, Approvisionnement_ProduitPackage>(approvisionnementModel);

                    var idDemande = await produitVendableRepository.ApprovisionnementProduitPackage(approv);
                    if (idDemande == null)
                    {
                        return false;
                    }
                    transaction.Commit();
                    return true;

                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public async Task<bool> ValiderApprovisionnementPackage(int ApprovisionnementId, int pdvId)
        {
            return await produitVendableRepository.ValiderApprovisionnementPackage(ApprovisionnementId, pdvId);
        }

        public IEnumerable<ProduitPack_AtelierModel> getProduitPackEnMagasin(int atelierId)
        {
            return mapper.Map<IEnumerable<ProduitPack_Atelier>, IEnumerable<ProduitPack_AtelierModel>>(produitVendableRepository.getProduitPackEnMagasin(atelierId));
        }
        public IEnumerable<ProduitPack_AtelierModel> FindFormesProduitPackEnMagasin(int produitId, int atelierID)
        {
            return mapper.Map<IEnumerable<ProduitPack_Atelier>, IEnumerable<ProduitPack_AtelierModel>>(produitVendableRepository.FindFormesProduitPackEnMagasin(produitId, atelierID));
        }

        public ProduitPack_AtelierModel FindformulairePackageMagasin(int ID)
        {
            return mapper.Map<ProduitPack_Atelier, ProduitPack_AtelierModel>(produitVendableRepository.FindformulairePackageMagasin(ID));
        }

        public IEnumerable<Approvisionnement_ProduitPackageModel> getListApprovisionnement(int aboId, int? atelierID, int? pdvId, string date)
        {
            return mapper.Map<IEnumerable<Approvisionnement_ProduitPackage>, IEnumerable<Approvisionnement_ProduitPackageModel>>(produitVendableRepository.getListApprovisionnement(aboId, atelierID, pdvId, date));
        }

        public async Task<bool> DeclarationPerte(Perte_PretModel perte_PretModel)
        {
            using (IDbContextTransaction transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    // Créer le produit
                    Perte_Pret perte_Pret = mapper.Map<Perte_PretModel, Perte_Pret>(perte_PretModel);
                    var idProduitVendable = await produitVendableRepository.DeclarationPerte(perte_Pret);
                    if (idProduitVendable == null)
                    {
                        return false;
                    }
                    transaction.Commit();
                    return true;

                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public IEnumerable<Perte_PretModel> getListPertesPrets(int aboId, int? atelierID, string date)
        {
            return mapper.Map<IEnumerable<Perte_Pret>, IEnumerable<Perte_PretModel>>(produitVendableRepository.getListPertesPrets(aboId, atelierID, date));
        }

        public IEnumerable<ProduitPret_AtelierModel> findFormesPret_Atelier(int atelierID, int prduitpretID)
        {
            return mapper.Map<IEnumerable<ProduitPret_Atelier>, IEnumerable<ProduitPret_AtelierModel>>(produitVendableRepository.findFormesPret_Atelier(atelierID, prduitpretID));
        }

        public ProduitPret_AtelierModel FindformulairePretMagasin(int ID)
        {
            return mapper.Map<ProduitPret_Atelier, ProduitPret_AtelierModel>(produitVendableRepository.FindformulairePretMagasin(ID));
        }

        public async Task<bool> DemanderPret(Demande_PretModel demande_PretModel)
        {
            using (IDbContextTransaction transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    Demande_Pret demande_Pret = mapper.Map<Demande_PretModel, Demande_Pret>(demande_PretModel);
                    var idDemande = await produitVendableRepository.DemanderPret(demande_Pret);
                    if (idDemande == null)
                    {
                        return false;
                    }
                    transaction.Commit();
                    return true;

                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public IEnumerable<ProduitPretConsomerModel> getListProduitConsomableEnMagasin(int Id, int pdv, int? familleID)
        {
            return mapper.Map<IEnumerable<ProduitPretConsomer>, IEnumerable<ProduitPretConsomerModel>>(produitVendableRepository.getListProduitConsomableEnMagasin(Id, pdv, familleID));
        }

        public async Task<bool> CreateProduitBase(ProduitBaseModel produitBaseModel)
        {

            using (IDbContextTransaction transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    ProduitBase produit = mapper.Map<ProduitBaseModel, ProduitBase>(produitBaseModel);
                    var idProduitbase = await produitVendableRepository.CreateProduitBase(produit);
                    if (idProduitbase == null)
                    {
                        return false;
                    }
                    transaction.Commit();
                    return true;

                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public IEnumerable<ProduitBaseModel> getListProduitBase(int Id, int? formeID)
        {
            return mapper.Map<IEnumerable<ProduitBase>, IEnumerable<ProduitBaseModel>>(produitVendableRepository.getListProduitBase(Id, formeID));
        }

        public Task<bool> updateFormulaireProduitBase(int id, ProduitBaseModel newProduitModel)
        {
            ProduitBase produit = mapper.Map<ProduitBaseModel, ProduitBase>(newProduitModel);
            return produitVendableRepository.updateFormulaireProduitBase(id, produit);
        }

        public Task<bool> deleteProduitBase(int ID)
        {
            return produitVendableRepository.deleteProduitBase(ID);
        }

        public ProduitBaseModel findFormulaireProduitBase(int produitId, int aboId)
        {
            return mapper.Map<ProduitBase, ProduitBaseModel>(produitVendableRepository.findFormulaireProduitBase(produitId, aboId));
        }

        public IEnumerable<PlanificationJourneeBaseModel> getListPlansBaseAtelier(int Id, int? atelierId, string etat, int? lieu, string date)
        {
            return mapper.Map<IEnumerable<PlanificationJourneeBase>, IEnumerable<PlanificationJourneeBaseModel>>(produitVendableRepository.getListPlansBaseAtelier(Id, atelierId, etat, lieu, date));
        }

        public async Task<bool> PlanifierBase(PlanificationJourneeBaseModel planModel)
        {
            using (IDbContextTransaction transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    // Créer le transporteur
                    foreach (PlanificationdeProductionBaseModel PlanProd in planModel.Planification_ProductionBase)
                    {
                        PlanProd.PlanificationProductionBase_DateCreation = DateTime.Now;
                        PlanProd.PlanificationProductionBase_AbonnementId = planModel.PlanificationJourneeBase_AbonnementID;
                        //value +=getProduitCoutDeRevient(PlanProd.PlanificationProduction_ProduitVendableId);

                    }

                    planModel.BonDe_Sortie.BonDeSortie_DateCreation = DateTime.Now;
                    planModel.BonDe_Sortie.BonDeSortie_AbonnementID = planModel.PlanificationJourneeBase_AbonnementID;
                    planModel.BonDe_Sortie.BonDeSortie_DateProduction = planModel.PlanificationJourneeBase_Date;
                    planModel.BonDe_Sortie.BonDeSortie_Libelle = "Bon De planification du :" + planModel.BonDe_Sortie.BonDeSortie_DateCreation.ToShortDateString();
                    foreach (BonDetailsModel details in planModel.BonDe_Sortie.Bon_Details)
                    {
                        details.BonDeSortie_BonDeSortieID = planModel.BonDe_Sortie.BonDeSortie_ID;

                    }
                    PlanificationJourneeBase plan = mapper.Map<PlanificationJourneeBaseModel, PlanificationJourneeBase>(planModel);
                    var idTransporteur = await produitVendableRepository.PlanifierBase(plan);
                    transaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public async Task<MatStockFlagModel> AccepterPlanificationBase(int planificationId, int adresse, List<BonViewModel> ListBons, string validePar)
        {
            var res = await produitVendableRepository.AccepterPlanificationBase(planificationId, adresse, ListBons, validePar);
            return  res;
        }

        public async Task<MatStockFlagModel> ValiderPlanificationBase(int planificationId)
        {
            //await produitVendableRepository.PlansSeenByAtelier(planificationI);
            return await produitVendableRepository.ValiderPlanificationBase(planificationId);
        }

        public IEnumerable<PlanificationdeProductionBaseModel> getDetailPlanBase(int aboId, int Id)
        {
            return mapper.Map<IEnumerable<PlanificationdeProductionBase>, IEnumerable<PlanificationdeProductionBaseModel>>(produitVendableRepository.getDetailPlanBase(aboId, Id));
        }

        public FicheTehcniqueProduitBaseModel GetFicheTechBase(int Id)
        {
            return mapper.Map<FicheTechniqueProduitBase, FicheTehcniqueProduitBaseModel>(produitVendableRepository.GetFicheTechBase(Id));
        }

        public IEnumerable<PlanificationdeProductionBaseModel> getDetailPlanProduitListBase(int aboId, int Id)
        {
            return mapper.Map<IEnumerable<PlanificationdeProductionBase>, IEnumerable<PlanificationdeProductionBaseModel>>(produitVendableRepository.getDetailPlanProduitListBase(aboId, Id));

        }

        public IEnumerable<PlanificationdeProductionBaseModel> getDetailByPlanBase(int aboId, int Id, int planId)
        {
            return mapper.Map<IEnumerable<PlanificationdeProductionBase>, IEnumerable<PlanificationdeProductionBaseModel>>(produitVendableRepository.getDetailByPlanBase(aboId, Id, planId));
        }

        public async Task<bool?> updateFormulaireQteProduitebase(int id, List<PlanificationdeProductionBaseModel> plans)
        {
            var res = await  produitVendableRepository.updateFormulaireQteProduitebase(id, plans);
            return res;
        }

        public IEnumerable<ProdBase_AtelierModel> getListProduitBaseStock(int atelierID, int aboID)
        {
            return mapper.Map<IEnumerable<ProdBase_Atelier>, IEnumerable<ProdBase_AtelierModel>>(produitVendableRepository.getListProduitBaseStock(atelierID, aboID));
        }

        public IEnumerable<FicheTech_ProduitBaseModel> GetFicheTech_ProduitBases(int Id, int aboID)
        {
            return mapper.Map<IEnumerable<FicheTech_ProduitBase>, IEnumerable<FicheTech_ProduitBaseModel>>(produitVendableRepository.GetFicheTech_ProduitBases(Id, aboID));
        }

        public Task<bool> updateFicheForme(int formeId, int produitID, decimal qteForme)
        {
            return produitVendableRepository.updateFicheForme(formeId, produitID, qteForme);
        }

        public IEnumerable<Taux_TVAModel> getListTVA(int aboID)
        {
            return mapper.Map<IEnumerable<Taux_TVA>, IEnumerable<Taux_TVAModel>>(produitVendableRepository.getListTVA(aboID));
        }

        public async Task<bool> CreateTVA(Taux_TVAModel tauxTVA)
        {
            using (IDbContextTransaction transaction = this.unitOfWork.BeginTransaction())
            {
                try
                {
                    // Créer le produit
                    Taux_TVA tva = mapper.Map<Taux_TVAModel, Taux_TVA>(tauxTVA);
                    var idTVA = await this.produitVendableRepository.CreateTVA(tva);
                    if (idTVA == null)
                    {
                        return false;
                    }
                    transaction.Commit();
                    return true;

                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public IEnumerable<Forme_ProduitModel> getFormesConsomableEnMagasin(int Id, int pdv)
        {
            return mapper.Map<IEnumerable<Forme_Produit>, IEnumerable<Forme_ProduitModel>>(produitVendableRepository.getFormesConsomableEnMagasin(Id, pdv));
        }

        public async Task<List<matStockViewModel>> SetProdsDeBase(int planificationId)
        {
            using (IDbContextTransaction transaction = this.unitOfWork.BeginTransaction())
            {
                try
                {
                    // Créer le produit
                    var idTVA = await produitVendableRepository.SetProdsDeBase(planificationId);
                    if (idTVA == null)
                    {
                        return null;
                    }
                    transaction.Commit();
                    return idTVA;

                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return null;
                }
            }
           
        }

        public List<matStockViewModel> getProdBasePlan(int planificationId)
        {
            return produitVendableRepository.getProdBasePlan(planificationId);
        }

        public Task<int?> PlanificationBaseAuto(int planID)
        {
            return produitVendableRepository.PlanificationBaseAuto(planID);
        }

        public async Task<bool> AnnulerPlanification(int planID)
        {
            using (IDbContextTransaction transaction = this.unitOfWork.BeginTransaction())
            {
                try
                {
                    var confirm = await produitVendableRepository.AnnulerPlanification(planID);
                    if (confirm == false)
                    {
                        return false;
                    }
                    transaction.Commit();
                    return confirm;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public async Task<bool> RetourStockPlan(int planID)
        {
            using (IDbContextTransaction transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    var confirm = await produitVendableRepository.RetourStockPlan(planID);
                    if (confirm == false)
                    {
                        return false;
                    }
                    transaction.Commit();
                    return confirm;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public IEnumerable<MatierePremiereModel> fillMatiereDemande(int planJourneeID)
        {
            return mapper.Map<IEnumerable<MatierePremiere>, IEnumerable<MatierePremiereModel>>(produitVendableRepository.fillMatiereDemande(planJourneeID));
        }

        public async Task<MatStockFlagModel> CloturerDemande(int demandeID)
        {
            return await produitVendableRepository.CloturerDemande(demandeID);
            
        }
    }
}
