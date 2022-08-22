using Domain.Entities;
using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.IRepositories
{
    public interface IFournisseurRepository
    {
        Task<int?> CreateFournisseur(Fournisseur fournisseur);
        IEnumerable<Fournisseur> getListFournisseur(int Id, int? VilleId, int? statut);
        IEnumerable<Fonction> getListFonction();
        IEnumerable<Ville> getListVilles();
        IEnumerable<FournisseurMatiere> getListMatiereLink(int fournisseurId);
        Fournisseur findFormulaireFournisseur(int formulaireFourisseurId);
        Task<bool> updateFormulaireFournisseur(int id, Fournisseur newFournisseur);
        Task<bool> deleteFournisseur(int ID, int code);
        Task<bool> deleteMatieresLink(int ID, int code);
        IEnumerable<MatierePremiere> getListMatiere(int Id);
        Task<bool?> AjouterMatiere(int idFournisseur, List<int> listMatiere);
        IEnumerable<BonDeCommande> GetBonDeCommandes(int aboID, int? pointStockID, int? fournisseurID,string name, int? date, string statut);
        Task<int?> CreateBonDeCommande(BonDeCommande bonDeCommande);
        IEnumerable<Article_BC> GetArticlesBC(int bonCommandeID);
        IEnumerable<Article_BL> GetArticlesBL(int bondeLivraisonID);
        IEnumerable<Article_BC> GetArticlesBCForBL(int bonCommandeID);
        IEnumerable<BonDeLivraison> GetBonDeLivraisons(int? fournisseurID, int? bonCommandeID, int aboID, string date, int? statut);
        Task<List<Article_BL>> CreateBonDeLivraison(BonDeLivraison bonDeLivraison);
        Task<int?> CreateFacture(Facture facture, List<BonDeLivraison_Model> listeBL);
        IEnumerable<Facture> GetFactures(int aboID, int? point, string date);
        BonDeCommande FindFormulaireBonDeCommande(int aboID, int? bonCommandeID);
        BonDeLivraison FindFormulaireBonDeLivraison(int aboID, int? bondeLivraison);
        List<ProduitVendable> getAllProds(int startRow, int maxRow);
    }
}
