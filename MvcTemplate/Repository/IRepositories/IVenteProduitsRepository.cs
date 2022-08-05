using Domain.Entities;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepositories
{
    public interface IVenteProduitsRepository
    {
        Task<int?> CreateVente(Vente vente);
        Task<int?> CreatePerte(Perte perte);
        Task<int?> ValiderCloture(List<int> caisses);
        Task<int?> CreateGratuite(Gratuite gratuite);
        Task<int?> PlanificationDeCloture(int aboId, int PointVenteId);
        Task<int?> PaiementCommande(Commande_Paiement Paiementcommande);
        Task<int?> LivrerCommande(int commandeId,string userId);
        Task<int?> RetourProduit(RetourProduits retour);
        Task<int?> AlimentationCaisse(AllimentationCaisse alimentation);
        Task<int?> RetraitCaisse(RetraitCaisse retraitCaisse);
        Task<int?> ClotureJournee(ClotureJournee cloture);
        Task<int?> CreateCommade(Commande commande);
        IEnumerable<Commande_Paiement> getListPaiementCommande(int commandeID);
        IEnumerable<Vente> getListVentes(int Id, string date);
        IEnumerable<Vente> getListVentesByPdv(int Id,int pdvid,string date);
        IEnumerable<VenteDetails> getListDetailsVentes(int? pv, string date, int aboId, int? VenteId);
        IEnumerable<VenteDetails> getListDetailsVentesApi(int? pv, string date, int aboId, int? VenteId);
        IEnumerable<RetourProduits> getListRetours(int Id, int pdvId, string date);
        IEnumerable<MouvementCaisse> getListMouvementsCaisseParCaisse(int? caisseId);
        IEnumerable<Retour_Details> getListDetails(int Id);
        IEnumerable<AllimentationCaisse> getListAlimentations(int Id,int? pdvId,string date);
        IEnumerable<RetraitCaisse> getListRetrait(int Id, int? Pdv, string date);
        IEnumerable<RetraitType> getListRetraitType();
        IEnumerable<MouvementCaisse> getListMouvementsCaisse(int? pv, string date, int aboId);
        IEnumerable<ClotureJournee> getListCloture(int CaisseId,int? Pdv,string date);
        IEnumerable<ClotureJournee> getListCloturePerPdv(int AboId, int Pdv,int?caisseId,string date);
        IEnumerable<Commande> getListCommandesNonPayee(int PointVenteId);
        IEnumerable<Commande> getListCommandes(int PointVenteId,int? statutLiv,int? statutPaiement,string date);
        IEnumerable<Statut_Livraison> getListStatutLivraison();
        IEnumerable<Statut_PaiementCommande> getListStatutPaiement();
        IEnumerable<CommandeDetail> getListDetailsCommande(int commandeId);
        IEnumerable<Perte> getListPertes(int PointVenteId, string date);
        IEnumerable<PerteDetails> getListDetailsPertes(int perteId);
        IEnumerable<Gratuite> getListGratuite(int PointVenteId, string date);
        IEnumerable<GratuiteDetails> getListDetailsGratuite(int gratuiteId);
        Vente findFormulaireVente(int formulaireVenteId);
        Vente findFormulaireVenteByticket(string ticketID);
        bool CalculeClotures(int PointVenteId);
        string GetCommandeUserName(string userId);
        IEnumerable<Commande> getListCommandesFiltered(int PointVenteId, string date, string nomComandeur);
        decimal GetSoleDebit(int caisseId, string date, int aboId);
        decimal GetSoleDebitBypv(int? pv, string date, int aboId);
        decimal GetSoleDebitBypvApi(int? pv, string date, int aboId);
        decimal GetSoleCredit(int caisseId, string date, int aboId);
        decimal GetSoleCreditBypv(int? pv, string date, int aboId);
        decimal GetSoleCreditBypvApi(int? pv, string date, int aboId);
        decimal GetAlimentationBypv(int? pv, string date, int aboId);
        decimal GetAlimentation(int caisseId, string date, int aboId);
        IEnumerable<ClotureJournee> getListClotureFiltered(int AboId,string date, int? pv);
        IEnumerable<Commande_Paiement> getListPaiementCommandeByPdv(int? pdvId, int aboId, string date);
        IEnumerable<Vente> GetListVente(int? pdvId, int aboId, string date);
        IEnumerable<AllimentationCaisse> GetListAlimentation(int? pdvId, int aboId, string date);
        IEnumerable<RetourProduits> GetListRetours(int? pdvId, int aboId, string date);
        IEnumerable<RetraitCaisse> GetListRetrait(int? pdvId, int aboId, string date);
        Task<int?> UpdateVente(int caisseId, string date);
        Task<bool?> UpdateProduitsPDV(List<ProdsQteApi> prodsList, int pdvID);
        Commande findFormulaireCommandeByticket(string ticketID);

    }
}
