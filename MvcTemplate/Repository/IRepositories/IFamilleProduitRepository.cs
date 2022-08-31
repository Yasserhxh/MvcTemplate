using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.IRepositories
{
    public interface IFamilleProduitRepository
    {
        Task<int?> CreateFamille(FamilleProduit familleProduit);
        Task<int?> CreatSousFamille(SousFamille sousFamille);
        IEnumerable<FamilleProduit> getListFamilles(int Id);
        IEnumerable<SousFamille> getListSousFamilles(int? Id, int aboID);
        FamilleProduit findFormulaireFamille(int formulaireFamilleId);
        Task<bool> updateFormulaireFamille(int id, FamilleProduit newFamile);
        Task<bool> deleteFormulaireFamille(int ID);
        IEnumerable<FamilleProduit> getListAllFamilles();
        IEnumerable<FamilleProduit> getListFamillesByPdv(int Id, int pdv);
    }
}
