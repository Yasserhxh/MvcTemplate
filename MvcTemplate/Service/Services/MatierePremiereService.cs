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
    public class MatierePremiereService : IMatierePremiereService
    {
        private readonly IMatierePremiereRepository matierePremiereRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public MatierePremiereService(IMatierePremiereRepository matierePremiereRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.matierePremiereRepository = matierePremiereRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }


        public async Task<bool> CreateAllergene(AllergeneModel allergeneModel)
        {
            using (IDbContextTransaction transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    // Créer Allergene.
                    Allergene allergene = mapper.Map<AllergeneModel, Allergene>(allergeneModel);
                    var idMatierePremiere = await matierePremiereRepository.CreateAllergene(allergene);

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

        public async Task<bool> CreateMatierePremiere(MatierePremiereModel matiereModel,List<int> ListeUnite)
        {
            using (IDbContextTransaction transaction = this.unitOfWork.BeginTransaction())
            {
                try
                {
                    // Créer matiere.
                    MatierePremiere matiere = mapper.Map<MatierePremiereModel, MatierePremiere>(matiereModel);
                    var idMatierePremiere = await this.matierePremiereRepository.CreateMatiere(matiere, ListeUnite);

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

        public Task<bool> deleteAllergie(int ID)
        {
            return this.matierePremiereRepository.deleteAllergie(ID);
        }

        public Task<bool> deleteMatiere(int ID)
        {
            return this.matierePremiereRepository.deleteMatiereP(ID);
        }

        public Task<bool> deleteMatierePremiereStockes(int ID)
        {
            return this.matierePremiereRepository.deleteMatierePremiereStockes(ID);
        }

        public AllergeneModel findFormulaireAllergie(int formulaireAllergieId)
        {
            return mapper.Map<Allergene, AllergeneModel>(this.matierePremiereRepository.findFormulaireAllergene(formulaireAllergieId));
        }

        public MatierePremiereModel findFormulaireMatiereP(int aboId, int formulaireMatiereId)
        {
            return mapper.Map<MatierePremiere, MatierePremiereModel>(this.matierePremiereRepository.findFormulaireMatiereP(aboId, formulaireMatiereId));
        }

        public IEnumerable<AllergeneModel> getListAllergene(int Id)
        {
            return mapper.Map<IEnumerable<Allergene>, IEnumerable<AllergeneModel>>(this.matierePremiereRepository.getListAllergene(Id));
        }

        public IEnumerable<ProduitFicheTechniqueModel> getListFicheTech(int Id)
        {
            return mapper.Map<IEnumerable<ProduitFicheTechnique>, IEnumerable<ProduitFicheTechniqueModel>>(this.matierePremiereRepository.getListFicheTechnique(Id));
        }

        public IEnumerable<Forme_StockageModel> getListFormeStockage(int Id)
        {
            return mapper.Map<IEnumerable<Forme_Stockage>, IEnumerable<Forme_StockageModel>>(this.matierePremiereRepository.getListFormeStockage(Id));
        }

        public IEnumerable<MatierePremiereModel> getListMatierePremiere(int Id,int? allergene,int? formeId)
        {
            return mapper.Map<IEnumerable<MatierePremiere>, IEnumerable<MatierePremiereModel>>(matierePremiereRepository.getListMatierePremiere(Id,allergene,formeId));
        }

        public IEnumerable<MatierePremiereStockageModel> getListMatiereStocker(int Id, int MatierePremiereStokage_Id)
        {
            return mapper.Map<IEnumerable<MatierePremiereStockage>, IEnumerable<MatierePremiereStockageModel>>(matierePremiereRepository.getListMatieresStockes(Id, MatierePremiereStokage_Id));
        }
        public IEnumerable<MatierePremiereStockageModel> getListMatiereStockerAll(int Id, int lieuId, int? zone, int? section)
        {
            return mapper.Map<IEnumerable<MatierePremiereStockage>, IEnumerable<MatierePremiereStockageModel>>(matierePremiereRepository.getListMatieresStockesAll(Id, lieuId, zone, section));
        }

        public IEnumerable<Section_StockageModel> getListSectionStockage()
        {
            return mapper.Map<IEnumerable<Section_Stockage>, IEnumerable<Section_StockageModel>>(matierePremiereRepository.getListSectionStockage());
        }

        public async Task<bool> StockerMatiere(int Id, MatierePremiereStockageModel matiereStockerModel )
        {
            using (IDbContextTransaction transaction = this.unitOfWork.BeginTransaction())
            {
                try
                {

                    MatierePremiereStockage matiere = mapper.Map<MatierePremiereStockageModel, MatierePremiereStockage>(matiereStockerModel);
                    var idProduitVendable = await this.matierePremiereRepository.StockerMatiere(Id, matiere);
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

        public Task<bool> updateFormulaireAllergie(int id, AllergeneModel AllergieModel)
        {
            Allergene allergie = mapper.Map<AllergeneModel, Allergene>(AllergieModel);
            return this.matierePremiereRepository.updateFormulaireAllergie(id, allergie);
        }

        public Task<bool> updateFormulaireMatiere(int id, MatierePremiereModel matiereModel)
        {
            MatierePremiere matiere = mapper.Map<MatierePremiereModel, MatierePremiere>(matiereModel);
            return this.matierePremiereRepository.updateFormulaireMatierePremiere(id, matiere);
        }

        public bool MatiereStocker(int ID)
        {
            var res = this.matierePremiereRepository.MatiereStocker(ID);
            return res;
        }

        public decimal CalculerPrix(int FormMatiereId, int FormUnite, decimal FormQuantite)
        {
            var prix = this.matierePremiereRepository.CalculerPrix(FormMatiereId, FormUnite, FormQuantite);
            return prix;
        }

        public string getUniteById(int uniteId)
        {
            return this.matierePremiereRepository.getUniteById(uniteId);
        }

        public IEnumerable<FournisseurModel> getListFournisseur(int Id)
        {
            return mapper.Map<IEnumerable<Fournisseur>, IEnumerable<FournisseurModel>>(matierePremiereRepository.getListFournisseur(Id));
        }

        public async Task<bool> CreateMatiereFamille(MatiereFamilleModel matiereFamilleModel)
        {
            using (IDbContextTransaction transaction = this.unitOfWork.BeginTransaction())
            {
                try
                {
                    // Creer la famille.
                    MatiereFamille matiereFamille = mapper.Map<MatiereFamilleModel, MatiereFamille>(matiereFamilleModel);
                    var idEntreprise = await matierePremiereRepository.CreateMatiereFamile(matiereFamille);


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

        public async Task<bool> CreateMatiereFamilleParent(MatiereFamille_ParentModel matiereFamille_ParentModel)
        {
            using (IDbContextTransaction transaction = this.unitOfWork.BeginTransaction())
            {
                try
                {
                    // Creer la famille.
                    MatireFamille_Parent matiereFamille_Parent = mapper.Map<MatiereFamille_ParentModel, MatireFamille_Parent>(matiereFamille_ParentModel);
                    var idEntreprise = await this.matierePremiereRepository.CreateMatiereFamilleParent(matiereFamille_Parent);


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

        public IEnumerable<MatiereFamilleModel> getListMatiereFamille(int Id)
        {
            return mapper.Map<IEnumerable<MatiereFamille>, IEnumerable<MatiereFamilleModel>>(matierePremiereRepository.getListMatiereFamille(Id));
        }

        public IEnumerable<MatiereFamille_ParentModel> getListMatiereFamilleParent(int Id)
        {
            return mapper.Map<IEnumerable<MatireFamille_Parent>, IEnumerable<MatiereFamille_ParentModel>>(matierePremiereRepository.getListMatiereFamilleParent(Id));
        }

        public async Task<bool> AjouterUnites(int idMatiere, List<int> listUnite)
        {
            using (IDbContextTransaction transaction = this.unitOfWork.BeginTransaction())
            {
                try
                {
                    var idEntreprise = await matierePremiereRepository.AjouterUnites(idMatiere, listUnite);
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

        public IEnumerable<Unite_MesureModel> getListUniteMesure(int Id, int aboId)
        {
            return mapper.Map<IEnumerable<Unite_Mesure>, IEnumerable<Unite_MesureModel>>(matierePremiereRepository.getListUniteMesure(Id,aboId));
        }

        public IEnumerable<Unite_MesureMatiereModel> getListUniteUtilisation(int matiereId)
        {
            return mapper.Map<IEnumerable<Unite_MesureMatiere>, IEnumerable<Unite_MesureMatiereModel>>(matierePremiereRepository.getListUniteUtilisation(matiereId));
        }

        public Task<bool> deleteUniteLink(int ID, int code)
        {
            return matierePremiereRepository.deleteUniteLink(ID, code);
        }

        public IEnumerable<MouvementStockModel> getListHistorique(int aboId, int matiereId)
        {
            return mapper.Map<IEnumerable<MouvementStock>, IEnumerable<MouvementStockModel>>(matierePremiereRepository.getListHistorique(aboId,matiereId));
        }

        public async Task<bool> DeclarationPerte(Perte_MatiereModel perte_MatiereModel)
        {
            using (IDbContextTransaction transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    // Créer perte.
                    Perte_Matiere perte_Matiere = mapper.Map<Perte_MatiereModel, Perte_Matiere>(perte_MatiereModel);
                    var idPerte = await matierePremiereRepository.DeclarationPerte(perte_Matiere);

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

        public IEnumerable<Perte_MatiereModel> getListPertes(int aboId, int? lieuStockID, string date, int? matiereId)
        {
            return mapper.Map<IEnumerable<Perte_Matiere>, IEnumerable<Perte_MatiereModel>>(matierePremiereRepository.getListPertes(aboId, lieuStockID, date, matiereId));
        }

        public async Task<bool> DeclarationPerteStock(Perte_MatiereStockModel perte_MatiereStockModel)
        {
            using (IDbContextTransaction transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    // Créer perte.
                    Perte_MatiereStock perte_MatiereStock = mapper.Map<Perte_MatiereStockModel, Perte_MatiereStock>(perte_MatiereStockModel);
                    var idPerte = await matierePremiereRepository.DeclarationPerteStock(perte_MatiereStock);

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

        public IEnumerable<Perte_MatiereStockModel> getListPertesStock(int aboId, int? lieuStockID, string date, int? matiereId)
        {
            return mapper.Map<IEnumerable<Perte_MatiereStock>, IEnumerable<Perte_MatiereStockModel>>(matierePremiereRepository.getListPertesStock(aboId, lieuStockID, date, matiereId));
        }

        public async Task<bool> CreateApprov(Approvisionnement_MatiereModel approvModel)
        {
            using (IDbContextTransaction transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    Approvisionnement_Matiere approv = mapper.Map<Approvisionnement_MatiereModel, Approvisionnement_Matiere>(approvModel);
                    var idApprov = await matierePremiereRepository.CreateApprov(approv);

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

        public IEnumerable<Approvisionnement_MatiereModel> getListApprov(int aboId, int? stockID, string date, string etat, int? pointPord)
        {
            return mapper.Map<IEnumerable<Approvisionnement_Matiere>, IEnumerable<Approvisionnement_MatiereModel>>(matierePremiereRepository.getListApprov(aboId, stockID, date, etat, pointPord));
        }
        public Task<bool> ValiderApprovisionnement(int ApprovisionnementId, string valideId, int pdvId)
        {
            return matierePremiereRepository.ValiderApprovisionnement(ApprovisionnementId, valideId, pdvId);
        }

        public IEnumerable<Taux_TVAModel> getListCoutTVA()
        {
            var res = mapper.Map<IEnumerable<Taux_TVA>, IEnumerable<Taux_TVAModel>>(matierePremiereRepository.getListCoutTVA());
            foreach( var item in res)
            {
                item.TauxTVA_pourcentageString = item.TauxTVA_Pourcentage.ToString("G29") + "" + "%";
            }
            return res;
        }
    }
}
