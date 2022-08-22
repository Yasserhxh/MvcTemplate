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
    public class FournisseurService : IFournisseurService
    {
        private readonly IFournisseurRepository fournisseurRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public FournisseurService(IFournisseurRepository fournisseurRepository, IMapper mapper,
          IUnitOfWork unitOfWork)
        {
            this.fournisseurRepository = fournisseurRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;

        }

        public async Task<bool?> AjouterMatiere(int idFournisseur, List<int> listMatiere)
        {
            using (IDbContextTransaction transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    // Creer le fournisseur.
                    var id = await fournisseurRepository.AjouterMatiere(idFournisseur, listMatiere);


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

        public async Task<bool> CreateBonDeCommande(BonDeCommande_Model bonDeCommandeModel)
        {
            using (IDbContextTransaction transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    BonDeCommande bonCommande = mapper.Map<BonDeCommande_Model, BonDeCommande>(bonDeCommandeModel);
                    var id = await fournisseurRepository.CreateBonDeCommande(bonCommande);


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

        public async Task<List<Article_BL>> CreateBonDeLivraison(BonDeLivraison_Model bonDeLivraisonModel)
        {
            using (IDbContextTransaction transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    BonDeLivraison bonDeLivraison = mapper.Map<BonDeLivraison_Model, BonDeLivraison>(bonDeLivraisonModel);
                    var id = await fournisseurRepository.CreateBonDeLivraison(bonDeLivraison);
                    transaction.Commit();
                    return id;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return null;
                }
            }
        }

        public async Task<bool> CreateFacture(FactureModel factureModel, List<BonDeLivraison_Model> listeBL)
        {
            using (IDbContextTransaction transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    Facture facture = mapper.Map<FactureModel, Facture>(factureModel);
                    var id = await fournisseurRepository.CreateFacture(facture, listeBL);
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

        public async Task<bool> CreateFournisseur(FournisseurModel fournisseurModel)
        {
            using (IDbContextTransaction transaction = this.unitOfWork.BeginTransaction())
            {
                try
                {
                    // Creer le fournisseur.
                    Fournisseur fournisseur = mapper.Map<FournisseurModel, Fournisseur>(fournisseurModel);
                    var idFournisseur = await this.fournisseurRepository.CreateFournisseur(fournisseur);


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

        public Task<bool> deleteFournisseur(int ID, int code)
        {
            return this.fournisseurRepository.deleteFournisseur(ID, code);
        }

        public Task<bool> deleteMatieresLink(int ID, int code)
        {
            return this.fournisseurRepository.deleteMatieresLink(ID, code);
        }

        public BonDeCommande_Model FindFormulaireBonDeCommande(int aboID, int? bonCommandeID)
        {
            return mapper.Map<BonDeCommande, BonDeCommande_Model>(fournisseurRepository.FindFormulaireBonDeCommande(aboID, bonCommandeID));
        }

        public BonDeLivraison_Model FindFormulaireBonDeLivraison(int aboID, int? bondeLivraison)
        {
            return mapper.Map<BonDeLivraison, BonDeLivraison_Model>(fournisseurRepository.FindFormulaireBonDeLivraison(aboID, bondeLivraison));
        }

        public FournisseurModel findFormulaireFournisseur(int formulaireFourisseurId)
        {
            return mapper.Map<Fournisseur, FournisseurModel>(fournisseurRepository.findFormulaireFournisseur(formulaireFourisseurId));
        }

        public paginationModel<ProduitVendable> getAllProds(int pg)
        {
            return fournisseurRepository.getAllProds(pg);
        }
        public IEnumerable<ArticleBC_Model> GetArticlesBC(int bonCommandeID)
        {
            return mapper.Map<IEnumerable<Article_BC>, IEnumerable<ArticleBC_Model>>(fournisseurRepository.GetArticlesBC(bonCommandeID));
        }  
        public IEnumerable<ArticleBC_Model> GetArticlesBCForBL(int bonCommandeID)
        {
            return mapper.Map<IEnumerable<Article_BC>, IEnumerable<ArticleBC_Model>>(fournisseurRepository.GetArticlesBCForBL(bonCommandeID));
        }

        public IEnumerable<ArticleBL_Model> GetArticlesBL(int bondeLivraisonID)
        {
            return mapper.Map<IEnumerable<Article_BL>, IEnumerable<ArticleBL_Model>>(fournisseurRepository.GetArticlesBL(bondeLivraisonID));
        }

        public IEnumerable<BonDeCommande_Model> GetBonDeCommandes(int aboID, int? pointStockID, int? fournisseurID, string name, int? date, string statut)
        {
            return mapper.Map<IEnumerable<BonDeCommande>, IEnumerable<BonDeCommande_Model>>(fournisseurRepository.GetBonDeCommandes(aboID, pointStockID, fournisseurID,name, date, statut));
        }

        public IEnumerable<BonDeLivraison_Model> GetBonDeLivraisons(int? fournisseurID, int? bonCommandeID, int aboID, string date, int? statut)
        {
            return mapper.Map<IEnumerable<BonDeLivraison>, IEnumerable<BonDeLivraison_Model>>(fournisseurRepository.GetBonDeLivraisons(fournisseurID, bonCommandeID, aboID, date, statut));
        }

        public IEnumerable<FactureModel> GetFactures(int aboID, int? point, string date)
        {
            return mapper.Map<IEnumerable<Facture>, IEnumerable<FactureModel>>(fournisseurRepository.GetFactures(aboID, point, date));
        }

        public IEnumerable<VilleModel> getListeVille()
        {
            return mapper.Map<IEnumerable<Ville>, IEnumerable<VilleModel>>(fournisseurRepository.getListVilles());
        }

        public IEnumerable<FonctionModel> getListFonction()
        {
            return mapper.Map<IEnumerable<Fonction>, IEnumerable<FonctionModel>>(fournisseurRepository.getListFonction());
        }

        public IEnumerable<FournisseurModel> getListFournisseur(int Id,int? VilleId, int? statut)
        {
            return mapper.Map<IEnumerable<Fournisseur>, IEnumerable<FournisseurModel>>(this.fournisseurRepository.getListFournisseur(Id,VilleId,statut));

        }

        public IEnumerable<MatierePremiereModel> getListMatiere(int Id)
        {
            return mapper.Map<IEnumerable<MatierePremiere>, IEnumerable<MatierePremiereModel>>(fournisseurRepository.getListMatiere(Id));

        }

        public IEnumerable<FournisseurMatiereModel> getListMatiereLink(int fournisseurId)
        {
            return mapper.Map<IEnumerable<FournisseurMatiere>, IEnumerable<FournisseurMatiereModel>>(fournisseurRepository.getListMatiereLink(fournisseurId));
        }

        public IEnumerable<StockAchat_Model> GetMatireStockAchat(int aboID, int? matiereID, string lotIntern)
        {
            return mapper.Map<IEnumerable<Stock_Achat>, IEnumerable<StockAchat_Model>>(fournisseurRepository.GetMatireStockAchat(aboID, matiereID, lotIntern));
        }

        /*  public IEnumerable<FournisseurModel> getListAllFournisseur()
 {
     return mapper.Map<IEnumerable<Fournisseur>, IEnumerable<FournisseurModel>>(this.fournisseurRepository.getListAllFournisseur());

 }*/

        public Task<bool> updateFormulaireFournisseur(int id, FournisseurModel newFournisseuModel)
        {
            Fournisseur fournisseur = mapper.Map<FournisseurModel, Fournisseur>(newFournisseuModel);
            return this.fournisseurRepository.updateFormulaireFournisseur(id, fournisseur);

        }
    }
}
