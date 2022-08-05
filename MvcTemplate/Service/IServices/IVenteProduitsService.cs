using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.IServices
{
    public interface IVenteProduitsService
    {
        Task<bool> CreateVente(VenteModel venteModel);
        Task<bool> PlanificationDeCloture(int aboId, int PointVenteId);
        Task<bool> CreateCommade(CommandeModel commandeModel);
        Task<bool> CreatePerte(PerteModel perteModel);
        Task<bool> CreateGratuite(GratuiteModel gratuiteModel);
        Task<bool> ValiderCloture(List<int> caisses);
        Task<bool> PaiementCommande(Commande_PaiementModel Paiementcommande);
        Task<bool> LivrerCommande(int commandeId,string userId);
        IEnumerable<Commande_PaiementModel> getListPaiementCommande(int commandeID);
        IEnumerable<Statut_LivraisonModel> getListStatutLivraison();
        IEnumerable<Statut_PaiementCommandeModel> getListStatutPaiement();
        IEnumerable<CommandeModel> getListCommandes(int PointVenteId,int? statutLiv,int? statutPaiement, string date);
        IEnumerable<CommandeModel> getListCommandesNonPayee(int PointVenteId);
        IEnumerable<CommandeApiModel> getListCommandesNonPayeeApi(int PointVenteId);
        IEnumerable<CommandeDetailModel> getListDetailsCommande(int commandeId); 
        IEnumerable<VenteModel> getListVente(int Id, string date);
        IEnumerable<VenteModel> getListVenteByPdv(int Id, int pdvid, string date);
        IEnumerable<VenteDetailsModel> getListDetailsVentes(int? pv, string date, int aboId, int? VenteId);
        IEnumerable<VenteDetailsModel> getListDetailsVentesApi(int? pv, string date, int aboId, int? VenteId);
        Task<bool> RetourProduit(RetourProduitsModel retourModel);
        Task<bool?> AlimentationCaisse(AllimentationCaisseModel alimentationModel);
        Task<bool?> RetraitCaisse(RetraitCaisseModel retraitCaisseModel);
        Task<bool> ClotureJournee(ClotureJourneeModel clotureModel);
        IEnumerable<RetourProduitsModel> getListRetours(int Id,int pdvId,string date);
        IEnumerable<RetourDetailsModel> getListDetails(int Id);
        IEnumerable<AllimentationCaisseModel> getListAlimentations(int Id,int? pdvId, string date);
        IEnumerable<MouvementCaisseModel> getListMouvementsCaisseParCaisse(int? caisseId);
        IEnumerable<RetraitCaisseModel> getListRetrait(int Id, int? Pdv, string date);
        IEnumerable<RetraitTypeModel> getListRetraitType();
        IEnumerable<MouvementCaisseModel> getListMouvementsCaisse(int? pv, string date, int aboId);
        IEnumerable<ClotureJourneeModel> getListCloture(int CaisseId,int? Pdv,string date);
        IEnumerable<ClotureJourneeModel> getListCloturePerPdv(int AboId, int Pdv,int? caisseId,string date);
        VenteModel findFormulaireVente(int formulaireVenteId);
        bool CalculeClotures(int PointVenteId);
        IEnumerable<PerteModel> getListPertes(int PointVenteId, string date);
        IEnumerable<PerteDetailsModel> getListDetailsPertes(int perteId);
        IEnumerable<GratuiteModel> getListGratuite(int PointVenteId, string date);
        IEnumerable<GratuiteDetailsModel> getListDetailsGratuite(int gratuiteId);
        IEnumerable<CommandeApiModel> getListCommandesFiltered(int PointVenteId, string date, string nomComandeur);
        decimal GetSoleDebit(int caisseId, string date, int aboId);
        decimal GetSoleCredit(int caisseId, string date, int aboId);
        decimal GetSoleDebitBypv(int? pv, string date, int aboId);
        decimal GetSoleDebitBypvApi(int? pv, string date, int aboId);
        decimal GetSoleCreditBypv(int? pv, string date, int aboId);
        decimal GetSoleCreditBypvApi(int? pv, string date, int aboId);
        decimal GetAlimentationBypv(int? pv, string date, int aboId);
        decimal GetAlimentation(int caisseId, string date, int aboId);
        IEnumerable<ClotureJourneeModel> getListClotureFiltered(int AboId, string date, int? pv);
        IEnumerable<RecettesViewModel> getListRecettes(int AboId, string date, int? pv);
        IEnumerable<DepensesViewModel> getListDepenses(int AboId, string date, int? pv);
        Task<bool> UpdateVente(int caisseId, string date);
        TicketModel GetTicketDetails(string ticketID);
        TicketModel GetTicketDetailsCommande(string ticketID);
        Task<bool?> UpdateProduitsPDV(List<ProdsQteApi> prodsList, int pdvID);

    }
}
