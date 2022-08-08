using Domain.Entities;
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
        IEnumerable<BonDeCommande> GetBonDeCommandes(int aboID, int? pointStockID, int? fournisseurID, string date);
        Task<int?> CreateBonDeCommande(BonDeCommande bonDeCommande);

            
    }
}
