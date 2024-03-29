﻿using Domain.Entities;
using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.IServices
{
    public interface IFournisseurService
    {
        Task<bool> CreateFournisseur(FournisseurModel fournisseur);
        IEnumerable<FournisseurModel> getListFournisseur(int Id, int? VilleId, int? statut);
        IEnumerable<FonctionModel> getListFonction();
        IEnumerable<VilleModel> getListeVille();
        FournisseurModel findFormulaireFournisseur(int formulaireFourisseurId);
        Task<bool> updateFormulaireFournisseur(int id, FournisseurModel newFournisseuModel);
        Task<bool> deleteFournisseur(int ID, int code);
        Task<bool> deleteMatieresLink(int ID, int code);
        IEnumerable<MatierePremiereModel> getListMatiere(int Id);
        Task<bool?> AjouterMatiere(int idFournisseur, List<int> listMatiere);
        IEnumerable<FournisseurMatiereModel> getListMatiereLink(int fournisseurId);
        IEnumerable<BonDeCommande_Model> GetBonDeCommandes(int aboID, int? pointStockID, int? fournisseurID, string name, int? date, string statut);
        Task<bool> CreateBonDeCommande(BonDeCommande_Model bonDeCommandeModel);
        IEnumerable<ArticleBC_Model> GetArticlesBC(int bonCommandeID);
        IEnumerable<ArticleBL_Model> GetArticlesBL(int bondeLivraisonID);
        IEnumerable<ArticleBC_Model> GetArticlesBCForBL(int bonCommandeID);
        IEnumerable<BonDeLivraison_Model> GetBonDeLivraisons(int? fournisseurID, int? bonCommandeID, int aboID, string date, int? statut);
        Task<List<Article_BL>> CreateBonDeLivraison(BonDeLivraison_Model bonDeLivraisonModel);
        Task<bool> CreateFacture(FactureModel factureModel, List<BonDeLivraison_Model> listeBL);
        IEnumerable<FactureModel> GetFactures(int aboID, int? bondeCommande, string numeroFac, int? date);
        BonDeCommande_Model FindFormulaireBonDeCommande(int aboID, int? bonCommandeID);
        BonDeLivraison_Model FindFormulaireBonDeLivraison(int aboID, int? bondeLivraison);
        paginationModel<ProduitVendableModel> getAllProds(int? Sous, string nom, int aboid, int pg);
        IEnumerable<StockAchat_Model> GetMatireStockAchat(int aboID, int? matiereID, string lotIntern);
        Task<bool> CreateOrdreTransfer(TransfertMatiere_Model transfertModel);
        IEnumerable<TransfertMatiere_Model> GetListeOrdreTransfert(int aboId, string statut, int? stockID, string date);
        IEnumerable<MatiereTransfert_Model> GetListeMatiereParOrdre(int? transferID, int? stockID, string matiereID, string lot, string statut, string date);
        Task<bool?> ReceptionMatiereAchats(ReceptionAchatModel receptionAchatModel);
        Task<bool> CreatePayementFacture(PayementFacture_Model payementFacture_Model);
        IEnumerable<Section_StockageModel> getListSections(int matiereID);
        IEnumerable<BonDeLivraison_Model> getlistFactureDetails(int factureID, int aboID);
        Task<bool> CreateDistributeur(DistributeurModel distributeurModel);
        Task<List<DistributeurModel>> getListDistributeur(int Id, int? statut, string rc);
        Task<bool> deleteDistributeur(int ID, int code);
        Task<List<PayementFacture_Model>> getListPayementFacture(int factureID, int aboID);
        Task<ArticleBL_Model> getArticleBL(int articleID, int aboID);

    }
}
