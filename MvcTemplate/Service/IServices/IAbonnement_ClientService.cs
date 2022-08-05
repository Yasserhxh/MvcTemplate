using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.IServices
{
    public interface IAbonnement_ClientService
    {
        Task<bool?> AjouterUtilisateur(int idAtelier, string responsableId);
        Task<bool> CreateClient(Abonnement_ClientModel ClientModel);
        Task<bool> CreateAbonnement(PaiementAbonnementModel paiement_AbonnementModel);
        IEnumerable<PaiementAbonnementModel> getListAbonnement(int? client);
        IEnumerable<VilleModel> getListVilles();
        IEnumerable<Abonnement_ClientModel> getListClient();
        IEnumerable<AtelierModel> getListAtelier(int Id, int?statut);
        IEnumerable<Atelier_UserModel> getListAtelierUser(string UserId);
        Abonnement_ClientModel findFormulaireClient(int formulaireClientId);
        Task<bool> updateFormulaireClient(int id, Abonnement_ClientModel ClientModel);
        Task<bool> deleteFormulaireClient(int ID);
        Task<bool> CreateAtelier(AtelierModel atelierModel);
        AtelierModel findFormulaireAtelier(int formulaireAtelierId);
        Task<bool> updateFormulaireAtelier(int id, AtelierModel newAtelier);
        Task<bool> deleteFormulaireAtelier(int ID, int code);
        IEnumerable<Lieu_StockageModel> getListStocks(int Id);
        IEnumerable<FamilleProduitModel> getListFamilles(int Id);
        IEnumerable<PointProduction_FamilleModel> getListFammilesProduction(int pdvId);
        Task<bool?> AjouterFamille(int atelierId, List<int> ListFamille);
        Task<bool> deleteFamillePdv(int id, int code);
    }
}
