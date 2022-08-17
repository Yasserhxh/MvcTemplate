using Domain.Entities;
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
        IEnumerable<BonDeCommande_Model> GetBonDeCommandes(int aboID, int? pointStockID, int? fournisseurID, string date, string statut);
        Task<bool> CreateBonDeCommande(BonDeCommande_Model bonDeCommandeModel);
        IEnumerable<ArticleBC_Model> GetArticlesBC(int bonCommandeID);
        IEnumerable<ArticleBC_Model> GetArticlesBCForBL(int bonCommandeID);
        IEnumerable<BonDeLivraison_Model> GetBonDeLivraisons(int? bonCommandeID, int aboID, string date, int? statut);
        Task<List<Article_BL>> CreateBonDeLivraison(BonDeLivraison_Model bonDeLivraisonModel);
        Task<bool> CreateFacture(FactureModel factureModel, List<BonDeLivraison_Model> listeBL);
        IEnumerable<FactureModel> GetFactures(int aboID, int? point, string date);
        BonDeCommande_Model FindFormulaireBonDeCommande(int aboID, int? bonCommandeID);
    }
}
