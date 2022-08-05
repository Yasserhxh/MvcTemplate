using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.IServices
{
    public interface IFamilleProduitService
    {
        Task<bool> CreateFamille(FamilleProduitModel familleProduitModel);
        Task<bool> CreatSousFamille(SousFamilleModel sousFamilleModel);
        IEnumerable<FamilleProduitModel> getListFamilles(int Id);
        IEnumerable<SousFamilleModel> getListSousFamilles(int Id);
        FamilleProduitModel findFormulaireFamille(int formulairefamilleProduitId);
        Task<bool> updateFormulaireFamille(int id, FamilleProduitModel newfamilleProduitModel);
        Task<bool> deleteFormulaireFamille(int ID);
        IEnumerable<FamilleProduitModel> getListAllFamilles();
        IEnumerable<FamilleProduitModel> getListFamillesByPdv(int Id, int pdv);
    }
}
