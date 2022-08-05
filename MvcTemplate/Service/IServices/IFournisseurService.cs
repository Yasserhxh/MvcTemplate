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

    }
}
