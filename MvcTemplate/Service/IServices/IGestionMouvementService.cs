using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.IServices
{
    public interface IGestionMouvementService
    {
        Task<bool> CreateTypeMouvement(MouvementTypeModel typeMouvement);
        Task<bool> AlimentationStock(MouvementStockModel mouvementStock);
        Task<bool> ReceptionStock(Reception_StockModel receptionModel);
        IEnumerable<FournisseurModel> getListFournisseur(int Id);
        IEnumerable<MatierePremiereStockageModel> getListMatiereStockage(int Id, int aboId, int adresse);
        IEnumerable<MatierePremiereStockageModel> getListMatiereStockageAll(int aboId, int userlieu);
        IEnumerable<MatierePremiereStockageModel> getListMatiereStockageAdmin(int aboId);
        IEnumerable<ProduitConsomableStokageModel> getProduitEnStockByProduit(int produitId, int atelierId);
        IEnumerable<MouvementTypeModel> getListMouvement(int Id);
        IEnumerable<MatierePremiereModel> getListMatierePremiere(int Id);
        MouvementTypeModel findFormulaireMouvement(int formulaireMouvementId);
        Task<bool> updateFormulaireMouvement(int id, MouvementTypeModel newMouvement);
        Task<bool> deleteMouvement(int ID);
        Task<bool> SendStock(MouvementStockModel mouvementStock, int userlieu);
        Task<bool> ReceptionProduitsPretConso(MouvementProduitsConsoModel mouvementStock);
        ProduitConsomableStokageModel findformulaireProduitStock(int Id, int aboId);
        IEnumerable<MouvementProduitsConsoModel> getListMouvementsProduits(int Id, int atelierID,int? produit,string date);
        IEnumerable<ProduitConsomableStokageModel> getProduitPretConsomers(int Id, int aboId, int atelierUserId);
        IEnumerable<ProduitConsomableStokageModel> getProduitPretConsomersByPorduit(int Id, int aboId, int atelierUserId,int produitId);
        IEnumerable<FournisseurProduitsModel> getListFournisseurProduits(int Id);
        IEnumerable<MouvementStockModel> getlistMouvements(int aboId, int? lieuExpe, int? lieuReception, int? matiere, string date);
        IEnumerable<MatierePremiereModel> getListMatierePremieresBC(int Id, int aboId);

    }
}
