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
        IEnumerable<Facture> GetFactures(int aboID, int? bondeCommande, string numeroFac, int? date);
        BonDeCommande FindFormulaireBonDeCommande(int aboID, int? bonCommandeID);
        BonDeLivraison FindFormulaireBonDeLivraison(int aboID, int? bondeLivraison);
        paginationModel<ProduitVendable> getAllProds(int pg);
        IEnumerable<Stock_Achat> GetMatireStockAchat(int aboID, int? matiereID, string lotIntern);
        Task<int?> CreateOrdreTransfer(Transfert_Matiere transfert);
        IEnumerable<Transfert_Matiere> GetListeOrdreTransfert(int aboId, string statut, int? stockID, string date);
        IEnumerable<Matiere_Transfert> GetListeMatiereParOrdre(int? transferID, int? stockID, string matiereID, string lot, string statut, string date);
        IEnumerable<Unite_Mesure> findFormulaireUnite(int unite);
        Task<bool?> ReceptionMatiereAchats(ReceptionAchatModel receptionAchatModel);
        Task<int?> CreatePayementFacture(Payement_Facture payement_Facture);
        IEnumerable<Section_Stockage> getListSections(int matiereID);
        IEnumerable<BonDeLivraison> getlistFactureDetails(int factureID, int aboID);
        Task<int?> CreateDistributeur(Distributeur distributeur);
        Task<List<Distributeur>> getListDistributeur(int Id, int? statut, string rc);
    }
}
