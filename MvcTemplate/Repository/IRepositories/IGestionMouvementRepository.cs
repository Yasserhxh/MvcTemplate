using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.IRepositories
{
    public interface IGestionMouvementRepository
    {
        Task<int?> CreateTypeMouvement(MouvementType typeMouvement);
        Task<int?> AlimentationStock(MouvementStock mouvementStock);
        Task<int?> ReceptionStock(Reception_Stock reception);
        IEnumerable<MouvementType> getListMouvement(int Id);
        IEnumerable<Fournisseur> getListFournisseur(int Id);
        IEnumerable<MatierePremiereStockage> getListMatiereStockage(int Id, int aboId, int adresse);
        IEnumerable<MatierePremiereStockage> getListMatiereStockageAll( int aboId, int userlieu);
        IEnumerable<MatierePremiereStockage> getListMatiereStockageAdmin(int aboId);
        IEnumerable<MatierePremiere> getListMatierePremiere(int Id);
        IEnumerable<ProduitConsomableStokage> getProduitEnStockByProduit(int produitId, int atelierId);
        MouvementType findFormulaireMouvement(int formulaireMouvementId);
        Task<bool> updateFormulaireMouvement(int id, MouvementType newMouvement);
        Task<bool> deleteMouvement(int ID);
        Task<int?> SendStock(MouvementStock mouvementStock, int userlieu);
        Task<int?> ReceptionProduitsPretConso(MouvementProduitsConso mouvementProduitConso);
        IEnumerable<MouvementProduitsConso> getListMouvementsProduits(int Id, int atelierID,int? produit,string date);
        IEnumerable<ProduitConsomableStokage> getProduitPretConsomers(int Id, int aboId,int atelierUserId);
        IEnumerable<ProduitConsomableStokage> getProduitPretConsomersByPorduit(int Id, int aboId,int atelierUserId, int produitId);
        ProduitConsomableStokage findformulaireProduitStock(int Id, int aboId);
        IEnumerable<FournisseurProduits> getListFournisseurProduits(int Id);
        IEnumerable<MouvementStock> getlistMouvements(int aboId, int? lieuExpe, int? lieuReception, int? matiere, string date);
    }
}
