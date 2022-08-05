using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.IServices
{
    public interface IPointVenteService
    {
        Task<bool?> AjouterUtilisateur(int idPointVente, string responsableId);
        Task<bool> CreatePointVente(Point_VenteModel point_VenteModel);
        Task<bool> CreatePositionVente(PositionVenteModel position);
        Task<bool> CreateAgentServeur(AgentServeurModel agentserveur);
        Task<bool> AjouterSalle(int id, SalleModel salle);
        Task<bool> AjouterTable(int id, List<TableModel> table);
        IEnumerable<Point_VenteModel> getListPointVente(int Id,int? type, int? statut);
        IEnumerable<PointVente_UserModel> getListPdvUser(string UserId);
        IEnumerable<Point_VenteModel> getListPointVenteActive(int Id);
        IEnumerable<PositionVenteModel> getListPositionVente(int Id);
        IEnumerable<SalleModel> getListSalles(int Id);
        IEnumerable<TableModel> getListTables(int Id);
        IEnumerable<ModePaiementModel> getListModePaiement();
        IEnumerable<AgentServeurModel> getListAgents(int Id); 
        IEnumerable<VilleModel> getListVilles();
        IEnumerable<PointVente_StockModel> getStockPointVenteAll(int Id);
        IEnumerable<PointVente_StockModel> getStockPointVente(int Id, int pdv);
        IEnumerable<PointVente_StockModel> getStockPointVenteByProduit(int Id, int pdv, int produitId);
        IEnumerable<PointVente_StockModel> getStockPointVenteAvecPlan(int Id, int? pdv);
        IEnumerable<ProduitPackageModel> getStockProduitPackage(int Id, int pdv);
        IEnumerable<Forme_ProduitModel> getStockFormePackage(int Id, int pdv);
        IEnumerable<ProduitPackageModel> getStockProduitPackageAll(int Id);
        Point_VenteModel findFormulairePointVente(int formulairePointVenteId);
        PositionVenteModel findFormulairePositionVente(int formulairePoseId);
        SalleModel findFormulaireSalle(int formulaireSalleId);
        Task<bool> updateFormulairePointVente(int id, Point_VenteModel newPointVenteModel);
        Task<bool> deletePointVente(int ID, int code);
        Task<bool> deleteFamillePdv(int id, int code);
        IEnumerable<FamilleProduitModel> getListFamilles(int Id);
        Task<bool?> AjouterFamille(int pointVenteId, List<int> ListFamille);
        IEnumerable<ProduitApproModel> getListProduitVendable(int Id, int AboId);
        IEnumerable<PointVente_FamilleModel> getListFammilesPointVente(int pdvId);
        IEnumerable<AtelierModel> getListAtelier(int Id);
        IEnumerable<Lieu_StockageModel> getListPointStock(int Id);
        IEnumerable<PointVenteApi> getListPointVenteActiveApi(int Id);
        IEnumerable<ProduitPack_AtelierModel> getListProduitPack(int Id, int AboId);
        Task<bool> CreateEchange(Echange_ProduitsModel echange_Produits);
        IEnumerable<Echange_ProduitsModel> getListEchanges(int AboID, string statut, string date, int? pdvF, int? pdvRec);
        IEnumerable<EchangeProduit_DetailsModel> getListEchangesDetails(int echangeID);
        IEnumerable<Point_VenteModel> getListePdvByCateg(int souscategID, int pdv);
        decimal findformulaireStockByFormeID(int Id, int pdv, int formeID);
        Task<IEnumerable<ApiUserModel>> getUsersAsync(int pdvId);
        Task<bool?> AccepterOrdreEchange(int echangeID, string validerPar, int pdvID);
        Task<bool?> AccepterLivraisonEchange(int echangeID, string validerPar, int pdvID);
        IEnumerable<Echange_ProduitsModel> getListEchangesEntrant(int AboID, string statut, string date, int? pdvF, int? pdvRec);
        IEnumerable<Echange_ProduitsModel> getListEchangesSortant(int AboID, string statut, string date, int? pdvF, int? pdvRec);
    }
}
