using Domain.Entities;
using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.IRepositories
{
    public interface IAbonnement_ClientRepository
    {
        Task<bool?> AjouterUtilisateur(int idAtelier, string responsableId);
        Task<int?> CreateClient(Abonnement_Client abonnement_Client);
        Task<int?> CreateAbonnement(Paiement_Abonnement paiement_Abonnement);
        IEnumerable<Abonnement_ClientModel> getListClient();
        IEnumerable<Paiement_Abonnement> getListAbonnement(int? client);
        IEnumerable<Atelier> getListAtelier(int Id,int? statut);
        IEnumerable<Atelier_User> getListAtelierUser(string UserId);
        Abonnement_Client findFormulaireClient(int formulaireClientId);
        Task<bool> updateFormulaireClient(int id, Abonnement_Client newClient);
        Task<bool> deleteFormulaireClient(int ID);
        Task<int?> CreateAtelier(Atelier atelier);

        IEnumerable<Ville> getListVilles();
        IEnumerable<Lieu_Stockage> getListStocks(int Id);
        Atelier findFormulaireAtelier(int formulaireAtelierId);
        Task<bool> updateFormulaireAtelier(int id, Atelier newAtelier);
        Task<bool> deleteFormulaireAtelier(int ID, int code);
        IEnumerable<FamilleProduit> getListFamilles(int Id);
        IEnumerable<PointPorduction_Famille> getListFammilesProduction(int pdvId);
        Task<bool?> AjouterFamille(int atelierId, List<int> ListFamille);
        Task<bool> deleteFamillePdv(int id, int code);
    }
}
